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
            Process? player = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            //TODO: beter selection required --> multiple player windows etc.
            if (player != null)
            {
                IntPtr handle = player.MainWindowHandle;
                SetForegroundWindow(handle); // input focus, but not in front/top most
                switch(command)
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
                MessageBox.Show("Error, invalid player process.");
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}