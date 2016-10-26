using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq; 

namespace KlasGarage.Objects.UIcomponents
{
    class GarageUI
    {
        private string[] arguments;
        private List<UIItem> kommandolista;
        private List<Garage<Vehicle>> garages;
        private Garage<Vehicle> garage;
        private List<UIItem> activeMenu;
        private UIItem active;
        private int activeindex;

        public GarageUI(List<Garage<Vehicle>> gar)
        {
            arguments = new string[0];
            garages = gar;
            
            garage = garages.First();
            kommandolista = new List<UIItem>();
            kommandolista.Add(new UIItem("Lista fordon", "Huvudmeny", ListaFordon, 1));
            kommandolista.Add(new UIItem("Skapa ett fordon", "Huvudmeny", SkapaFordon, 2));
            kommandolista.Add(new UIItem("Ta bort ett fordon", "Huvudmeny", TaBortFordon, 3));
            kommandolista.Add(new UIItem("Byt garage", "Huvudmeny", BytGarage, 3));
            kommandolista.Add(new UIItem("Sök på Regnr", "Huvudmeny", SokPaRegnr, 4));
            kommandolista.Add(new UIItem("Sök på olika variabler", "Huvudmeny", SokPaOlikaVariabler, 5));
            kommandolista.Add(new UIItem("Spara till hårddisken", "Huvudmeny", SparaTillDisk, 6));
            kommandolista.Add(new UIItem("Skapa bil", "Skapa fordon", SkapaBil, 1));
            kommandolista.Add(new UIItem("Skapa båt", "Skapa fordon", SkapaBat, 2));
            kommandolista.Add(new UIItem("Skapa buss", "Skapa fordon", SkapaBuss, 3));
            kommandolista.Add(new UIItem("Skapa motorcykel", "Skapa fordon", SkapaMC, 4));
            kommandolista.Add(new UIItem("Skapa flygplan", "Skapa fordon", SkapaFlygplan, 5));
            kommandolista.Add(new UIItem("Skapa nytt garage", "Byt garage", SkapaGarage, 1));
            kommandolista.Add(new UIItem("Välj garage", "Byt garage", ValjGarage, 2));
            kommandolista.Add(new UIItem("REG_NR", "Sök på olika variabler"," ", SokFras, 1));
            kommandolista.Add(new UIItem("Color", "Sök på olika variabler", " ", SokFras, 1));
            kommandolista.Add(new UIItem("Construction year", "Sök på olika variabler", " ", SokFras, 1));
            kommandolista.Add(new UIItem("Number of wheels", "Sök på olika variabler", " ", SokFras, 1));
            kommandolista.Add(new UIItem("Type", "Sök på olika variabler", " ", SokFras, 1));
            kommandolista.Add(new UIItem("Mileage", "Sök på olika variabler", " ", SokFras, 1));
            kommandolista.Add(new UIItem("License Requirement", "Sök på olika variabler", " ", SokFras, 1));
            kommandolista.Add(new UIItem("Sök", "Sök på olika variabler", SokMig, 2));
            var query = from item in kommandolista
                         where item.Category == "Huvudmeny"
                         orderby item.ID
                         select item;
            activeMenu = query.ToList<UIItem>();
                
            active = activeMenu.ElementAt(0);
            
            activeindex = 0;
        }

       

        

        private void SparaTillDisk()
        {
            var document = new XDocument();
            var allagarage = new XElement("Garages");
            foreach (var gar in garages)
            {
                var garra = new XElement("Garage");
                garra.Add(new XAttribute("Name", gar.Name));
                garra.Add(new XAttribute("Capacity", gar.Max));
                foreach (var fordon in gar)
                {
                    var reg = new XElement("REG_NR", fordon.REG_NR);
                    var col = new XElement("Color", fordon.Color);
                    var nowhels = new XElement("NumberofWheels", fordon.NumberofWheels);
                    var conyear = new XElement("ConstructionYear", fordon.ConstructionYear);
                    if (fordon is LandVehicle)
                    {
                        LandVehicle landvehicle = fordon as LandVehicle;
                        var mil = new XElement("Mileage", landvehicle.Mileage);
                        var lic = new XElement("LicenseRequirement", landvehicle.LicenseRequirement);
                        if (fordon is Buss)
                        {
                            Buss buss = fordon as Buss;
                            var noseats = new XElement("NumberofSeats", buss.NumberofSeats);
                            var line = new XElement("Line", buss.Line);
                            garra.Add(new XElement(fordon.Type, reg, col, nowhels, conyear, mil, lic, noseats, line));
                        }
                        else if (fordon is Car)
                        {
                            Car car = fordon as Car;
                            var bagvol = new XElement("BaggageVolume", car.BaggageVolume);
                            var fuel = new XElement("FuelType", car.FuelType);
                            garra.Add(new XElement(fordon.Type, reg, col, nowhels, conyear, mil, lic, bagvol, fuel));
                        }
                        else if (fordon is Motorcycle)
                        {
                            Motorcycle motorcycle = fordon as Motorcycle;
                            var brand = new XElement("Brand", motorcycle.Brand);
                            var cat = new XElement("Category", motorcycle.Category);
                            garra.Add(new XElement(fordon.Type, reg, col, nowhels, conyear, mil, lic, brand, cat));
                        }
                    }
                    else if (fordon is Boat)
                    {
                        Boat boat = fordon as Boat;
                        var buoy = new XElement("Buoyancy", boat.Buoyancy);
                        var len = new XElement("Length", boat.Length);
                        garra.Add(new XElement(fordon.Type, reg, col, nowhels, conyear, buoy, len));
                    }
                    else if(fordon is Airplane)
                    {
                        Airplane airplane = fordon as Airplane;
                        var max = new XElement("MaxAltitude", airplane.MaxAltitude);
                        var line = new XElement("AirLine", airplane.AirLine);
                        garra.Add(new XElement(fordon.Type, reg, col, nowhels, conyear, max, line));
                    }
                    
                }
                allagarage.Add(garra);
            }
            document.Add(allagarage);
            document.Save("vehicles.xml");
        }

        private void TaBortFordon()
        {
            List<UIItem> tempmeny = new List<UIItem>();
            foreach (Vehicle fordon in garage)
            {
                tempmeny.Add(new UIItem(fordon.Type + ":" + fordon.REG_NR, "TaBortMig", TaBortMig, 0));
            }
            activeMenu = tempmeny;
            active = activeMenu.First();
            activeindex = 0;
        }

        private void TaBortMig()
        {
            string name = active.Command.Split(':').First();
            string reg = active.Command.Split(':').Last();
            Vehicle bort = garage.Where(b => b.Type == name && b.REG_NR == reg).First();

            if (!garage.Remove(bort))
            {
                Console.WriteLine("Could not remove {0} {1}", name, reg);

            }
            else
            {
                Console.WriteLine("Are you sure you want to remove {0} {1}? Type 'REMOVE' to continue");
                string svar = Console.ReadLine();
                if (svar == "REMOVE")
                {
                    Console.WriteLine("{0} {1} has been removed!", name, reg);
                }
                else
                {
                    garage.Add(bort);
                }
            }
                Console.ReadKey();
            SetToMainMenu();
        }

        private void ValjGarage()
        {
            List<UIItem> tempmeny = new List<UIItem>();
            for (int i = 0; i < garages.Count(); i++)
            {
                tempmeny.Add(new UIItem(garages.ElementAt(i).Name, "Alla garage", AllaGarage, i + 1));
            }
            activeMenu = tempmeny;
            activeindex = 0;
            active = activeMenu.First();
        }

        private void AllaGarage()
        {
            var query = from garra in garages
                        where garra.Name == active.Command
                        select garra;
            garage = query.First();
            SetToMainMenu();
        }

        private void SkapaGarage()
        {
            Console.WriteLine("Input new garage name:");
            string name = Console.ReadLine();
            Console.WriteLine("Input new garage capacity:");
            int max = 0;
            while (!int.TryParse(Console.ReadLine(), out max))
            { 
                Console.Clear();
                Console.WriteLine("Input an integer");
            }
            garage = new Garage<Vehicle>(name, max);
            garages.Add(garage);
            SetToMainMenu();
        }

        private void SkapaFlygplan()
        {
            Vehicle temp = SkapaGenerisktFordon("Airplane", 3);
            Console.WriteLine("Input maximum altitude:");
            int max = 0;
            while (!int.TryParse(Console.ReadLine(), out max))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("Input an integer!");
            }
            Console.WriteLine("Input airline name:");
            string airl = Console.ReadLine();

            garage.Add(new Airplane(temp.REG_NR, temp.Color, temp.NumberofWheels, temp.ConstructionYear, max, airl));
            SetToMainMenu();
        }

        private void SetToMainMenu()
        {
            var query = from item in kommandolista
                        where item.Category == "Huvudmeny"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();
            active = activeMenu.ElementAt(0);
            activeindex = 0;
        }

        private void SkapaMC()
        {
            LandVehicle temp = SkapaLandfordon("Motorcycle", 2);
            Console.WriteLine("Input brand:");
            string brand = Console.ReadLine();
            Console.WriteLine("Input category of bike:");
            string cat = Console.ReadLine();
            garage.Add(new Motorcycle(temp.REG_NR, temp.Color, temp.NumberofWheels, temp.ConstructionYear, temp.Mileage, temp.LicenseRequirement, brand, cat));
            SetToMainMenu();
        }

        private void SkapaBuss()
        {
            LandVehicle temp = SkapaLandfordon("Buss", 8);
            Console.WriteLine("Input number of seats:");
            int noseats = 0;
            while (!int.TryParse(Console.ReadLine(), out noseats))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("Input an integer!");
            }
            Console.WriteLine("Input line number:");
            int line = 0;
            while (!int.TryParse(Console.ReadLine(), out line))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("Input an integer!");
            }
            garage.Add(new Buss(temp.REG_NR, temp.Color, temp.NumberofWheels, temp.ConstructionYear, temp.Mileage, temp.LicenseRequirement, noseats, line));
            SetToMainMenu();
        }

        private LandVehicle SkapaLandfordon(string type, int nowheels)
        {
            Vehicle temp = SkapaGenerisktFordon(type, nowheels);
            Console.WriteLine("Input mileage:");
            int miles = 0;
            while (!int.TryParse(Console.ReadLine(), out miles))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("Input an integer!");
            }
            Console.WriteLine("Input driver license requirement:");
            string license = Console.ReadLine();
            return new LandVehicle(type, temp.REG_NR, temp.Color, nowheels, temp.ConstructionYear, miles, license);
        }

        private Vehicle SkapaGenerisktFordon(string type, int nowheels)
        {
            Console.WriteLine("Input reg. no:");
            string regnr = Console.ReadLine();
            Console.WriteLine("Input color:");
            string color = Console.ReadLine();
            Console.WriteLine("Input construction year:");
            int conyear = 0;
            while (!int.TryParse(Console.ReadLine(), out conyear))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("Input an integer!");
            }
            return new Vehicle(type, regnr, color, nowheels, conyear);
        }

        private void SkapaBat()
        {
            Vehicle temp = SkapaGenerisktFordon("Boat", 0);
            Console.WriteLine("Input buoyancy:");
            int buoy = 0;
            while (!int.TryParse(Console.ReadLine(), out buoy))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("Input an integer!");
            }
            Console.WriteLine("Input length:");
            int len = 0;
            while (!int.TryParse(Console.ReadLine(), out len))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("Input an integer!");
            }
            garage.Add(new Boat(temp.REG_NR, temp.Color, temp.NumberofWheels, temp.ConstructionYear, buoy, len));
            SetToMainMenu();
        }

        private void SkapaBil()
        {
            LandVehicle temp = SkapaLandfordon("Car", 4);
            Console.WriteLine("Input baggage volume:");
            double bagvol = 0;
            while (double.TryParse(Console.ReadLine(), out bagvol))
            {
                Console.SetCursorPosition(0, Console.CursorTop -1);
                Console.WriteLine("Input a double!");
            }
            Console.WriteLine("Input fuel type:");
            string fuel = Console.ReadLine();
            garage.Add(new Car(temp.REG_NR, temp.Color, temp.NumberofWheels, temp.ConstructionYear,temp.Mileage, temp.LicenseRequirement, bagvol, fuel));
            SetToMainMenu();
        }


        public bool RunMenu ()
        {
            Console.Clear();
            bool avsluta = false;
            foreach (string argument in arguments)
            {
                Console.WriteLine(argument);
            }
            foreach (UIItem punkt in activeMenu)
            {
                if (punkt == active)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(punkt.Command + ((punkt.Additional != "")? ": " + punkt.Additional : ""));
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(punkt.Command + ((punkt.Additional != "") ? ": " + punkt.Additional : ""));
                }
            }
            ConsoleKeyInfo svar = Console.ReadKey();
            switch (svar.Key)
            {
                case ConsoleKey.UpArrow:
                    if (activeindex > 0)
                    {
                        activeindex -= 1;  
                    }
                    else
                    {
                        activeindex = activeMenu.Count - 1;
                    }
                    active = activeMenu.ElementAt(activeindex);
                    break;
                case ConsoleKey.DownArrow:
                    if (activeindex < activeMenu.Count - 1)
                    {
                        activeindex += 1;  
                    }
                    else
                    {
                        activeindex = 0;
                    }
                    active = activeMenu.ElementAt(activeindex);
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    active.Invoke();
                    //avsluta = true;
                    break;
                case ConsoleKey.Escape:
                    avsluta = true;
                    break;
                default:
                    break;
            }
            return !avsluta;
        }



        public void ListaFordon()
        {
            var fordonlista = garage.OrderBy(f => f.Type).ToList<Vehicle>();
            
            PrintaFordon(fordonlista);
        }

        public void PrintaFordon(List<Vehicle> fordonlista)
        {
            Console.Clear();
            Vehicle tmp = null;
            foreach (Vehicle fordon in fordonlista)
            {
                if (tmp == null || fordon.Type != tmp.Type)
                {
                    PrintHeader(fordon.Type);
                    tmp = fordon;
                }
                Console.WriteLine(fordon);
            }
            Console.ReadKey();
        }

        private void PrintHeader(string type)
        {
            switch (type)
            {
                case "Car":
                    Console.WriteLine();
                    Console.WriteLine("{0, -12} {1,-6} {2,-6} {3, 5} {4, 4} {5, 6} {6, 7} {7, -9}    {8, -6}", "Type", "REG_NR", "Color", "Wheel", "Year", "Miles", "L. Req.", "Baggage", "Fuel");
                    Console.WriteLine("==============================================================================");
                    break;
                case "Buss":
                    
                    Console.WriteLine();
                    Console.WriteLine("{0, -12} {1,-6} {2,-6} {3, 5} {4, 4} {5, 6} {6, 7} {7, -5}    {8, -4}", "Type", "REG_NR", "Color", "Wheel", "Year", "Miles", "L. Req.", "Seats", "Line");
                    Console.WriteLine("==============================================================================");
                    
                    break;
                case "Airplane":
                    
                    Console.WriteLine();
                    Console.WriteLine("{0, -12} {1,-6} {2,-6} {3, 5} {4, 4} {5, 8} {6, 7} ", "Type", "REG_NR", "Color", "Wheel", "Year", "Altitude", "Airline");
                    Console.WriteLine("==============================================================================");
                    
                    break;
                case "Motorcycle":
                    
                    Console.WriteLine();
                    Console.WriteLine("{0, -12} {1,-6} {2,-6} {3, 5} {4, 4} {5, 6} {6, 7} {7, -16} {8, -9}", "Type", "REG_NR", "Color", "Wheel", "Year", "Miles", "License", "Brand", "Category");
                    Console.WriteLine("==============================================================================");
                    
                    break;
                case "Boat":
                    
                    Console.WriteLine();
                    Console.WriteLine("{0, -12} {1,-6} {2,-6} {3, 5} {4, 4} {5, 9} {6, 7}", "Type", "REG_NR", "Color", "Wheel", "Year", "Buoyancy", "Length");
                    Console.WriteLine("==============================================================================");
                    
                    break;
                default:
                    break;
            }
        }

        public void SkapaFordon()
        {
            var query = from item in kommandolista
                        where item.Category == "Skapa fordon"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();
            active = activeMenu.First();
            activeindex = 0;
        }

        public void BytGarage()
        {
            var query = from item in kommandolista
                        where item.Category == "Byt garage"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();
            active = activeMenu.First();
            activeindex = 0;
        }

        public void SokPaRegnr()
        {
            string reg = Console.ReadLine();
            var query = garage.Where(b => b.REG_NR.Contains(reg)).OrderBy(b => b.Type).ToList<Vehicle>();
            PrintaFordon(query);
        }

        public void SokPaOlikaVariabler()
        {
            var query = from item in kommandolista
                        where item.Category == "Sök på olika variabler"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();
            active = activeMenu.First();
            activeindex = 0;
        }

        private void SokFras()
        {
            active.Additional = Console.ReadLine();
        }

        private void SokMig()
        {
            var query = from item in kommandolista
                        where item.Category == "Sök på olika variabler" && item.Additional != " "
                        orderby item.ID
                        select item;
            List<UIItem> sokFrasLista = query.ToList<UIItem>();
            var result = from item in garage
                         select item;
            foreach (var item in sokFrasLista)
            {
                switch (item.Command)
                {
                    case "REG_NR":

                }
            }

        }
    }
}
