using System;
using System.Collections.Generic;
using System.Text;

namespace las.datamanager.structures
{
  public class BoundingCube
  {
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    public BoundingCube()
    {
      minX = float.MaxValue;
      maxX = float.MinValue;
      minY = float.MaxValue;
      maxY = float.MinValue;
      minZ = float.MaxValue;
      maxZ = float.MinValue;
    }

    public BoundingCube(float minx, float miny, float minz, float maxx, float maxy, float maxz)
    {
      minX = minx;
      maxX = maxx;
      minY = miny;
      maxY = maxy;
      minZ = minz;
      maxZ = maxz;
    }

    public float width 
    {
      get
      {
        return maxX - minX;
      }

      private set { } 
    }

    public float depth
    {
      get
      {
        return maxY - minY;
      }

      private set { }
    }

    public float height
    {
      get
      {
        return maxZ - minZ;
      }

      private set { }
    }

  }
}
