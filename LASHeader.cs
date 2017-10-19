using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace laslib
{
  [Serializable]
	[StructLayout(LayoutKind.Explicit, Size = 227)]
	public struct LASHeader
	{
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public char[] Signature;

		[FieldOffset(24)]
		public byte VersionMajor;

		[FieldOffset(25)]
		public byte VersionMinor;

		[FieldOffset(90)]
		public ushort DayOfYear;

		[FieldOffset(92)]
		public ushort Year;

		[FieldOffset(94)]
		public ushort Headersize;

		[FieldOffset(96)]
		public uint OffsetToData;

		[FieldOffset(100)]
		public uint NbrVariableLengthRecords;

		[FieldOffset(104)]
		public byte PointDataFormat;

		[FieldOffset(105)]
		public ushort PointDataRecordLength;

		[FieldOffset(107)]
		public uint NumberOfPointRecords;

		[FieldOffset(111)]
		public uint NumberOfPointsByReturn0;

		[FieldOffset(115)]
		public uint NumberOfPointsByReturn1;

		[FieldOffset(119)]
		public uint NumberOfPointsByReturn2;

		[FieldOffset(123)]
		public uint NumberOfPointsByReturn3;

		[FieldOffset(127)]
		public uint NumberOfPointsByReturn4;

		[FieldOffset(131)]
		public double ScaleFactorX;

		[FieldOffset(139)]
		public double ScaleFactorY;

		[FieldOffset(147)]
		public double ScaleFactorZ;

		[FieldOffset(155)]
		public double OffsetX;

		[FieldOffset(163)]
		public double OffsetY;

		[FieldOffset(171)]
		public double OffsetZ;

		[FieldOffset(179)]
		public double MaxX;

		[FieldOffset(187)]
		public double MinX;

		[FieldOffset(195)]
		public double MaxY;

		[FieldOffset(203)]
		public double MinY;

		[FieldOffset(211)]
		public double MaxZ;

		[FieldOffset(219)]
		public double MinZ;


		public override string ToString()
		{

			Type type = typeof(LASHeader);

			FieldInfo[] fields = type.GetFields();

			StringBuilder sb = new StringBuilder();

			sb.AppendLine("LAS File Header");

			for (int i = 0; i < fields.Length; i++)
			{
				sb.AppendFormat("{0}: {1}\n", fields[i].Name, fields[i].GetValue(this));
			}

			return sb.ToString();


		}
	}
}
