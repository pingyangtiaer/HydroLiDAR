namespace TerraForm.Configuration
{
  partial class SettingsForm
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
      this.label1 = new System.Windows.Forms.Label();
      this.tbGPUMem = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.tbSameListIt = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.tbClosestListIt = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.tbNormalMethodIt = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.tbPositionRadius = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.tbBufferedDist = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.tbBufferedFOV = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.tbMinDist = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.tbMaxDist = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.tbFOV = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.tbPPL = new System.Windows.Forms.TextBox();
      this.label12 = new System.Windows.Forms.Label();
      this.tbFPSlimit = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.label15 = new System.Windows.Forms.Label();
      this.tbScaleZ = new System.Windows.Forms.TextBox();
      this.label14 = new System.Windows.Forms.Label();
      this.tbScaleY = new System.Windows.Forms.TextBox();
      this.label13 = new System.Windows.Forms.Label();
      this.cbGlobalPointScaleFactor = new System.Windows.Forms.CheckBox();
      this.tbScaleX = new System.Windows.Forms.TextBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(95, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "GPU Memory(MB):";
      // 
      // tbGPUMem
      // 
      this.tbGPUMem.Location = new System.Drawing.Point(149, 19);
      this.tbGPUMem.Name = "tbGPUMem";
      this.tbGPUMem.ShortcutsEnabled = false;
      this.tbGPUMem.Size = new System.Drawing.Size(100, 20);
      this.tbGPUMem.TabIndex = 2;
      this.tbGPUMem.Text = "256";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.tbSameListIt);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.tbClosestListIt);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.tbNormalMethodIt);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Location = new System.Drawing.Point(328, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(230, 111);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Normal Vector Calculation Max Iterations";
      // 
      // tbSameListIt
      // 
      this.tbSameListIt.Location = new System.Drawing.Point(109, 73);
      this.tbSameListIt.Name = "tbSameListIt";
      this.tbSameListIt.ShortcutsEnabled = false;
      this.tbSameListIt.Size = new System.Drawing.Size(38, 20);
      this.tbSameListIt.TabIndex = 10;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 76);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(64, 13);
      this.label4.TabIndex = 9;
      this.label4.Text = "On same list";
      // 
      // tbClosestListIt
      // 
      this.tbClosestListIt.Location = new System.Drawing.Point(109, 49);
      this.tbClosestListIt.Name = "tbClosestListIt";
      this.tbClosestListIt.ShortcutsEnabled = false;
      this.tbClosestListIt.Size = new System.Drawing.Size(38, 20);
      this.tbClosestListIt.TabIndex = 8;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 52);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(72, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "On closest list";
      // 
      // tbNormalMethodIt
      // 
      this.tbNormalMethodIt.Location = new System.Drawing.Point(109, 24);
      this.tbNormalMethodIt.Name = "tbNormalMethodIt";
      this.tbNormalMethodIt.ShortcutsEnabled = false;
      this.tbNormalMethodIt.Size = new System.Drawing.Size(38, 20);
      this.tbNormalMethodIt.TabIndex = 6;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 27);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(84, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Basic expansion";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.tbPositionRadius);
      this.groupBox2.Controls.Add(this.label10);
      this.groupBox2.Controls.Add(this.tbBufferedDist);
      this.groupBox2.Controls.Add(this.label9);
      this.groupBox2.Controls.Add(this.tbBufferedFOV);
      this.groupBox2.Controls.Add(this.label8);
      this.groupBox2.Controls.Add(this.tbMinDist);
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Controls.Add(this.tbMaxDist);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.tbFOV);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.tbGPUMem);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Location = new System.Drawing.Point(12, 12);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(294, 205);
      this.groupBox2.TabIndex = 6;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Rendering";
      // 
      // tbPositionRadius
      // 
      this.tbPositionRadius.Location = new System.Drawing.Point(149, 175);
      this.tbPositionRadius.Name = "tbPositionRadius";
      this.tbPositionRadius.ShortcutsEnabled = false;
      this.tbPositionRadius.Size = new System.Drawing.Size(100, 20);
      this.tbPositionRadius.TabIndex = 14;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(12, 178);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(120, 13);
      this.label10.TabIndex = 13;
      this.label10.Text = "Buffered position radius:";
      // 
      // tbBufferedDist
      // 
      this.tbBufferedDist.Location = new System.Drawing.Point(149, 149);
      this.tbBufferedDist.Name = "tbBufferedDist";
      this.tbBufferedDist.ShortcutsEnabled = false;
      this.tbBufferedDist.Size = new System.Drawing.Size(100, 20);
      this.tbBufferedDist.TabIndex = 12;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(12, 152);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(95, 13);
      this.label9.TabIndex = 11;
      this.label9.Text = "Buffered Distance:";
      // 
      // tbBufferedFOV
      // 
      this.tbBufferedFOV.Location = new System.Drawing.Point(149, 123);
      this.tbBufferedFOV.Name = "tbBufferedFOV";
      this.tbBufferedFOV.ShortcutsEnabled = false;
      this.tbBufferedFOV.Size = new System.Drawing.Size(100, 20);
      this.tbBufferedFOV.TabIndex = 10;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(12, 126);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(74, 13);
      this.label8.TabIndex = 9;
      this.label8.Text = "Buffered FOV:";
      // 
      // tbMinDist
      // 
      this.tbMinDist.Location = new System.Drawing.Point(149, 97);
      this.tbMinDist.Name = "tbMinDist";
      this.tbMinDist.ShortcutsEnabled = false;
      this.tbMinDist.Size = new System.Drawing.Size(100, 20);
      this.tbMinDist.TabIndex = 8;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(12, 100);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(72, 13);
      this.label7.TabIndex = 7;
      this.label7.Text = "Min Distance:";
      // 
      // tbMaxDist
      // 
      this.tbMaxDist.Location = new System.Drawing.Point(149, 71);
      this.tbMaxDist.Name = "tbMaxDist";
      this.tbMaxDist.ShortcutsEnabled = false;
      this.tbMaxDist.Size = new System.Drawing.Size(100, 20);
      this.tbMaxDist.TabIndex = 6;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(12, 74);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(75, 13);
      this.label6.TabIndex = 5;
      this.label6.Text = "Max Distance:";
      // 
      // tbFOV
      // 
      this.tbFOV.Location = new System.Drawing.Point(149, 45);
      this.tbFOV.Name = "tbFOV";
      this.tbFOV.ShortcutsEnabled = false;
      this.tbFOV.Size = new System.Drawing.Size(100, 20);
      this.tbFOV.TabIndex = 4;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(12, 48);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(72, 13);
      this.label5.TabIndex = 3;
      this.label5.Text = "Field-Of-View:";
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(483, 306);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 7;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.tbPPL);
      this.groupBox3.Controls.Add(this.label12);
      this.groupBox3.Controls.Add(this.tbFPSlimit);
      this.groupBox3.Controls.Add(this.label11);
      this.groupBox3.Location = new System.Drawing.Point(328, 123);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(230, 77);
      this.groupBox3.TabIndex = 8;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "General";
      // 
      // tbPPL
      // 
      this.tbPPL.Location = new System.Drawing.Point(135, 48);
      this.tbPPL.Name = "tbPPL";
      this.tbPPL.ShortcutsEnabled = false;
      this.tbPPL.Size = new System.Drawing.Size(65, 20);
      this.tbPPL.TabIndex = 14;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(14, 51);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(101, 13);
      this.label12.TabIndex = 13;
      this.label12.Text = "Max Points per Leaf";
      this.label12.Click += new System.EventHandler(this.label12_Click);
      // 
      // tbFPSlimit
      // 
      this.tbFPSlimit.Location = new System.Drawing.Point(135, 22);
      this.tbFPSlimit.Name = "tbFPSlimit";
      this.tbFPSlimit.ShortcutsEnabled = false;
      this.tbFPSlimit.Size = new System.Drawing.Size(65, 20);
      this.tbFPSlimit.TabIndex = 12;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(14, 25);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(47, 13);
      this.label11.TabIndex = 11;
      this.label11.Text = "FPS limit";
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.label15);
      this.groupBox4.Controls.Add(this.tbScaleZ);
      this.groupBox4.Controls.Add(this.label14);
      this.groupBox4.Controls.Add(this.tbScaleY);
      this.groupBox4.Controls.Add(this.label13);
      this.groupBox4.Controls.Add(this.cbGlobalPointScaleFactor);
      this.groupBox4.Controls.Add(this.tbScaleX);
      this.groupBox4.Location = new System.Drawing.Point(12, 223);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(245, 106);
      this.groupBox4.TabIndex = 15;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Global point scaling factors";
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(98, 77);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(39, 13);
      this.label15.TabIndex = 20;
      this.label15.Text = "scaleZ";
      // 
      // tbScaleZ
      // 
      this.tbScaleZ.Location = new System.Drawing.Point(148, 74);
      this.tbScaleZ.Name = "tbScaleZ";
      this.tbScaleZ.ShortcutsEnabled = false;
      this.tbScaleZ.Size = new System.Drawing.Size(53, 20);
      this.tbScaleZ.TabIndex = 19;
      this.tbScaleZ.Text = "0.1";
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(98, 51);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(39, 13);
      this.label14.TabIndex = 18;
      this.label14.Text = "scaleY";
      // 
      // tbScaleY
      // 
      this.tbScaleY.Location = new System.Drawing.Point(148, 48);
      this.tbScaleY.Name = "tbScaleY";
      this.tbScaleY.ShortcutsEnabled = false;
      this.tbScaleY.Size = new System.Drawing.Size(53, 20);
      this.tbScaleY.TabIndex = 17;
      this.tbScaleY.Text = "0.1";
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(98, 26);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(39, 13);
      this.label13.TabIndex = 16;
      this.label13.Text = "scaleX";
      // 
      // cbGlobalPointScaleFactor
      // 
      this.cbGlobalPointScaleFactor.AutoSize = true;
      this.cbGlobalPointScaleFactor.Location = new System.Drawing.Point(12, 25);
      this.cbGlobalPointScaleFactor.Name = "cbGlobalPointScaleFactor";
      this.cbGlobalPointScaleFactor.Size = new System.Drawing.Size(52, 17);
      this.cbGlobalPointScaleFactor.TabIndex = 16;
      this.cbGlobalPointScaleFactor.Text = "Apply";
      this.cbGlobalPointScaleFactor.UseVisualStyleBackColor = true;
      // 
      // tbScaleX
      // 
      this.tbScaleX.Location = new System.Drawing.Point(148, 23);
      this.tbScaleX.Name = "tbScaleX";
      this.tbScaleX.ShortcutsEnabled = false;
      this.tbScaleX.Size = new System.Drawing.Size(53, 20);
      this.tbScaleX.TabIndex = 12;
      this.tbScaleX.Text = "0.1";
      // 
      // Settings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(580, 341);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Name = "Settings";
      this.Text = "Settings";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbGPUMem;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox tbNormalMethodIt;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbSameListIt;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox tbClosestListIt;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox tbPositionRadius;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox tbBufferedDist;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox tbBufferedFOV;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox tbMinDist;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox tbMaxDist;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox tbFOV;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.TextBox tbFPSlimit;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox tbPPL;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.CheckBox cbGlobalPointScaleFactor;
    private System.Windows.Forms.TextBox tbScaleX;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.TextBox tbScaleZ;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox tbScaleY;
  }
}