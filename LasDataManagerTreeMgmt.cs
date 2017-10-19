using System;
using System.Collections.Generic;
using System.Text;
using las.datamanager;
using laslib;
using System.Drawing;
using las.datamanager.structures;
using System.Runtime.InteropServices;

namespace las.datamanager
{
  /// <summary>
  /// part of class that contains multiple tree management logic
  /// </summary>
  public partial class LasDataManager
  {    
    public List<QTreeWrapper> QTrees = new List<QTreeWrapper>();    //qtrees that 
    public BoundingCube GlobalBoundingCube { get; set; }                // global bounding box sorrounding all qtrees. in world coordinates

    public static bool ApplyGlobalPointsScaleFactor = false; 
    public static Point3D GlobalPointsScaleFactor = new Point3D(1,1,1);                 //global scale factors in all directions

    private static float FOV = -1;

    public static void setFOV(float newFOV)
    {
      FOV = newFOV;

      //precalculate some FOV values that are needed later to avoid unnecessary calculation when rendering
      QTreeNode.cosAlphaBasicFOV = (float)Math.Cos(newFOV * Constants.DEG2RAD_MULT / 2.0f);
      QTreeNode.sinAlphaBasicFOV = (float)Math.Sin(newFOV * Constants.DEG2RAD_MULT / 2.0f);
      QTreeNode.cosAlphaBufferedFOV = (float)Math.Cos((newFOV + 2 * LasDataManager.BufferedFOV) * Constants.DEG2RAD_MULT / 2.0f);
      QTreeNode.sinAlphaBufferedFOV = (float)Math.Sin((newFOV + 2 * LasDataManager.BufferedFOV) * Constants.DEG2RAD_MULT / 2.0f);   
    }

    /// <summary>
    /// adds qtree to the collection and initializes it
    /// </summary>
    /// <param name="filename"></param>
    protected QTreeWrapper AddQtreeFromFile(string filename)
    {
      Console.WriteLine("Adding qtree from file: {0}", filename);
      QTreeWrapper qtreeWrapper = GetQtreeByFilename(filename);

      if (qtreeWrapper != null)
      {
        Console.WriteLine("Qtree already exists");
        return qtreeWrapper;
      }

      qtreeWrapper = new QTreeWrapper();
      qtreeWrapper.filename = filename;
      qtreeWrapper.lasFile = new LASFile(filename);
			qtreeWrapper.lasFile = adjustParameters(qtreeWrapper.lasFile);

			if (QTrees.Count == 0)
			{
				Console.WriteLine("First loaded qtree. positioning at (0,0)");
			}
			else
			{
				Console.WriteLine("Not first qtree to be loaded. Setting offset coordinates relative to the first qtree");

				QTreeWrapper first = QTrees[0];
        qtreeWrapper.positionOffset = new Point2f(
          (float)(qtreeWrapper.lasFile.header.MinX - first.lasFile.header.MinX),
          (float)(qtreeWrapper.lasFile.header.MinY - first.lasFile.header.MinY));

				Console.WriteLine("Offset coordinates for tree {0} are {1}", QTrees.Count, qtreeWrapper.positionOffset);
			}



      if (loadingStatusEvent != null)
      {
        loadingStatusEvent("Creating quad tree", 0.02f);
      }
            
      qtreeWrapper.qtree = new QTree(this, qtreeWrapper.lasFile);

      QTrees.Add(qtreeWrapper);
      CalculateGlobalBoundingBox();

      return qtreeWrapper;
    }

		/// <summary>
		/// adjusts qtrees parameters per global scaling configuration
		/// </summary>
		/// <param name="qtreeWrapper"></param>
		private LASFile adjustParameters(LASFile lasfile)
		{
			double b = lasfile.header.MaxX - lasfile.header.MinX;

      lasfile.header.MinX *= LasDataManager.GlobalPointsScaleFactor.x;
      lasfile.header.MaxX *= LasDataManager.GlobalPointsScaleFactor.x;

			b = lasfile.header.MaxX - lasfile.header.MinX;

			b = lasfile.header.MaxY - lasfile.header.MinY;
      lasfile.header.MinY *= LasDataManager.GlobalPointsScaleFactor.y;
      lasfile.header.MaxY *= LasDataManager.GlobalPointsScaleFactor.y;
			b = lasfile.header.MaxY - lasfile.header.MinY;

			b = lasfile.header.MaxZ - lasfile.header.MinZ;
      lasfile.header.MinZ *= LasDataManager.GlobalPointsScaleFactor.z;
      lasfile.header.MaxZ *= LasDataManager.GlobalPointsScaleFactor.z;
			b = lasfile.header.MaxZ - lasfile.header.MinZ;

			return lasfile;
		}

    
    /// <summary>
    /// gets qtree from the collection. returns null if it does not exist
    /// </summary>
    /// <param name="filename"></param>
    /// <returns>qtree, or null if it doesn't exist</returns>
    public QTreeWrapper GetQtreeByFilename(string filename)
    {
      for (int i = 0; i < QTrees.Count; i++)
      {
        if (QTrees[i].filename == filename)
        {
          return QTrees[i];
        }
      }

      return null;
    }

    public QTreeWrapper GetQtreeByGuid(Guid id)
    {
      for (int i = 0; i < QTrees.Count; i++)
      {
        if (QTrees[i].qtree.TreeID == id)
        {
          return QTrees[i];
        }
      }

      return null;
    }

    public void CalculateGlobalBoundingBox()
    {
      Console.WriteLine("Calculating BoundingCube box and offsets");

			/*
      if (QTrees.Count == 1)
      {
        Console.WriteLine("We have exactly 1 tree. its min values will represent offset to all data");
        GlobalPointsOffset = new Point3D( 
          -(float)QTrees[0].lasFile.header.MinX,
          -(float)QTrees[0].lasFile.header.MinY,
          -(float)QTrees[0].lasFile.header.MinZ);
      }
			 */ 

      Console.WriteLine("Calculating global bounding box for all trees");

      for (int i = 0; i < QTrees.Count; i++)
      {
        if (QTrees[i].lasFile.header.MaxX > GlobalBoundingCube.maxX)
        {
          GlobalBoundingCube.maxX = (float)QTrees[i].lasFile.header.MaxX;
        }

        if (QTrees[i].lasFile.header.MaxY > GlobalBoundingCube.maxY)
        {
          GlobalBoundingCube.maxY = (float)QTrees[i].lasFile.header.MaxY;
        }

        if (QTrees[i].lasFile.header.MaxZ > GlobalBoundingCube.maxZ)
        {
          GlobalBoundingCube.maxZ = (float)QTrees[i].lasFile.header.MaxZ;
        }

        if (GlobalBoundingCube.minX > QTrees[i].lasFile.header.MinX)
        {
          GlobalBoundingCube.minX = (float)QTrees[i].lasFile.header.MinX;
        }

        if (GlobalBoundingCube.minY > QTrees[i].lasFile.header.MinY)
        {
          GlobalBoundingCube.minY = (float)QTrees[i].lasFile.header.MinY;
        }

        if (GlobalBoundingCube.minZ > QTrees[i].lasFile.header.MinZ)
        {
          GlobalBoundingCube.minZ = (float)QTrees[i].lasFile.header.MinZ;
        }
      }
    }

		/// <summary>
		/// renders all qtrees
		/// </summary>    
		public void RenderingPass(float FOV, float near, float far, Vector3f eyeVector, Point2f position)
		{
			for (int i = 0; i < QTrees.Count; i++)
			{
				RenderingPass(QTrees[i], FOV, near, far, eyeVector, position);				
			}
		}

		/// <summary>
		/// renders the QTree in current openGl context
		/// </summary>
		private void RenderingPass(QTreeWrapper qtree, float FOV, float near, float far, Vector3f eyeVector,Point2f position)
		{
			QTree lasTree = qtree.qtree;

			if (lasTree != null)
			{
				lasTree.RenderingPass(FOV, near, far, eyeVector, position - qtree.positionOffset);

        /*
				Point3D tempPoint = new Point3D();
				int pointSize = Marshal.SizeOf(tempPoint);

				int allowedPointsInMemory = dedicatedPointMemory / pointSize;

				float percent = (float)lasTree.numberOfLoadedPoints / (float)allowedPointsInMemory;

				//allow discrepancy of 15%
				if (Math.Abs(percent - LOD) > 0.1)
				{
					LOD = (float)allowedPointsInMemory / (float)qtree.lasFile.header.NumberOfPointRecords;
					//lasTree.CascadeLOD(LOD);
				}
        */
			}
		}

  }
}
