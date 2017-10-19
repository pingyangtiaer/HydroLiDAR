using System;
using System.Collections.Generic;
using System.Text;

namespace laslib
{
    [Serializable]
    enum DataMemberFlag
    {
        ReturnNumber = 1,
        NumberOfReturns = 2,
        ScanDirection = 4,
        FlightLineEdge = 8,
        Classification = 16,
        ScanAngleRank = 32,
        Time = 64
    };

    [Serializable]
    public class ClassificationType
    {
        public const int Created = 0;
        public const int Unclassified = 1;
        public const int Ground = 2;
        public const int LowVegetation = 3;
        public const int MediumVegetation = 4;
        public const int HighVegetation = 5;
        public const int Building = 6;
        public const int LowPoint = 7;
        public const int ModelKeyPoint = 8;
        public const int Water = 9;
        // = 10 // reserved for ASPRS Definition
        // = 11 // reserved for ASPRS Definition
        public const int OverlapPoints = 12;
        // = 13-31 // reserved for ASPRS Definition
    };
      

}
