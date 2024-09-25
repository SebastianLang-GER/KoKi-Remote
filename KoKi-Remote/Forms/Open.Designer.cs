namespace KoKi_Remote
{
    partial class Open
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Open));
            flowLayoutPanelOpen = new FlowLayoutPanel();
            buttonOpen = new Button();
            labelInformation = new Label();
            linkLabel1 = new LinkLabel();
            flowLayoutPanelOpen.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanelOpen
            // 
            flowLayoutPanelOpen.AutoSize = true;
            flowLayoutPanelOpen.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanelOpen.Controls.Add(buttonOpen);
            flowLayoutPanelOpen.Dock = DockStyle.Bottom;
            flowLayoutPanelOpen.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanelOpen.Location = new Point(0, 126);
            flowLayoutPanelOpen.Name = "flowLayoutPanelOpen";
            flowLayoutPanelOpen.Padding = new Padding(3);
            flowLayoutPanelOpen.Size = new Size(484, 35);
            flowLayoutPanelOpen.TabIndex = 0;
            // 
            // buttonOpen
            // 
            buttonOpen.Location = new Point(400, 6);
            buttonOpen.Name = "buttonOpen";
            buttonOpen.Size = new Size(75, 23);
            buttonOpen.TabIndex = 0;
            buttonOpen.Text = "Open";
            buttonOpen.UseVisualStyleBackColor = true;
            buttonOpen.Click += buttonOpen_Click;
            // 
            // labelInformation
            // 
            labelInformation.AutoSize = true;
            labelInformation.Location = new Point(12, 9);
            labelInformation.Name = "labelInformation";
            labelInformation.Size = new Size(195, 15);
            labelInformation.TabIndex = 1;
            labelInformation.Text = "Open the PWA in your webbrowser";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
            linkLabel1.Location = new Point(12, 24);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(102, 15);
            linkLabel1.TabIndex = 2;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "http://localhost:80";
            // 
            // Open
            // 
            AcceptButton = buttonOpen;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 161);
            Controls.Add(linkLabel1);
            Controls.Add(labelInformation);
            Controls.Add(flowLayoutPanelOpen);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Open";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Open | KoKi-Remote";
            flowLayoutPanelOpen.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelOpen;
        private Button buttonOpen;
        private Label labelInformation;
        private LinkLabel linkLabel1;
    }
}