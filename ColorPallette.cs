using System;
using System.Collections.Generic;
using System.Text;

namespace las.datamanager.structures
{
	public enum ColoringType
	{
		Classification,
		Height,
		Monochrome
	};

  [Serializable]
	public class ColorPalette
	{
		private float[] cR;
		private float[] cG;
		private float[] cB;

		private byte[] heightMapR;
		private byte[] heightMapG;
		private byte[] heightMapB;
		
		private float minHeight;
		private float maxHeight;

		public ColoringType ColorMode { get; set; }

		public ColorPalette(float minHeight, float maxHeight)
		{
			cR = new float[256];
			cG = new float[256];
			cB = new float[256];

			this.minHeight = minHeight;
			this.maxHeight = maxHeight;

      ColorMode = ColoringType.Monochrome;

			ConstructHeightMap();

			FillWithSampleValues();
		}

		

		private void ConstructHeightMap()
		{
			byte R = 0;
			byte G = 255;
			byte B = 0;

			int maxValueOfColor = 255;

			heightMapR = new byte[maxValueOfColor * 2];
			heightMapG = new byte[maxValueOfColor * 2];
			heightMapB = new byte[maxValueOfColor * 2];

			int j = 0;
			
		//increase green
			for (int i = 0; i < maxValueOfColor; i++)
			{
				heightMapR[j] = R;
				heightMapG[j] = G--;
				heightMapB[j] = B++;
				j++;
			}

			//increase red
			for (int i = 0; i < maxValueOfColor; i++)
			{
				heightMapR[j] = R++;
				heightMapG[j] = G;
				heightMapB[j] = B--;
				j++;
			} 
		}

		public void FillWithSampleValues()
		{
			cR[0] = 1;
			cG[0] = 0;
			cB[0] = 0;

			cR[1] = 1;
			cG[1] = 0;
			cB[1] = 0;

			cR[2] = 0;
			cG[2] = 0.6f;
			cB[2] = 0;

			cR[3] = 1;
			cG[3] = 0;
			cB[3] = 1;

			cR[4] = 1;
			cG[4] = 1;
			cB[4] = 1;

			cR[5] = 0;
			cG[5] = 1;
			cB[5] = 1;

			cR[6] = 0;
			cG[6] = 0;
			cB[6] = 1;

		}

		public void GetColor(byte index, out float R, out float G, out float B)
		{
			if (index > 255)
			{
				R = 0;
				G = 0;
				B = 0;
				return;
			}

			R = cR[index];
			G = cG[index];
			B = cB[index];

		}


		public void GetColor(float height, out byte R, out byte G, out byte B)
		{
			float position = ((float)(height - minHeight)/(maxHeight-minHeight));


			int idx = (int)(position * (float)heightMapB.Length);

			if (idx >= heightMapB.Length)
				idx = heightMapB.Length - 1;

			R = heightMapR[heightMapB.Length - idx - 1];
			G = heightMapG[heightMapB.Length - idx - 1];
			B = heightMapB[heightMapB.Length - idx - 1];

			
			R = heightMapR[idx];
			G = heightMapG[idx];
			B = heightMapB[idx];


			R = 255;
			G = 255;
			B = 0;

		}

		public void GetColor(float height, out float R, out float G, out float B)
		{
			float position = ((float)(height - minHeight) / (maxHeight - minHeight));
      
			int idx = (int)(position * (float)heightMapB.Length);

			if (idx >= heightMapB.Length)
				idx = heightMapB.Length - 1;
      		

			R = 1.0f;
			G = 0;
			B = 0;

			R = heightMapR[idx] / 255.0f;
			G = heightMapG[idx] / 255.0f;
			B = heightMapB[idx] / 255.0f;

			if (R > 0.5 && B > 0)
			{
				float a = R * G * 10;
			}


		}

	}
}
