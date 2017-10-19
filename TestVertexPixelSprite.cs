using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;
using System.Drawing;
using System.Drawing.Imaging;
using las.datamanager;
using Tao.Cg;
using System.Windows.Forms;
using Tao.FreeGlut;
using System.Diagnostics;

using las.datamanager.structures;

namespace TerraForm
{
	public class TestVertexPixelSprite : IRenderEngine
	{
		#region Private fields
		private int[] texture = new int[3];    
		private int selectedTexture = 2;

		private int m_CG_vertexProfile = -1;
		private int m_CG_fragmentProfile = -1;
		private IntPtr m_CGcontext;
		private IntPtr m_CGp_vertexProgramPass1;
		private IntPtr m_CGp_fragmentProgramPass1;
		private IntPtr m_CGp_vertexProgramPass2;
		private IntPtr m_CGp_fragmentProgramPass2;
		private IntPtr m_CGp_vertexProgramPass3;
		private IntPtr m_CGp_fragmentProgramPass3;

		//parameters pass1
		private IntPtr m_CGparam_modelViewProjPass1;
		private IntPtr m_CGparam_modelViewITPass1;					//inverse transpose of model view matrix
		private IntPtr m_CGparam_modelViewPass1;
		private IntPtr m_CGparam_eyePositionPass1;					//our position
		private IntPtr m_CGparam_sizeFactorPass1;
		private IntPtr m_CGparam_zoffsetPass1;
		private IntPtr m_CGparam_farcullPass1;
		private IntPtr m_CGparam_viewParamsPass1;
		private IntPtr m_CGparam_splatRotationCutoffDistPass1;

		//parameters pass2
		private IntPtr m_CGparam_modelViewProjPass2;
    private IntPtr m_CGparam_modelViewITPass2;					//inverse transpose of model view matrix
		private IntPtr m_CGparam_modelViewPass2;
		private IntPtr m_CGparam_eyePositionPass2;
		private IntPtr m_CGparam_sizeFactorPass2;
		private IntPtr m_CGparam_farcullPass2;
		private IntPtr m_CGparam_viewParamsPass2;
		private IntPtr m_CGparam_splatRotationCutoffDistPass2;
		#endregion

		public override void Init()
		{
			renderingConfiguration = new RenderingConfigurator();
			renderingConfiguration.cutoffDistance = -1;
			renderingConfiguration.colorType = ColoringType.Classification;
			renderingConfiguration.pointSize = 1;
			renderingConfiguration.pointTexture = PointTexture.Gaussian;
			
			LoadGLTextures();
			InitCg();
		}

		private void InitCg()
		{
			

			m_CG_vertexProfile = CgGl.cgGLGetLatestProfile(CgGl.CG_GL_VERTEX);

			CgGl.cgGLSetOptimalOptions(m_CG_vertexProfile);
			checkForCgError("selecting vertex profile");

			m_CG_fragmentProfile = CgGl.cgGLGetLatestProfile(CgGl.CG_GL_FRAGMENT);

			CgGl.cgGLSetOptimalOptions(m_CG_fragmentProfile);
			checkForCgError("selecting fragment profile");

			// Create the context...
			m_CGcontext = Cg.cgCreateContext();
			checkForCgError("creating context");

			string cas = Application.StartupPath;

			//load pass1
			m_CGp_vertexProgramPass1 = Cg.cgCreateProgramFromFile(m_CGcontext,
																Cg.CG_SOURCE,
																cas+"/SpriteVertexProgram.cg",
																m_CG_vertexProfile,
																"main_pass1",
																null);
			checkForCgError("creating vertex program pass1 from file SpriteVertexProgram.cg");						
			CgGl.cgGLLoadProgram(m_CGp_vertexProgramPass1);
			checkForCgError("loading vertex program");
			
			m_CGp_fragmentProgramPass1 = Cg.cgCreateProgramFromFile(m_CGcontext,
																Cg.CG_SOURCE,
																cas + "/SpriteFragmentProgram.cg",
																m_CG_fragmentProfile,
																"main_pass1",
																null);
			checkForCgError("creating fragment program from file SpriteFragmentProgram.cg");
			CgGl.cgGLLoadProgram(m_CGp_fragmentProgramPass1);
			checkForCgError("loading fragment program");

			//load pass2
			m_CGp_vertexProgramPass2 = Cg.cgCreateProgramFromFile(m_CGcontext,
																Cg.CG_SOURCE,
																cas + "/SpriteVertexProgram.cg",
																m_CG_vertexProfile,
																"main_pass2",
																null);
			checkForCgError("creating vertex program pass2 from file SpriteVertexProgram.cg");
			CgGl.cgGLLoadProgram(m_CGp_vertexProgramPass2);
			checkForCgError("loading vertex program");

			m_CGp_fragmentProgramPass2 = Cg.cgCreateProgramFromFile(m_CGcontext,
																Cg.CG_SOURCE,
																cas + "/SpriteFragmentProgram.cg",
																m_CG_fragmentProfile,
																"main_pass2",
																null);
			checkForCgError("creating fragment program from file SpriteFragmentProgram.cg");
			CgGl.cgGLLoadProgram(m_CGp_fragmentProgramPass2);
			checkForCgError("loading fragment program");

			//load pass3
			m_CGp_vertexProgramPass3 = Cg.cgCreateProgramFromFile(m_CGcontext,
																Cg.CG_SOURCE,
																cas + "/SpriteVertexProgram.cg",
																m_CG_vertexProfile,
																"main_pass3",
																null);
			checkForCgError("creating vertex program pass3 from file SpriteVertexProgram.cg");
			CgGl.cgGLLoadProgram(m_CGp_vertexProgramPass3);
			checkForCgError("loading vertex program");

			m_CGp_fragmentProgramPass3 = Cg.cgCreateProgramFromFile(m_CGcontext,
																Cg.CG_SOURCE,
																cas + "/SpriteFragmentProgram.cg",
																m_CG_fragmentProfile,
																"main_pass3",
																null);
			checkForCgError("creating fragment program from file SpriteFragmentProgram.cg");
			CgGl.cgGLLoadProgram(m_CGp_fragmentProgramPass3);
			checkForCgError("loading fragment pass3 program");

			//pass1 parameters
			m_CGparam_modelViewProjPass1 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass1, "modelViewProj");
			checkForCgError("getting modal view proj");

			m_CGparam_modelViewITPass1 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass1, "modelViewIT");
			checkForCgError("getting modal view IT");

			m_CGparam_modelViewPass1 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass1, "modelView");
			checkForCgError("getting modal view");

			m_CGparam_eyePositionPass1 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass1, "eyePos");
			checkForCgError("getting eyePos");

			m_CGparam_sizeFactorPass1 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass1, "sizeFactor");
			checkForCgError("getting sizeFactor");

			m_CGparam_zoffsetPass1 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass1, "zoffset");
			checkForCgError("getting zoffset");

			CgGl.cgGLSetParameter1f(m_CGparam_zoffsetPass1, 0.003f);
			checkForCgError("setting zoffset");

			m_CGparam_splatRotationCutoffDistPass1 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass1, "splatRotationCutoffDist");
			checkForCgError("getting splatRotationCutoffDist");

			CgGl.cgGLSetParameter1f(m_CGparam_zoffsetPass1, 0.003f);
			checkForCgError("setting zoffset");


			m_CGparam_farcullPass1 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass1, "farcull");
			checkForCgError("getting farcull");

			CgGl.cgGLSetParameter1f(m_CGparam_farcullPass1, WorldView.farCull);
			checkForCgError("setting farcull");

			m_CGparam_viewParamsPass1 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass1, "viewParams");
			checkForCgError("getting viewParams");
			

			//pass2 parameters
			m_CGparam_modelViewProjPass2 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass2, "modelViewProj");
			checkForCgError("getting modal view proj2");

			m_CGparam_modelViewITPass2 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass2, "modelViewIT");
			checkForCgError("getting modal view IT2");

			m_CGparam_modelViewPass2 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass2, "modelView");
			checkForCgError("getting modal view2");

			m_CGparam_eyePositionPass2 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass2, "eyePos");
			checkForCgError("getting eyePos2");

			m_CGparam_sizeFactorPass2 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass2, "sizeFactor");
			checkForCgError("getting sizeFactor2");

			m_CGparam_splatRotationCutoffDistPass2 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass2, "splatRotationCutoffDist");
			checkForCgError("getting splatRotationCutoffDist");

			CgGl.cgGLSetParameter1f(m_CGparam_sizeFactorPass1, 1f);
			checkForCgError("setting sizeFactor");

			CgGl.cgGLSetParameter1f(m_CGparam_sizeFactorPass2, 1f);
			checkForCgError("setting sizeFactor2");

			m_CGparam_farcullPass2 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass2, "farcull");
			checkForCgError("getting farcull");

			CgGl.cgGLSetParameter1f(m_CGparam_farcullPass2, WorldView.farCull);
			checkForCgError("setting farcull");

			m_CGparam_viewParamsPass2 = Cg.cgGetNamedParameter(m_CGp_vertexProgramPass2, "viewParams");
			checkForCgError("getting viewParams");
			
		}

		private void checkForCgError(string situation)
		{
			int error;
			string errorString = Cg.cgGetLastErrorString(out error);

			if (error != Cg.CG_NO_ERROR)
			{
				Console.WriteLine("{0}- {1}- {2}",
					"error in my program", situation, errorString);
				if (error == Cg.CG_COMPILER_ERROR)
				{
          Console.WriteLine("{0}", Cg.cgGetLastListing(m_CGcontext));
				}
			}
		}

		public override void RenderScene(float FOV, float near, float far, Vector3f eyeVector, float xPos, float yPos)
		{
      base.RenderScene(FOV, near, far, eyeVector, xPos, yPos);

			Gl.glTexEnvf(Gl.GL_POINT_SPRITE_ARB, Gl.GL_COORD_REPLACE_ARB, Gl.GL_TRUE);
			Gl.glEnable(Gl.GL_POINT_SPRITE_ARB);
			//control point size from vertex program?
			Gl.glEnable(Gl.GL_VERTEX_PROGRAM_POINT_SIZE_ARB);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[selectedTexture]);

			//enable additive blending
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA); //set the blend function							

			Gl.glDepthFunc(Gl.GL_LEQUAL);		//less or equal for depth
						
			
			#region PASS 1
			//enable writing into depth buffer to 
			Gl.glDepthMask(Gl.GL_TRUE);

			EnableShadersPass1(eyeVector);
						
			Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
			Gl.glEnableClientState(Gl.GL_NORMAL_ARRAY);
			Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
			dataSource.RenderingPass(FOV, near, far, eyeVector, new Point2f( xPos, yPos));
			Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
			Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
			Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);
			Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);
      
			#endregion			
			
			#region PASS 2
				
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

			Gl.glDepthMask(Gl.GL_FALSE);
			
			EnableShadersPass2(eyeVector);

			Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
			Gl.glEnableClientState(Gl.GL_NORMAL_ARRAY);
			Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
      VBOUtils.RenderVBOsFastSecondPass();
      VBOUtils.ClearSecondPassQueue();
			Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
			Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
			Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);
			Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0);
			

			#endregion
			
			DisableShaders(); 
						
			Gl.glDepthMask(Gl.GL_TRUE);
			Gl.glDisable(Gl.GL_POINT_SPRITE_ARB);			
		}

		private void SetShaderParametersPass1(Vector3f eyeVect)
		{			
			CgGl.cgGLSetParameter3f(m_CGparam_eyePositionPass1, ViewerPosition.x, ViewerPosition.y, ViewerPosition.z);
			checkForCgError("setting eyePos");			

			// Track the combined model-view-projection matrix			
			CgGl.cgGLSetStateMatrixParameter(m_CGparam_modelViewProjPass1,
													CgGl.CG_GL_MODELVIEW_PROJECTION_MATRIX,
													CgGl.CG_GL_MATRIX_IDENTITY);
			checkForCgError("setting model view projection matrix parameter");

			//set modelViewIT matrix
			CgGl.cgGLSetStateMatrixParameter(m_CGparam_modelViewITPass1,
													CgGl.CG_GL_MODELVIEW_MATRIX,
													CgGl.CG_GL_MATRIX_INVERSE_TRANSPOSE);
			checkForCgError("setting model view IT matrix parameter");

			CgGl.cgGLSetStateMatrixParameter(m_CGparam_modelViewPass1,
										CgGl.CG_GL_MODELVIEW_MATRIX,
										CgGl.CG_GL_MATRIX_IDENTITY);
			checkForCgError("setting model view matrix parameter");
		}

		private void SetShaderParametersPass2(Vector3f eyeVect)
		{
			CgGl.cgGLSetParameter3f(m_CGparam_eyePositionPass2, ViewerPosition.x, ViewerPosition.y, ViewerPosition.z);
			checkForCgError("setting eyePos2");

			// Track the combined model-view-projection matrix			
			CgGl.cgGLSetStateMatrixParameter(m_CGparam_modelViewProjPass2,
													CgGl.CG_GL_MODELVIEW_PROJECTION_MATRIX,
													CgGl.CG_GL_MATRIX_IDENTITY);
			checkForCgError("setting model view projection matrix parameter2");

			//set modelViewIT matrix
			CgGl.cgGLSetStateMatrixParameter(m_CGparam_modelViewITPass2,
													CgGl.CG_GL_MODELVIEW_MATRIX,
													CgGl.CG_GL_MATRIX_INVERSE_TRANSPOSE);
			checkForCgError("setting model view IT matrix parameter2");

			CgGl.cgGLSetStateMatrixParameter(m_CGparam_modelViewPass2,
										CgGl.CG_GL_MODELVIEW_MATRIX,
										CgGl.CG_GL_MATRIX_IDENTITY);
			checkForCgError("setting model view matrix parameter2");
		}

		private void EnableShadersPass1(Vector3f eyeVector)
		{
			SetShaderParametersPass1(eyeVector);

			CgGl.cgGLBindProgram(m_CGp_vertexProgramPass1);
			checkForCgError("binding vertex program");

			CgGl.cgGLEnableProfile(m_CG_vertexProfile);
			checkForCgError("enabling vertex profile");
						
			CgGl.cgGLBindProgram(m_CGp_fragmentProgramPass1);
			checkForCgError("binding fragment program");

			CgGl.cgGLEnableProfile(m_CG_fragmentProfile);
			checkForCgError("enabling fragment profile");
			 
		}

		private void DisableShaders()
		{			
			CgGl.cgGLDisableProfile(m_CG_fragmentProfile);
			checkForCgError("disabling fragment profile");

			CgGl.cgGLDisableProfile(m_CG_vertexProfile);
			checkForCgError("disabling fragment profile");
		}

		private void EnableShadersPass2(Vector3f eyeVector)
		{
			SetShaderParametersPass2(eyeVector);
			
			CgGl.cgGLBindProgram(m_CGp_vertexProgramPass2);
			checkForCgError("binding vertex program2");

			CgGl.cgGLEnableProfile(m_CG_vertexProfile);
			checkForCgError("enabling vertex profile2");

			CgGl.cgGLBindProgram(m_CGp_fragmentProgramPass2);
			checkForCgError("binding fragment program2");

			CgGl.cgGLEnableProfile(m_CG_fragmentProfile);
			checkForCgError("enabling fragment profile2");
		}

		private void EnableShadersPass3()
		{
			CgGl.cgGLBindProgram(m_CGp_vertexProgramPass3);
			checkForCgError("binding vertex program3");

			CgGl.cgGLEnableProfile(m_CG_vertexProfile);
			checkForCgError("enabling vertex profile3");

			CgGl.cgGLBindProgram(m_CGp_fragmentProgramPass3);
			checkForCgError("binding fragment program3");

			CgGl.cgGLEnableProfile(m_CG_fragmentProfile);
			checkForCgError("enabling fragment profile3");
		}

		public override void Destroy()
		{
			Cg.cgDestroyProgram(m_CGp_vertexProgramPass1);
			Cg.cgDestroyProgram(m_CGp_fragmentProgramPass1);
			Cg.cgDestroyProgram(m_CGp_vertexProgramPass2);
			Cg.cgDestroyProgram(m_CGp_fragmentProgramPass2);
			Cg.cgDestroyProgram(m_CGp_vertexProgramPass3);
			Cg.cgDestroyProgram(m_CGp_fragmentProgramPass3);
			Cg.cgDestroyContext(m_CGcontext);

      if (texture[0] != 0)
      {
        Gl.glDeleteTextures(texture.Length, texture);
      }
		}


		private bool LoadGLTextures()
		{
			bool status = false;                                                // Status Indicator
			Bitmap[] textureImage = new Bitmap[3];

			string cas = Application.StartupPath;
			textureImage[0] = new Bitmap(Application.StartupPath + "\\square.png");                // Load The Bitmap
      textureImage[1] = new Bitmap(Application.StartupPath + "\\circle.png");                // Load The Bitmap
      textureImage[2] = new Bitmap(Application.StartupPath + "\\gauss2D.png");                // Load The Bitmap

			Gl.glGenTextures(3, texture);                            // Create The Texture


			for (int i = 0; i < textureImage.Length; i++)
			{
				// Check For Errors, If Bitmap's Not Found, Quit
				if (textureImage[i] != null)
				{
					status = true;

					textureImage[i].RotateFlip(RotateFlipType.RotateNoneFlipY);
					Rectangle rectangle = new Rectangle(0, 0, textureImage[i].Width, textureImage[i].Height);
					BitmapData bitmapData = textureImage[i].LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

					// Typical Texture Generation Using Data From The Bitmap
					Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[i]);
					Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_ALPHA, textureImage[i].Width, textureImage[i].Height,
						0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);


					Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
					Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);

					if (textureImage[i] != null)
					{                                   // If Texture Exists
						textureImage[i].UnlockBits(bitmapData);                     // Unlock The Pixel Data From Memory
						textureImage[i].Dispose();                                  // Dispose The Bitmap
					}
				}
			}


			return status;                                                      // Return The Status
		}

		public override void RenderingConfigurationChanged(RenderingConfigurator conf)
		{
			renderingConfiguration = conf;

			if (LasDataManager.ColorPallette != null)
			{
				LasDataManager.ColorPallette.ColorMode = renderingConfiguration.colorType;
			}

			selectedTexture = (int)renderingConfiguration.pointTexture;

			CgGl.cgGLSetParameter1f(m_CGparam_sizeFactorPass1, renderingConfiguration.pointSize);
			checkForCgError("setting sizeFactor");

			CgGl.cgGLSetParameter1f(m_CGparam_sizeFactorPass2, renderingConfiguration.pointSize);
			checkForCgError("setting sizeFactor2");

			CgGl.cgGLSetParameter1f(m_CGparam_zoffsetPass1, renderingConfiguration.Zoffset);
			checkForCgError("setting zoffset");

			CgGl.cgGLSetParameter1f(m_CGparam_splatRotationCutoffDistPass1, renderingConfiguration.splatRotationCutoffDistance);
			checkForCgError("setting splatRotationCutoffDistancePass1");

			CgGl.cgGLSetParameter1f(m_CGparam_splatRotationCutoffDistPass2, renderingConfiguration.splatRotationCutoffDistance);
			checkForCgError("setting splatRotationCutoffDistancePass2");
		}
    	
		public override void WindowResized(int width, int height, float FOV)
		{

			CgGl.cgGLSetParameter3f(m_CGparam_viewParamsPass1, width, height, FOV);
			checkForCgError("setting viewParams pass 1");

			CgGl.cgGLSetParameter3f(m_CGparam_viewParamsPass2, width, height, FOV);
			checkForCgError("setting viewParams pass 2");

		}
	}
}
