using System;
using System.Collections.Generic;
using System.Text;

namespace TerraForm
{
	public class RenderMetrics
	{
		public float FPS;
		public float frameRenderTime;
		
    private static RenderMetrics instance = null;

    private RenderMetrics()
    {
    }

    public static RenderMetrics getInstance()
    {
      if (instance == null)
      {
        instance = new RenderMetrics();
      }

      return instance;
    }

	}
}
