using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects
{
    class Car:LandVehicle
    {
        public double BaggageVolume { get; private set; }
        public string FuelType { get; private set; }
        public Car (string regnr, string color, int nowheels, int conyear, int miles, string license, double bagvol, string fuel) : base("Car",regnr, color, nowheels, conyear, miles, license)
        {
            BaggageVolume = bagvol;
            FuelType = fuel;
        }
        public override string ToString()
        {
            return base.ToString() + String.Format("    {0, 4:0.00}      {1, -9}", BaggageVolume, FuelType);
        }
    }
}
