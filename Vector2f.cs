using System;
using System.Collections.Generic;
using System.Text;

namespace las.datamanager.structures
{
  [Serializable]
  public struct Vector2f
  {
    public float x;
		public float y;

		public Vector2f(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

    public static Vector2f operator +(Vector2f v1, Vector2f v2)
		{
      return new Vector2f(v1.x + v2.x, v1.y + v2.y);
		}

    public static Vector2f operator -(Vector2f v1, Vector2f v2)
		{
			return new Vector2f(v1.x - v2.x, v1.y - v2.y);
		}

    public static Vector2f operator -(Vector2f v)
		{
      return new Vector2f(-v.x, -v.y);
		}

		public void Normalize()
		{
			double length = Math.Sqrt(x * x + y * y);

			x = (float)(x / length);
			y = (float)(y / length);
		}

    public float Length
    {
      get
      {
        return (float)Math.Sqrt(x * x + y * y);
      }
      private set
      {
      }
    }

    public static Vector2f operator *(Vector2f v, float n)
    {
      return new Vector2f(v.x * n, v.y * n);
    }
  }
}
