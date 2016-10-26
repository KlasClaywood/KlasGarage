using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects
{
    class Motorcycle:LandVehicle
    {
        public string Brand { get; private set; }
        public string Category { get; private set; }
        public Motorcycle (string regnr, string color, int nowheels, int conyear, int miles, string license, string brand, string cat): base("Motorcycle", regnr,color, nowheels, conyear, miles, license)
        {
            Brand = brand;
            Category = cat;
        }
        public override string ToString()
        {
            return base.ToString() + String.Format(" {0} {1}", Brand, Category);
        }
    }
}
