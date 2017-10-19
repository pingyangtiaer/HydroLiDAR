using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tao.Glfw;
using Tao.OpenGl;
using las.datamanager;
using TerraForm.Configuration;
using TerraForm.MetricsPanel;
using laslib;

using las.datamanager.structures;
using TerraForm.AboutBox;

namespace TerraForm
{
  public partial class TerraFormMain : Form
  {
    private ProgressForm progresForm;
    private ConfigurationForm configurationForm;
    private MetricsPanel.MetricsPanel metricsPanel;
    private MiniMap.MiniMapForm miniMap;
    private WorldView worldViewForm = null;
    private Movement.MovementForm movementForm;
 
    private LasDataManager dataManager;

    public TerraFormMain()
    {
      InitializeComponent();

      configurationForm = new ConfigurationForm();
      metricsPanel = new TerraForm.MetricsPanel.MetricsPanel();
      miniMap = new TerraForm.MiniMap.MiniMapForm();
      movementForm = new TerraForm.Movement.MovementForm();

      configurationForm.MdiParent = this;
      configurationForm.Show();
      metricsPanel.MdiParent = this;
      metricsPanel.Show();
      miniMap.MdiParent = this;
      miniMap.Show();
      movementForm.MdiParent = this;
      movementForm.Show();
    }

    private void OpenFile(object sender, EventArgs e)
    {
      if (dataManager == null)
      {
        createWorldViewButton_Click(null, null);
      }

      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Las Files (*.las)|*.las;*." + LasDataManager.serializiationExtension + "|All Files (*.*)|*.*";
     
      if (openFileDialog.ShowDialog(this) == DialogResult.OK)
      {
        string file = openFileDialog.FileName;

        progresForm = new ProgressForm();
        progresForm.Show();

        dataManager.Load(file, forcePreprocessingToolStripMenuItem.Checked);

        LasDataManager.ColorPallette = new ColorPalette(dataManager.GlobalBoundingCube.minZ, dataManager.GlobalBoundingCube.maxZ);

        if (dataManager.QTrees.Count == 1)
        {
          worldViewForm.MovePositionToData();
        }

        Console.WriteLine("Memmory before collection: {0}", (float)System.GC.GetTotalMemory(false) / (1024 * 1024));
        System.GC.Collect();
        Console.WriteLine("Memmory after collection: {0}", (float)System.GC.GetTotalMemory(false) / (1024 * 1024));

      }
    }

    void dataManager_loadingStatusEvent(string status, float progress)
    {
      progresForm.setProgress(status, progress);
    }

    void dataManager_loadingEndedEvent(bool success)
    {
      progresForm.Hide();
      progresForm.Dispose();
      progresForm = null;
    }

    private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void shadersToolStripMenuItem_Click(object sender, EventArgs e)
    {
      simplePointsToolStripMenuItem.Checked = false;
      shadersToolStripMenuItem.Checked = true;
    }

    private IRenderEngine GetSelectedRenderEngine()
    {
      if (simplePointsToolStripMenuItem.Checked)
      {
        return new SimplePointRenderEngine();
      }
      else
      {
        return new TestVertexPixelSprite();
      }
    }

    private void createWorldViewButton_Click(object sender, EventArgs e)
    {


      dataManager = LasDataManager.GetInstance();
      dataManager.loadingStatusEvent += new LasFileLoadingStatus(dataManager_loadingStatusEvent);
      dataManager.loadingEndedEvent += new LasFileLoadingEnd(dataManager_loadingEndedEvent);

      worldViewForm = new WorldView();
      worldViewForm.FormClosed += new FormClosedEventHandler(worldViewForm_FormClosed);

      worldViewForm.positionChangedEvent += movementForm.updatePosition;
      movementForm.changePositionEvent += worldViewForm.ChangePosition;

      worldViewForm.updateFormsEvent += miniMap.Refresh;
      worldViewForm.updateFormsEvent += metricsPanel.Refresh;
      worldViewForm.updateFormsEvent += configurationForm.Refresh;

      worldViewForm.MdiParent = this;
      worldViewForm.Text = "World View";
      worldViewForm.RenderEngine = GetSelectedRenderEngine();
      worldViewForm.RenderEngine.Minimap = miniMap;
      worldViewForm.DataSource = dataManager;
      
      worldViewForm.RenderEngine.Init();
      worldViewForm.OnResize(null, null);

      configurationForm.ConfigurationChangedEvent += new ConfigurationChanged(worldViewForm.RenderEngine.RenderingConfigurationChanged);
      configurationForm.updateRendering();

      //childForm.MovePositionToData();
      worldViewForm.Show();
    }

    void worldViewForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      worldViewForm = null;

      dataManager.Close();
      dataManager = null;

    }

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {      
      SettingsForm settingsForm = new SettingsForm();
      settingsForm.ShowDialog();
    }

    private void simplePointsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      simplePointsToolStripMenuItem.Checked = true;
      shadersToolStripMenuItem.Checked = false;
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AboutBox1 a = new AboutBox1();
      a.ShowDialog();
    }
  }
}
