using System;
using System.Collections.Generic;
using System.Text;
using las.datamanager.structures;

namespace TerraForm
{
	public enum PointTexture
	{
		Square = 0,
		Circle = 1,
		Gaussian = 2
	}

	
	public struct RenderingConfigurator
	{
		public float pointSize;
		public float Zoffset;
		public ColoringType colorType;
		public float cutoffDistance;		//when to display single distance
		public PointTexture pointTexture;
		public float splatRotationCutoffDistance;    
	}
}
