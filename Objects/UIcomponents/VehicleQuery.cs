using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects.UIcomponents
{
    class VehicleQuery
    {
        public string Command { get; set; }
        public string Value { get; set; }

        public VehicleQuery(string com, string val)
        {
            Command = com;
            Value = val;
        }
    }
}
