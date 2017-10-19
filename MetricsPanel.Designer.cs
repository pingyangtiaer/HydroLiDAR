namespace TerraForm.MetricsPanel
{
	partial class MetricsPanel
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
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.indexing = new System.Windows.Forms.Label();
      this.indexingNoDisk = new System.Windows.Forms.Label();
      this.avgLoad = new System.Windows.Forms.Label();
      this.avgLoadNoDisk = new System.Windows.Forms.Label();
      this.avgPPL = new System.Windows.Forms.Label();
      this.avgPPLactual = new System.Windows.Forms.Label();
      this.FPS = new System.Windows.Forms.Label();
      this.pointsDrawn = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.label9 = new System.Windows.Forms.Label();
      this.frameDrawTime = new System.Windows.Forms.Label();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.numberOfNonEmptyLeafs = new System.Windows.Forms.Label();
      this.numberOfPoints = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.lblPointsInsideView = new System.Windows.Forms.Label();
      this.label15 = new System.Windows.Forms.Label();
      this.lblNumPointsInVBOs = new System.Windows.Forms.Label();
      this.label16 = new System.Windows.Forms.Label();
      this.lblNumVBOs = new System.Windows.Forms.Label();
      this.label14 = new System.Windows.Forms.Label();
      this.lblRAM2GPU = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.label13 = new System.Windows.Forms.Label();
      this.btnRenderToggle = new System.Windows.Forms.Button();
      this.lbdfvgsd = new System.Windows.Forms.Label();
      this.lblMemory = new System.Windows.Forms.Label();
      this.btnRefresh = new System.Windows.Forms.Button();
      this.label17 = new System.Windows.Forms.Label();
      this.btnLeafLoading_toggle = new System.Windows.Forms.Button();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 69);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(50, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Indexing:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 121);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(77, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Avg. load (ms):";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 147);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(114, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Avg. load no disk (ms):";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 173);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(101, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Avg. points per leaf:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 199);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(133, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Actual avg. points per leaf:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 95);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(87, 13);
      this.label6.TabIndex = 0;
      this.label6.Text = "Indexing no disk:";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(16, 17);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(30, 13);
      this.label7.TabIndex = 6;
      this.label7.Text = "FPS:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(16, 43);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(71, 13);
      this.label8.TabIndex = 7;
      this.label8.Text = "Points drawn:";
      // 
      // indexing
      // 
      this.indexing.AutoSize = true;
      this.indexing.Location = new System.Drawing.Point(66, 69);
      this.indexing.Name = "indexing";
      this.indexing.Size = new System.Drawing.Size(27, 13);
      this.indexing.TabIndex = 0;
      this.indexing.Text = "num";
      // 
      // indexingNoDisk
      // 
      this.indexingNoDisk.AutoSize = true;
      this.indexingNoDisk.Location = new System.Drawing.Point(99, 95);
      this.indexingNoDisk.Name = "indexingNoDisk";
      this.indexingNoDisk.Size = new System.Drawing.Size(27, 13);
      this.indexingNoDisk.TabIndex = 9;
      this.indexingNoDisk.Text = "num";
      // 
      // avgLoad
      // 
      this.avgLoad.AutoSize = true;
      this.avgLoad.Location = new System.Drawing.Point(99, 121);
      this.avgLoad.Name = "avgLoad";
      this.avgLoad.Size = new System.Drawing.Size(27, 13);
      this.avgLoad.TabIndex = 10;
      this.avgLoad.Text = "num";
      // 
      // avgLoadNoDisk
      // 
      this.avgLoadNoDisk.AutoSize = true;
      this.avgLoadNoDisk.Location = new System.Drawing.Point(126, 147);
      this.avgLoadNoDisk.Name = "avgLoadNoDisk";
      this.avgLoadNoDisk.Size = new System.Drawing.Size(27, 13);
      this.avgLoadNoDisk.TabIndex = 11;
      this.avgLoadNoDisk.Text = "num";
      // 
      // avgPPL
      // 
      this.avgPPL.AutoSize = true;
      this.avgPPL.Location = new System.Drawing.Point(113, 173);
      this.avgPPL.Name = "avgPPL";
      this.avgPPL.Size = new System.Drawing.Size(27, 13);
      this.avgPPL.TabIndex = 12;
      this.avgPPL.Text = "num";
      // 
      // avgPPLactual
      // 
      this.avgPPLactual.AutoSize = true;
      this.avgPPLactual.Location = new System.Drawing.Point(139, 199);
      this.avgPPLactual.Name = "avgPPLactual";
      this.avgPPLactual.Size = new System.Drawing.Size(27, 13);
      this.avgPPLactual.TabIndex = 13;
      this.avgPPLactual.Text = "num";
      // 
      // FPS
      // 
      this.FPS.AutoSize = true;
      this.FPS.Location = new System.Drawing.Point(66, 17);
      this.FPS.Name = "FPS";
      this.FPS.Size = new System.Drawing.Size(27, 13);
      this.FPS.TabIndex = 14;
      this.FPS.Text = "num";
      // 
      // pointsDrawn
      // 
      this.pointsDrawn.Location = new System.Drawing.Point(93, 43);
      this.pointsDrawn.Name = "pointsDrawn";
      this.pointsDrawn.Size = new System.Drawing.Size(90, 13);
      this.pointsDrawn.TabIndex = 15;
      this.pointsDrawn.Text = "num";
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(16, 72);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(87, 13);
      this.label9.TabIndex = 16;
      this.label9.Text = "Frame draw time:";
      // 
      // frameDrawTime
      // 
      this.frameDrawTime.AutoSize = true;
      this.frameDrawTime.Location = new System.Drawing.Point(109, 72);
      this.frameDrawTime.Name = "frameDrawTime";
      this.frameDrawTime.Size = new System.Drawing.Size(27, 13);
      this.frameDrawTime.TabIndex = 17;
      this.frameDrawTime.Text = "num";
      // 
      // tabControl1
      // 
      this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.HotTrack = true;
      this.tabControl1.Location = new System.Drawing.Point(1, 3);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(201, 258);
      this.tabControl1.TabIndex = 18;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.numberOfNonEmptyLeafs);
      this.tabPage1.Controls.Add(this.numberOfPoints);
      this.tabPage1.Controls.Add(this.label11);
      this.tabPage1.Controls.Add(this.label10);
      this.tabPage1.Controls.Add(this.label5);
      this.tabPage1.Controls.Add(this.label1);
      this.tabPage1.Controls.Add(this.label2);
      this.tabPage1.Controls.Add(this.label3);
      this.tabPage1.Controls.Add(this.label4);
      this.tabPage1.Controls.Add(this.avgPPLactual);
      this.tabPage1.Controls.Add(this.label6);
      this.tabPage1.Controls.Add(this.avgPPL);
      this.tabPage1.Controls.Add(this.indexing);
      this.tabPage1.Controls.Add(this.avgLoadNoDisk);
      this.tabPage1.Controls.Add(this.indexingNoDisk);
      this.tabPage1.Controls.Add(this.avgLoad);
      this.tabPage1.Location = new System.Drawing.Point(4, 25);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(193, 229);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "LAS Data";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // numberOfNonEmptyLeafs
      // 
      this.numberOfNonEmptyLeafs.AutoSize = true;
      this.numberOfNonEmptyLeafs.Location = new System.Drawing.Point(151, 43);
      this.numberOfNonEmptyLeafs.Name = "numberOfNonEmptyLeafs";
      this.numberOfNonEmptyLeafs.Size = new System.Drawing.Size(27, 13);
      this.numberOfNonEmptyLeafs.TabIndex = 17;
      this.numberOfNonEmptyLeafs.Text = "num";
      // 
      // numberOfPoints
      // 
      this.numberOfPoints.AutoSize = true;
      this.numberOfPoints.Location = new System.Drawing.Point(102, 16);
      this.numberOfPoints.Name = "numberOfPoints";
      this.numberOfPoints.Size = new System.Drawing.Size(27, 13);
      this.numberOfPoints.TabIndex = 16;
      this.numberOfPoints.Text = "num";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(6, 16);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(90, 13);
      this.label11.TabIndex = 15;
      this.label11.Text = "Number of points:";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(6, 43);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(139, 13);
      this.label10.TabIndex = 14;
      this.label10.Text = "Number of non-empty leafs::";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.lblPointsInsideView);
      this.tabPage2.Controls.Add(this.label15);
      this.tabPage2.Controls.Add(this.lblNumPointsInVBOs);
      this.tabPage2.Controls.Add(this.label16);
      this.tabPage2.Controls.Add(this.lblNumVBOs);
      this.tabPage2.Controls.Add(this.label14);
      this.tabPage2.Controls.Add(this.lblRAM2GPU);
      this.tabPage2.Controls.Add(this.label12);
      this.tabPage2.Controls.Add(this.label7);
      this.tabPage2.Controls.Add(this.frameDrawTime);
      this.tabPage2.Controls.Add(this.label8);
      this.tabPage2.Controls.Add(this.label9);
      this.tabPage2.Controls.Add(this.FPS);
      this.tabPage2.Controls.Add(this.pointsDrawn);
      this.tabPage2.Location = new System.Drawing.Point(4, 25);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(193, 229);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Rendering";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // lblPointsInsideView
      // 
      this.lblPointsInsideView.AutoSize = true;
      this.lblPointsInsideView.Location = new System.Drawing.Point(137, 175);
      this.lblPointsInsideView.Name = "lblPointsInsideView";
      this.lblPointsInsideView.Size = new System.Drawing.Size(27, 13);
      this.lblPointsInsideView.TabIndex = 25;
      this.lblPointsInsideView.Text = "num";
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(16, 175);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(115, 13);
      this.label15.TabIndex = 24;
      this.label15.Text = "Points in view avg(ms):";
      // 
      // lblNumPointsInVBOs
      // 
      this.lblNumPointsInVBOs.AutoSize = true;
      this.lblNumPointsInVBOs.Location = new System.Drawing.Point(117, 148);
      this.lblNumPointsInVBOs.Name = "lblNumPointsInVBOs";
      this.lblNumPointsInVBOs.Size = new System.Drawing.Size(27, 13);
      this.lblNumPointsInVBOs.TabIndex = 23;
      this.lblNumPointsInVBOs.Text = "num";
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Location = new System.Drawing.Point(16, 148);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(80, 13);
      this.label16.TabIndex = 22;
      this.label16.Text = "Points in VBOs:";
      // 
      // lblNumVBOs
      // 
      this.lblNumVBOs.AutoSize = true;
      this.lblNumVBOs.Location = new System.Drawing.Point(117, 124);
      this.lblNumVBOs.Name = "lblNumVBOs";
      this.lblNumVBOs.Size = new System.Drawing.Size(27, 13);
      this.lblNumVBOs.TabIndex = 21;
      this.lblNumVBOs.Text = "num";
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(16, 124);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(89, 13);
      this.label14.TabIndex = 20;
      this.label14.Text = "Number of VBOs:";
      // 
      // lblRAM2GPU
      // 
      this.lblRAM2GPU.AutoSize = true;
      this.lblRAM2GPU.Location = new System.Drawing.Point(117, 98);
      this.lblRAM2GPU.Name = "lblRAM2GPU";
      this.lblRAM2GPU.Size = new System.Drawing.Size(27, 13);
      this.lblRAM2GPU.TabIndex = 19;
      this.lblRAM2GPU.Text = "num";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(16, 98);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(95, 13);
      this.label12.TabIndex = 18;
      this.label12.Text = "RAM2GPU in MiB:";
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.label17);
      this.tabPage3.Controls.Add(this.btnLeafLoading_toggle);
      this.tabPage3.Controls.Add(this.label13);
      this.tabPage3.Controls.Add(this.btnRenderToggle);
      this.tabPage3.Controls.Add(this.lbdfvgsd);
      this.tabPage3.Controls.Add(this.lblMemory);
      this.tabPage3.Controls.Add(this.btnRefresh);
      this.tabPage3.Location = new System.Drawing.Point(4, 25);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(193, 229);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Diag";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(14, 120);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(45, 13);
      this.label13.TabIndex = 31;
      this.label13.Text = "Render:";
      // 
      // btnRenderToggle
      // 
      this.btnRenderToggle.Location = new System.Drawing.Point(112, 115);
      this.btnRenderToggle.Name = "btnRenderToggle";
      this.btnRenderToggle.Size = new System.Drawing.Size(75, 23);
      this.btnRenderToggle.TabIndex = 30;
      this.btnRenderToggle.Text = "Stop";
      this.btnRenderToggle.UseVisualStyleBackColor = true;
      this.btnRenderToggle.Click += new System.EventHandler(this.btnRenderToggle_Click);
      // 
      // lbdfvgsd
      // 
      this.lbdfvgsd.AutoSize = true;
      this.lbdfvgsd.Location = new System.Drawing.Point(14, 18);
      this.lbdfvgsd.Name = "lbdfvgsd";
      this.lbdfvgsd.Size = new System.Drawing.Size(47, 13);
      this.lbdfvgsd.TabIndex = 28;
      this.lbdfvgsd.Text = "Memory:";
      // 
      // lblMemory
      // 
      this.lblMemory.AutoSize = true;
      this.lblMemory.Location = new System.Drawing.Point(64, 18);
      this.lblMemory.Name = "lblMemory";
      this.lblMemory.Size = new System.Drawing.Size(27, 13);
      this.lblMemory.TabIndex = 29;
      this.lblMemory.Text = "num";
      // 
      // btnRefresh
      // 
      this.btnRefresh.Location = new System.Drawing.Point(111, 43);
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new System.Drawing.Size(75, 23);
      this.btnRefresh.TabIndex = 27;
      this.btnRefresh.Text = "Refresh";
      this.btnRefresh.UseVisualStyleBackColor = true;
      this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Location = new System.Drawing.Point(13, 91);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(68, 13);
      this.label17.TabIndex = 33;
      this.label17.Text = "Leaf loading:";
      // 
      // btnLeafLoading_toggle
      // 
      this.btnLeafLoading_toggle.Location = new System.Drawing.Point(111, 86);
      this.btnLeafLoading_toggle.Name = "btnLeafLoading_toggle";
      this.btnLeafLoading_toggle.Size = new System.Drawing.Size(75, 23);
      this.btnLeafLoading_toggle.TabIndex = 32;
      this.btnLeafLoading_toggle.Text = "Stop";
      this.btnLeafLoading_toggle.UseVisualStyleBackColor = true;
      this.btnLeafLoading_toggle.Click += new System.EventHandler(this.btnLeafLoading_toggle_Click);
      // 
      // MetricsPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(203, 259);
      this.Controls.Add(this.tabControl1);
      this.Location = new System.Drawing.Point(1050, 390);
      this.Name = "MetricsPanel";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Metrics";
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label indexing;
		private System.Windows.Forms.Label indexingNoDisk;
		private System.Windows.Forms.Label avgLoad;
		private System.Windows.Forms.Label avgLoadNoDisk;
		private System.Windows.Forms.Label avgPPL;
		private System.Windows.Forms.Label avgPPLactual;
		private System.Windows.Forms.Label FPS;
		private System.Windows.Forms.Label pointsDrawn;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label frameDrawTime;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label numberOfNonEmptyLeafs;
		private System.Windows.Forms.Label numberOfPoints;
    private System.Windows.Forms.Label lblRAM2GPU;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label lblNumPointsInVBOs;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Label lblNumVBOs;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Label lblPointsInsideView;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.Label lbdfvgsd;
    private System.Windows.Forms.Label lblMemory;
    private System.Windows.Forms.Button btnRefresh;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Button btnRenderToggle;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.Button btnLeafLoading_toggle;
	}
}