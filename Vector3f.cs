using System;
using System.Collections.Generic;
using System.Text;

namespace las.datamanager.structures
{  
  [Serializable]
	public struct Vector3f
	{
		public float x;
		public float y;
		public float z;

		public Vector3f(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static Vector3f operator +(Vector3f v1, Vector3f v2)
		{
			return new Vector3f(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
		}

		public static Vector3f operator -(Vector3f v1, Vector3f v2)
		{
			return new Vector3f(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
		}

		public static Vector3f operator -(Vector3f v)
		{
			return new Vector3f(-v.x, -v.y, -v.z);
		}

		public static Vector3f CrossProduct(Vector3f v1, Vector3f v2)
		{
			return new Vector3f(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
		}

		public static Vector3f CrossProduct(Point3D v1, Point3D v2)
		{
			return new Vector3f(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
		}

		public void Normalize()
		{
			double length = Math.Sqrt(x * x + y * y + z * z);

			x = (float)(x / length);
			y = (float)(y / length);
			z = (float)(z / length);
		}

    public float Length
    {
      get
      {
        return (float)Math.Sqrt(x * x + y * y + z * z);
      }
      private set
      {
      }
    }

    public static Vector3f operator *(Vector3f v, float n)
    {
      return new Vector3f(v.x * n, v.y * n, v.z * n);
    }


		/// <summary>
		/// calculates angle to p on XZ plane. Both vectors must be normalized beforehand
		/// </summary>
		/// <param name="p"></param>
		/// <returns>angle, in degrees</returns>
		public float CalculateXZAngle(Vector3f p)
		{
			//angle is scalar product of vects on XZ plane			
			return (float)(Math.Acos(x*p.x + z* p.z) * 57.295779513082320876798154814105);
		}

	}

  

  


}
