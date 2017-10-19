using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using System.Threading;
using System.Diagnostics;
using laslib;
using las.datamanager;

using las;

namespace TerraForm
{
	[DebuggerDisplay("")]
	public partial class WorldView : Form
	{
		public static float FOV = 75;
    public static float nearCull = 2;
		public static float farCull = 1000;

		private float m_camposX = 15f, m_camposY = 15f, m_camposZ = 15f;
		private float m_camvectX = -1f, m_camvectY = -1f, m_camvectZ = -1f;
		
		private float angle = 0;

    private int simpleOpenGLControlWidth;
    private int simpleOpenGLControlHeight;

		private Random rand = new Random((int)System.DateTime.Now.Ticks);


		Thread animationThread;
		Thread interactionThread;
		bool interactionThreadRunning;
		private bool animationThreadRunning = true;
		public static int fps_limit = 100;

		private IRenderEngine renderEngine;

		public IRenderEngine RenderEngine
		{
			get { return renderEngine; }
			set
			{
				renderEngine = value;
				if (dataSource != null)
				{
					renderEngine.DataSource = dataSource;
				}
			}
		}
		
		private LasDataManager dataSource;

		private bool readOnlyDataSource;

		/// <summary>
		/// indicates if data source should is only to be read. If true requests will not be made to it
		/// </summary>
		public bool ReadOnlyDataSource
		{
			get { return readOnlyDataSource; }
			set { readOnlyDataSource = value; }
		}

		/// <summary>
		/// provides data to display
		/// </summary>
		public LasDataManager DataSource
		{
			get { return dataSource; }
			set
			{
				dataSource = value;

				if (renderEngine != null)
				{
					renderEngine.DataSource = value;
				}
			}
		}

		public WorldView()
		{
			InitializeComponent();
			simpleOpenGlControl.InitializeContexts();

			simpleOpenGLControlHeight = simpleOpenGlControl.Height;
			simpleOpenGLControlWidth = simpleOpenGlControl.Width;

			Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

			this.OnResize(null, null);

			Gl.glShadeModel(Gl.GL_SMOOTH);    //use flat for faster calc

			//makes Z-buffer read only. Supposed to remove artifacts wen rendering unsorted splats
			Gl.glEnable(Gl.GL_DEPTH_TEST);    //for lights
			
			Gl.glEnable(Gl.GL_TEXTURE_2D);      

			Gl.glEnable(Gl.GL_BLEND);   //alpha blending
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA); //set the blend function
			Gl.glEnable(Gl.GL_COLOR_MATERIAL);    //take color from material set vith glColor*f

			//glut for some quick tasks
			Glut.glutInit();
      
			animationThread = new Thread(new ThreadStart(Animate));
			animationThreadRunning = true;
			animationThread.Start();

			interactionThread = new Thread(new ThreadStart(ProcessInteraction));
			interactionThreadRunning = true;
			interactionThread.Start();

			frameCounterStopwatch.Start();      
		}
    	
		void camera()
		{
			Gl.glRotatef(xrot, 1.0f, 0.0f, 0.0f); //rotate our camera on teh x-axis (left and right)
			Gl.glRotatef(yrot, 0.0f, 1.0f, 0.0f); //rotate our camera on the y-axis (up and down)
			Gl.glTranslated(-xpos, -ypos, -zpos); //translate the screen to the position of our camera
		}
    		
		Stopwatch watch = new Stopwatch();

		//counts average frames drawn in last FPSinterval seconds
		long fpsSampleIntervalMilis = 2000;
		int frameCounter = 0;
		Stopwatch frameCounterStopwatch = new Stopwatch();
		
		private void OnPaint(object sender, PaintEventArgs e)
		{
			frameCounter++;

			watch.Stop();			
			base.OnPaint(e);			 

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
      			
			RenderScene();

			stopwatch.Stop();

			//this.Text = "Frame rendered in " + stopwatch.ElapsedMilliseconds + " ms. Between frames: "+watch.ElapsedMilliseconds+", points: "+QTreeLeaf.total_drawn_points_in_pass;
						
			//Console.WriteLine("f#"+frameCounter+" rendered in " + stopwatch.ElapsedMilliseconds + "ms, between frames: " + watch.ElapsedMilliseconds + "ms, points: " + QTreeLeaf.total_drawn_points_in_pass);

			if (frameCounterStopwatch.ElapsedMilliseconds > fpsSampleIntervalMilis)
			{
				frameCounterStopwatch.Stop();

				float fps = 1000.0f * (float)frameCounter / frameCounterStopwatch.ElapsedMilliseconds;
				float avgTime = (float)frameCounterStopwatch.ElapsedMilliseconds / frameCounter;

        Text = String.Format( "FPS: {0}, points drawn: {1}", fps, LasMetrics.GetInstance().pointsDrawn );

				RenderMetrics.getInstance().FPS = fps;
        RenderMetrics.getInstance().frameRenderTime = avgTime;
        
        if (updateFormsEvent != null)
        {
          updateFormsEvent();
        }

        frameCounter = 0;
				frameCounterStopwatch.Reset();
				frameCounterStopwatch.Start();
			}			

			watch.Reset();
			watch.Start();			
		}

		public void OnResize(object sender, EventArgs e)
		{
			simpleOpenGLControlWidth = simpleOpenGlControl.Width;
			simpleOpenGLControlHeight = simpleOpenGlControl.Height;

			simpleOpenGlControl.MakeCurrent();
			Gl.glViewport(0, 0, simpleOpenGLControlWidth, simpleOpenGLControlHeight);
			Gl.glMatrixMode(Gl.GL_PROJECTION); //set it so we can play with the 'camera'
			Gl.glLoadIdentity(); //replace the current matrix with the Identity Matrix
			Glu.gluPerspective(FOV, (double)simpleOpenGLControlWidth / (double)simpleOpenGLControlHeight, nearCull, farCull);
			Gl.glMatrixMode(Gl.GL_MODELVIEW); //switch back the the model editing mode.

			if( renderEngine != null )
				renderEngine.WindowResized(simpleOpenGLControlWidth, simpleOpenGLControlHeight, FOV);
		}

		private void Animate()
		{      
			Stopwatch stopwatch = new Stopwatch();

			int sleep = 0;

			stopwatch.Start();

			while (animationThreadRunning)
			{
				sleep = 1000 / fps_limit;
				angle += 1.0f;

				if (this.Visible)
				{
					stopwatch.Reset();
					stopwatch.Start();
					simpleOpenGlControl.Draw();
					stopwatch.Stop();

					sleep -= (int)stopwatch.ElapsedMilliseconds;
				}

				if (sleep < 0)
				{
					sleep = 0;
				}

				//sleep between two processings of keyboard events
				Thread.Sleep(sleep);
			}
		}

		private void ProcessInteraction()
		{
      int interactionInterval = 10;

			while (interactionThreadRunning)
			{
				ProcessKeys();
				//sleep between two processings of keyboard events
				Thread.Sleep(interactionInterval);
			}
		}

		private void SetCamera()
		{
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();
			Glu.gluLookAt(m_camposX, m_camposY, m_camposZ,
					m_camposX + m_camvectX, m_camposY + m_camvectY, m_camposZ + m_camvectZ, 0, 1, 0); // eye(x,y,z), focal(x,y,z), up(x,y,z)

		}

		private void WorldView_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (renderEngine != null)
			{
				renderEngine.Destroy();
			}

      animationThreadRunning = false;
      interactionThreadRunning = false;
      
      Console.WriteLine("Waiting for threads to finish their jobs");
      this.Text = "Waiting for threads to finish their jobs";
      animationThread.Join();
      interactionThread.Join();      
		}
	}
}