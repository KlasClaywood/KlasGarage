using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects
{
    class Vehicle
    {
        public string Type { get; private set; }
        public string REG_NR { get; private set; }
        public string Color { get; private set; }
        public int NumberofWheels { get; private set; }
        public int ConstructionYear { get; private set; }

        public Vehicle(string type, string regnr, string color, int nowheels, int conyear)
        {
            Type = type;
            REG_NR = regnr;
            Color = color;
            NumberofWheels = nowheels;
            ConstructionYear = conyear;
        }
        public override string ToString()
        {
            return String.Format("{0, -11}  {1,-6} {2,-6} {3, 5} {4, 4}", Type, REG_NR, Color, NumberofWheels, ConstructionYear);
        }
    }
}
