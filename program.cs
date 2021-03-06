﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KlasGarage.Objects;
using KlasGarage.Objects.UIcomponents;
using System.IO;
using System.Xml.Linq;

namespace KlasGarage
{
    class program
    {
        public static void FillGarage(Garage<Vehicle> kallesGarage)
        {
            kallesGarage.Add(new Boat("RAA938", "Silver", 0, 1970, 14, 15));
            kallesGarage.Add(new Boat("RAB938", "Blue", 0, 1971, 13, 33));
            kallesGarage.Add(new Boat("RBA938", "White", 0, 1972, 10, 64));
            kallesGarage.Add(new Boat("RBB938", "Black", 0, 1973, 20, 13));
            kallesGarage.Add(new Airplane("AIRBIG", "White", 3, 2013, 20000, "Norwegian"));
            kallesGarage.Add(new Airplane("AIRMED", "Blue", 3, 2013, 20000, "SAS"));
            kallesGarage.Add(new Airplane("AIRSMA", "Red", 3, 2013, 20000, "American Airlines"));
            kallesGarage.Add(new Airplane("AIRTIN", "White", 3, 2013, 20000, "RyanAir"));
            kallesGarage.Add(new Buss("BUS123", "Blue", 8, 2000, 13000, "B+", 52, 208));
            kallesGarage.Add(new Buss("BUS124", "Blue", 8, 2000, 13000, "B+", 53, 708));
            kallesGarage.Add(new Buss("BUS125", "Blue", 8, 2000, 13000, "B+", 64, 608));
            kallesGarage.Add(new Buss("BUS126", "Tan", 8, 2000, 13000, "B+", 64, 508));
            kallesGarage.Add(new Buss("BUS127", "Blue", 8, 2000, 13000, "B+", 64, 408));
            kallesGarage.Add(new Buss("BUS128", "Blue", 8, 2000, 13000, "B+", 52, 308));
            kallesGarage.Add(new Buss("BUS129", "Blue", 8, 2000, 13000, "B+", 52, 211));
            kallesGarage.Add(new Car("XGC964", "Silver", 4, 2008, 32000, "B", 1.5, "FlexiFuel"));
            kallesGarage.Add(new Car("CMJ750", "Black", 4, 2003, 31000, "B", 1.2, "Diesel"));
            kallesGarage.Add(new Car("UJZ493", "Blue", 4, 2009, 30000, "B", 1.24, "Bensin"));
            kallesGarage.Add(new Car("AAB112", "Red", 4, 2014, 29000, "B", 1.1, "Gasoline"));
            kallesGarage.Add(new Car("ABB122", "Green", 4, 2005, 33000, "B", 1.9, "Guzzoline"));
            kallesGarage.Add(new Car("BBB222", "White", 4, 2016, 34000, "B", 0.8, "Electicity"));
            kallesGarage.Add(new Motorcycle("TOI532", "Black", 2, 1986, 8000, "C", "Harley-Davidsson", "Chopper"));
            kallesGarage.Add(new Motorcycle("TAI532", "Black", 2, 1995, 3000, "C", "Harley-Davidsson", "Chopper"));
            kallesGarage.Add(new Motorcycle("TEI532", "Black", 2, 1997, 14000, "C", "Harley-Davidsson", "Cruiser"));
            kallesGarage.Add(new Motorcycle("TUI532", "Black", 2, 1999, 2000, "C", "Harley-Davidsson", "Chopper"));
            kallesGarage.Add(new Motorcycle("TII532", "Black", 2, 1996, 1000, "C", "Harley-Davidsson", "Cruiser"));

        }
        public static void Main()
        {
            //Garage<Vehicle> GreatGarage = new Garage<Vehicle>("Klas Garage", 50);
            //FillGarage(GreatGarage);
            List<Garage<Vehicle>> AllGarages = LoadAllGarages();
            //List<Garage<Vehicle>> AllGarages = new List<Garage<Vehicle>>();
            //AllGarages.Add(GreatGarage);
            GarageUI garageui = new GarageUI(AllGarages);
            Console.WindowWidth = 90;
            
            
           
            while (garageui.RunMenu())
            {

            }
            
            /*
            foreach (Vehicle fordon in GreatGarage)
            {
                Console.WriteLine(fordon);
            }
            Console.ReadLine();
            var query = from vehicle in GreatGarage
                        where vehicle.Type == "Car"
                        orderby vehicle.REG_NR
                        select vehicle;
            foreach (Vehicle fordon in query)
            {
                Console.WriteLine(fordon);
            }*/

        }
        public static List<Garage<Vehicle>> LoadAllGarages()
        {
            List<Garage<Vehicle>> AllGarages = new List<Garage<Vehicle>>();
            var document = XDocument.Load("vehicles.xml");

            var garageQuery = from element in document.Element("Garages").Elements("Garage")
                              select element;
            foreach (var elem in garageQuery)
            {
                Garage<Vehicle> tmpGarage = new Garage<Vehicle>(elem.Attribute("Name").Value, int.Parse(elem.Attribute("Capacity").Value));
                var CarQuery = from element in elem.Elements("Car")                                   
                                   select element;
                var BussQuery = from element in elem.Elements("Buss")
                                select element;
                var AirplaneQuery = from element in elem.Elements("Airplane")
                                    select element;
                var MCQuery = from element in elem.Elements("Motorcycle")
                              select element;
                var BoatQuery = from element in elem.Elements("Boat")
                                select element;
                foreach (var element in CarQuery)
                {
                    
                    tmpGarage.Add(new Car(element.Element("REG_NR").Value
                                    , element.Element("Color").Value
                                    , int.Parse(element.Element("NumberofWheels").Value)
                                    , int.Parse(element.Element("ConstructionYear").Value)
                                    , int.Parse(element.Element("Mileage").Value)
                                    , element.Element("LicenseRequirement").Value
                                    , double.Parse(element.Element("BaggageVolume").Value.Replace('.', ','))
                                    , element.Element("FuelType").Value));
                }
                foreach (var element in BussQuery)
                {
                    tmpGarage.Add(new Buss(element.Element("REG_NR").Value
                                    , element.Element("Color").Value
                                    , int.Parse(element.Element("NumberofWheels").Value)
                                    , int.Parse(element.Element("ConstructionYear").Value)
                                    , int.Parse(element.Element("Mileage").Value)
                                    , element.Element("LicenseRequirement").Value
                                    , int.Parse(element.Element("NumberofSeats").Value)
                                    , int.Parse(element.Element("Line").Value)));
                }
                foreach (var element in AirplaneQuery)
                {
                    tmpGarage.Add(new Airplane(element.Element("REG_NR").Value
                                    , element.Element("Color").Value
                                    , int.Parse(element.Element("NumberofWheels").Value)
                                    , int.Parse(element.Element("ConstructionYear").Value)
                                    , int.Parse(element.Element("MaxAltitude").Value)
                                    , element.Element("AirLine").Value));
                }
                foreach (var element in MCQuery)
                {
                    tmpGarage.Add(new Motorcycle(element.Element("REG_NR").Value
                                    , element.Element("Color").Value
                                    , int.Parse(element.Element("NumberofWheels").Value)
                                    , int.Parse(element.Element("ConstructionYear").Value)
                                    , int.Parse(element.Element("Mileage").Value)
                                    , element.Element("LicenseRequirement").Value
                                    , element.Element("Brand").Value
                                    , element.Element("Category").Value));
                }
                foreach (var element in BoatQuery)
                {
                    tmpGarage.Add(new Boat(element.Element("REG_NR").Value
                                    , element.Element("Color").Value
                                    , int.Parse(element.Element("NumberofWheels").Value)
                                    , int.Parse(element.Element("ConstructionYear").Value)                                    
                                    , int.Parse(element.Element("Buoyancy").Value)
                                    , int.Parse(element.Element("Length").Value)));
                }
                AllGarages.Add(tmpGarage);
                
            }
            return AllGarages;
        }
        public static void GarageUI(Garage<Vehicle> garage)
        {

        }
    }
}
