using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects
{
    class Airplane:Vehicle
    {
        public int MaxAltitude { get; private set; }
        public string AirLine { get; private set; }
        public Airplane(string regnr, string color, int nowheels, int conyear, int max, string airl):base("Airplane", regnr, color, nowheels, conyear)
        {
            MaxAltitude = max;
            AirLine = airl;
        }
        public override string ToString()
        {
            return base.ToString() + String.Format(" {0, 6} {1, -25}", MaxAltitude, AirLine);
        }
    }
}
