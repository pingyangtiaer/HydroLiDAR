using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace laslib
{
	/// <summary>
	/// interface for access to point data
	/// </summary>
	public interface ILASPoint
	{
		int GetX();
		int GetY();
		int GetZ();
		ushort GetIntensity();
		bool GetEdge();
		bool GetPositiveScanDirection();
		int GetReturnNumber();
		int GetNumberOfReturns();
		byte GetClassification();
		int GetScanAngle();
		double GetGPSTime();
		Type GetFormat();
		string ToString();
		string ToString(double offsetX, double offsetY, double offsetZ,
				double scaleX, double scaleY, double scleZ);
	}

  [Serializable]
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct LASPointFormat0 : ILASPoint
	{
		[FieldOffset(0)]
		public int X;

		[FieldOffset(4)]
		public int Y;

		[FieldOffset(8)]
		public int Z;

		[FieldOffset(12)]
		public ushort Intensity;

		[FieldOffset(14)]
		public byte Flags;

		[FieldOffset(15)]
		public byte Classification;

		[FieldOffset(16)]
		public char ScanAngle;

		/// <summary>
		/// in 1.0 specs. in 1.1 this is User Data field
		/// </summary>
		[FieldOffset(17)]
		public byte FileMarker;

		/// <summary>
		/// in 1.0 specs. In 1.1 his is Point source ID
		/// </summary>
		[FieldOffset(18)]
		public ushort UserBitField;

		#region ILASPoint Members

		public int GetX()
		{
			return X;
		}

		public int GetY()
		{
			return Y;
		}

		public int GetZ()
		{
			return Z;
		}

		public ushort GetIntensity()
		{
			return Intensity;
		}

		public bool GetEdge()
		{
			// MSB flags bit
			return (Flags & 0x80) != 0;
		}

		public bool GetPositiveScanDirection()
		{
			// 6th flags bit
			return (Flags & 0x40) != 0;
		}

		public int GetNumberOfReturns()
		{
			//bits 3,4,5
			byte rot = (byte)((Flags >> 3) & 0x07);

			return Convert.ToInt32(rot);
		}

		public int GetReturnNumber()
		{
			//bits 0,1,2
			byte rot = (byte)(Flags & 0x07);
			return Convert.ToInt32(rot);
		}

		public byte GetClassification()
		{
			return Classification;
		}

		public int GetScanAngle()
		{
			return (int)ScanAngle;
		}

		public double GetGPSTime()
		{
			return 0;
		}

		public Type GetFormat()
		{
			return typeof(LASPointFormat0);
		}

		public override string ToString()
		{
			return string.Format("Point ({0},{1},{2})", X, Y, Z);
		}

		public string ToString(double offsetX, double offsetY, double offsetZ,
				double scaleX, double scaleY, double scaleZ)
		{
			return string.Format("Point ({0},{1},{2})", offsetX + scaleX * X,
					offsetY + scaleY * Y,
					offsetZ + scaleZ * Z);
		}
		#endregion
	}

  [Serializable]
	[StructLayout(LayoutKind.Explicit, Size = 28)]
	public struct LASPointFormat1 : ILASPoint
	{
		[FieldOffset(0)]
		public int X;

		[FieldOffset(4)]
		public int Y;

		[FieldOffset(8)]
		public int Z;

		[FieldOffset(12)]
		public ushort Intensity;

		[FieldOffset(14)]
		public byte Flags;

		[FieldOffset(15)]
		public byte Classification;

		[FieldOffset(16)]
		public char ScanAngle;

		/// <summary>
		/// in 1.0 specs. in 1.1 this is User Data field
		/// </summary>
		[FieldOffset(17)]
		public byte FileMarker;

		/// <summary>
		/// in 1.0 specs. In 1.1 his is Point source ID
		/// </summary>
		[FieldOffset(18)]
		public ushort UserBitField;

		[FieldOffset(20)]
		public double GPSTime;

		#region ILASPoint Members

		public int GetX()
		{
			return X;
		}

		public int GetY()
		{
			return Y;
		}

		public int GetZ()
		{
			return Z;
		}

		public ushort GetIntensity()
		{
			return Intensity;
		}

		public bool GetEdge()
		{
			// MSB flags bit
			return (Flags & 0x80) != 0;
		}

		public bool GetPositiveScanDirection()
		{
			// 6th flags bit
			return (Flags & 0x40) != 0;
		}

		public int GetNumberOfReturns()
		{
			//bits 3,4,5
			byte rot = (byte)((Flags >> 3) & 0x07);

			return Convert.ToInt32(rot);
		}

		public int GetReturnNumber()
		{
			//bits 0,1,2
			byte rot = (byte)(Flags & 0x07);
			return Convert.ToInt32(rot);
		}

		public byte GetClassification()
		{
			return Classification;
		}

		public int GetScanAngle()
		{
			return (int)ScanAngle;
		}

		public double GetGPSTime()
		{
			return GPSTime;
		}

		public Type GetFormat()
		{
			return typeof(LASPointFormat1);
		}

		public override string ToString()
		{
			String text = "Point: (";
			text += X + ", ";
			text += Y + ", ";
			text += Z + ") ";

			return text;
		}

		public string ToString(double offsetX, double offsetY, double offsetZ,
			 double scaleX, double scaleY, double scaleZ)
		{
			return string.Format("Point ({0},{1},{2})", offsetX + scaleX * X,
					offsetY + scaleY * Y,
					offsetZ + scaleZ * Z);
		}

		#endregion
	}
}
