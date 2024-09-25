using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace KoKi_Remote
{
    public class MultimediaPlayer
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        [JsonIgnore]
        public string? ProcessName { get; set; }
        [JsonIgnore]
        public string? WindowTitle { get; set; }
        public ControlCommands Commands { get; set; } = new ControlCommands();

        public enum ControlCommandOptions
        {
            PlayPause,
            Stop,
            Previous,
            Next
        }

        /// <summary>
        /// <a href="https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.sendkeys.send?view=windowsdesktop-8.0#remarks">Usage of keystrokes.</a>
        /// </summary>
        public class ControlCommands
        {
            public String PlayPause { get; set; } = String.Empty;
            public String Stop { get; set; } = String.Empty;
            public String Previous { get; set; } = String.Empty;
            public String Next { get; set; } = String.Empty;
            //public Keys? PlayPause { get; set; }
            //public Keys? Stop { get; set; }
            //public Keys? Previous { get; set; }
            //public Keys? Next { get; set; }
        }

        public void SendCommand(ControlCommandOptions command)
        {
            //TODO: beter selection required --> multiple player windows etc.
            Process? player = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            if (player != null)
            {
                IntPtr handle = player.MainWindowHandle;
                if (player.MainWindowHandle != IntPtr.Zero)
                {
                    if (WindowTitle != null && WindowTitle != string.Empty)
                    {
                        if (player.MainWindowTitle.Contains(WindowTitle))
                        {
                            SwitchToThisWindow(player.MainWindowHandle, true);
                            //TEST: alternative
                            //if (IsIconic(player.MainWindowHandle))
                            //{
                            //    ShowWindow(player.MainWindowHandle, 9);
                            //}
                            //SetForegroundWindow(player.MainWindowHandle);
                        }
                        else
                        {
#if DEBUG
                            Console.Error.WriteLine($"Error: Player window \"{WindowTitle}\" not found.");
#endif
                        }
                    }
                }
                switch (command)
                {
                    case ControlCommandOptions.PlayPause:
                        SendKeys.SendWait(Commands.PlayPause);
                        break;
                    case ControlCommandOptions.Stop:
                        SendKeys.SendWait(Commands.Stop);
                        break;
                    case ControlCommandOptions.Previous:
                        SendKeys.SendWait(Commands.Previous);
                        break;
                    case ControlCommandOptions.Next:
                        SendKeys.SendWait(Commands.Next);
                        break;
                }
            }
            else
            {
#if DEBUG
                Console.Error.WriteLine($"Error: Invalid player process \"{ProcessName}\".");
#endif
                throw new Exception("Invalid player process \"{ProcessName}\".");
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
    }
}