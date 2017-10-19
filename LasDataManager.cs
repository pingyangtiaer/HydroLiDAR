using System;
using System.Collections.Generic;
using System.Text;
using laslib;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using las.datamanager.structures;
using System.Drawing;

namespace las.datamanager
{

	public delegate void LasFileLoadingStatus(String status, float progress);
	public delegate void LasFileLoadingEnd(bool success);
  
	/// <summary>
	/// Manages data dynamic loading and unloading of data from las file to a quadtree hierarchy. 
	/// </summary>    
	public partial class LasDataManager
	{
		public static bool CALCULATE_NORMALS = true;

		public static float BufferedFOV = 30;
		public static float BufferedDistance = 100;
    public static float BufferedPositionRadius = 50;    //circle around current position that is buffered

    public static string serializiationExtension = "srl";

		private Queue<QTreeLeaf> leafLoadRequestQueue;
		private Thread leafLoadingThread;
		private bool runLeafLoader;
		private int leafLoaderMSsleepInterval = 10;

		public event LasFileLoadingStatus loadingStatusEvent;
		public event LasFileLoadingEnd loadingEndedEvent;
		
		private float SelectionLOD;			//for loaded points. NOT for displayed points!!!

    public static int dedicatedPointMemory = 262144000;		//in bytes
     
		public static ColorPalette ColorPallette;

		private static LasDataManager instance = null;

		#region Public properties
		public int LeafLoaderMSsleepInterval
		{
			get { return leafLoaderMSsleepInterval; }
			set { leafLoaderMSsleepInterval = value; }
		}


		/// <summary>
		/// Amount of memory dedicated to loaded LAS points. In bytes. 
		/// Points are stored in las.dataManager.Point3D structure
		/// </summary>
		public int DedicatedPointMemory
		{
			get { return dedicatedPointMemory; }
			set { dedicatedPointMemory = value; }
		}

	
		#endregion

    #region Constructors
		public static LasDataManager GetInstance()
		{
			if (instance == null)
			{
				instance = new LasDataManager();
			}
			return instance;
		}

    static LasDataManager()
    {

    }

    private LasDataManager()
    {
      //init leaf loading thread
      leafLoadRequestQueue = new Queue<QTreeLeaf>(500);
      runLeafLoader = true;
      leafLoadingThread = new Thread(new ThreadStart(LeafLoader));
      leafLoadingThread.Start();

      //set event handler
      QTreeLeaf.LeafLoadRequestEvent += new LeafLoadRequest(RequestLeafLoad);
      			
      GlobalBoundingCube = new BoundingCube();
    }     

    ~LasDataManager()  // destructor
    {
      Close();
    } 
    #endregion

    /// <summary>
    /// loads and initializes the file
    /// </summary>
    /// <param name="filename"></param>
		public void Load(string filename, bool forcePreprocessing )
		{
      if (loadingStatusEvent != null)
      {
        loadingStatusEvent("Opening LAS file", 0.01f);
      }

      QTreeWrapper tree = AddQtreeFromFile(filename);
      Initialize(tree, forcePreprocessing);
      
		}

    /// <summary>
    /// serializes qtree to file.
    /// </summary>
    /// <param name="rewriteIfExists"></param>
    private void SerializeQtreeToFile(QTreeWrapper qtree, bool rewriteIfExists)
    {
      Console.WriteLine("Serializing qtree to file");
      
      string srlFilename = qtree.filename + "." + serializiationExtension;
      FileStream fileStreamObject = null;

      if (File.Exists(srlFilename) && !rewriteIfExists)
      {
        Console.WriteLine("File already exists and rewrite flag is set to false. Returning");
        return;
      }
      
      try
      {         
        fileStreamObject = new FileStream(srlFilename, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStreamObject, qtree.qtree);                       
      }
      finally
      {
        if (fileStreamObject != null)
        {
          fileStreamObject.Close();
        }
      }

      Console.WriteLine("Serialization finished");
    }

    /// <summary>
    /// deserializes the tree from the file
    /// </summary>
    /// <returns>true if successful, false otherwise</returns>
    private QTree DeserializeQtreeFromFile(string filename)
    {
      Console.WriteLine("Deserilaizing qtree from file");

      string srlFilename = filename+"."+serializiationExtension;

      if (!File.Exists(srlFilename))
      {
        Console.WriteLine("Deserialization failed. The file does not exist.");
        return null;
      }
            
      FileStream fileStreamObject = null;
      QTree tree = null;

      try
      {
        fileStreamObject = new FileStream(srlFilename, FileMode.Open);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        tree = (binaryFormatter.Deserialize(fileStreamObject)) as QTree;
      }
      catch (Exception e)
      {
        Console.WriteLine("Deserialization failed: {0}"+e.ToString());
        
      }
      finally
      {
        if (fileStreamObject != null)
        {
          fileStreamObject.Close();
        }
      }

      Console.WriteLine("Deserialization finished");
      return tree;
    }

    

		/// <summary>
		/// initializes data manager with data from las file and fills QTree structure with preliminary data
		/// </summary>
		public void Initialize(QTreeWrapper qtree, bool forcePreprocessing)
		{
      if (forcePreprocessing)
      {
        Console.WriteLine("Preprocessing forced. Not using serialized even if available");
        PreprocessDataFromFile(qtree);
        SerializeQtreeToFile(qtree, true);
      }
      else
      {
        QTree des = DeserializeQtreeFromFile(qtree.filename);

        if (des == null)
        {
          Console.WriteLine("Deserialization failed. Preprocessing.");
          PreprocessDataFromFile(qtree);
          SerializeQtreeToFile(qtree, true);
        }
        else
        {
          qtree.qtree = des;
        }
      }

      if (loadingEndedEvent != null)
      {
        loadingEndedEvent(true);
      }
		}

    private void PreprocessDataFromFile(QTreeWrapper qtreeWrapper)
    {
      Stopwatch initTimer = new Stopwatch();
      initTimer.Start();
      qtreeWrapper.lasFile.PositionAtStartOfPointData();
      uint index = 0;
      uint numAllPoints = qtreeWrapper.lasFile.header.NumberOfPointRecords;

      Point3D[] lineOfPoints = null;
      int step = 100000;

      determineInitialSelectionLOD();

      TimeSpan accReadTime = new TimeSpan();

      while (index < numAllPoints)
      {
        if (loadingStatusEvent != null)
          loadingStatusEvent("Indexing points..." + index + "/" + numAllPoints, (float)index / (float)numAllPoints);

        TimeSpan before = initTimer.Elapsed;
        lineOfPoints = GetPoints(qtreeWrapper, index, step);
        accReadTime += (initTimer.Elapsed - before);

        if (lineOfPoints != null)
        {
          qtreeWrapper.qtree.Initialize(index, lineOfPoints, SelectionLOD);
        }

        index += (uint)lineOfPoints.Length;
        lineOfPoints = null;
      }

      initTimer.Stop();

      LasMetrics.GetInstance().indexing = initTimer.ElapsedMilliseconds;
      LasMetrics.GetInstance().indexingNoDisk = (initTimer.Elapsed - accReadTime).TotalMilliseconds;


      LasMetrics.GetInstance().avgPointsPerLeafActual = (double)numAllPoints / LasMetrics.GetInstance().numberOfNonEmptyLeafs;
      LasMetrics.GetInstance().numberOfPoints += numAllPoints;

      Console.WriteLine("lasDataManager.Initialize() took {0} ms for {1} points", initTimer.ElapsedMilliseconds, numAllPoints);
      Console.WriteLine("reading from disk took {0} ms, indexing without reading from disk took {1} ms", accReadTime.TotalMilliseconds, LasMetrics.GetInstance().indexingNoDisk);
      Console.WriteLine("nr. of all points: {0}, avg. points per leaf: {1}, without empty: {2}", numAllPoints, numAllPoints / LasMetrics.GetInstance().numberOfLeafs, LasMetrics.GetInstance().avgPointsPerLeafActual);
      Console.WriteLine("QTreeLeaf.findEnd() took {0} ms", QTreeLeaf.findStepStopWatch.ElapsedMilliseconds);
      Console.WriteLine("array copying() took {0} ms", QTreeLeaf.arrayCompyTimer.ElapsedMilliseconds);
      Console.WriteLine("mem alocated: {0}kb", GC.GetTotalMemory(false) / 1024);
    }

    
		/// <summary>
		/// calculates LOD based on dedicated amount of memory. Based on ability of video memory being able to hold
    /// 1000000 points
		/// </summary>
		/// <param name="amountOfMemory"></param>
		private void determineInitialSelectionLOD()
		{
			SelectionLOD = 1f;
			      
			Point3D tempPoint = new Point3D();

			int pointSize = Marshal.SizeOf(tempPoint);

      //calculate selection LOD based on the video memory being able to hold 1miomemory
			SelectionLOD = ((float)dedicatedPointMemory / (float)VBOUtils.PointInformationSize) / 1000000;   

      if (SelectionLOD > 1.0f)
      {
        SelectionLOD = 1.0f;
      }      
		}		

		public Point3D[] GetPoints(QTreeWrapper qtree, uint index, int numberOfPoints)
		{
			//Console.WriteLine("getPoints entry. mem alocated: {0}", GC.GetTotalMemory(false));

			Point3D[] pts = null;

      float minX = (float)((qtree.lasFile.header.MinX - qtree.lasFile.header.OffsetX));
      float minY = (float)((qtree.lasFile.header.MinY - qtree.lasFile.header.OffsetY));
      float minZ = (float)((qtree.lasFile.header.MinZ - qtree.lasFile.header.OffsetZ));

      float scaleX = (float)qtree.lasFile.header.ScaleFactorX;
      float scaleY = (float)qtree.lasFile.header.ScaleFactorY;
      float scaleZ = (float)qtree.lasFile.header.ScaleFactorZ;

			//Console.WriteLine("loading point at index={0}, number to load={1}", index, numberOfPoints);

      if (qtree.lasFile.header.PointDataFormat == 0)
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
        LASPointFormat0[] pointArray = (LASPointFormat0[])qtree.lasFile.GetPoints(index, numberOfPoints);
				stopwatch.Stop();
				//Console.WriteLine("loading took: {0} ms", stopwatch.ElapsedMilliseconds);

				long loading = stopwatch.ElapsedMilliseconds;

				stopwatch.Reset();
				stopwatch.Start();

				if (pointArray != null)
				{
					int count = pointArray.Length;
					index += (uint)count;
					pts = new Point3D[numberOfPoints];

					for (int i = 0; i < count; i++)
					{
						pts[i].x = pointArray[i].X * scaleX * LasDataManager.GlobalPointsScaleFactor.x - minX;
            pts[i].y = (pointArray[i].Y) * scaleY * LasDataManager.GlobalPointsScaleFactor.y - minY;
            pts[i].z = (pointArray[i].Z) * scaleZ * LasDataManager.GlobalPointsScaleFactor.z - minZ;

						pts[i].colorIndex = pointArray[i].Classification;

            if (pts[i].z < GlobalBoundingCube.minZ)
            {
              GlobalBoundingCube.minZ = pts[i].z;
            }
            if (pts[i].y > GlobalBoundingCube.maxZ)
            {
              GlobalBoundingCube.maxZ = pts[i].z;
            }
					}
				}

				pointArray = null;

				stopwatch.Stop();
				long transforming = stopwatch.ElapsedMilliseconds;
				//Console.WriteLine("transforming points: {0} ms", stopwatch.ElapsedMilliseconds);

			}
			else
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
        LASPointFormat1[] pointArray = (LASPointFormat1[])qtree.lasFile.GetPoints(index, numberOfPoints);
				stopwatch.Stop();
				//Console.WriteLine("loading poins: {0} ms", stopwatch.ElapsedMilliseconds);
				long loading = stopwatch.ElapsedMilliseconds;
				stopwatch.Reset();
				stopwatch.Start();

				if (pointArray != null)
				{
					int count = pointArray.Length;
					index += (uint)count;
					pts = new Point3D[numberOfPoints];

					for (int i = 0; i < count; i++)
					{
            pts[i].x = pointArray[i].X * scaleX * LasDataManager.GlobalPointsScaleFactor.x - minX;
            pts[i].y = (pointArray[i].Y) * scaleY * LasDataManager.GlobalPointsScaleFactor.y - minY;
            pts[i].z = (pointArray[i].Z) * scaleZ * LasDataManager.GlobalPointsScaleFactor.z - minZ;

						pts[i].colorIndex = pointArray[i].Classification;
                        
            if (pts[i].z < GlobalBoundingCube.minZ)
						{
              GlobalBoundingCube.minZ = pts[i].z;
						}
            if (pts[i].y > GlobalBoundingCube.maxZ)
						{
              GlobalBoundingCube.maxZ = pts[i].z;
						}             
					}
				}

				pointArray = null;

				stopwatch.Stop();
				long transforming = stopwatch.ElapsedMilliseconds;
				//Console.WriteLine("transforming poins: {0} ms", stopwatch.ElapsedMilliseconds);
			}

			//Console.WriteLine("getPoints exit. mem alocated: {0} ", GC.GetTotalMemory(false));

			return pts;
		}

		public void RequestLeafLoad(QTreeLeaf leaf)
		{
			if (leaf.State == LoadedState.UNLOADED && leaf.ListInfos.Count > 0 && leafLoadRequestQueue.Count < 1000)
			{
				leaf.State = LoadedState.REQUEST_LOAD;

        if (leaf.ListInfos.Count > 0)
        {
          leafLoadRequestQueue.Enqueue(leaf);
        }
			}
		}

		/// <summary>
		/// leads leafs in a separate thread
		/// </summary>
		private void LeafLoader()
		{

      TimeSpan accLoadTime = new TimeSpan();
      long loadCount = 0;
      TimeSpan accReadTime = new TimeSpan();
      TimeSpan readTemp = new TimeSpan();
      Stopwatch stopwatch = new Stopwatch();

			long accPointsPerLeaf = 0;
      QTreeLeaf leaf = null;

			while (runLeafLoader)
			{
				if ( LasMetrics.GetInstance().leafLoadToggle && leafLoadRequestQueue.Count > 0)
				{          
					leaf = leafLoadRequestQueue.Dequeue();

          if (leaf == null)
          {
            continue;
          }

					if (leaf.State == LoadedState.REQUEST_LOAD)  //if leaf still wants to be loaded
					{
            stopwatch.Start();
            long readTicks = 0;

						for (int i = 0; i < leaf.ListInfos.Count; i++)
						{
              readTemp = stopwatch.Elapsed;

							Point3D[] pts = GetPoints(GetQtreeByGuid(leaf.ParentTreeID), leaf.ListInfos[i].startingPointIndex, leaf.ListInfos[i].numberOfPoints);

              readTicks = stopwatch.ElapsedTicks;
              accReadTime += (stopwatch.Elapsed-readTemp);

							leaf.InsertPoints(i, pts);
						}

						if (CALCULATE_NORMALS == true)
						{
							leaf.CalculateNormals();
						}

            long loadTicks = stopwatch.ElapsedTicks;
            accLoadTime += stopwatch.Elapsed;

            stopwatch.Stop();

            loadCount++;

            stopwatch.Reset();

						leaf.State = LoadedState.BUFFERED_IN_RAM;

            if (loadCount > 0)
            {

              double avgLoadTime = accLoadTime.TotalMilliseconds / loadCount;
              double avgLoadTimeNoRead = (accLoadTime - accReadTime).TotalMilliseconds / loadCount;

              //Console.WriteLine("Avg. time from disk to GPU: {0} ms, without disk reads: {1} ms", avgLoadTime, avgLoadTimeNoRead);

              LasMetrics.GetInstance().avgLoad = avgLoadTime;
              LasMetrics.GetInstance().avgLoadNoDisk = avgLoadTimeNoRead;

							accPointsPerLeaf += leaf.NumberOfPoints;
              LasMetrics.GetInstance().avgPointsPerLeaf = (double)accPointsPerLeaf / loadCount;
            }
					}

          leaf = null;
				}
				else
				{
					Thread.Sleep(leafLoaderMSsleepInterval);
				}       
			}
		}


		public void Close()
		{
      if (leafLoadingThread != null)
      {
        runLeafLoader = false;
        leafLoadingThread.Join();
      }

      for (int i = 0; i < QTrees.Count; i++)
      {
        QTrees[i].lasFile.Close();
      }
		}
		    
	}
}
