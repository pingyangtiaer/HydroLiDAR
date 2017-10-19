using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TerraForm
{
	[System.Diagnostics.DebuggerDisplay("")]
	public partial class ProgressForm : Form
	{	

		public ProgressForm()
		{
			InitializeComponent();
		}

		public void setProgress(String text, float percent)
		{
			
			progressText.Text = text;
			progressBarControl1.Value = (int)(percent * 100.0f);

			progressValue.Text = progressBarControl1.Value.ToString() + "%";

      if (percent >= 0.99)
        Hide();
      else
			  Refresh();
		}
	}
}