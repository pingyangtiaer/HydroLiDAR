using System;
using System.Collections.Generic;
using System.Text;
using las.datamanager;
using Tao.OpenGl;
using System.Drawing;
using System.Drawing.Imaging;
using Tao.FreeGlut;
using las.datamanager.structures;

namespace TerraForm
{
	public class SimplePointRenderEngine : IRenderEngine
	{			
		public override void Init()
		{
			renderingConfiguration = new RenderingConfigurator();
			renderingConfiguration.cutoffDistance = -1;
			renderingConfiguration.colorType = ColoringType.Classification;
			renderingConfiguration.pointSize = 1;
			renderingConfiguration.pointTexture = PointTexture.Square;
		}

		public override void Destroy()
		{		
		}


		public override void RenderScene(float FOV, float near, float far, Vector3f eyeVector, float xPos, float yPos)
		{
      base.RenderScene(FOV, near, far, eyeVector, xPos, yPos);

			if (dataSource == null)
			{
				return;
			}

			Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
			Gl.glEnableClientState(Gl.GL_NORMAL_ARRAY);
			Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
      dataSource.RenderingPass(FOV, near, far, eyeVector, new Point2f(xPos, yPos));
			Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
			Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
			Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);
			Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);
		}


		public override void RenderingConfigurationChanged(RenderingConfigurator conf)
		{
			renderingConfiguration = conf;
		}

		public override void WindowResized(int width, int height, float FOV)
		{
		}
	}
}
