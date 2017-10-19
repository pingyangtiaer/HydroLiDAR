using System;
using System.Collections.Generic;
using System.Text;

namespace las.datamanager.structures
{
  [Serializable]
  public struct Point2f
  {
    public float x;
    public float y;

    public Point2f(float x, float y)
    {
      this.x = x;
      this.y = y;
    }

    public static Point2f operator +(Point2f v1, Point2f v2)
    {
      return new Point2f(v1.x + v2.x, v1.y + v2.y);
    }

    public static Point2f operator -(Point2f v1, Point2f v2)
    {
      return new Point2f(v1.x - v2.x, v1.y - v2.y);
    }

    public static Point2f operator -(Point2f v)
    {
      return new Point2f(-v.x, -v.y);
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

    public static Point2f operator *(Point2f v, float n)
    {
      return new Point2f(v.x * n, v.y * n);
    }

    public override string ToString()
    {
      return String.Format("({0},{1})", x,y);
    }

    /// <summary>
    /// checks if p2 is on the same side of the half plane, 
    /// split by the line formed by a and b, as the 'this' point
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    private bool SameSide(Point2f a, Point2f b, Point2f p2)
    {
      float bax = b.x-a.x;
      float bay = b.y-a.y;

      float thisax = x-a.x;
      float thisay = y-a.y;

      float p2ax = p2.x-a.x;
      float p2ay = p2.y-a.y;

      float z1 = bax * thisay - bay * thisax;
      float z2 = bax * p2ay - bay * p2ax;
     
      //returns true if both are on the same side
      if (z1 > 0)
      {
        return z2 > 0;
      }
      else
      {
        return z2 < 0;
      }
    }

    /// <summary>
    /// checks if point is inside triangle using cross products 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public bool IsPointInTriangle(Point2f a, Point2f b, Point2f c)
    {
      if (SameSide(b, c, a) && SameSide(c, a, b) && SameSide(a, b, c))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// dot product
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static float operator *(Point2f v1, Point2f v2)
    {
      return v1.x * v2.x + v1.y * v2.y;
    }

    /// <summary>
    /// cross product. actually returns only the z coordinate value, since x and y are 0. 
    /// actually returns length of vector Z - you need Z vector when calculating cross product
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static float operator %(Point2f v1, Point2f v2)
    {
      return v1.x * v2.y - v1.y * v2.x;
    }

    public float DistanceTo(Point2f p)
    {
      return (float)Math.Sqrt((p.x - x) * (p.x - x) + (p.y - y) * (p.y - y));
    }

  }
}
