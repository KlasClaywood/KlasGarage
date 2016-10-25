using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects
{
    class Buss:LandVehicle
    {
        public int NumberofSeats { get; private set; }
        public int Line { get; private set; }
        public Buss (string regnr, string color, int nowheels, int conyear, int miles, string license, int noseats, int line): base("Buss",regnr,color,nowheels,conyear,miles,license)
        {
            NumberofSeats = noseats;
            Line = line;
        }
        public override string ToString()
        {
            return base.ToString() + String.Format(" {0, 3} {1, 4}", NumberofSeats, Line);
        }
    }
}
