namespace TerraForm
{
    partial class TerraFormMain
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
          this.components = new System.ComponentModel.Container();
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerraFormMain));
          this.menuStrip = new System.Windows.Forms.MenuStrip();
          this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
          this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
          this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
          this.forcePreprocessingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.renderEngineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.simplePointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.shadersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
          this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStrip = new System.Windows.Forms.ToolStrip();
          this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
          this.statusStrip = new System.Windows.Forms.StatusStrip();
          this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
          this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
          this.menuStrip.SuspendLayout();
          this.toolStrip.SuspendLayout();
          this.statusStrip.SuspendLayout();
          this.SuspendLayout();
          // 
          // menuStrip
          // 
          this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.helpMenu});
          this.menuStrip.Location = new System.Drawing.Point(0, 0);
          this.menuStrip.Name = "menuStrip";
          this.menuStrip.Size = new System.Drawing.Size(1272, 24);
          this.menuStrip.TabIndex = 0;
          this.menuStrip.Text = "MenuStrip";
          // 
          // fileMenu
          // 
          this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
          this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
          this.fileMenu.Name = "fileMenu";
          this.fileMenu.Size = new System.Drawing.Size(35, 20);
          this.fileMenu.Text = "&File";
          // 
          // openToolStripMenuItem
          // 
          this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
          this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
          this.openToolStripMenuItem.Name = "openToolStripMenuItem";
          this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
          this.openToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
          this.openToolStripMenuItem.Text = "&Open";
          this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenFile);
          // 
          // toolStripSeparator3
          // 
          this.toolStripSeparator3.Name = "toolStripSeparator3";
          this.toolStripSeparator3.Size = new System.Drawing.Size(148, 6);
          // 
          // exitToolStripMenuItem
          // 
          this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
          this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
          this.exitToolStripMenuItem.Text = "E&xit";
          this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
          // 
          // editMenu
          // 
          this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forcePreprocessingToolStripMenuItem,
            this.renderEngineToolStripMenuItem,
            this.settingsToolStripMenuItem});
          this.editMenu.Name = "editMenu";
          this.editMenu.Size = new System.Drawing.Size(56, 20);
          this.editMenu.Text = "&Options";
          // 
          // forcePreprocessingToolStripMenuItem
          // 
          this.forcePreprocessingToolStripMenuItem.Checked = true;
          this.forcePreprocessingToolStripMenuItem.CheckOnClick = true;
          this.forcePreprocessingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
          this.forcePreprocessingToolStripMenuItem.Name = "forcePreprocessingToolStripMenuItem";
          this.forcePreprocessingToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
          this.forcePreprocessingToolStripMenuItem.Text = "Force preprocessing";
          // 
          // renderEngineToolStripMenuItem
          // 
          this.renderEngineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simplePointsToolStripMenuItem,
            this.shadersToolStripMenuItem});
          this.renderEngineToolStripMenuItem.Name = "renderEngineToolStripMenuItem";
          this.renderEngineToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
          this.renderEngineToolStripMenuItem.Text = "Render engine";
          // 
          // simplePointsToolStripMenuItem
          // 
          this.simplePointsToolStripMenuItem.Name = "simplePointsToolStripMenuItem";
          this.simplePointsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
          this.simplePointsToolStripMenuItem.Text = "Simple points";
          this.simplePointsToolStripMenuItem.Click += new System.EventHandler(this.simplePointsToolStripMenuItem_Click);
          // 
          // shadersToolStripMenuItem
          // 
          this.shadersToolStripMenuItem.Checked = true;
          this.shadersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
          this.shadersToolStripMenuItem.Name = "shadersToolStripMenuItem";
          this.shadersToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
          this.shadersToolStripMenuItem.Text = "Shaders";
          this.shadersToolStripMenuItem.Click += new System.EventHandler(this.shadersToolStripMenuItem_Click);
          // 
          // settingsToolStripMenuItem
          // 
          this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
          this.settingsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
          this.settingsToolStripMenuItem.Text = "Settings";
          this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
          // 
          // helpMenu
          // 
          this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
          this.helpMenu.Name = "helpMenu";
          this.helpMenu.Size = new System.Drawing.Size(40, 20);
          this.helpMenu.Text = "&Help";
          // 
          // toolStripSeparator8
          // 
          this.toolStripSeparator8.Name = "toolStripSeparator8";
          this.toolStripSeparator8.Size = new System.Drawing.Size(149, 6);
          // 
          // aboutToolStripMenuItem
          // 
          this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
          this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
          this.aboutToolStripMenuItem.Text = "&About ...";
          this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
          // 
          // toolStrip
          // 
          this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton});
          this.toolStrip.Location = new System.Drawing.Point(0, 24);
          this.toolStrip.Name = "toolStrip";
          this.toolStrip.Size = new System.Drawing.Size(1272, 25);
          this.toolStrip.TabIndex = 1;
          this.toolStrip.Text = "ToolStrip";
          // 
          // openToolStripButton
          // 
          this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
          this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
          this.openToolStripButton.Name = "openToolStripButton";
          this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
          this.openToolStripButton.Text = "Open";
          this.openToolStripButton.Click += new System.EventHandler(this.OpenFile);
          // 
          // statusStrip
          // 
          this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
          this.statusStrip.Location = new System.Drawing.Point(0, 968);
          this.statusStrip.Name = "statusStrip";
          this.statusStrip.Size = new System.Drawing.Size(1272, 22);
          this.statusStrip.TabIndex = 2;
          this.statusStrip.Text = "StatusStrip";
          // 
          // toolStripStatusLabel
          // 
          this.toolStripStatusLabel.Name = "toolStripStatusLabel";
          this.toolStripStatusLabel.Size = new System.Drawing.Size(38, 17);
          this.toolStripStatusLabel.Text = "Status";
          // 
          // TerraFormMain
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(1272, 990);
          this.Controls.Add(this.toolStrip);
          this.Controls.Add(this.statusStrip);
          this.Controls.Add(this.menuStrip);
          this.IsMdiContainer = true;
          this.MainMenuStrip = this.menuStrip;
          this.Name = "TerraFormMain";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "TerraForm";
          this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
          this.menuStrip.ResumeLayout(false);
          this.menuStrip.PerformLayout();
          this.toolStrip.ResumeLayout(false);
          this.toolStrip.PerformLayout();
          this.statusStrip.ResumeLayout(false);
          this.statusStrip.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
		 private System.Windows.Forms.ToolTip ToolTip;
     private System.Windows.Forms.ToolStripMenuItem forcePreprocessingToolStripMenuItem;
     private System.Windows.Forms.ToolStripMenuItem renderEngineToolStripMenuItem;
     private System.Windows.Forms.ToolStripMenuItem simplePointsToolStripMenuItem;
     private System.Windows.Forms.ToolStripMenuItem shadersToolStripMenuItem;
     private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}



