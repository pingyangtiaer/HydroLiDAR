using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using System.Threading;
using laslib;
using las.datamanager;

namespace TerraForm
{
	public delegate void PositionChanged(float x, float y, float z);
	
	/// <summary>
	/// part of class dedicated to movement in WorldView
	/// </summary>
	public partial class WorldView : Form
	{
		public event PositionChanged positionChangedEvent;
		
		#region Private fields
		private bool mouseLook = false;
		private int mOldX = -1;
		private int mOldY = -1;

		float xpos = 0, ypos = 0, zpos = 0, xrot = 0, yrot = 0;
		float mouseSensitivity = 200f;
		float keyboardSensitivity = 0.3f;

		private bool m_keyDownW = false;
		private bool m_keyDownS = false;
		private bool m_keyDownA = false;
		private bool m_keyDownD = false;
		private bool m_keyDownE = false;
		private bool m_keyDownV = false;
		private bool m_keyDownSpace = false;
		private bool m_keyDownC = false;
		private bool m_keyDownShift = false; 
		#endregion

		/// <summary>
		/// moves camera to the beginning of the data
		/// </summary>
		internal void MovePositionToData()
		{      
      xpos = (dataSource.GlobalBoundingCube.maxX - dataSource.GlobalBoundingCube.minX)/2.0f;
      ypos = (dataSource.GlobalBoundingCube.maxZ - dataSource.GlobalBoundingCube.minZ) / 2.0f;
      zpos = (dataSource.GlobalBoundingCube.maxY - dataSource.GlobalBoundingCube.minY) / 2.0f;			
      
      xpos = 0;
      ypos = 100;
      zpos = 0;

      yrot = 130;
        
		}

		public void ChangePosition(float x, float y, float z)
		{
			xpos = x;
			ypos = y;
			zpos = z;			
		}

		private new void KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Modifiers & Keys.Shift) != 0)        //shift was pressed
			{
				m_keyDownShift = true;
			}

			switch (e.KeyCode)
			{
				case Keys.W:
					m_keyDownW = true;
					break;
				case Keys.S:
					m_keyDownS = true;
					break;
				case Keys.A:
					m_keyDownA = true;
					break;
				case Keys.D:
					m_keyDownD = true;
					break;
				case Keys.I:
					m_keyDownE = true;
					break;
				case Keys.K:
					m_keyDownV = true;
					break;
				case Keys.C:
					m_keyDownC = true;
					break;
				case Keys.Space:
					m_keyDownSpace = true;
					break;
				case Keys.M:
					mouseLook = !mouseLook;
					break;							
			}

			//ProcessKeys();

			e.Handled = true;

			//set flag to redraw scene to true
			Redraw = true;
		}

		private new void KeyUp(object sender, KeyEventArgs e)
		{
			if ((e.Modifiers & Keys.Shift) != 0)        //shift was pressed
			{
				m_keyDownShift = false;
			}

			switch (e.KeyCode)
			{
				case Keys.W:
					m_keyDownW = false;
					break;
				case Keys.S:
					m_keyDownS = false;
					break;
				case Keys.A:
					m_keyDownA = false;
					break;
				case Keys.D:
					m_keyDownD = false;
					break;
				case Keys.I:
					m_keyDownE = false;
					break;
				case Keys.K:
					m_keyDownV = false;
					break;
				case Keys.C:
					m_keyDownC = false;
					break;
				case Keys.Space:
					m_keyDownSpace = false;
					break;
			}

			//ProcessKeys();

			e.Handled = true;

			//set flag to redraw scene to true
			Redraw = true;
		}


		//processes pressed keys
		private void ProcessKeys()
		{
			double xrotrad, yrotrad;

			float keySens = keyboardSensitivity;

			if (m_keyDownShift)        //shift was pressed
			{
				keySens *= 3.0f;
			}

			if (m_keyDownW)
			{
				yrotrad = (yrot / 180 * 3.141592654f);
				xrotrad = (xrot / 180 * 3.141592654f);
				xpos += (float)(Math.Sin(yrotrad)) * keySens;
				zpos -= (float)(Math.Cos(yrotrad)) * keySens;
				ypos -= (float)(Math.Sin(xrotrad)) * keySens;
			}
			if (m_keyDownS)
			{
				yrotrad = (yrot / 180 * 3.141592654f);
				xrotrad = (xrot / 180 * 3.141592654f);
				xpos -= (float)Math.Sin(yrotrad) * keySens;
				zpos += (float)Math.Cos(yrotrad) * keySens;
				ypos += (float)Math.Sin(xrotrad) * keySens;

			}
			if (m_keyDownA)
			{
				yrotrad = (yrot / 180 * 3.141592654f);
				xpos -= (float)Math.Cos(yrotrad) * keySens;
				zpos -= (float)Math.Sin(yrotrad) * keySens;

			}
			if (m_keyDownD)
			{
				yrotrad = (yrot / 180 * 3.141592654f);
				xpos += (float)Math.Cos(yrotrad) * keySens;
				zpos += (float)Math.Sin(yrotrad) * keySens;
			}
			if (m_keyDownSpace)
			{
				ypos += keySens;
			}
			if (m_keyDownC)
			{
				ypos -= keySens;

			}
			if (m_keyDownE)
			{
				yrotrad = (yrot / 180 * 3.141592654f);
				xpos += (float)(Math.Sin(yrotrad)) * keySens;
				zpos -= (float)(Math.Cos(yrotrad)) * keySens;
			}
			if (m_keyDownV)
			{
				yrotrad = (yrot / 180 * 3.141592654f);
				xpos -= (float)Math.Sin(yrotrad) * keySens;
				zpos += (float)Math.Cos(yrotrad) * keySens;

			}

			if (renderEngine != null)
			{
				renderEngine.ViewerPosition = new las.datamanager.structures.Point3D(xpos, ypos, zpos);
			}

			if (positionChangedEvent != null)
			{
				positionChangedEvent(xpos, ypos, zpos);
			}					
		}
				
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (mOldX < 0)
			{
				mOldX = e.X;
				mOldY = e.Y;
			}

			float dx = (float)(e.X - mOldX) / simpleOpenGLControlWidth;
			float dy = (float)(e.Y - mOldY) / simpleOpenGLControlHeight;


			dx *= mouseSensitivity;
			dy *= mouseSensitivity;

			mOldX = e.X;
			mOldY = e.Y;

			if (mouseLook || (!mouseLook && e.Button == MouseButtons.Left))
			{

				yrot += dx;

				if (yrot < -360)
				{
					yrot += 360;
				}

				xrot += dy;

				if (xrot > 89.99f)
				{
					xrot = 89.99f;
				}
				else if (xrot < -89.99f)
				{
					xrot = -89.99f;
				}

				//set flag to redraw scene to true
				Redraw = true;
			}

		}

		private void OnMouseLeave(object sender, EventArgs e)
		{
			mOldX = -1;
			mOldY = -1;
		}
	}
}
