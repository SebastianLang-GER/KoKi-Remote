using System.Diagnostics;

namespace KoKi_Remote
{
    public partial class Open : Form
    {
        public Open()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"http://localhost:{Properties.Settings.Default.Port}",
                UseShellExecute = true
            });
            Close();
        }
    }
}
