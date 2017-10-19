using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using las.datamanager;
using las.datamanager.structures;

namespace TerraForm.Configuration
{
  public partial class SettingsForm : Form
  {
    public SettingsForm()
    {
      InitializeComponent();

      //load values into the interface

      tbGPUMem.Text = ((int)((double)LasDataManager.dedicatedPointMemory/(1024.0*1024.0))).ToString();
      tbFOV.Text = WorldView.FOV.ToString();
      tbMinDist.Text = WorldView.nearCull.ToString();
      tbMaxDist.Text = WorldView.farCull.ToString();

      tbBufferedDist.Text = LasDataManager.BufferedDistance.ToString();
      tbBufferedFOV.Text = LasDataManager.BufferedFOV.ToString();
      tbPositionRadius.Text = LasDataManager.BufferedPositionRadius.ToString();

      tbNormalMethodIt.Text = QTreeLeaf.MAX_ITERATIONS_NORMAL.ToString();
      tbClosestListIt.Text = QTreeLeaf.MAX_ITERATIONS_SECOND.ToString();
      tbSameListIt.Text = QTreeLeaf.MAX_ITERATIONS_SAME.ToString();

      tbFPSlimit.Text = WorldView.fps_limit.ToString();
      tbPPL.Text = QTree.POINTS_PER_LEAF.ToString();

      if ( !LasDataManager.ApplyGlobalPointsScaleFactor )
      {
        //TODO: some manually converted test point clouds contained weird scaling, here is a way to manually fix it
        cbGlobalPointScaleFactor.Checked = false;
      }
      else
      {
        cbGlobalPointScaleFactor.Checked = true;
        tbScaleX.Text = LasDataManager.GlobalPointsScaleFactor.x.ToString();
        tbScaleY.Text = LasDataManager.GlobalPointsScaleFactor.y.ToString();
        tbScaleZ.Text = LasDataManager.GlobalPointsScaleFactor.z.ToString();
      }

    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      bool success = false;

      //save values into their parameters
      try
      {
        LasDataManager.dedicatedPointMemory = int.Parse(tbGPUMem.Text) * 1014 * 1024;
        WorldView.FOV = float.Parse( tbFOV.Text );
        WorldView.nearCull = float.Parse( tbMinDist.Text );
        WorldView.farCull = float.Parse( tbMaxDist.Text );
        WorldView.fps_limit = int.Parse(tbFPSlimit.Text);

        LasDataManager.BufferedDistance = float.Parse(tbBufferedDist.Text);
        LasDataManager.BufferedFOV = float.Parse(tbBufferedFOV.Text);
        LasDataManager.BufferedPositionRadius = float.Parse(tbPositionRadius.Text);

        QTreeLeaf.MAX_ITERATIONS_NORMAL = int.Parse(tbNormalMethodIt.Text);
        QTreeLeaf.MAX_ITERATIONS_SECOND = int.Parse(tbClosestListIt.Text);
        QTreeLeaf.MAX_ITERATIONS_SAME = int.Parse(tbSameListIt.Text);

        QTree.POINTS_PER_LEAF = int.Parse(tbPPL.Text);

        if (cbGlobalPointScaleFactor.Checked)
        {
          LasDataManager.ApplyGlobalPointsScaleFactor = true;
          //TODO: some manually converted test point clouds contained weird scaling, here is a way to manually fix it
          LasDataManager.GlobalPointsScaleFactor = new Point3D(float.Parse(tbScaleX.Text), float.Parse(tbScaleY.Text), float.Parse(tbScaleZ.Text));
        }
        else
        {
          LasDataManager.ApplyGlobalPointsScaleFactor = false;
          LasDataManager.GlobalPointsScaleFactor = new Point3D(1.0f,1.0f, 1.0f);
        }

        success = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error saving current settings: " + ex.Message, "Error in values", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      if (success)
      {
        this.Close();
      }
    }

    private void label12_Click(object sender, EventArgs e)
    {

    }
  }
}
