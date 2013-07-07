using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Eto.Drawing;
using KindleClippings;

namespace KindleClippingsGUI
{
    class ClippingForm : Form
    {
        private const string DateFormat = "F";
        private Clipping _clipping { get; set; }

        internal ClippingForm(Clipping clipping)
        {
            _clipping = clipping;
            BuildForm();
        }

        private void BuildForm()
        {
            this.ClientSize = new Size(600, 500);
            this.Title = "View Clipping";

            var layout = new DynamicLayout(this);

            layout.AddRow(new Label { Text = "Book Name:" }, GetDataControl(_clipping.BookName));
            layout.AddRow(new Label { Text = "Author:" }, GetDataControl(_clipping.Author));
            layout.AddRow(new Label { Text = "Clipping Type:" }, GetDataControl(ClippingDatabase.GetClippingType(_clipping.ClippingType)));
            layout.AddRow(new Label { Text = "Page:" }, GetDataControl(_clipping.Page));
            layout.AddRow(new Label { Text = "Location:" }, GetDataControl(_clipping.Location));
            layout.AddRow(new Label { Text = "Date Added:" }, GetDataControl(_clipping.DateAdded.ToString(DateFormat)));
            layout.AddRow(new Label { Text = "Clipping Text:" }, GetDataControl(_clipping.Text, true));
        }

        private Control GetDataControl(string value, bool multiline = false)
        {
            TextControl ctl;

            if (multiline) {
                var txt = new TextArea { ReadOnly = true };
                txt.Wrap = true;
                ctl = txt;
            } else {
                var txt = new TextBox { ReadOnly = true };
                ctl = txt;
            }

            ctl.Text = value;

            return ctl;
        }
    }
}
