using System;
using System.Collections.Generic;
using System.Text;
using las.datamanager.structures;
using Tao.OpenGl;

namespace las.datamanager
{
  /// <summary>
  /// consists of methods used regarding the VBOs
  /// </summary>
  public class VBOUtils
  {
    /// <summary>
    /// this list will hold the VBOs that have to be rendered during the first pass of rendering,
    /// so they can be rendered quickly in the second pass again
    /// </summary>
    private static List<VBOStorageInformation> secondPassVBOs = new List<VBOStorageInformation>(2000);
    
    public const int PointInformationSize = 40;   //in bytes

    private static int pointsDrawnInPass = 0;

    /// <summary>
    /// copies points into the VBOs. returns number of points that were copied
    /// </summary>
    /// <param name="nbrPts"></param>
    /// <param name="vertices"></param>
    /// <param name="normals"></param>
    /// <param name="colors"></param>
    /// <param name="serverBufferIds"></param>
    /// <returns></returns>
    unsafe public static void CopyPointsToVBOs(float[] interleavedData, VBOStorageInformation serverBufferIds)
    {
      //create and initialize vertexbuffer
      Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, serverBufferIds.InterleavedVBO);
      VBOUtils.CheckGlError("Tried first binding of interleaved VBO during glBegin() and glEnd()");

      long bytesCopied = 0;
      int size = 0;

      fixed (float* vptr = interleavedData)
      {
        size = (sizeof(float) * interleavedData.Length);
        bytesCopied += size;
        Gl.glBufferData(Gl.GL_ARRAY_BUFFER, (IntPtr)size, (IntPtr)vptr, Gl.GL_STATIC_DRAW);
      }
      VBOUtils.CheckGlError("Error copying interleaved data to VBO");

      LasMetrics.GetInstance().bytesTransferedToGPU += bytesCopied;
      LasMetrics.GetInstance().numberOfPointsLoadedIntoExistingVBOs += serverBufferIds.NumberOfPoints;    
    }

    public static void DeleteFromGPU(VBOStorageInformation serverBufferIds)
    {
      //free memory on server
      if (serverBufferIds.NumberOfPoints == 0)
      {
        return;
      }
      int[] buffersToDelete = new int[3];
      buffersToDelete[0] = serverBufferIds.VertexVBO;
      buffersToDelete[1] = serverBufferIds.NormalVBO;
      buffersToDelete[2] = serverBufferIds.ColorVBO;
      Gl.glDeleteBuffers(buffersToDelete.Length, buffersToDelete);

      LasMetrics.GetInstance().bytesCurrentlyOnGPURAM -= serverBufferIds.NumberOfPoints * 3 * 4;   //3=every point has color, position and normal; 4=every value has 4 bytes
      LasMetrics.GetInstance().numberOfExistingVBOs--;
      LasMetrics.GetInstance().numberOfPointsLoadedIntoExistingVBOs -= serverBufferIds.NumberOfPoints;

      serverBufferIds.ColorVBO = serverBufferIds.NormalVBO = serverBufferIds.VertexVBO = 0;
      serverBufferIds.NumberOfPoints = 0;      
    }

    unsafe public static VBOStorageInformation GenerateVBOs(int numberOfPoints)
    {
      //generate free buffer names for 3 different buffers
      int bufferId = 0;

      Gl.glGenBuffers(1, out bufferId);

      VBOUtils.CheckGlError("Tried first binding of VBO during glBegin() and glEnd()");

      if (bufferId == 0)
      {
        throw new ApplicationException("ID of VBO should be set but was not!!!");
      }

      VBOStorageInformation vboSI = new VBOStorageInformation();
      vboSI.InterleavedVBO = bufferId;
      vboSI.NumberOfPoints = numberOfPoints;

      LasMetrics.GetInstance().numberOfExistingVBOs++;
      return vboSI;
    }

    public static void RenderVBO(VBOStorageInformation vbo)
    {

      if (LasMetrics.GetInstance().renderToggle == false )
      {
        return;
      }

      Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, vbo.InterleavedVBO);

      //array is interleaved like this: VVVNNNCCCC - this is 10*4 bytes.       
      

      //try glInterleavedArrays instead of this three calls, should be faster
      Gl.glInterleavedArrays(Gl.GL_C4F_N3F_V3F, PointInformationSize, IntPtr.Zero);

      Gl.glDrawArrays(Gl.GL_POINTS, 0, vbo.NumberOfPoints);

      pointsDrawnInPass += vbo.NumberOfPoints;      

      //add vbo for fast second pass rendering
      secondPassVBOs.Add(vbo);
    }

    public static void RenderVBOsFastSecondPass()
    {
      if (LasMetrics.GetInstance().renderToggle == false || secondPassVBOs.Count == 0)
      {
        return;
      }

      int numVBOSIs = secondPassVBOs.Count;
      VBOStorageInformation vbo = null;

      int stride = 40;
      
      for (int i = 0; i < numVBOSIs; i++)
      {
        vbo = secondPassVBOs[i];
        Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, vbo.InterleavedVBO);
        
        Gl.glInterleavedArrays(Gl.GL_C4F_N3F_V3F, stride, IntPtr.Zero);
        Gl.glDrawArrays(Gl.GL_POINTS, 0, vbo.NumberOfPoints);
      }      
    }

    public static void ClearSecondPassQueue()
    {
      secondPassVBOs.Clear();

      LasMetrics.GetInstance().pointsDrawn = pointsDrawnInPass;
      pointsDrawnInPass = 0;
    }

    public static void CheckGlError(string msg)
    {
      int glError = Gl.glGetError();

      if (glError == Gl.GL_OUT_OF_MEMORY)
      {
        throw new ApplicationException("Not enough memory: " + msg);
      }
      else if (glError != Gl.GL_NO_ERROR)
      {
        throw new ApplicationException(msg);
      }
    }
  }
}
