namespace TerraForm.MiniMap
{
  partial class MiniMapForm
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
      this.mapPanel = new System.Windows.Forms.Panel();
      this.refreshTimer = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // mapPanel
      // 
      this.mapPanel.BackColor = System.Drawing.Color.White;
      this.mapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mapPanel.Location = new System.Drawing.Point(0, 0);
      this.mapPanel.Name = "mapPanel";
      this.mapPanel.Size = new System.Drawing.Size(150, 126);
      this.mapPanel.TabIndex = 0;
      this.mapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPanel_Paint);
      // 
      // refreshTimer
      // 
      this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
      // 
      // MiniMapForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(150, 126);
      this.ControlBox = false;
      this.Controls.Add(this.mapPanel);
      this.Location = new System.Drawing.Point(1100, 700);
      this.Name = "MiniMapForm";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Mini Map";
      this.TopMost = true;
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel mapPanel;
    private System.Windows.Forms.Timer refreshTimer;
  }
}