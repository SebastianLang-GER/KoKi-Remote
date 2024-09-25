using System.Runtime.InteropServices;

namespace KoKi_Remote
{
    internal static partial class Program
    {
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        private static List<MultimediaPlayer> players = GetMultimediaPlayers();
        private static int playerIndex = 0;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            bool firstAppInstance = true;

            // single instance application
            using (new Mutex(initiallyOwned: true, "KoKi-Remote", out firstAppInstance))
            {
                if (firstAppInstance)
                {
                    StartWebServer();
                    ApplicationConfiguration.Initialize();
                    Application.Run(new TrayIconApp());
                }
            }
        }

        private static List<MultimediaPlayer> GetMultimediaPlayers()
        {
            return new List<MultimediaPlayer>
            {
                new MultimediaPlayer
                {
                    Name = "VLC Media Player",
                    Logo = "images/vlc.svg",
                    ProcessName = "vlc",
                    WindowTitle = "VLC media player",
                    Commands = new MultimediaPlayer.ControlCommands
                    {
                        PlayPause = " ",
                        Stop = "s",
                        Previous = "p",
                        Next = "n"
                    }
                },
                new MultimediaPlayer
                {
                    Name = "easyDCP Player+",
                    Logo = "images/easydcp.webp",
                    ProcessName = "easyDCP Player+",
                    WindowTitle = "easyDCP Player+",
                    Commands = new MultimediaPlayer.ControlCommands
                    {
                        PlayPause = " ",
                        Stop = "s",
                        Previous = "p",
                        Next = "n"
                    }
                },
                new MultimediaPlayer
                {
                    Name = "easyDCP Player+ NE",
                    Logo = "images/easydcp.webp",
                    ProcessName = "easyDCP Player+ NE",
                    WindowTitle = "easyDCP Player+ NE",
                    Commands = new MultimediaPlayer.ControlCommands
                    {
                        PlayPause = " ",
                        Stop = "s",
                        Previous = "p",
                        Next = "n"
                    }
                }
            };
        }
    }
}