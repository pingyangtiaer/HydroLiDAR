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
using las.datamanager;
using las.datamanager.structures;
using System.Diagnostics;

namespace TerraForm
{
  public delegate void UpdateForms();

	public partial class WorldView : Form
	{
		//true if scene should be redrawn
		public static bool Redraw = true;

    public event UpdateForms updateFormsEvent;
    
		/// <summary>
		/// Draws a frame.
		/// </summary>
		private void RenderScene()
		{
			//look at vector. Rotated only around y axis. Presumably we are looking at 0,0, -1
			Vector3f eyeVect = new Vector3f();

			float yrotrad = (float)(0.017453292519943295769236907684886 * (yrot + 90.0));
			eyeVect.x = -(float)Math.Cos(yrotrad);
			eyeVect.y = 0;
			eyeVect.z = -(float)Math.Sin(yrotrad);
			eyeVect.Normalize();

			Gl.glClearColor(1.0f, 1.0f, 1.0f, 1.0f); //clear the color of the window
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT); //Clear teh Color Buffer (more buffers later on)

			Gl.glLoadIdentity(); //load the Identity Matrix

			camera();

			if (renderEngine != null)
			{	
				renderEngine.RenderScene(FOV/1.5f + 0.5f*FOV, nearCull, farCull, eyeVect, xpos, zpos);
			}			
		}
	}
}
