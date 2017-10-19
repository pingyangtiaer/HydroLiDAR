using System;
using System.Collections.Generic;
using System.Text;

namespace las
{
	public class LasMetrics
	{
		public double indexing = 0;
		public double indexingNoDisk = 0;
		public double avgPointsPerLeaf = 0;
		public double avgPointsPerLeafActual = 0;

		public long numberOfLeafs = 0;
		public long numberOfNonEmptyLeafs = 0;
		public long numberOfPoints = 0;

		public double avgLoad = 0;
		public double avgLoadNoDisk = 0;

    public long bytesTransferedToGPU = 0;
    public long bytesCurrentlyOnGPURAM = 0;

    public long numberOfExistingVBOs = 0;
    public long numberOfPointsLoadedIntoExistingVBOs = 0;

    public TimeSpan pointsInsideViewMilis = new TimeSpan(0);
    public int pointsInsideViewCounted = 0;

    public int pointsDrawn = 0;

    public bool renderToggle = true;
    public bool leafLoadToggle = true;

    private static LasMetrics instance;

    private LasMetrics()
    {
    }

    public static LasMetrics GetInstance()
    {
      if (instance == null)
      {
        instance = new LasMetrics();
      }

      return instance;
    }

	}
}
