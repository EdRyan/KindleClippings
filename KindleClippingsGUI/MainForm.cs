using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using KindleClippings;

namespace KindleClippingsGUI
{
    class MainForm : Form
    {
        private Control _inputSection { get; set; }
        private Control _outputSection { get; set; }
        private TabControl _tabGroup { get; set; }
        private TextBox _fileSelectTextBox { get; set; }
        private ComboBox _deviceSelectBox { get; set; }
        private Label _pathLabel { get; set; }
        private TreeView _treeView { get; set; }

        private const string FromFileTabName = "From File";
        private const string FromDeviceTabName = "From Device";

        public MainForm()
        {
            this.ClientSize = new Size(800, 600);
            this.Title = "Kindle Clippings Parser";
            this.Icon = Common.BookIcon;

            CreateInputSection();
            CreateOutputSection();

            var layout = new DynamicLayout(this);
            layout.AddRow(_inputSection);
            layout.AddRow(_outputSection);

            PopulateDevices();
        }

        private void CreateInputSection()
        {
            var inGrp = new GroupBox { Text = "Input" };

            var layout = new DynamicLayout(inGrp);
            layout.BeginVertical();

            _tabGroup = new TabControl();

            var fileTab = CreateFromFileTab();
            var deviceTab = CreateFromDeviceTab();

            _tabGroup.TabPages.Add(fileTab);
            _tabGroup.TabPages.Add(deviceTab);

            layout.AddRow(_tabGroup);

            var parseButton = new Button { Text = "Parse" };
            parseButton.Click += parseButton_Click;

            layout.BeginHorizontal();
            layout.AddCentered(parseButton);
            layout.EndHorizontal();

            layout.EndVertical();

            _inputSection = inGrp;
        }

        private TabPage CreateFromFileTab()
        {
            var fileTab = new TabPage { Text = FromFileTabName };
            var fileLayout = new DynamicLayout(fileTab);

            var browseLabel = new Label { Text = "Select Clippings File: ", Size = new Size(150, 20) };

            _fileSelectTextBox = new TextBox { Size = new Size(480, 20) };

            var browseButton = new Button { Text = "Browse" };
            browseButton.Click += browseButton_Click;

            fileLayout.BeginVertical();
            fileLayout.BeginHorizontal();
            fileLayout.AddRow(browseLabel, _fileSelectTextBox, browseButton);
            fileLayout.EndHorizontal();
            fileLayout.EndVertical();

            return fileTab;
        }

        private TabPage CreateFromDeviceTab()
        {
            var deviceTab = new TabPage { Text = FromDeviceTabName };
            var deviceLayout = new DynamicLayout(deviceTab);

            var selectDeviceLabel = new Label { Text = "Select Device: ", Size = new Size(150, 20) };

            _deviceSelectBox = new ComboBox { Size = new Size(480, 20) };

            var refreshButton = new Button { Text = "Refresh" };
            refreshButton.Click += refreshButton_Click;

            deviceLayout.BeginVertical();
            deviceLayout.BeginHorizontal();
            deviceLayout.AddRow(selectDeviceLabel, _deviceSelectBox, refreshButton);
            deviceLayout.EndHorizontal();
            deviceLayout.EndVertical();

            return deviceTab;
        }

        private void CreateOutputSection()
        {
            var outGrp = new GroupBox { Text = "Output" };

            var layout = new DynamicLayout(outGrp);
            layout.BeginVertical();

            var sourceLabel = new Label { Text = "Source: ", Size = new Size(50, 20) };
            _pathLabel = new Label();

            layout.BeginHorizontal();
            layout.AddRow(sourceLabel, _pathLabel);
            layout.EndHorizontal();

            _treeView = new TreeView();
            _treeView.MouseDoubleClick += _treeView_MouseDoubleClick;

            layout.BeginHorizontal();
            layout.AddRow(_treeView);
            layout.EndHorizontal();

            layout.EndVertical();

            _outputSection = outGrp;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            BrowseForFile();
        }

        private void BrowseForFile()
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog(this) == DialogResult.Ok)
                _fileSelectTextBox.Text = dialog.FileName;
        }

        private string GetCurrentlySelectedDrive()
        {
            return _deviceSelectBox.SelectedKey;
        }

        void refreshButton_Click(object sender, EventArgs e)
        {
            PopulateDevices();
        }

        private void PopulateDevices()
        {
            var selectedDrive = GetCurrentlySelectedDrive();
            bool hasPriorSelection = !String.IsNullOrEmpty(selectedDrive);

            _deviceSelectBox.Items.Clear();

            foreach (var device in Common.GetRemovableDrives())
            {
                var driveLetter = device.RootDirectory.FullName;
                var volumeLabel = device.VolumeLabel;

                var listItem = new ListItem { Key = driveLetter, Text = volumeLabel + " (" + driveLetter + ")" };
                _deviceSelectBox.Items.Add(listItem);

                if ((hasPriorSelection && driveLetter == selectedDrive) || volumeLabel == Common.DefaultKindleVolumeLabel) _deviceSelectBox.SelectedKey = driveLetter;
            }
        }

        private void parseButton_Click(object sender, EventArgs e)
        {
            Parse();
        }

        private void Parse()
        {
            string path;

            switch (_tabGroup.SelectedPage.Text)
            {
                case FromFileTabName:
                    path = _fileSelectTextBox.Text;
                    break;
                case FromDeviceTabName:
                    path = GetCurrentlySelectedDrive() + Common.MyClippingsRelativePath;
                    break;
                default:
                    throw new Exception("Error: could not determine what tab you are on.");
            }

            if (File.Exists(path))
            {
                try
                {
                    var clippings = MyClippingsParser.Parse(path).ToList();

                    var authorDict = ClippingOrganizer.GroupClippingsByAuthorAndBook(clippings);

                    _pathLabel.Text = path;

                    PopulateTree(authorDict.Values);

                    MessageBox.Show(clippings.Count + " clippings parsed.", "Parsing Complete");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Parsing Error", MessageBoxType.Error);
                }
            }
            else
            {
                MessageBox.Show("Could not find the file specified.", "Parsing Error", MessageBoxType.Error);
            }
        }

        private void PopulateTree(IEnumerable<Author> authors)
        {
            var root = new TreeItem
            {
                Key = "ROOT",
                Text = "My Clippings",
                Expanded = true
            };

            _treeView.DataStore = root;

            foreach (var author in authors.OrderBy(a => a.Name))
            {
                var authorName = String.IsNullOrEmpty(author.Name) ? "Unknown Author" : author.Name;

                var authorItem = new TreeItem
                {
                    Text = authorName,
                    Expanded = false,
                    Image = Common.PersonIcon
                };

                foreach (var book in author.Books.Values.OrderBy(b => b.Name))
                {
                    var bookName = String.IsNullOrEmpty(book.Name) ? "Unknown Book" : book.Name;

                    var bookItem = new TreeItem
                    {
                        Text = bookName,
                        Expanded = false,
                        Image = Common.BookIcon
                    };

                    foreach (var clipping in book.Clippings.OrderBy(c => c.BeginningLocation).ThenBy(c => c.BeginningPage))
                    {
                        var id = ClippingDatabase.AddClipping(clipping);

                        var hasPage = !String.IsNullOrEmpty(clipping.Page);
                        var hasLocation = !String.IsNullOrEmpty(clipping.Location);

                        string clippingText = "";

                        if (hasPage)
                        {
                            clippingText += "Page " + clipping.Page;
                        }
                        else if (hasLocation)
                        {
                            clippingText += "Location " + clipping.Location;
                        }
                        else clippingText += "Unknown Location";

                        var preview = Preview(clipping.Text);
                        if (!String.IsNullOrEmpty(preview)) clippingText += ": " + preview;

                        bookItem.Children.Add(new TreeItem { Text = clippingText, Key = id.ToString(), Image = Common.GetClippingTypeIcon(clipping.ClippingType) });
                    }

                    authorItem.Children.Add(bookItem);
                }

                root.Children.Add(authorItem);
            }

            _treeView.RefreshData();
        }

        private string Preview(string text)
        {
            if (String.IsNullOrEmpty(text)) return String.Empty;

            const int maxPreviewLen = 90; // characters
            var textLen = text.Length;

            if (textLen > maxPreviewLen) return text.Substring(0, maxPreviewLen) + "...";

            return text;
        }
        
        private void _treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenSelectedTreeItem();
        }

        private void OpenSelectedTreeItem()
        {
            var item = _treeView.SelectedItem;

            if (item == null || string.IsNullOrEmpty(item.Key)) return;

            try
            {
                OpenClippingWindow(int.Parse(item.Key));
            }
            catch
            {
                // if the Key doesn't parse to an int then it's not a clipping
            }
        }

        private void OpenClippingWindow(int clippingId)
        {
            var clipping = ClippingDatabase.GetClipping(clippingId);
            OpenClippingWindow(clipping);
        }

        private void OpenClippingWindow(Clipping clipping)
        {
            var form = new ClippingForm(clipping);
            form.Show();
        }
    }
}
