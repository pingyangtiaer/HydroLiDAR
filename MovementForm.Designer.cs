namespace TerraForm.Movement
{
	partial class MovementForm
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
			this.tbX = new System.Windows.Forms.TextBox();
			this.tbY = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbZ = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.forceBtn = new System.Windows.Forms.Button();
			this.lblX = new System.Windows.Forms.Label();
			this.lblY = new System.Windows.Forms.Label();
			this.lblZ = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(20, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "X: ";
			// 
			// tbX
			// 
			this.tbX.Location = new System.Drawing.Point(103, 6);
			this.tbX.Name = "tbX";
			this.tbX.Size = new System.Drawing.Size(75, 20);
			this.tbX.TabIndex = 1;
			this.tbX.Text = "0.0";
			// 
			// tbY
			// 
			this.tbY.Location = new System.Drawing.Point(103, 32);
			this.tbY.Name = "tbY";
			this.tbY.Size = new System.Drawing.Size(75, 20);
			this.tbY.TabIndex = 3;
			this.tbY.Text = "0.0";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Y: ";
			// 
			// tbZ
			// 
			this.tbZ.Location = new System.Drawing.Point(103, 58);
			this.tbZ.Name = "tbZ";
			this.tbZ.Size = new System.Drawing.Size(75, 20);
			this.tbZ.TabIndex = 5;
			this.tbZ.Text = "0.0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Z: ";
			// 
			// forceBtn
			// 
			this.forceBtn.Location = new System.Drawing.Point(103, 84);
			this.forceBtn.Name = "forceBtn";
			this.forceBtn.Size = new System.Drawing.Size(75, 23);
			this.forceBtn.TabIndex = 6;
			this.forceBtn.Text = "Force";
			this.forceBtn.UseVisualStyleBackColor = true;
			this.forceBtn.Click += new System.EventHandler(this.forceBtn_Click);
			// 
			// lblX
			// 
			this.lblX.AutoSize = true;
			this.lblX.Location = new System.Drawing.Point(38, 9);
			this.lblX.Name = "lblX";
			this.lblX.Size = new System.Drawing.Size(22, 13);
			this.lblX.TabIndex = 7;
			this.lblX.Text = "0.0";
			// 
			// lblY
			// 
			this.lblY.AutoSize = true;
			this.lblY.Location = new System.Drawing.Point(38, 35);
			this.lblY.Name = "lblY";
			this.lblY.Size = new System.Drawing.Size(22, 13);
			this.lblY.TabIndex = 8;
			this.lblY.Text = "0.0";
			// 
			// lblZ
			// 
			this.lblZ.AutoSize = true;
			this.lblZ.Location = new System.Drawing.Point(39, 61);
			this.lblZ.Name = "lblZ";
			this.lblZ.Size = new System.Drawing.Size(22, 13);
			this.lblZ.TabIndex = 9;
			this.lblZ.Text = "0.0";
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// MovementForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(190, 119);
			this.Controls.Add(this.lblZ);
			this.Controls.Add(this.lblY);
			this.Controls.Add(this.lblX);
			this.Controls.Add(this.forceBtn);
			this.Controls.Add(this.tbZ);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbY);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbX);
			this.Controls.Add(this.label1);
			this.Location = new System.Drawing.Point(1000, 500);
			this.Name = "MovementForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Movement";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MovementForm_Paint);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbX;
		private System.Windows.Forms.TextBox tbY;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbZ;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button forceBtn;
		private System.Windows.Forms.Label lblX;
		private System.Windows.Forms.Label lblY;
		private System.Windows.Forms.Label lblZ;
		private System.Windows.Forms.Timer timer1;
	}
}