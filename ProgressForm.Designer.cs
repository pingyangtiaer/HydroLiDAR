namespace TerraForm
{
	partial class ProgressForm
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
      this.progressBarControl1 = new ProgressBarControl.ProgressBarControl();
      this.progressText = new System.Windows.Forms.Label();
      this.progressValue = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // progressBarControl1
      // 
      this.progressBarControl1.BackColor = System.Drawing.Color.Transparent;
      this.progressBarControl1.Location = new System.Drawing.Point(55, 50);
      this.progressBarControl1.Maximum = 100;
      this.progressBarControl1.Minimum = 0;
      this.progressBarControl1.Name = "progressBarControl1";
      this.progressBarControl1.ProgressBarColor = System.Drawing.Color.Blue;
      this.progressBarControl1.Size = new System.Drawing.Size(348, 26);
      this.progressBarControl1.TabIndex = 0;
      this.progressBarControl1.Value = 0;
      // 
      // progressText
      // 
      this.progressText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.progressText.Location = new System.Drawing.Point(51, 17);
      this.progressText.Name = "progressText";
      this.progressText.Size = new System.Drawing.Size(352, 24);
      this.progressText.TabIndex = 1;
      this.progressText.Text = "progressText";
      this.progressText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // progressValue
      // 
      this.progressValue.BackColor = System.Drawing.Color.Transparent;
      this.progressValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.progressValue.ForeColor = System.Drawing.Color.White;
      this.progressValue.Location = new System.Drawing.Point(199, 52);
      this.progressValue.Name = "progressValue";
      this.progressValue.Size = new System.Drawing.Size(80, 20);
      this.progressValue.TabIndex = 2;
      this.progressValue.Text = "99%";
      this.progressValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ProgressForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(455, 93);
      this.ControlBox = false;
      this.Controls.Add(this.progressValue);
      this.Controls.Add(this.progressText);
      this.Controls.Add(this.progressBarControl1);
      this.Name = "ProgressForm";
      this.Opacity = 0.8;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Loading...";
      this.TopMost = true;
      this.ResumeLayout(false);

		}

		#endregion

		private ProgressBarControl.ProgressBarControl progressBarControl1;
		private System.Windows.Forms.Label progressText;
		private System.Windows.Forms.Label progressValue;
	}
}