using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using las.datamanager.structures;
using las.datamanager;

namespace TerraForm.Configuration
{
	public delegate void ConfigurationChanged(RenderingConfigurator rc);

	public partial class ConfigurationForm : Form
	{
		RenderingConfigurator rconf = new RenderingConfigurator();

		public RenderingConfigurator CurrentConfiguration
		{
			get { 
        return rconf; 
      }
			set { 
        rconf = value;
			  updateRendering();
			}
		}
	

		public event ConfigurationChanged ConfigurationChangedEvent;

		public ConfigurationForm()
		{
			InitializeComponent();
			rbGauss.Checked = true;
			rconf.pointSize = 1f;
			rconf.Zoffset = 0.003f;
			rconf.splatRotationCutoffDistance = trackSplatRotationCutoff.Value;

			lblSplatrotationCutoff.Text = trackSplatRotationCutoff.Value.ToString();
		}

		private void pointSizeBar_Scroll(object sender, EventArgs e)
		{
			updateRendering();
		}

		private void rbClassification_CheckedChanged(object sender, EventArgs e)
		{
			updateRendering();
		}

		private void rbHeight_CheckedChanged(object sender, EventArgs e)
		{
			updateRendering();
		}

		public void updateRendering()
		{			
			rconf.pointSize = (float)pointSizeBar.Value * 0.01f;
			lblPointSize.Text = rconf.pointSize.ToString()+"x";

			rconf.Zoffset = (float)(zoffsetBar.Value - zoffsetBar.Maximum / 2)/5000.0f;
			
			if (rbClassification.Checked)
			{
				rconf.colorType = ColoringType.Classification;
			}
			else if (rbHeight.Checked)
			{
				rconf.colorType = ColoringType.Height;
			}
			else if (rbMonochrome.Checked)
			{
				rconf.colorType = ColoringType.Monochrome;
			}

			if (rbSquare.Checked)
			{
				rconf.pointTexture = PointTexture.Square;
			}
			else if (rbCircle.Checked)
			{
				rconf.pointTexture = PointTexture.Circle;
			}
			else if (rbGauss.Checked)
			{
				rconf.pointTexture = PointTexture.Gaussian;
			}

			rconf.splatRotationCutoffDistance = trackSplatRotationCutoff.Value;

			if (ConfigurationChangedEvent != null)
			{
				ConfigurationChangedEvent(rconf);
			}
		}

		private void rbSquare_CheckedChanged(object sender, EventArgs e)
		{
			updateRendering();
		}

		private void rbCircle_CheckedChanged(object sender, EventArgs e)
		{
			updateRendering();
		}

		private void rbGauss_CheckedChanged(object sender, EventArgs e)
		{
			updateRendering();
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			updateRendering();
		}

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      updateRendering();
    }

		private void trackSplatRotationCutoff_Scroll(object sender, EventArgs e)
		{
			lblSplatrotationCutoff.Text = trackSplatRotationCutoff.Value.ToString();
			updateRendering();
		}

    private void cbPerNodeLOD_CheckedChanged(object sender, EventArgs e)
    {
      QTreeNode.PerNodeLOD = cbPerNodeLOD.Checked;
    }		
	}
}