using System;
using System.Collections.Generic;
using System.Text;

namespace las.datamanager.structures
{
  [Serializable]
	public struct Point3D
	{
		public float x;
		public float y;
		public float z;

		public float nx;
		public float ny;
		public float nz;

		public byte colorIndex;

    public bool pointAvailable;   //flag for used in multiresolution LOD

    public float Length
    {
      get
      {
        return (float)Math.Sqrt(x * x + y * y + z * z);
      }
      private set
      {
        ;
      }
    }

		public Point3D(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;

			this.nx = 0;
			this.ny = 0;
			this.nz = 0;

			colorIndex = 0;
      pointAvailable = false;
		}

		public float SquareDistance(Point3D other)
		{
			return (x - other.x) * (x - other.x) + (y - other.y) * (y - other.y) + (z - other.z) * (z - other.z);
		}


		public static Point3D operator +(Point3D p1, Point3D p2)
		{
			return new Point3D(p1.x+p2.x, p1.y+p2.y, p1.z+p2.z);
		}

		public static Point3D operator -(Point3D p1, Point3D p2)
		{
			return new Point3D(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
		}

    
	}
}
