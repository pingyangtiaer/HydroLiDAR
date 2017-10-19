namespace TerraForm.Configuration
{
	partial class ConfigurationForm
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rbMonochrome = new System.Windows.Forms.RadioButton();
      this.rbHeight = new System.Windows.Forms.RadioButton();
      this.rbClassification = new System.Windows.Forms.RadioButton();
      this.pointSizeBar = new System.Windows.Forms.TrackBar();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.rbGauss = new System.Windows.Forms.RadioButton();
      this.rbCircle = new System.Windows.Forms.RadioButton();
      this.rbSquare = new System.Windows.Forms.RadioButton();
      this.zoffsetBar = new System.Windows.Forms.TrackBar();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.lblPointSize = new System.Windows.Forms.Label();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.lblSplatrotationCutoff = new System.Windows.Forms.Label();
      this.trackSplatRotationCutoff = new System.Windows.Forms.TrackBar();
      this.cbPerNodeLOD = new System.Windows.Forms.CheckBox();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pointSizeBar)).BeginInit();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.zoffsetBar)).BeginInit();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackSplatRotationCutoff)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
      this.groupBox1.Controls.Add(this.rbMonochrome);
      this.groupBox1.Controls.Add(this.rbHeight);
      this.groupBox1.Controls.Add(this.rbClassification);
      this.groupBox1.Location = new System.Drawing.Point(12, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(179, 90);
      this.groupBox1.TabIndex = 8;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Coloring mode";
      // 
      // rbMonochrome
      // 
      this.rbMonochrome.AutoSize = true;
      this.rbMonochrome.Location = new System.Drawing.Point(17, 67);
      this.rbMonochrome.Name = "rbMonochrome";
      this.rbMonochrome.Size = new System.Drawing.Size(86, 17);
      this.rbMonochrome.TabIndex = 2;
      this.rbMonochrome.TabStop = true;
      this.rbMonochrome.Text = "monochrome";
      this.rbMonochrome.UseVisualStyleBackColor = true;
      this.rbMonochrome.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
      // 
      // rbHeight
      // 
      this.rbHeight.AutoSize = true;
      this.rbHeight.Location = new System.Drawing.Point(17, 44);
      this.rbHeight.Name = "rbHeight";
      this.rbHeight.Size = new System.Drawing.Size(54, 17);
      this.rbHeight.TabIndex = 1;
      this.rbHeight.TabStop = true;
      this.rbHeight.Text = "height";
      this.rbHeight.UseVisualStyleBackColor = true;
      this.rbHeight.CheckedChanged += new System.EventHandler(this.rbHeight_CheckedChanged);
      // 
      // rbClassification
      // 
      this.rbClassification.AutoSize = true;
      this.rbClassification.Location = new System.Drawing.Point(17, 20);
      this.rbClassification.Name = "rbClassification";
      this.rbClassification.Size = new System.Drawing.Size(85, 17);
      this.rbClassification.TabIndex = 0;
      this.rbClassification.TabStop = true;
      this.rbClassification.Text = "classification";
      this.rbClassification.UseVisualStyleBackColor = true;
      this.rbClassification.CheckedChanged += new System.EventHandler(this.rbClassification_CheckedChanged);
      // 
      // pointSizeBar
      // 
      this.pointSizeBar.Location = new System.Drawing.Point(6, 15);
      this.pointSizeBar.Maximum = 500;
      this.pointSizeBar.Minimum = 1;
      this.pointSizeBar.Name = "pointSizeBar";
      this.pointSizeBar.Size = new System.Drawing.Size(118, 45);
      this.pointSizeBar.TabIndex = 9;
      this.pointSizeBar.TickFrequency = 10;
      this.pointSizeBar.Value = 100;
      this.pointSizeBar.Scroll += new System.EventHandler(this.pointSizeBar_Scroll);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rbGauss);
      this.groupBox2.Controls.Add(this.rbCircle);
      this.groupBox2.Controls.Add(this.rbSquare);
      this.groupBox2.Location = new System.Drawing.Point(10, 172);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(181, 95);
      this.groupBox2.TabIndex = 11;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Point texture";
      // 
      // rbGauss
      // 
      this.rbGauss.AutoSize = true;
      this.rbGauss.Location = new System.Drawing.Point(16, 69);
      this.rbGauss.Name = "rbGauss";
      this.rbGauss.Size = new System.Drawing.Size(69, 17);
      this.rbGauss.TabIndex = 2;
      this.rbGauss.TabStop = true;
      this.rbGauss.Text = "Gaussian";
      this.rbGauss.UseVisualStyleBackColor = true;
      this.rbGauss.CheckedChanged += new System.EventHandler(this.rbGauss_CheckedChanged);
      // 
      // rbCircle
      // 
      this.rbCircle.AutoSize = true;
      this.rbCircle.Location = new System.Drawing.Point(16, 45);
      this.rbCircle.Name = "rbCircle";
      this.rbCircle.Size = new System.Drawing.Size(50, 17);
      this.rbCircle.TabIndex = 1;
      this.rbCircle.TabStop = true;
      this.rbCircle.Text = "circle";
      this.rbCircle.UseVisualStyleBackColor = true;
      this.rbCircle.CheckedChanged += new System.EventHandler(this.rbCircle_CheckedChanged);
      // 
      // rbSquare
      // 
      this.rbSquare.AutoSize = true;
      this.rbSquare.Location = new System.Drawing.Point(16, 21);
      this.rbSquare.Name = "rbSquare";
      this.rbSquare.Size = new System.Drawing.Size(57, 17);
      this.rbSquare.TabIndex = 0;
      this.rbSquare.TabStop = true;
      this.rbSquare.Text = "square";
      this.rbSquare.UseVisualStyleBackColor = true;
      this.rbSquare.CheckedChanged += new System.EventHandler(this.rbSquare_CheckedChanged);
      // 
      // zoffsetBar
      // 
      this.zoffsetBar.Location = new System.Drawing.Point(5, 19);
      this.zoffsetBar.Maximum = 500;
      this.zoffsetBar.Minimum = 1;
      this.zoffsetBar.Name = "zoffsetBar";
      this.zoffsetBar.Size = new System.Drawing.Size(168, 45);
      this.zoffsetBar.TabIndex = 12;
      this.zoffsetBar.TickFrequency = 50;
      this.zoffsetBar.Value = 270;
      this.zoffsetBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.lblPointSize);
      this.groupBox3.Controls.Add(this.pointSizeBar);
      this.groupBox3.Location = new System.Drawing.Point(11, 102);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(180, 64);
      this.groupBox3.TabIndex = 14;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Point size";
      // 
      // lblPointSize
      // 
      this.lblPointSize.AutoSize = true;
      this.lblPointSize.Location = new System.Drawing.Point(130, 24);
      this.lblPointSize.Name = "lblPointSize";
      this.lblPointSize.Size = new System.Drawing.Size(35, 13);
      this.lblPointSize.TabIndex = 10;
      this.lblPointSize.Text = "label1";
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.zoffsetBar);
      this.groupBox4.Location = new System.Drawing.Point(12, 273);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(179, 68);
      this.groupBox4.TabIndex = 15;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Z-offset";
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.lblSplatrotationCutoff);
      this.groupBox5.Controls.Add(this.trackSplatRotationCutoff);
      this.groupBox5.Location = new System.Drawing.Point(12, 353);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(179, 69);
      this.groupBox5.TabIndex = 16;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Splat rotation cutoff distance";
      // 
      // lblSplatrotationCutoff
      // 
      this.lblSplatrotationCutoff.AutoSize = true;
      this.lblSplatrotationCutoff.Location = new System.Drawing.Point(120, 28);
      this.lblSplatrotationCutoff.Name = "lblSplatrotationCutoff";
      this.lblSplatrotationCutoff.Size = new System.Drawing.Size(35, 13);
      this.lblSplatrotationCutoff.TabIndex = 14;
      this.lblSplatrotationCutoff.Text = "label1";
      // 
      // trackSplatRotationCutoff
      // 
      this.trackSplatRotationCutoff.Location = new System.Drawing.Point(5, 19);
      this.trackSplatRotationCutoff.Maximum = 500;
      this.trackSplatRotationCutoff.Minimum = 1;
      this.trackSplatRotationCutoff.Name = "trackSplatRotationCutoff";
      this.trackSplatRotationCutoff.Size = new System.Drawing.Size(109, 45);
      this.trackSplatRotationCutoff.TabIndex = 13;
      this.trackSplatRotationCutoff.TickFrequency = 100;
      this.trackSplatRotationCutoff.Value = 100;
      this.trackSplatRotationCutoff.Scroll += new System.EventHandler(this.trackSplatRotationCutoff_Scroll);
      // 
      // cbPerNodeLOD
      // 
      this.cbPerNodeLOD.AutoSize = true;
      this.cbPerNodeLOD.Checked = true;
      this.cbPerNodeLOD.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbPerNodeLOD.Location = new System.Drawing.Point(17, 434);
      this.cbPerNodeLOD.Name = "cbPerNodeLOD";
      this.cbPerNodeLOD.Size = new System.Drawing.Size(93, 17);
      this.cbPerNodeLOD.TabIndex = 17;
      this.cbPerNodeLOD.Text = "per node LOD";
      this.cbPerNodeLOD.UseVisualStyleBackColor = true;
      this.cbPerNodeLOD.CheckedChanged += new System.EventHandler(this.cbPerNodeLOD_CheckedChanged);
      // 
      // ConfigurationForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(203, 463);
      this.Controls.Add(this.cbPerNodeLOD);
      this.Controls.Add(this.groupBox5);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Location = new System.Drawing.Point(1050, 0);
      this.Name = "ConfigurationForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Configuration";
      this.TopMost = true;
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pointSizeBar)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.zoffsetBar)).EndInit();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackSplatRotationCutoff)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbHeight;
		private System.Windows.Forms.RadioButton rbClassification;
    private System.Windows.Forms.TrackBar pointSizeBar;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rbGauss;
		private System.Windows.Forms.RadioButton rbCircle;
    private System.Windows.Forms.RadioButton rbSquare;
		private System.Windows.Forms.TrackBar zoffsetBar;
    private System.Windows.Forms.RadioButton rbMonochrome;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.TrackBar trackSplatRotationCutoff;
		private System.Windows.Forms.Label lblSplatrotationCutoff;
		private System.Windows.Forms.Label lblPointSize;
    private System.Windows.Forms.CheckBox cbPerNodeLOD;
	}
}