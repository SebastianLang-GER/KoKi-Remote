using Microsoft.VisualBasic;
using System.Diagnostics;

namespace KoKi_Remote
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            //TEMP
            foreach (var item in Process.GetProcesses())
            {
                if (item.MainWindowTitle != string.Empty)
                {
                    listBox1.Items.Add(item.ProcessName + " (" + item.MainWindowTitle + ")");
                }
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            //TODO: save settings
            Properties.Settings.Default.Save();
        }
    }
}
