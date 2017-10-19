using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TerraForm.Movement
{
	public delegate void ChangePosition(float x, float y, float z);

	public partial class MovementForm : Form
	{
		private float xPos = 0, yPos = 0, zPos = 0;

		public event ChangePosition changePositionEvent;

		public MovementForm()
		{
			InitializeComponent();
		}

		private void forceBtn_Click(object sender, EventArgs e)
		{
			if (changePositionEvent != null)
			{
				changePositionEvent( float.Parse(tbX.Text), float.Parse(tbY.Text), float.Parse(tbZ.Text) );
			}
		}

		public void updatePosition(float x, float y, float z)
		{
			xPos = x;
			yPos = y;
			zPos = z;

			//BeginInvoke(new MethodInvoker(Refresh));
		}

		private void MovementForm_Paint(object sender, PaintEventArgs e)
		{
			lblX.Text = xPos.ToString();
			lblY.Text = yPos.ToString();
			lblZ.Text = zPos.ToString();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			Refresh();
		}		
	}
}
