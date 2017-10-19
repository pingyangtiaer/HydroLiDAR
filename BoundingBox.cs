using System;
using System.Collections.Generic;
using System.Text;

namespace las.datamanager.structures
{
  [Serializable]
  public struct BoundingBox
  {
    public float x;
    public float y;
    public float width;
    public float height;

    //min and max values of points within the box
    public float minZ;
    public float maxZ;

    public BoundingBox(float x, float y, float width, float height)
    {
      this.x = x;
      this.y = y;
      this.height = height;
      this.width = width;

      minZ = float.MaxValue;
      maxZ = float.MinValue;
    }

    public bool contains(float X, float Y)
    {
      if (X >= x && X <= x + width &&
        Y >= y && Y <= y + height)
        return true;

      return false;
    }

    public bool contains(Point2f p)
    {
      if (p.x >= x && p.x <= x + width &&
        p.y >= y && p.y <= y + height)
        return true;

      return false;
    }

    public bool contains(Vector2f v)
    {
      if (v.x >= x && v.x <= x + width &&
        v.y >= y && v.y <= y + height)
        return true;

      return false;
    }

    public bool contains(Point3D p)
    {
      if (p.x >= x && p.x <= x + width &&    
        p.y >= y && p.y <= y + height)
        return true;

      return false;
    }

    public Point2f Center
    {
      get { return new Point2f(x + width / 2, y + height / 2); }
    }

    public Point2f TopLeft
    {
      get { return new Point2f(x, y); }
    }

    public Point2f TopRight
    {
      get { return new Point2f(x + width, y); }
    }

    public Point2f BottomLeft
    {
      get { return new Point2f(x, y + height); }
    }

    public Point2f BottomRight
    {
      get { return new Point2f(x + width, y + height); }
    }

    public Point2f[] Corners
    {
      get
      {
        Point2f[] corners = new Point2f[4];
        corners[0].x = x;
        corners[0].y = y;
        corners[1].x = x + width;
        corners[1].y = y;
        corners[2].x = x;
        corners[2].y = y + height;
        corners[3].x = x + width;
        corners[3].y = y + height;

        return corners;
      }
    }

    public bool intersects(Point2f p1, Point2f p2, Point2f p3)
    {
      return intersectsLine(p1.x, p1.y, p2.x, p2.y)
           || intersectsLine(p2.x, p2.y, p3.x, p3.y)
           || intersectsLine(p3.x, p3.y, p1.x, p1.y);
    }

    private bool intersectsLine(float x0, float y0, float x1, float y1)
    {
      float top_intersection, bottom_intersection, toptrianglepoint, bottomtrianglepoint;

      //k and n parameters if line
      float k = (y1 - y0) / (x1 - x0);
      float n = y0 - (k * x0);

      //k > 0 means ascending line, therefore top intersection is with the right line of rect
      if (k > 0)
      {
        top_intersection = (k * x + n);
        bottom_intersection = (k * (x + width) + n);
      }
      else
      {
        top_intersection = (k * (x + width) + n);
        bottom_intersection = (k * x + n);
      }

      // work out the top and bottom extents for the triangle
      if (y0 < y1)
      {
        toptrianglepoint = y0;
        bottomtrianglepoint = y1;
      }
      else
      {
        toptrianglepoint = y1;
        bottomtrianglepoint = y0;
      }

      // and calculate the overlap between those two bounds
      float topoverlap = top_intersection > toptrianglepoint ? top_intersection : toptrianglepoint;
      float botoverlap = bottom_intersection < bottomtrianglepoint ? bottom_intersection : bottomtrianglepoint;

      // (topoverlap<botoverlap) :
      // if the intersection isn't the right way up then we have no overlap

      // (!((botoverlap<t) || (topoverlap>b)) :
      // If the bottom overlap is higher than the top of the rectangle or the top overlap is
      // lower than the bottom of the rectangle we don't have intersection. So return the negative
      // of that. Much faster than checking each of the points is within the bounds of the rectangle.
      return (topoverlap < botoverlap) && (!((botoverlap < y) || (topoverlap > (y + height))));

    }


  }
}
