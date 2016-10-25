using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects
{
    class LandVehicle:Vehicle
    {
        public int Mileage { get; set; }
        public string LicenseRequirement { get; private set; }
        public LandVehicle (string type,string regnr, string color, int nowheels, int conyear, int miles, string license): base (type, regnr, color, nowheels, conyear)
        {
            Mileage = miles;
            LicenseRequirement = license;
        }
        public override string ToString()
        {
            return base.ToString() + String.Format(" {0, 9} {1, -3}", Mileage, LicenseRequirement);
        }
    }
}
