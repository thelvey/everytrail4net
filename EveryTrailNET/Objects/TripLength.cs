using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveryTrailNET.Objects
{
    public class Length
    {
        public enum LengthUnits{Metric, Standard};
        public LengthUnits Units { get; set; }
        public double Amount { get; set; }
    }
}
