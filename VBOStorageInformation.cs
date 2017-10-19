using System;
using System.Collections.Generic;
using System.Text;

namespace las.datamanager.structures
{  
  /// <summary>
  /// contains location of server side VBOs
  /// </summary>
  public class VBOStorageInformation
  {
    public int VertexVBO = 0;
    public int NormalVBO = 0;
    public int ColorVBO = 0;

    public int InterleavedVBO = 0;

    public int NumberOfPoints = 0;
  }
}
