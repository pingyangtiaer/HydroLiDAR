using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using Tao.OpenGl;
using System.Threading;
using las.datamanager.structures;

namespace las.datamanager
{
  //requests a leaf to be loaded into RAM
  public delegate void LeafLoadRequest(QTreeLeaf leaf);

  [Serializable]
  public class QTreeLeaf
  {
    public Guid ParentTreeID { get; private set; }

    private QTreeNode ParentNode { get; set; }

    //if newdata was loaded on the GPU and screen needs redrawing
    public static bool LoadedNewData = true;

    //threshold used in testing for long and narrow triangles. It looks at ratio of two sides agains one
    public const float LONG_NARROW_THRESHOLD = 0.1f;      

    private int numberOfPoints;

    public int numberOfLoadedPoints;

    public static int OPENGL_PRIMITIVE = Gl.GL_POINTS;

    public LoadedState State = LoadedState.UNLOADED;

    public float SelectionLOD = 1f;

    public static float ReductionToParentCoef = 0.7f;   // percentage of points that will be contained on the in the parent node(s). 0.7 means that 30% of points will be on the above level

    public BoundingBox boundingBox;

    //class containg info about VBO
    [NonSerialized]
    private VBOStorageInformation serverBufferId;

    [NonSerialized]
    private Stack<VBOStorageInformation> parentVBOs;
        
    public bool visible = false;

    public List<Point3D[]> ContainedPointLists = new List<Point3D[]>();

    //indices of points
    public List<ListInfo> ListInfos;		
        
    private static Random rand = new Random((int)System.DateTime.Now.Ticks);
    private static float[] randArray = null;

    public static event LeafLoadRequest LeafLoadRequestEvent;

    //number of points designated for this leaf
    public int NumberOfPoints
    {
      get { return numberOfPoints; }
    }

    public QTreeLeaf(BoundingBox bb, Guid parentTreeID, QTreeNode parentNode)
    {
      ParentTreeID = parentTreeID;
      this.ParentNode = parentNode;

      QTreeWrapper parent = LasDataManager.GetInstance().GetQtreeByGuid(parentTreeID);
      parentVBOs = new Stack<VBOStorageInformation>();   //stored on this level

      ListInfos = new List<ListInfo>();
      LasMetrics.GetInstance().numberOfLeafs++;

      boundingBox = bb;

      if (randArray == null)
      {
        randArray = new float[3000];

        for (int i = 0; i < randArray.Length; i++)
        {
          randArray[i] = (float)rand.NextDouble();
        }
      }
    }

    public static Stopwatch findStepStopWatch = new Stopwatch();
    public static int numberOfSmallLists = 0;
    public static Stopwatch arrayCompyTimer = new Stopwatch();

    /// <summary>
    /// insert points into this node. Method does not check if these points belong here or not
    /// </summary>
    /// <param name="firstPointIndex">index of first point in source array in las file</param>
    /// <param name="source"></param>
    /// <param name="fromIdx"></param>
    /// <returns>next unused point or source.Length if all points were used</returns>
    internal int InsertPreliminary(uint firstPointIndex, Point3D[] source, int fromIdx, float selectionLOD)
    {
      findStepStopWatch.Start();
      int toIndex = findEndWithStep(source, fromIdx, source.Length - 1);

      findStepStopWatch.Stop();

      int sourcePtsLength = toIndex - fromIdx + 1;

      if (numberOfPoints == 0 && sourcePtsLength > 0)
      {
        LasMetrics.GetInstance().numberOfNonEmptyLeafs++;
      }
      numberOfPoints += sourcePtsLength;
      
      if (toIndex - fromIdx < 0)
      {
        numberOfSmallLists++;
        //do not add
      }
      else
      {
        this.SelectionLOD = selectionLOD;

        ListInfo listInfo = new ListInfo();
        listInfo.numberOfPoints = (ushort)sourcePtsLength;
        listInfo.startingPointIndex = firstPointIndex + (uint)fromIdx;		//have to add offset to get proper point        
        ListInfos.Add(listInfo);
      }

      return toIndex + 1;		//should be pointing on next available point if any
    }

    private bool pointsMutex = false;

    /// <summary>
    /// removes all points from Contained point lists
    /// </summary>
    public bool ClearAllPoints()
    {
      if (pointsMutex == false)
      {
        pointsMutex = true;
        ContainedPointLists.Clear();

        if (State == LoadedState.BUFFERED_IN_RAM)
        {
          State = LoadedState.UNLOADED;
        }
        pointsMutex = false;

        return true;
      }

      return false;
    }


    /// <summary>
    /// calculates normals for all loaded points in this Leaf
    /// </summary>
    public bool CalculateNormals()
    {
      while (pointsMutex == true)
        ;

      pointsMutex = true;

      if (ContainedPointLists.Count < 2 || LasDataManager.CALCULATE_NORMALS == false)
      {
        return false;
      }

      for (int i = 0; i < ContainedPointLists.Count; i++)
      {
        CalculateNormals(i);
      }

      pointsMutex = false;

      return true;
    }

    public static int MAX_ITERATIONS_NORMAL = 5;
    public static int MAX_ITERATIONS_SECOND = 6;
    public static int MAX_ITERATIONS_SAME = 15;
        
    /// <summary>
    /// calculates normals for point list at specified index
    /// </summary>
    /// <param name="pointListIdx"></param>
    private void CalculateNormals(int pointListIdx)
    {

      //2 consecutive points from the pointListIdx array will be taken and closest from the previous 
      //or next point list

      Point3D[] pointList = ContainedPointLists[pointListIdx];

      int len = pointList.Length;
      int closestPointIdxPrevList = -1;
      int closestPointIdxNextList = -1;

      int closestListIdx = -1;
      int closestPointIdxOnClosestList = -1;

      //iterate over array and calculate normals for one point back. last point will have same normal as one before
      for (int i = 1; i < pointList.Length - 1; i++)
      {
        Point3D p1 = pointList[i];
        Point3D p2 = pointList[i - 1];
        Point3D p3;

        closestPointIdxPrevList = findClosestPoint(pointListIdx - 1, p2, p1, i);
        closestPointIdxNextList = findClosestPoint(pointListIdx + 1, p2, p1, i);

        if (closestPointIdxPrevList < 0 && closestPointIdxNextList < 0)
        {
          //problem - only one list in BB?!     
          pointList[i].nx = 0;
          pointList[i].ny = 1;
          pointList[i].nz = 0;
          continue;
        }
        else
        {
          //calculate and compare distances from closest points on other lists between each other and pick
          //the point that is closer
          float len1 = float.MaxValue;
          float len2 = float.MaxValue;

          if (closestPointIdxPrevList >= 0)
          {
            Point3D p3a = ContainedPointLists[pointListIdx - 1][closestPointIdxPrevList];
            len1 = (p3a - p1).Length + (p3a - p2).Length;
          }

          if (closestPointIdxNextList >= 0)
          {
            Point3D p3b = ContainedPointLists[pointListIdx + 1][closestPointIdxNextList];
            len2 = (p3b - p1).Length + (p3b - p2).Length;
          }

          if (len1 < len2)
          {
            p3 = ContainedPointLists[pointListIdx - 1][closestPointIdxPrevList];
            closestListIdx = pointListIdx - 1;
            closestPointIdxOnClosestList = closestPointIdxPrevList;
          }
          else
          {
            p3 = ContainedPointLists[pointListIdx + 1][closestPointIdxNextList];
            closestListIdx = pointListIdx + 1;
            closestPointIdxOnClosestList = closestPointIdxNextList;
          }
        }
                
        //handle long And narrow triangles by expanding p1 and p2 farther away from each other        
        if (QTreeLeaf.isLongAndNarrow(p1, p2, p3))
        {
          bool solutionFound = false;

          int leftIndex = i - 2;
          int rightIndex = i + 1;

          Point3D left = p1, right = p2;
          int iterationsNeeded = 0;
          while (leftIndex >= 0 && rightIndex < pointList.Length && iterationsNeeded < MAX_ITERATIONS_NORMAL)
          {
            iterationsNeeded++;

            p1 = pointList[leftIndex];
            p2 = pointList[rightIndex];

            if (!isLongAndNarrow(p1, p2, p3))
            {
              solutionFound = true;
              break;
            }

            leftIndex--;
            rightIndex++;
          }

          if (!solutionFound)
          {
            p1 = pointList[i];

            iterationsNeeded = 0;

            //go through closest list and all the point there to find a more suitable p2
            for (int cli = closestPointIdxOnClosestList + 1; cli < ContainedPointLists[closestListIdx].Length
              && iterationsNeeded < MAX_ITERATIONS_SECOND
              ; cli++)
            {
              p2 = ContainedPointLists[closestListIdx][cli];

              iterationsNeeded++;

              if (!isLongAndNarrow(p1, p2, p3))
              {
                solutionFound = true;
                break;
              }
            }

            if (!solutionFound)
            {
              iterationsNeeded = 0;
              //try the other way too
              for (int cli = closestPointIdxOnClosestList - 1; cli >= 0
                && iterationsNeeded < MAX_ITERATIONS_SECOND; cli--)
              {
                p2 = ContainedPointLists[closestListIdx][cli];

                iterationsNeeded++;

                if (!isLongAndNarrow(p1, p2, p3))
                {
                  solutionFound = true;
                  break;
                }
              }
            }

            if (!solutionFound)
            {
              //TODO: find yet another way or revert to the first three points
              p2 = pointList[i - 1];

              //seach for p3 on this same list, in case it goes back and forth like it sometimes does

              iterationsNeeded = 0;
              //up to point i
              for (int s = 0; s < pointList.Length && iterationsNeeded < MAX_ITERATIONS_SAME; s++)
              {
                if (s < i - 5 || s > i + 5)
                {
                  Point3D ppp = pointList[s];
                  iterationsNeeded++;

                  if (!isLongAndNarrow(p1, p2, ppp))
                  {
                    solutionFound = true;
                    break;
                  }
                }
              }

            }
          }
        }


        //Vector3f normal = Vector3f.CrossProduct(pointList[i - 1] - pointList[i+1], pointList[i+1] - closestPointList[closestPointIdx]);
        //Vector3f normal = Vector3f.CrossProduct(pointList[i - 1] - pointList[i + 1], closestPointList[closestPointIdx] - pointList[i + 1]);
        Vector3f normal = Vector3f.CrossProduct(p2 - p1, p3 - p1);

        normal.Normalize();
        pointList[i].nx = normal.x;
        pointList[i].ny = normal.y;
        pointList[i].nz = normal.z;

        if (normal.z < 0)    //z and y coordinates are inverted!! - Z is height here
        {
          pointList[i].nx = -normal.x;
          pointList[i].ny = -normal.y;
          pointList[i].nz = -normal.z;

        }
      }

      //last point has same normal as the one before her. first one is same as second one
      if (len > 1)
      {
        pointList[0].nx = pointList[1].nx;
        pointList[0].ny = pointList[1].ny;
        pointList[0].nz = pointList[1].nz;


        pointList[len - 1].nx = pointList[len - 2].nx;
        pointList[len - 1].ny = pointList[len - 2].ny;
        pointList[len - 1].nz = pointList[len - 2].nz;
      }
    }

    /// <summary>
    /// finds closest point to points p1 and p2 on list with index onListIndex
    /// </summary>
    /// <param name="onListIndex"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    protected int findClosestPoint(int onListIndex, Point3D p1, Point3D p2, int startIndex)
    {
      if (onListIndex < 0 || onListIndex >= ContainedPointLists.Count)
      {
        return -1;
      }

      Point3D[] pl = ContainedPointLists[onListIndex];

      int closestIndex = -1;
      float minLength = float.MaxValue;

      int consecutiveIncrementsInLength = 0;
      int consecutiveIncrementsInLengthThreshold = 5;


      //search in the positiove direction
      for (int i = startIndex; i < pl.Length; i++)
      {
        Point3D third = pl[i];

        //calculate distances to both points
        float lengthSum = (p1 - third).Length + (p2 - third).Length;

        if (minLength > lengthSum)
        {
          minLength = lengthSum;
          closestIndex = i;
          consecutiveIncrementsInLength = 0;
        }
        else
        {
          consecutiveIncrementsInLength++;
        }

        //if length increases too much there is no point in checking additional points
        if (consecutiveIncrementsInLength == consecutiveIncrementsInLengthThreshold)
        {
          break;
        }
      }

      //so we start from the end of list
      if (startIndex > pl.Length)
      {
        startIndex = pl.Length;
      }

      //check in other direction also
      consecutiveIncrementsInLength = 0;
      for (int i = startIndex - 1; i >= 0; i--)
      {
        Point3D third = pl[i];

        float lengthSum = (p1 - third).Length + (p2 - third).Length;

        if (minLength > lengthSum)
        {
          minLength = lengthSum;
          closestIndex = i;
          consecutiveIncrementsInLength = 0;
        }
        else
        {
          consecutiveIncrementsInLength++;
        }

        //if length increases too much there is no point in checking additional points
        if (consecutiveIncrementsInLength == consecutiveIncrementsInLengthThreshold)
        {
          break;
        }
      }

      return closestIndex;
    }

    public static bool isLongAndNarrow(Point3D p1, Point3D p2, Point3D p3)
    {
      float a = (p1 - p2).Length;
      float b = (p1 - p3).Length;
      float c = (p3 - p2).Length;

      float coef;

      coef = c / (a + b);

      if (coef < LONG_NARROW_THRESHOLD || coef > (1.0 - LONG_NARROW_THRESHOLD))
      {
        return true;
      }

      coef = a / (b + c);

      if (coef < LONG_NARROW_THRESHOLD || coef > (1.0 - LONG_NARROW_THRESHOLD))
      {
        return true;
      }

      coef = b / (a + c);

      if (coef < LONG_NARROW_THRESHOLD || coef > (1.0 - LONG_NARROW_THRESHOLD))
      {
        return true;
      }

      return false;
    }

    private int FindClosestPoint(int listIndexOfPoints, int idxPoint1, int idxPoint2, int otherListIndex)
    {
      if (otherListIndex < 0 || ContainedPointLists.Count == 0)
      {
        return -1;
      }

      int closestPointIndex = -1;

      Point3D[] otherList = ContainedPointLists[otherListIndex];
      Point3D point1 = ContainedPointLists[listIndexOfPoints][idxPoint1];
      Point3D point2 = ContainedPointLists[listIndexOfPoints][idxPoint2];


      int searchIndex = otherList.Length / 2;
      
      //distance to both points has to be minimal
      float distToMiddle = point1.SquareDistance(otherList[searchIndex]) + point2.SquareDistance(otherList[searchIndex]);
      float distInPositiveDirection = float.MaxValue;
      float distInNegativeDirection = float.MaxValue;

      if (searchIndex < otherList.Length - 1)
      {
        distInPositiveDirection = point1.SquareDistance(otherList[searchIndex + 1]) +
          point2.SquareDistance(otherList[searchIndex + 1]) -
          distToMiddle;
      }

      if (searchIndex > 0)
      {
        distInNegativeDirection = point1.SquareDistance(otherList[searchIndex - 1]) +
          point2.SquareDistance(otherList[searchIndex - 1]) -
          distToMiddle; ;
      }

      float minDist = distToMiddle;
      closestPointIndex = searchIndex;
      float tempDist;

      if (distInNegativeDirection < distInPositiveDirection)
      {
        //points in negative direction are closer
        searchIndex--;

        while (searchIndex >= 0)
        {
          tempDist = point1.SquareDistance(otherList[searchIndex]) +
          point2.SquareDistance(otherList[searchIndex]);

          if (tempDist < minDist)
          {
            minDist = tempDist;
            closestPointIndex = searchIndex;
          }
          else
          {
            //min dist was already found if distances are getting bigger
            break;
          }

          searchIndex--;
        }
      }
      else
      {
        //points in positive direction are closer
        searchIndex++;

        while (searchIndex < otherList.Length)
        {
          tempDist = point1.SquareDistance(otherList[searchIndex]) +
          point2.SquareDistance(otherList[searchIndex]);

          if (tempDist < minDist)
          {
            minDist = tempDist;
            closestPointIndex = searchIndex;
          }
          else
          {
            //min dist was already found if distances are getting bigger
            break;
          }

          searchIndex++;
        }
      }

      return closestPointIndex;
    }

    /// <summary>
    /// finds closest point
    /// </summary>
    /// <param name="listIndexOfPoint"></param>
    /// <param name="listIndex"></param>
    /// <returns>closest list index. -1 if not found</returns>
    private int FindClosestList(int listIndex)
    {
      //first find closest point to point1. We will find it on the list that has first point closest to
      //the first point of listIndexOfPoint
      int closestListIndex = -1;
      float minDist = 1000000;
      float tempDist;

      Point3D point1 = ContainedPointLists[listIndex][0];		//first point

      //iterate over all point lists except ours
      for (int i = 0; i < ContainedPointLists.Count; i++)
      {
        if (i != listIndex)
        {
          //first check first point
          tempDist = point1.SquareDistance(ContainedPointLists[i][0]);

          if (tempDist < minDist)
          {
            minDist = tempDist;
            closestListIndex = i;
          }
          else
          {
            //check last point in list - point that are closest usually come from opposite direction
            tempDist = point1.SquareDistance(ContainedPointLists[i][ContainedPointLists[i].Length - 1]);

            if (tempDist < minDist)
            {
              minDist = tempDist;
              closestListIndex = i;
            }
          }
        }
      }

      return closestListIndex;
    }

    #region Find end point implementations
    private int findEndWithStep(Point3D[] source, int from, int to)
    {
      if (from == to)
      {
        return from;
      }

      int step = 20;

      int i = from;
      for (i = from; i <= to; i += step)
      {
        //if bb does not contain point we have gone too far and have to turn back
        if (!boundingBox.contains(source[i]))
        {
          //return findEndBisection(source, i - step, i);

          for (int j = i - 1; j >= i - step && j >= 0; j--)
          {
            if (boundingBox.contains(source[j]))
            {
              return j;
            }
          }
        }
        else
        {
          if (source[i].z < boundingBox.minZ)
            boundingBox.minZ = source[i].z;
          if (source[i].z > boundingBox.maxZ)
            boundingBox.maxZ = source[i].z;
        }
      }

      //if some point at the end are left unaccounted for because of step we have to iterate them manually
      if (i != to)
      {
        for (i = i; i <= to; i++)
        {
          //if bb does not contain point previous point is our key
          if (!boundingBox.contains(source[i]))
          {
            return i - 1;
          }

        }
      }

      return to;
    }
    #endregion

    /// <summary>
    /// inserts points into ContainedPoints
    /// </summary>
    /// <param name="i"></param>
    /// <param name="pts"></param>
    internal void InsertPoints(int listIndex, Point3D[] pts)
    {
      if (pts == null)
      {
        return;
      }

      //code that reduces given points
      //how many points to skip when loading
      int step = (int)Math.Ceiling((1.0 / SelectionLOD));		//be conservative

      int sourcePtsLength = pts.Length;

      if (step < sourcePtsLength)
      {
        if (SelectionLOD < 0.99)
        {
          //points that are skipped will not be copied
          Point3D[] reducedPts = new Point3D[sourcePtsLength / step];

          int i = 0;
          int srcIdx = 0;

          while (i < reducedPts.Length && srcIdx < sourcePtsLength)
          {
            reducedPts[i] = pts[srcIdx];
            i++;

            int a = (int)randArray[(i % randArray.Length)];
            srcIdx += (int)(randArray[(i % randArray.Length)] * step + 1.0);    //+1.0 is here to round up
          }

          while (i < reducedPts.Length)
          {
            reducedPts[i++] = pts[rand.Next(sourcePtsLength)];
          }

          //last point has to be in
          //pts[pts.Length-1] = source[fromIdx + sourcePtsLength - 1];

          ContainedPointLists.Add(reducedPts);

          numberOfLoadedPoints += reducedPts.Length;
        }
        else
        {
          //full level of detail - load all points

          //create new array with points conforming to initial LOD					
          ContainedPointLists.Add(pts);
          numberOfLoadedPoints += pts.Length;

        }
      }
    }


    /// <summary>
    /// issues request to place the leaf into the buffer if unloaded
    /// </summary>
    public void BufferLeaf()
    {
      // points have to be buffered
      if (State == LoadedState.UNLOADED)
      {
        //request load					
        if (LeafLoadRequestEvent != null)
        {
          LeafLoadRequestEvent(this);
        }
      }
    }

    /// <summary>
    /// renders the leaf from VBO
    /// </summary>
    public void Render(bool renderOverride)
    {
      visible = true;

      if (State == LoadedState.BUFFERED_IN_RAM)
      {
        LoadIntoVBO();
        LoadedNewData = true;
      }

      if (State == LoadedState.BUFFERED_IN_GPU)
      {
        //render
        //enable vertex array
        if (renderOverride)
        {
          VBOUtils.RenderVBO(serverBufferId);          
        }        
      }
      else if (State == LoadedState.UNLOADED)
      {
        if (LeafLoadRequestEvent != null)
        {
          LeafLoadRequestEvent(this);
        }
      }
    }

    protected unsafe void LoadIntoVBO()
    {
      if (serverBufferId != null)
      {
        throw new ApplicationException("should not occur");
      }

      if (parentVBOs == null)
      {
        parentVBOs = new Stack<VBOStorageInformation>();
      }

      QTreeWrapper parentTree = LasDataManager.GetInstance().GetQtreeByGuid(ParentTreeID);

      #region Create vertex arrays
      //form three arrays to copy to server memory
      
      ColorPalette pallette = LasDataManager.ColorPallette;

      //code that reduces given points
      //how many points to skip when loading
      int step = (int)Math.Ceiling((1.0 / ReductionToParentCoef));		//be conservative
      
      int allLevels = parentTree.qtree.NumberOfTreeLevels;
            
      int pointsOnLevel = NumberOfPoints / allLevels;
      int pointsOnLastLevel = pointsOnLevel + NumberOfPoints - pointsOnLevel * allLevels;
      
      
      if( pointsOnLevel < 5 )
      {
        //no need to overburden the system with separate arrays for very small amounts of points
        allLevels = 1;
        pointsOnLastLevel = NumberOfPoints;
      }
   
      float[][] interleavedArrayAtLevel = new float[allLevels][];

      int interleavedDataStride = VBOUtils.PointInformationSize;
      int byteStride = interleavedDataStride / 4;

      for( int i=0; i< allLevels; i++ )
      {
        if (i == (allLevels - 1))
        {
          //last(leaf) level also contains all remaining points           
          interleavedArrayAtLevel[i] = new float[pointsOnLastLevel * byteStride];  //10 is for 3*V,3*N and 4*C bytes
        }
        else if (pointsOnLevel > 0)
        {
          interleavedArrayAtLevel[i] = new float[pointsOnLevel * byteStride];
        }        
      }

      int currentLevel = 0;   //we will iterate mod allLevels so the points are distributed fairly
      int pointIndex = 0;     //point index inside an array for a level. increased only when we return to locating points to the frst level
      int numAllocatedPoints = 0;     //counts total number of allcoated point 
      int lastPointsThreshold = NumberOfPoints - pointsOnLastLevel + pointsOnLevel;   //threshold which determines from where on points are only on the last level
      
      //generate points for all levels in one iteration over all the points
      for (int j = 0; j < ContainedPointLists.Count; j++)
      {
        Point3D[] pts = ContainedPointLists[j];
        int len = pts.Length;

        for (int k = 0; k < len; k++)
        {          
          Point3D p = pts[k];

          //C4
          GetColorFromPalette(interleavedArrayAtLevel[currentLevel], pallette, pointIndex, p);
          interleavedArrayAtLevel[currentLevel][pointIndex + 3] = 1;

          //N3
          interleavedArrayAtLevel[currentLevel][pointIndex + 4] = p.nx;
          interleavedArrayAtLevel[currentLevel][pointIndex + 5] = p.nz;
          interleavedArrayAtLevel[currentLevel][pointIndex + 6] = p.ny;
                    
          //V3
          interleavedArrayAtLevel[currentLevel][pointIndex + 7] = p.x + parentTree.positionOffset.x;
          interleavedArrayAtLevel[currentLevel][pointIndex + 8] = p.z;
          interleavedArrayAtLevel[currentLevel][pointIndex + 9] = p.y + parentTree.positionOffset.y;
                   
          numAllocatedPoints++;   //increased for every point

          if (numAllocatedPoints < lastPointsThreshold)
          {
            currentLevel++;

            if (currentLevel == allLevels)
            {
              //increase point index only when going back to the first level
              pointIndex += byteStride;	//3 values for vertices, 3 for colors and 3 for normals
              currentLevel = 0;
            }
          }
          else
          {
            currentLevel = allLevels - 1;
            pointIndex += byteStride;    //point index is always updated, because we only insert points to the last layer now
          }          
        }
      }
      #endregion

      //only if points wil bedistributed between nodes propagate them up the hierarchy
      if (allLevels > 1)
      {
        //load all arrays into VBOStorageInformation objects and pass them along to parents
        Stack<VBOStorageInformation> vbosForParents = new Stack<VBOStorageInformation>(allLevels - 1);  //for parents
        parentVBOs.Clear();

        for (int i = 0; i < allLevels - 1; i++)
        {
          VBOStorageInformation vbos = VBOUtils.GenerateVBOs(pointsOnLevel);

          VBOUtils.CopyPointsToVBOs(interleavedArrayAtLevel[i], vbos);

          //insert into stack so parents can take them out
          vbosForParents.Push(vbos);
          parentVBOs.Push(vbos);
        }

        ParentNode.AddVBO(vbosForParents);
      }

      //load also into this leafs VBO
      serverBufferId = VBOUtils.GenerateVBOs(pointsOnLastLevel);

      VBOUtils.CopyPointsToVBOs(interleavedArrayAtLevel[allLevels - 1], serverBufferId);      
      
      ClearAllPoints();
      State = LoadedState.BUFFERED_IN_GPU;
    }

    unsafe private static void GetColorFromPalette(float[] colors, ColorPalette pallette, int pointIndex, Point3D p)
    {
      if (pallette != null)
      {
        switch (pallette.ColorMode)
        {
          case ColoringType.Classification:
            pallette.GetColor(p.colorIndex,
              out colors[pointIndex + ColorIndexes.R],
              out colors[pointIndex + ColorIndexes.G],
              out colors[pointIndex + ColorIndexes.B]);
            break;
          case ColoringType.Height:
            pallette.GetColor(p.z,
              out colors[pointIndex + ColorIndexes.R],
              out colors[pointIndex + ColorIndexes.G],
              out colors[pointIndex + ColorIndexes.B]);
            break;
          case ColoringType.Monochrome:
            colors[pointIndex + ColorIndexes.R] = 0.5f;
            colors[pointIndex + ColorIndexes.G] = 0.5f;
            colors[pointIndex + ColorIndexes.B] = 0.5f;
            break;
          default:
            break;
        }
      }
      else
      {
        colors[pointIndex + ColorIndexes.R] = 1;
        colors[pointIndex + ColorIndexes.G] = 1;
        colors[pointIndex + ColorIndexes.B] = 0;
      }
    }

    internal void SetInvisibleAndUnbuffered()
    {
      visible = false;

      if (ClearAllPoints())
      {
        numberOfLoadedPoints = 0;
        
        if (serverBufferId != null)
        {
          VBOUtils.DeleteFromGPU(serverBufferId);
          serverBufferId = null;

          if (parentVBOs.Count > 0)
          {
            ParentNode.RemoveVBO(parentVBOs);
          }
        }

        State = LoadedState.UNLOADED;
      }
    }
  }
}
