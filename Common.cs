using System;
using System.Collections.Generic;
using System.Text;

namespace las.datamanager
{
  [Serializable]
	public class ListInfo
	{
		public uint startingPointIndex;
		public ushort numberOfPoints;		    
	}

	public enum LoadedState
	{
		UNLOADED,			//not loaded
		REQUEST_LOAD,
		BUFFERED_IN_RAM,			//buffered in RAM and out of sight
		BUFFERED_IN_GPU			//buffered on GPU and out of sight
	}

  [Serializable]
	public class VBOids
	{
		public static int VERTEX = 0;
		public static int NORMAL = 1;
		public static int COLOR = 2;
	}

  [Serializable]
	public class ColorIndexes
	{
		public static int R = 0;
		public static int G = 1;
		public static int B = 2;
	}

  public class Constants
  {
    public static float DEG2RAD_MULT = 0.017453292519943295769236907684886f;
  }
}
