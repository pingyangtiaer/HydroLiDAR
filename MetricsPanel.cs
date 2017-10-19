using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using las;
using las.datamanager;

namespace TerraForm.MetricsPanel
{
	public partial class MetricsPanel : Form
	{
		public MetricsPanel()
		{
			InitializeComponent();

			indexing.Text = "";
			indexingNoDisk.Text = "";
			avgLoad.Text = "";
			avgLoadNoDisk.Text = "";
			avgPPL.Text = "";
			avgPPLactual.Text = "";

			numberOfNonEmptyLeafs.Text = "";
			numberOfPoints.Text = "";
			FPS.Text = "";
			pointsDrawn.Text = "";
			frameDrawTime.Text = "";

      lblNumPointsInVBOs.Text = "";
      lblNumVBOs.Text = "";

      lblPointsInsideView.Text = "";
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			indexing.Text = LasMetrics.GetInstance().indexing.ToString();
      indexingNoDisk.Text = LasMetrics.GetInstance().indexingNoDisk.ToString();
      avgLoad.Text = LasMetrics.GetInstance().avgLoad.ToString();
      avgLoadNoDisk.Text = LasMetrics.GetInstance().avgLoadNoDisk.ToString();
      avgPPL.Text = LasMetrics.GetInstance().avgPointsPerLeaf.ToString();
      avgPPLactual.Text = LasMetrics.GetInstance().avgPointsPerLeafActual.ToString();
      
      numberOfNonEmptyLeafs.Text = LasMetrics.GetInstance().numberOfNonEmptyLeafs.ToString();
      numberOfPoints.Text = LasMetrics.GetInstance().numberOfPoints.ToString();

      lblNumPointsInVBOs.Text = LasMetrics.GetInstance().numberOfPointsLoadedIntoExistingVBOs.ToString();
      lblNumVBOs.Text = LasMetrics.GetInstance().numberOfExistingVBOs.ToString();

      lblPointsInsideView.Text = ((double)LasMetrics.GetInstance().pointsInsideViewMilis.TotalMilliseconds / LasMetrics.GetInstance().pointsInsideViewCounted).ToString();

      if (RenderMetrics.getInstance() != null)
			{
        FPS.Text = RenderMetrics.getInstance().FPS.ToString();
        frameDrawTime.Text = RenderMetrics.getInstance().frameRenderTime.ToString();

        if (LasMetrics.GetInstance().pointsDrawn > 0)
        {
          pointsDrawn.Text = LasMetrics.GetInstance().pointsDrawn.ToString();
        }
        
        double copiedToGPU = (double)LasMetrics.GetInstance().bytesTransferedToGPU/(1024.0*1024.0);
        lblRAM2GPU.Text = copiedToGPU.ToString();
			}

			tabControl1.SelectedTab.Refresh();
		}

    private void btnRefresh_Click(object sender, EventArgs e)
    {
      lblMemory.Text = ((float)GC.GetTotalMemory(true)/(1024*1024)).ToString();
    }

    private void btnRenderToggle_Click(object sender, EventArgs e)
    {
      if (btnRenderToggle.Text == "Start")
      {
        btnRenderToggle.Text = "Stop";
        LasMetrics.GetInstance().renderToggle = true;
      }
      else
      {
        LasMetrics.GetInstance().renderToggle = false;
        btnRenderToggle.Text = "Start";
      }
    }

    private void btnLeafLoading_toggle_Click(object sender, EventArgs e)
    {
      if (btnLeafLoading_toggle.Text == "Start")
      {
        btnLeafLoading_toggle.Text = "Stop";
        LasMetrics.GetInstance().leafLoadToggle = true;
      }
      else
      {
        LasMetrics.GetInstance().leafLoadToggle = false;
        btnLeafLoading_toggle.Text = "Start";
      }
    }
		
	}
}
