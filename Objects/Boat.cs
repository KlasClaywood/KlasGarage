using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects
{
    class Boat:Vehicle
    {
        public int Buoyancy { get; private set; }
        public int Length { get; private set; }
        public Boat (string regnr, string color, int nowheels, int conyear, int buoy, int len):base("Boat",regnr,color,nowheels,conyear)
        {
            Buoyancy = buoy;
            Length = len;
        }
        public override string ToString()
        {
            return base.ToString() + String.Format(" {0, 3} {1, 3}m", Buoyancy, Length);
        }
    }
}
