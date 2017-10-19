using System;
using System.Collections.Generic;
using System.Text;
using las.datamanager;
using Tao.OpenGl;
using las.datamanager.structures;

namespace TerraForm
{
	public abstract class IRenderEngine
	{
		protected LasDataManager dataSource;

		private Point3D viewPosition;
		
		protected RenderingConfigurator renderingConfiguration;
				
		public Point3D ViewerPosition
		{
			get { return viewPosition; }
			set { viewPosition = value; }
		}

    //minimap reference
    public MiniMap.MiniMapForm Minimap { get; set; }

		public LasDataManager DataSource
		{
			get { return dataSource; }
			
      set 
      { 
        dataSource = value;

        if (Minimap != null)
        {
          Minimap.dataSource = value;
        }
      }
		}

		public abstract void Init();
		public abstract void Destroy();

		public abstract void WindowResized(int width, int height, float FOV);

		/// <summary>
		/// renders data contained in dataSource
		/// </summary>
    public virtual void RenderScene(float FOV, float near, float far, Vector3f eyeVector, float xPos, float yPos)
    {
      LasDataManager.setFOV(FOV);

      if (Minimap != null)
      {
        Minimap.updateMap(FOV, near, far, eyeVector, xPos, yPos);        
      }
    }

		public abstract void RenderingConfigurationChanged(RenderingConfigurator conf);



	}
}
