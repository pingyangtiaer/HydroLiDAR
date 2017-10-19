using System;
using System.Collections.Generic;
using System.Text;
using las.datamanager;
using laslib;
using las.datamanager.structures;

namespace las.datamanager
{
  public class QTreeWrapper
  {
    public QTree qtree { get; set; }
    public string filename { get; set; }
    public LASFile lasFile { get; set; }

		//offsets for the data representation in las file coordinates - y is the depth coord
    public Point2f positionOffset { get; set; }
  }
}
