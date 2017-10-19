using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using las.datamanager;
using las.datamanager.structures;

namespace TerraForm.MiniMap
{
  public partial class MiniMapForm : Form
  {
    public las.datamanager.LasDataManager dataSource { get; set; }

		private Color[] minimapColors = new Color[] { Color.Cyan, Color.Red, Color.White, Color.Orange,
		Color.Orange, Color.Pink, Color.Gray, Color.Green };

    private float FOV;
    private float near;
    private float far;
    private Vector3f eyeVector;
    private float xPos;
    private float yPos;
   
    public MiniMapForm()
    {
      InitializeComponent();
      
      refreshTimer.Interval = 1000;
      refreshTimer.Start();
    }

    private void mapPanel_Paint(object sender, PaintEventArgs e)
    {      
      if (dataSource != null && dataSource.QTrees.Count > 0)
      {
        Graphics g = e.Graphics;
        
        int offset = 5;
        int mmSide = mapPanel.Width - 2 * offset;

        //draw sorrounding box 
        g.DrawRectangle(Pens.Black, offset, offset, mmSide, mmSide);

        //draw global bounding box(gbb) with position relative to the sorrounding box
        BoundingCube gbb = dataSource.GlobalBoundingCube;        
				Rectangle gbb_draw_rect = new Rectangle();

				float xAxisScale = 1;
				float yAxisScale = 1;

				if (gbb.width > gbb.depth)
				{
					//gbb is wider and will span from x=0 to x=mmSide;
					gbb_draw_rect.X = offset;
					gbb_draw_rect.Width = mmSide;

					yAxisScale = gbb.depth / gbb.width;
					float gbb_height = mmSide * yAxisScale;

					gbb_draw_rect.Y = offset + (int)((mmSide - gbb_height) / 2.0f);
					gbb_draw_rect.Height = (int)gbb_height;
				}
				else
				{
					//gbb is higher than wider, so it will have dominant vertical axis
					gbb_draw_rect.Y = offset;
					gbb_draw_rect.Height = mmSide;

					xAxisScale = gbb.width / gbb.depth;
					float gbb_width = mmSide * xAxisScale;
					
					gbb_draw_rect.X = offset + (int)((mmSide - gbb_width) /2.0f);
					gbb_draw_rect.Width = (int)gbb_width;
				}

				g.FillRectangle(Brushes.Blue, gbb_draw_rect);

				double xMin = float.MaxValue;
				double yMin = float.MaxValue;
				
				//draw all bounding boxes of all loaded point trees
				for( int i=0; i< dataSource.QTrees.Count; i++ )
				{
					QTreeWrapper qtree = dataSource.QTrees[i];
					Rectangle qrect = new Rectangle();

					if (xMin > qtree.lasFile.header.MinX)
					{
						xMin = qtree.lasFile.header.MinX;
					}
					if (yMin > qtree.lasFile.header.MinY)
					{
						yMin = qtree.lasFile.header.MinY;
					}
					
					qrect.X = (int)(gbb_draw_rect.X + mmSide * xAxisScale * (qtree.lasFile.header.MinX - gbb.minX) / gbb.width);
					qrect.Y = (int)(gbb_draw_rect.Y + mmSide * yAxisScale * (qtree.lasFile.header.MinY - gbb.minY) / gbb.depth);
					qrect.Width = (int)(gbb_draw_rect.Width * (qtree.qtree.RootNode.boundingBox.width / gbb.width));
					qrect.Height = (int)(gbb_draw_rect.Height * (qtree.qtree.RootNode.boundingBox.height / gbb.depth));

					Brush b = new SolidBrush(Color.FromArgb(100, minimapColors[i]));
					g.FillRectangle(b, qrect);
				}

				//calculate the largest offset in negative values over the first loaded Qtree
				xMin -= dataSource.QTrees[0].lasFile.header.MinX;
				yMin -= dataSource.QTrees[0].lasFile.header.MinY;
				
				//draw users position and lookAt vector
				Point userPos = new Point();
				int circleRadius = 5;
				//offsets to gbb and offset within gbb are a must, because coords will be relative to the (0,0)
				//of gbb
				userPos.X = (int)(gbb_draw_rect.X + mmSide * ((xPos-xMin)/gbb.width) * xAxisScale);
				userPos.Y = (int)(gbb_draw_rect.Y + mmSide * ((yPos-yMin)/gbb.depth) * yAxisScale);
				g.FillEllipse(Brushes.Yellow, userPos.X - circleRadius, userPos.Y - circleRadius,
					2 * circleRadius, 2* circleRadius);

				Point userLookAt = new Point();
				userLookAt.X = (int)(userPos.X + eyeVector.x*100.0f);
				userLookAt.Y = (int)(userPos.Y + eyeVector.z*100.0f);
				g.DrawLine(Pens.Yellow, userPos, userLookAt);

				
      }
    }

    
    public void updateMap(float FOV, float near, float far, Vector3f eyeVector, float xPos, float yPos)
    {
      this.FOV = FOV;
      this.near = near;
      this.far = far;
      this.eyeVector = eyeVector;
      this.xPos = xPos;
      this.yPos = yPos;
    }

    private void refreshTimer_Tick(object sender, EventArgs e)
    {
      Refresh();
    }
  }
}
