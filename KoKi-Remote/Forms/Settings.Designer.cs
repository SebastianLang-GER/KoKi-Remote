namespace KoKi_Remote
{
    partial class Settings
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            tabControlSettings = new TabControl();
            tabPageWebServer = new TabPage();
            tabPageMultimediaPlayers = new TabPage();
            listBox1 = new ListBox();
            flowLayoutPanelControlButtons = new FlowLayoutPanel();
            buttonApply = new Button();
            tabPageProjectors = new TabPage();
            tabPageCameras = new TabPage();
            tabControlSettings.SuspendLayout();
            tabPageMultimediaPlayers.SuspendLayout();
            flowLayoutPanelControlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlSettings
            // 
            tabControlSettings.Controls.Add(tabPageWebServer);
            tabControlSettings.Controls.Add(tabPageMultimediaPlayers);
            tabControlSettings.Controls.Add(tabPageProjectors);
            tabControlSettings.Controls.Add(tabPageCameras);
            tabControlSettings.Dock = DockStyle.Fill;
            tabControlSettings.Location = new Point(0, 0);
            tabControlSettings.Name = "tabControlSettings";
            tabControlSettings.SelectedIndex = 0;
            tabControlSettings.Size = new Size(784, 326);
            tabControlSettings.TabIndex = 0;
            // 
            // tabPageWebServer
            // 
            tabPageWebServer.Location = new Point(4, 24);
            tabPageWebServer.Name = "tabPageWebServer";
            tabPageWebServer.Padding = new Padding(3);
            tabPageWebServer.Size = new Size(776, 298);
            tabPageWebServer.TabIndex = 0;
            tabPageWebServer.Text = "Webserver/API/PWA";
            tabPageWebServer.UseVisualStyleBackColor = true;
            // 
            // tabPageMultimediaPlayers
            // 
            tabPageMultimediaPlayers.Controls.Add(listBox1);
            tabPageMultimediaPlayers.Location = new Point(4, 24);
            tabPageMultimediaPlayers.Name = "tabPageMultimediaPlayers";
            tabPageMultimediaPlayers.Padding = new Padding(3);
            tabPageMultimediaPlayers.Size = new Size(776, 298);
            tabPageMultimediaPlayers.TabIndex = 1;
            tabPageMultimediaPlayers.Text = "Multimedia players";
            tabPageMultimediaPlayers.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(3, 3);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(770, 292);
            listBox1.TabIndex = 1;
            // 
            // flowLayoutPanelControlButtons
            // 
            flowLayoutPanelControlButtons.AutoSize = true;
            flowLayoutPanelControlButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanelControlButtons.Controls.Add(buttonApply);
            flowLayoutPanelControlButtons.Dock = DockStyle.Bottom;
            flowLayoutPanelControlButtons.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanelControlButtons.Location = new Point(0, 326);
            flowLayoutPanelControlButtons.Name = "flowLayoutPanelControlButtons";
            flowLayoutPanelControlButtons.Padding = new Padding(3);
            flowLayoutPanelControlButtons.Size = new Size(784, 35);
            flowLayoutPanelControlButtons.TabIndex = 0;
            // 
            // buttonApply
            // 
            buttonApply.Location = new Point(700, 6);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(75, 23);
            buttonApply.TabIndex = 0;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // tabPageProjectors
            // 
            tabPageProjectors.Location = new Point(4, 24);
            tabPageProjectors.Name = "tabPageProjectors";
            tabPageProjectors.Padding = new Padding(3);
            tabPageProjectors.Size = new Size(776, 298);
            tabPageProjectors.TabIndex = 2;
            tabPageProjectors.Text = "Projectors";
            tabPageProjectors.UseVisualStyleBackColor = true;
            // 
            // tabPageCameras
            // 
            tabPageCameras.Location = new Point(4, 24);
            tabPageCameras.Name = "tabPageCameras";
            tabPageCameras.Padding = new Padding(3);
            tabPageCameras.Size = new Size(776, 298);
            tabPageCameras.TabIndex = 3;
            tabPageCameras.Text = "Cameras";
            tabPageCameras.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            AcceptButton = buttonApply;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 361);
            Controls.Add(tabControlSettings);
            Controls.Add(flowLayoutPanelControlButtons);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Settings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings | KoKi-Remote";
            Load += Settings_Load;
            tabControlSettings.ResumeLayout(false);
            tabPageMultimediaPlayers.ResumeLayout(false);
            flowLayoutPanelControlButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabControlSettings;
        private TabPage tabPageWebServer;
        private TabPage tabPageMultimediaPlayers;
        private FlowLayoutPanel flowLayoutPanelControlButtons;
        private Button buttonApply;
        private ListBox listBox1;
        private TabPage tabPageProjectors;
        private TabPage tabPageCameras;
    }
}
