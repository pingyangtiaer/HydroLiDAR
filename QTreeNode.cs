using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using las.datamanager.structures;
using System.Diagnostics;

namespace las.datamanager
{
  [Serializable]
	public class QTreeNode
	{
    private QTreeNode ParentNodeID { get; set; }

		public const int NW = 0;
		public const int NE = 1;
		public const int SW = 2;
		public const int SE = 3;

		public static int NUM_NODES = 0;

		public static float MAX_WIDTH;
		public static float MAX_HEIGHT;

		private QTreeNode[] nodes;
		private QTreeLeaf leaf;

    public static bool PerNodeLOD = true;   //flag indicating if per node LOD is enforced
    private float nodeHierarchyLevel = 0;   //percentage of the node down the hierarchy. 0 is at the top, 1 at the bottom

		public BoundingBox boundingBox;
		public bool visible;
		public int numberOfPoints = 0;
		public int numberOfLoadedPoints = 0;

    //ids of VBOs - vertex, normal, color - that contain points for this level
    [NonSerialized]
    private List<VBOStorageInformation> serverBufferIds = new List<VBOStorageInformation>();  //VBOS from all children

    public QTreeNode(BoundingBox boundingBox, QTree parentTree, QTreeNode parentNode, int hierarchyLevel)
		{
			NUM_NODES++;     

      this.ParentNodeID = parentNode;
			this.boundingBox = boundingBox;

      nodeHierarchyLevel = (float)hierarchyLevel / parentTree.NumberOfTreeLevels;
      
			//if bounding box is bigger than allowed split it nodes
			if (boundingBox.width > MAX_WIDTH || boundingBox.height > MAX_HEIGHT)
			{
				nodes = new QTreeNode[4];

				BoundingBox bb = new BoundingBox();
				bb.width = boundingBox.width / 2.0f;
				bb.height = boundingBox.height / 2.0f;
				bb.x = boundingBox.x + bb.width;
				bb.y = boundingBox.y;
        nodes[NE] = new QTreeNode(bb, parentTree, this, hierarchyLevel + 1);

				bb.x = boundingBox.x;
        nodes[NW] = new QTreeNode(bb, parentTree, this, hierarchyLevel + 1);

				bb.y += bb.height;
        nodes[SW] = new QTreeNode(bb, parentTree, this, hierarchyLevel + 1);

				bb.x += bb.width;
        nodes[SE] = new QTreeNode(bb, parentTree, this, hierarchyLevel + 1);
			}
			else
			{
				//node is final - contains only data which is present in QTreeLeaf
        leaf = new QTreeLeaf(boundingBox, parentTree.TreeID, this);
			}
		}


		/// <summary>
		/// inserts preliminary data into node
		/// </summary>
		/// <param name="firstPointIndex"></param>
		/// <param name="pts"></param>
		/// <param name="fromIdx">position where to take points from,insert points from </param>
		/// <returns>pointer to next available point. If whole array was used length is returned</returns>
		public int InsertPreliminary(uint firstPointIndex, Point3D[] source, int fromIdx, float LOD)
		{
			if (fromIdx >= source.Length)
				return fromIdx;

			Point3D point = source[fromIdx];

			if (leaf != null)
			{
				int ret = leaf.InsertPreliminary(firstPointIndex, source, fromIdx, LOD);

				if (leaf.boundingBox.minZ < boundingBox.minZ)
					boundingBox.minZ = leaf.boundingBox.minZ;

				if (leaf.boundingBox.maxZ < boundingBox.maxZ)
					boundingBox.maxZ = leaf.boundingBox.maxZ;

				numberOfPoints = leaf.NumberOfPoints;
				numberOfLoadedPoints = leaf.numberOfLoadedPoints;
				return ret;
			}
			else
			{
				int nextFrom = source.Length;		//index to next available point to return

				if (point.x <  (boundingBox.x + boundingBox.width / 2.0f))
				{
					//west side
					if (point.y < (boundingBox.y + boundingBox.height / 2.0f))
					{
						//north
						nextFrom = nodes[NW].InsertPreliminary(firstPointIndex, source, fromIdx, LOD);						
					}
					else
					{
						//south
						nextFrom = nodes[SW].InsertPreliminary(firstPointIndex, source, fromIdx, LOD);					
					}
				}
				else
				{
					//east side					
					if (point.y < (boundingBox.y + boundingBox.height / 2.0f))
					{
						//north
						nextFrom = nodes[NE].InsertPreliminary(firstPointIndex, source, fromIdx, LOD);						
					}
					else
					{
						//south
						nextFrom = nodes[SE].InsertPreliminary(firstPointIndex, source, fromIdx, LOD);						
					}
				}

				//if points were inserted in a node and there are some left, insert them on same level
				// have to check if next point is still on current level
				if (nextFrom != source.Length && boundingBox.contains(source[nextFrom]))
				{
					while (nextFrom != source.Length && boundingBox.contains(source[nextFrom]))
					{
						point = source[nextFrom];

						if (point.x < (boundingBox.x + boundingBox.width / 2.0f))
						{
							//west side
							if (point.y < (boundingBox.y + boundingBox.height / 2.0f))
							{
								//north
								nextFrom = nodes[NW].InsertPreliminary(firstPointIndex, source, nextFrom, LOD);
							}
							else
							{
								//south
								nextFrom = nodes[SW].InsertPreliminary(firstPointIndex, source, nextFrom, LOD);
							}
						}
						else
						{
							//east side					
							if (point.y < (boundingBox.y + boundingBox.height / 2.0f))
							{
								//north
								nextFrom = nodes[NE].InsertPreliminary(firstPointIndex, source, nextFrom, LOD);
							}
							else
							{
								//south
								nextFrom = nodes[SE].InsertPreliminary(firstPointIndex, source, nextFrom, LOD);
							}
						}

						numberOfPoints = nodes[SE].numberOfPoints + nodes[NE].numberOfPoints + nodes[SW].numberOfPoints + nodes[NW].numberOfPoints;
						numberOfLoadedPoints = nodes[SE].numberOfLoadedPoints + nodes[NE].numberOfLoadedPoints + nodes[SW].numberOfLoadedPoints + nodes[NW].numberOfLoadedPoints; ;
				
					}

					//perform boundingBoy checkups
					updateBoundingBox();

					return nextFrom;					
				}

				updateBoundingBox();

				numberOfPoints = nodes[SE].numberOfPoints + nodes[NE].numberOfPoints + nodes[SW].numberOfPoints + nodes[NW].numberOfPoints;
				numberOfLoadedPoints = nodes[SE].numberOfLoadedPoints + nodes[NE].numberOfLoadedPoints + nodes[SW].numberOfLoadedPoints + nodes[NW].numberOfLoadedPoints; ;
				return nextFrom;
			}
		}

		private void updateBoundingBox()
		{
			if (nodes[SE].boundingBox.minZ < boundingBox.minZ)
				boundingBox.minZ = nodes[SE].boundingBox.minZ;

			if (nodes[SE].boundingBox.maxZ < boundingBox.maxZ)
				boundingBox.maxZ = nodes[SE].boundingBox.maxZ;

			if (nodes[SW].boundingBox.minZ < boundingBox.minZ)
				boundingBox.minZ = nodes[SW].boundingBox.minZ;

			if (nodes[SW].boundingBox.maxZ < boundingBox.maxZ)
				boundingBox.maxZ = nodes[SW].boundingBox.maxZ;

			if (nodes[NE].boundingBox.minZ < boundingBox.minZ)
				boundingBox.minZ = nodes[NE].boundingBox.minZ;

			if (nodes[NE].boundingBox.maxZ < boundingBox.maxZ)
				boundingBox.maxZ = nodes[NE].boundingBox.maxZ;

			if (nodes[NW].boundingBox.minZ < boundingBox.minZ)
				boundingBox.minZ = nodes[NW].boundingBox.minZ;

			if (nodes[NW].boundingBox.maxZ < boundingBox.maxZ)
				boundingBox.maxZ = nodes[NW].boundingBox.maxZ;
		}
    		
        
    public static float cosAlphaBasicFOV;
    public static float sinAlphaBasicFOV;
    public static float cosAlphaBufferedFOV;
    public static float sinAlphaBufferedFOV;
    
    internal int NumberOfPointsInsideFOV(bool bufferedFOV, float far, Vector3f eyeVector, Point2f position)
    {
      float cosAlpha, sinAlpha;

      if (bufferedFOV)
      {
        cosAlpha = cosAlphaBufferedFOV;
        sinAlpha = sinAlphaBufferedFOV;
      }
      else
      {
        cosAlpha = cosAlphaBasicFOV;
        sinAlpha = sinAlphaBasicFOV;
      }

     
      Point2f farPoint = new Point2f(eyeVector.x, eyeVector.z) * far;  //far point straight ahead, origin in (0,0)

      float dist = farPoint.Length;

      float cosAfarx = cosAlpha * farPoint.x;
      float sinAfarx = sinAlpha * farPoint.x;
      float cosAfary = cosAlpha * farPoint.y;
      float sinAfary = sinAlpha * farPoint.y;

      //points for contain test. they encompass the FOV triangle
      Point2f rotatedAlphaPoint = new Point2f(
        position.x + cosAfarx - sinAfary, position.y + sinAfarx + cosAfary);

      Point2f rotatedMinusAlphaPoint = new Point2f( 
        position.x + cosAfarx + sinAfary, position.y -sinAfarx + cosAfary);

      float lenAlpha = (rotatedAlphaPoint - position).Length;
      float lenAlphaMinus = (rotatedMinusAlphaPoint - position).Length;

      //new algo: test if three points forming a view triangle lie within the box or only a part if current node 
      int pointsInside = 0;

      if (boundingBox.contains(position))
      {
        pointsInside++;
      }
      if (boundingBox.contains(rotatedAlphaPoint))
      {
        pointsInside++;
      }
      if (boundingBox.contains(rotatedMinusAlphaPoint))
      {
        pointsInside++;
      }
           
      if (pointsInside == 0)
      {
        //if no points from the view triangle are inside the bounding box, we check if any of the 
        //boxes points are inside triangle
        Point2f[] points = boundingBox.Corners;

        for (int i = 0; i < points.Length; i++)
        {
          if (points[i].IsPointInTriangle(position, rotatedAlphaPoint, rotatedMinusAlphaPoint))
          {
            pointsInside = 1;
            break;
          }
        }
      }      

      if (pointsInside == 0)
      {
        //if still nothing, check for BB and triangle intersection. 
        if (boundingBox.intersects( position, rotatedAlphaPoint, rotatedMinusAlphaPoint))
        {
          pointsInside = 1;
        }
      }

      return pointsInside;
    }

    public static Stopwatch pointsInsideFOV = new Stopwatch();

    /// <summary>
    /// rendering pass for a node
    /// </summary>
    /// <param name="FOV"></param>
    /// <param name="near"></param>
    /// <param name="far"></param>
    /// <param name="eyeVector"></param>
    /// <param name="position"></param>
    /// <param name="renderOverride">flag that prevents node to be rendered even if in view field, if its too far down the hierarchy</param>
    internal void RenderingPass(float FOV, float near, float far, Vector3f eyeVector, Point2f position, bool renderOverride)
    {      
      //new algo: test if three points forming a view triangle lie within the box or only a part if current node       
      pointsInsideFOV.Reset();
      pointsInsideFOV.Start();   
      int pointsInside = NumberOfPointsInsideFOV(false, far, eyeVector, position);
      pointsInsideFOV.Stop();
      LasMetrics.GetInstance().pointsInsideViewMilis += pointsInsideFOV.Elapsed;
      LasMetrics.GetInstance().pointsInsideViewCounted++;

      //if render override flag was not set to false, check if it can be set here
      //renderOverride set to false prevents node or leaf to be rendered if too far away,
      //although it can still be loaded into memory. This prevents far away regions to be
      //drawn in too much detail that isn't even visible      
      if (QTreeNode.PerNodeLOD && renderOverride && !boundingBox.contains(position))
      {
        float closestCorner = position.DistanceTo(boundingBox.Center);

        //check if nearest corner is close enough to warant rendering this level
        for (int i = 0; i < 4; i++)
        {
          float distToPosition = position.DistanceTo(boundingBox.Corners[i]);

          if (closestCorner > distToPosition)
          {
            closestCorner = distToPosition;               
          }
        }        

        //calculate distance relative to the far point. determines which nodes will be drawn and which not
        float relativeDistance = 1.0f - closestCorner / far;

        if (relativeDistance < nodeHierarchyLevel)
        {
          renderOverride = false;
        }
      }
      
      if (pointsInside == 3)
      {        
        //all points are seen - contents of the node should immediately be drawn without further checks
        if (renderOverride)
        {
          RenderVisible();
        }
      }
      else if (pointsInside > 0)
      {
        //if only some of the points are inside, checks should be performed in children
        if (renderOverride)
        {
          Render();
        }

        if (leaf != null)
        {
          //if only a part is inside, render it anyway.
          leaf.Render(renderOverride);          
        }
        else
        {
          //render also this node. parts of this node that are visible will be already loaded and therefore visible
          nodes[NE].RenderingPass(FOV, near, far, eyeVector, position, renderOverride);
          nodes[NW].RenderingPass(FOV, near, far, eyeVector, position, renderOverride);
          nodes[SE].RenderingPass(FOV, near, far, eyeVector, position, renderOverride);
          nodes[SW].RenderingPass(FOV, near, far, eyeVector, position, renderOverride);           
        }          
      }
      else
      {
        //no points are inside
        //count number of buffered corners. if:
        //- all buffered - place leaf in buffer
        // -some buffered - delve further
        //- none buffered - cascadeInvisibility

        //new values for buffered checks + values are modified with Buffered values
        
        int bufferedPointsInside = NumberOfPointsInsideFOV( 
          true, 
          far + LasDataManager.BufferedDistance,
          eyeVector, 
          position - new Point2f(eyeVector.x, eyeVector.z) * LasDataManager.BufferedPositionRadius );
                
        if (bufferedPointsInside == 3)
        {
          //all points are within buffer range- buffer all lower nodes and leafs
          if (leaf != null)
          {
            leaf.BufferLeaf();
          }
          else
          {
            CascadeBuffering();
          }
        }
        else if (bufferedPointsInside > 0)
        {
          //if only some of the points are inside, checks should be performed in children
          if (leaf != null)
          {
            //if 2 or more points are inside buffered area buffer it anyways
            if (bufferedPointsInside > 1)
            {
              leaf.BufferLeaf();
            }
          }
          else
          {
            nodes[NE].RenderingPass(FOV, near, far, eyeVector, position, renderOverride);
            nodes[NW].RenderingPass(FOV, near, far, eyeVector, position, renderOverride);
            nodes[SE].RenderingPass(FOV, near, far, eyeVector, position, renderOverride);
            nodes[SW].RenderingPass(FOV, near, far, eyeVector, position, renderOverride);    
          }
        }
        else
        {
          //none are buffered - remove if in buffer
          CascadeInvisibility();          
        }
      }
    }

    private void CascadeBuffering()
    {
      if (leaf != null)
      {
        leaf.BufferLeaf();
        return;
      }

      nodes[NE].CascadeBuffering();
      nodes[NW].CascadeBuffering();
      nodes[SE].CascadeBuffering();
      nodes[SW].CascadeBuffering();
    }
    
		/// <summary>
		/// called when node is known to be visible. no additional tests have to be performed
		/// </summary>
		private void RenderVisible()
		{
      Render();   //render this node

			if (leaf != null)
			{
				leaf.Render(true);
        numberOfLoadedPoints = leaf.numberOfLoadedPoints;
				return;
			}

      nodes[NE].RenderVisible();
			nodes[NW].RenderVisible();
			nodes[SE].RenderVisible();
			nodes[SW].RenderVisible();

			numberOfLoadedPoints = 
				nodes[NE].numberOfLoadedPoints +
				nodes[NW].numberOfLoadedPoints +
				nodes[SE].numberOfLoadedPoints +
				nodes[SW].numberOfLoadedPoints;
		}
    
		//toggles entire subtrees visibility. Removes points them from buffer too
		internal void CascadeInvisibility()
		{
      if (leaf != null)
      {
        leaf.SetInvisibleAndUnbuffered();
      }
      else
      {
        nodes[NE].CascadeInvisibility();
        nodes[NW].CascadeInvisibility();
        nodes[SE].CascadeInvisibility();
        nodes[SW].CascadeInvisibility();
      }

			numberOfLoadedPoints = 0;
		}

    /// <summary>
    /// called from the child leaf or node. loads a subset of passed points into its VBOs and passes the rest upward
    /// 
    /// </summary>
    public void AddVBO(Stack<VBOStorageInformation> vbos)
    {
      if (serverBufferIds == null)
      {
        serverBufferIds = new List<VBOStorageInformation>();
      }

      //only insert if the node does not contain a leaf
      if (leaf == null)
      {
        VBOStorageInformation vbo = vbos.Pop();
        serverBufferIds.Add(vbo);
      }

      //top most node finishes it
      if (ParentNodeID != null)
      {
        ParentNodeID.AddVBO(vbos);
      }
    }

    public void RemoveVBO( Stack<VBOStorageInformation> vbos)
    {
      //only delete if its not the node farthes in the hierarchy
      if (leaf == null)
      {
        //if leaf not null remove the VBO that is at this level and delete it from GPU memory
        VBOStorageInformation thisLevelVBO = vbos.Pop();
        bool removed = serverBufferIds.Remove(thisLevelVBO);

        if (removed)
        {          
          VBOUtils.DeleteFromGPU(thisLevelVBO );
        }
      }

      if (ParentNodeID!=null)
      {
        ParentNodeID.RemoveVBO(vbos);
      }
    }

    private void Render()
    {
      if (serverBufferIds == null)
      {
        return;
      }

      for (int i = 0; i < serverBufferIds.Count; i++)
      {
        VBOUtils.RenderVBO(serverBufferIds[i]);        
      }

    }
	}
}
