using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;

namespace KindleClippingsGUI
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();

            app.Initialized += delegate
            {
                app.MainForm = new MainForm();
                app.MainForm.Show();
            };

            app.Run();
        }
    }
}
