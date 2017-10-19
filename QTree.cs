using System;
using System.Collections.Generic;
using System.Text;
using laslib;
using System.Diagnostics;

using las.datamanager.structures;

namespace las.datamanager
{
  [Serializable]
	public class QTree
	{    
    public Guid TreeID { get; private set; }

		#region Private fields
		public static int POINTS_PER_LEAF = 100000;

		private QTreeNode root;

		private LASHeader las_header;

		private int numberOfLeafs;

		public int numberOfLoadedPoints = 0;
		public int numberOfPoints = 0;
		public float LOD;

    /// <summary>
    /// number of levels this tree contains, including the leaf level and the root node
    /// </summary>
    public int NumberOfTreeLevels { get; set; }   

		#endregion

		#region Public properties
		public int NumberOfLeafs
		{
			get { return numberOfLeafs; }
		}

    public QTreeNode RootNode
    {
      get { return root; }
    }
		#endregion		

		public QTree(LasDataManager dMgr, LASFile lasfile)
		{
      TreeID = Guid.NewGuid();

      las_header = lasfile.header;

			int num_Leafs = (int)las_header.NumberOfPointRecords / POINTS_PER_LEAF;
      
      double d = Math.Sqrt((double)las_header.NumberOfPointRecords / POINTS_PER_LEAF);

      NumberOfTreeLevels = (int)Math.Ceiling(Math.Log(d, 2)) + 1;
      double nodesOneSide = Math.Pow(2, NumberOfTreeLevels - 1);

      double currLeafs = nodesOneSide * nodesOneSide;

			double dWidth = las_header.MaxX - las_header.MinX;
			double dHeight = las_header.MaxY - las_header.MinY;

			QTreeNode.MAX_WIDTH = (float)(dWidth / nodesOneSide);
			QTreeNode.MAX_HEIGHT = (float)(dHeight / nodesOneSide);

			Console.WriteLine("Creating QTree");
			Console.WriteLine("Map width={0} height={1}", dWidth, dHeight);
			Console.WriteLine("Calculated points per leaf: {0} , actual: {1}, leafs: {2}", POINTS_PER_LEAF, (double)las_header.NumberOfPointRecords / (double)currLeafs, currLeafs);		
			Console.WriteLine("Node width={0} height={1}", QTreeNode.MAX_WIDTH, QTreeNode.MAX_HEIGHT);			

			//init qleaf direct access array
			numberOfLeafs = (int)currLeafs;

			root = new QTreeNode(new BoundingBox(0,0, (float)dWidth, (float)dHeight), this, null, 0);

			Console.WriteLine("Created nodes={0}", QTreeNode.NUM_NODES);
		}
		
		/// <summary>
		/// inserts preliminary data into tree structure. Should be only called one for every set of points
		/// or initialization will fail!!!!
		/// </summary>
		public void Initialize(uint index, Point3D[] source, float LOD)
		{
			root.InsertPreliminary(index, source, 0, LOD);

			numberOfLoadedPoints = root.numberOfLoadedPoints;
			numberOfPoints = root.numberOfPoints;
		}

		internal void RenderingPass(float FOV, float near, float far, Vector3f eyeVector, Point2f position)
		{
			root.RenderingPass(FOV, near, far, eyeVector, position, true);

			numberOfLoadedPoints = root.numberOfLoadedPoints;
			numberOfPoints = root.numberOfPoints;
		}
	}
}
