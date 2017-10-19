using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;



namespace laslib
{  
	public class LASFile
	{
		#region Private Fields
		private FileStream m_fileStream;
		private BinaryReader m_reader;
		public LASHeader header;
		
		public Type pointType;
		public Type pointArrayType;

		private int sizeOfPointStruct;
		
		private IntPtr pointBuffer;				//buffer for single point
		private IntPtr pointArrayBuffer;				//buffer for a line of points

		private int numArrayPoints = 200;		//number of points in an array to read	
		#endregion
		
		#region Public properties
		//number of points that are loaded in array by default. Can be increased
		public int DafaultNumberOfLinePoints
		{
			get { return numArrayPoints; }
			set
			{

				if (numArrayPoints < value)
				{
					//delete old buffer and create new, bigger one
					if (pointArrayBuffer != null)
					{
						Marshal.FreeHGlobal(pointArrayBuffer);
					}

					//create bigger buffer
					pointArrayBuffer = Marshal.AllocHGlobal(sizeOfPointStruct * value);
					numArrayPoints = value;					
				}
			}
		} 
		#endregion
		
		public LASFile(String lasfile)
		{
			if (File.Exists(lasfile))
			{	

				m_fileStream = new FileStream(lasfile, FileMode.Open, FileAccess.Read, FileShare.Read, numArrayPoints*28);

				m_reader = new BinaryReader(m_fileStream);

				header = (LASHeader)RawDeserialize(m_reader.ReadBytes(Marshal.SizeOf(typeof(LASHeader))),
						typeof(LASHeader));
 
        if (header.PointDataFormat == 0)
				{
					pointType = typeof(LASPointFormat0);
					pointArrayType = typeof(LASPointFormat0[]);
					sizeOfPointStruct = Marshal.SizeOf(typeof(LASPointFormat0));					
				}
				else if (header.PointDataFormat == 1)
				{
					pointType = typeof(LASPointFormat1);
					pointArrayType = typeof(LASPointFormat1[]);
					sizeOfPointStruct = Marshal.SizeOf(typeof(LASPointFormat1));					
				}

				//allocate point buffer
				pointBuffer = Marshal.AllocHGlobal(sizeOfPointStruct);
				pointArrayBuffer = Marshal.AllocHGlobal(sizeOfPointStruct * numArrayPoints);
			}
		}

		public bool Close()
		{
			if (m_reader != null)
			{
				m_reader.Close();				
			}

      try
      {
        //free point buffer
        if (pointBuffer != null)
        {
          Marshal.FreeHGlobal(pointBuffer);
        }

        if (pointArrayBuffer != null)
        {
          Marshal.FreeHGlobal(pointArrayBuffer);
        }
      }
      catch (Exception)
      {
        Console.WriteLine("ERROR: freeing memory");
      }

			return true;
		}


		/// <summary>
		/// positions stream at the beginning of point data
		/// </summary>
		/// <returns></returns>
		public bool PositionAtStartOfPointData()
		{
			if (m_fileStream == null || m_reader == null || header.OffsetToData == 0)
			{
				return false;
			}

			bool success = true;

			try
			{
				m_fileStream.Seek(header.OffsetToData, SeekOrigin.Begin);

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				success = false;
			}

			return success;
		}

		/// <summary>
		/// gets next point
		/// </summary>
		/// <returns>point in format written in header</returns>
		public ILASPoint GetNextPoint()
		{
			if (m_reader == null)
			{
				return null;
			}

			byte[] pointdata = m_reader.ReadBytes(sizeOfPointStruct);

			if (pointdata.Length != sizeOfPointStruct)
			{
				return null;
			}

			/*
			Marshal.Copy(pointdata, 0, pointBuffer, sizeOfPointStruct);
			ILASPoint retobj = Marshal.PtrToStructure(pointBuffer, pointType);
             
			return retobj;
			 */

			Marshal.Copy(pointdata, 0, pointBuffer, sizeOfPointStruct);

			return (ILASPoint)Marshal.PtrToStructure(pointBuffer, pointType);
		}

		public ILASPoint GetPointN(uint n)
		{
			if (m_reader == null)
			{
				return null;
			}

			positionAtPointN(n);

			byte[] pointdata = m_reader.ReadBytes(sizeOfPointStruct);

			if (pointdata.Length != sizeOfPointStruct)
			{
				return null;
			}	

			Marshal.Copy(pointdata, 0, pointBuffer, sizeOfPointStruct);
			
			return (ILASPoint)Marshal.PtrToStructure(pointBuffer, pointType);
		}


		/// <summary>
		/// gets an array of points starting at the current position
		/// </summary>
		/// <returns></returns>
		public unsafe object GetPoints(int number)
		{

			if (m_reader == null)
			{
				return null;
			}

			byte[] pointdata = m_reader.ReadBytes(number * sizeOfPointStruct);

			if (pointdata == null)
			{
				return null;
			}

			//number of points actually read
			int numPointsRead = pointdata.Length / sizeOfPointStruct;

			//Console.WriteLine("num points to read: {0}, num read: {1}", number, numPointsRead);

			if (header.PointDataFormat == 0)
			{
				LASPointFormat0[] arr = new LASPointFormat0[numPointsRead];

				fixed (LASPointFormat0* p = arr)
				{
					IntPtr MyIntPtr = (IntPtr)p;
					Marshal.Copy(pointdata, 0, MyIntPtr, pointdata.Length);
				}

				return arr;
			}
			else if (header.PointDataFormat == 1)
			{
				LASPointFormat1[] arr = new LASPointFormat1[numPointsRead];

				fixed (LASPointFormat1* p = arr)
				{
					IntPtr MyIntPtr = (IntPtr)p;
					Marshal.Copy(pointdata, 0, MyIntPtr, pointdata.Length);
				}

				return arr;
			}
			
			return null;
		}

		/// <summary>
		/// gets points at specified index
		/// </summary>
		/// <param name="startIndex"></param>
		/// <param name="number"></param>
		/// <returns></returns>
		public unsafe object GetPoints(uint startIndex, int number)
		{
			if (m_reader == null)
			{
				return null;
			}

			positionAtPointN(startIndex);

			byte[] pointdata = m_reader.ReadBytes(sizeOfPointStruct * number);

			if (pointdata == null)
			{
				return null;
			}
			
			//number of points actually read
			int numPointsRead = pointdata.Length / sizeOfPointStruct;

			if (numPointsRead == 0)
				return null;

			//Console.WriteLine("num points to read: {0}, num read: {1}", number, numPointsRead);

			if (header.PointDataFormat == 0)
			{
				LASPointFormat0[] pf0_buffer = new LASPointFormat0[numPointsRead];

				fixed (LASPointFormat0* p = pf0_buffer)
				{
					IntPtr MyIntPtr = (IntPtr)p;
					Marshal.Copy(pointdata, 0, MyIntPtr, pointdata.Length);
				}

				return pf0_buffer;
			}
			else if (header.PointDataFormat == 1)
			{
				LASPointFormat1[] pf1_buffer = new LASPointFormat1[numPointsRead];

				fixed (LASPointFormat1* p = pf1_buffer)
				{
					IntPtr MyIntPtr = (IntPtr)p;
					Marshal.Copy(pointdata, 0, MyIntPtr, pointdata.Length);
				}

				return pf1_buffer;
			}
						
			//if smth goes wrong return null
			return null;
		}
		

		public object RawDeserializePoint(byte[] pointdata)
		{
			if (sizeOfPointStruct > pointdata.Length)
			{
				return null;
			}

			Marshal.Copy(pointdata, 0, pointBuffer, sizeOfPointStruct);

			object retobj = Marshal.PtrToStructure(pointBuffer, pointType);

			return retobj;
		}


		public static object RawDeserialize(byte[] rawdatas, Type anytype)
		{

			int rawsize = Marshal.SizeOf(anytype);

			if (rawsize > rawdatas.Length)

				return null;

			IntPtr buffer = Marshal.AllocHGlobal(rawsize);

			Marshal.Copy(rawdatas, 0, buffer, rawsize);

			object retobj = Marshal.PtrToStructure(buffer, anytype);

			Marshal.FreeHGlobal(buffer);

			return retobj;



		}

		public static byte[] RawSerialize(object anything)
		{
			int rawsize = Marshal.SizeOf(anything);

			IntPtr buffer = Marshal.AllocHGlobal(rawsize);

			Marshal.StructureToPtr(anything, buffer, false);

			byte[] rawdatas = new byte[rawsize];

			Marshal.Copy(buffer, rawdatas, 0, rawsize);

			Marshal.FreeHGlobal(buffer);

			return rawdatas;
		}

		#region Private Methods
		/// <summary>
		/// positions file pointer at N-th point
		/// </summary>
		/// <param name="n"></param>
		private void positionAtPointN(uint n)
		{
			m_fileStream.Seek(header.OffsetToData + n * sizeOfPointStruct, SeekOrigin.Begin);
		} 
		#endregion
	}
}
