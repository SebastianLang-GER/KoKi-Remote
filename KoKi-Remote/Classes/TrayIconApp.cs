using System.Diagnostics;
using Windows.UI.ViewManagement;

namespace KoKi_Remote
{
    public class TrayIconApp : ApplicationContext
    {
        private UISettings uiSettings = new();
        private NotifyIcon trayIcon;

        public TrayIconApp()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = GetAppIcon(),
                ContextMenuStrip = new ContextMenuStrip()
                {
                    Items =
                    {
                        new ToolStripMenuItem("Open", Resources.Open, Open_Click, "toolStripMenuItemOpen")
                        {
                            ShortcutKeys = Keys.Control | Keys.O,
                            Font = new Font(Control.DefaultFont,FontStyle.Bold)
                        },
                        new ToolStripSeparator(),
                        new ToolStripMenuItem("Settings", Resources.Settings, Settings_Click, "toolStripMenuItemSettings"),
                        new ToolStripMenuItem("Info", Resources.Info, Info_Click, "toolStripMenuItemInfo"),
                        new ToolStripSeparator(),
                        new ToolStripMenuItem("Exit", Resources.Exit, Exit_Click, "toolStripMenuItemExit")
                        {
                            ShortcutKeys = Keys.Alt | Keys.F4
                        }
                    }
                },
                Visible = true
            };

            trayIcon.Click += TrayIcon_Click;
            trayIcon.DoubleClick += TrayIcon_DoubleClick;
            uiSettings.ColorValuesChanged += UISettings_ColorValuesChanged;
        }

        private void Open_Click(object? sender, EventArgs e)
        {
            new Open().Show();
        }

        private void Info_Click(object? sender, EventArgs e)
        {
            //new Info().Show();
        }

        private void Settings_Click(object? sender, EventArgs e)
        {
            new Settings().Show();
        }

        private void Exit_Click(object? sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void TrayIcon_Click(object? sender, EventArgs e)
        {
            trayIcon.ContextMenuStrip?.Show(Cursor.Position);
        }

        private void TrayIcon_DoubleClick(object? sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"http://localhost:{Properties.Settings.Default.Port}",
                UseShellExecute = true
            });
        }

        private void UISettings_ColorValuesChanged(UISettings sender, object args)
        {
            trayIcon.Icon = GetAppIcon();
        }

        private Icon GetAppIcon()
        {
            var color = uiSettings.GetColorValue(UIColorType.Background);
            if (color.R == 0xFF && color.G == 0xFF && color.B == 0xFF)
            {
                // light mode
                return Resources.AppIconBlack;
            }
            else
            {
                // dark mode
                return Resources.AppIconWhite;
            }
        }
    }
}
