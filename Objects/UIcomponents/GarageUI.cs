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
            kommandolista.Add(new UIItem("List vehicles", "Huvudmeny", ListaVehicle, 0));
            kommandolista.Add(new UIItem("List types of vehicle", "Huvudmeny", ListaTyper, 1));
            kommandolista.Add(new UIItem("Create new vehicle", "Huvudmeny", FyllTyp, 2));
            kommandolista.Add(new UIItem("Remove vehicle", "Huvudmeny", TaBortVehicle, 3));
            kommandolista.Add(new UIItem("Change garage", "Huvudmeny", BytGarage, 3));
            kommandolista.Add(new UIItem("Search on Regnr", "Huvudmeny", SokPaRegnr, 4));
            kommandolista.Add(new UIItem("Search on different variables", "Huvudmeny", SokPaOlikaVariabler, 5));
            kommandolista.Add(new UIItem("Save garages to disc", "Huvudmeny", SparaTillDisk, 6));
            kommandolista.Add(new UIItem("Exit", "Huvudmeny", SparaTillDisk, 7));
            kommandolista.Add(new UIItem("Return to main menu", "Return", SetToMainMenu, 100));

            kommandolista.Add(new UIItem("Skapa bil", "Skapa vehicle", SkapaBil, 1));
            kommandolista.Add(new UIItem("Skapa båt", "Skapa vehicle", SkapaBat, 2));
            kommandolista.Add(new UIItem("Skapa buss", "Skapa vehicle", SkapaBuss, 3));
            kommandolista.Add(new UIItem("Skapa motorcykel", "Skapa vehicle", SkapaMC, 4));
            kommandolista.Add(new UIItem("Skapa flygplan", "Skapa vehicle", SkapaFlygplan, 5));

            kommandolista.Add(new UIItem("Create new garage", "Byt garage", SkapaGarage, 1));
            kommandolista.Add(new UIItem("Choose garage", "Byt garage", ValjGarage, 2));

            kommandolista.Add(new UIItem("REG_NR", "Sök på olika variabler"," ","string", SokFras, 1));
            kommandolista.Add(new UIItem("Color", "Sök på olika variabler", " ", "string", SokFras, 1));
            kommandolista.Add(new UIItem("Construction year", "Sök på olika variabler", " ", "int", SokFras, 1));
            kommandolista.Add(new UIItem("Number of wheels", "Sök på olika variabler", " ", "int", SokFras, 1));
            kommandolista.Add(new UIItem("Type", "Sök på olika variabler", " ","string", FyllTypSok, 0));
            kommandolista.Add(new UIItem("Mileage", "Sök på olika variabler", " ","int", SokFras, 1));
            kommandolista.Add(new UIItem("License requirement", "Sök på olika variabler", " ", "string", SokFras, 1));
            kommandolista.Add(new UIItem("Search", "Sök på olika variabler", SokMig, 2));

            kommandolista.Add(new UIItem("Type", "Create Vehicle", " ", "string", FyllTyp, 1));
            kommandolista.Add(new UIItem("REG_NR", "Create Vehicle", " ", "string", Fyll, 2));
            kommandolista.Add(new UIItem("Color", "Create Vehicle", " ", "string", Fyll, 3));
            kommandolista.Add(new UIItem("Number of wheels", "Create Vehicle", " ", "int", Fyll, 4));
            kommandolista.Add(new UIItem("Construction year", "Create Vehicle", " ", "int", Fyll, 5));

            kommandolista.Add(new UIItem("Mileage", "Create LandVehicle", " ", "int", Fyll, 6));
            kommandolista.Add(new UIItem("License requirement", "Create LandVehicle", " ", "string", Fyll, 7));

            kommandolista.Add(new UIItem("Brand", "Create Motorcycle", " ", "string", Fyll, 8));
            kommandolista.Add(new UIItem("Category", "Create Motorcycle", " ", "string", Fyll, 9));

            kommandolista.Add(new UIItem("Baggage volume", "Create Car", " ", "double", Fyll, 10));
            kommandolista.Add(new UIItem("Fuel type", "Create Car", " ", "string", Fyll, 11));

            kommandolista.Add(new UIItem("Number of seats", "Create Buss", " ", "int", Fyll, 12));
            kommandolista.Add(new UIItem("Line", "Create Buss", " ", "int", Fyll, 13));

            kommandolista.Add(new UIItem("Buoyancy", "Create Boat", " ", "int", Fyll, 14));
            kommandolista.Add(new UIItem("Length", "Create Boat", " ", "int", Fyll, 15));

            kommandolista.Add(new UIItem("Maximum altitude", "Create Airplane", " ", "int", Fyll, 16));
            kommandolista.Add(new UIItem("Airline", "Create Airplane", " ", "int", Fyll, 17));

            kommandolista.Add(new UIItem("Finish", "Create Vehicle", Create, 18));

            kommandolista.Add(new UIItem("Car", "Fill Type", CreateCar, 1));
            kommandolista.Add(new UIItem("Buss", "Fill Type", CreateBuss, 2));
            kommandolista.Add(new UIItem("Motorcycle", "Fill Type", CreateMC, 3));
            kommandolista.Add(new UIItem("Boat", "Fill Type", CreateBoat, 4));
            kommandolista.Add(new UIItem("Airplane", "Fill Type", CreateAirplane, 5));

            kommandolista.Add(new UIItem("Car", "Search Type", SokPaOlikaVariabler, 1));
            kommandolista.Add(new UIItem("Buss", "Search Type", SokPaOlikaVariabler, 2));
            kommandolista.Add(new UIItem("Motorcycle", "Search Type", SokPaOlikaVariabler, 3));
            kommandolista.Add(new UIItem("Boat", "Search Type", SokPaOlikaVariabler, 4));
            kommandolista.Add(new UIItem("Airplane", "Search Type", SokPaOlikaVariabler, 5));
            var query = from item in kommandolista
                         where item.Category == "Huvudmeny"
                         orderby item.ID
                         select item;
            activeMenu = query.ToList<UIItem>();
                
            active = activeMenu.ElementAt(0);
            
            activeindex = 0;
        }

        private void FyllTypSok()
        {
            var query = from item in kommandolista
                        where item.Category == "Search Type" || item.Category == "Return"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();

            active = activeMenu.ElementAt(0);

            activeindex = 0;
        }

        private void ListaTyper2()
        {
            List<UIItem> nyMeny = new List<UIItem>();
            var types = (from vehicle in garage
                         group vehicle by vehicle.Type into typ                         
                         select typ.Key).ToArray();
            var antal = (from vehicle in garage
                         group vehicle by vehicle.Type into ant
                         select ant.Count()).ToArray();
            for (int i = 0; i < types.Length; i++ )
            {
                nyMeny.Add(new UIItem(types[i], "Type List", antal[i].ToString(), ShowType, i));
            }
            nyMeny.Add(new UIItem("Return to main menu", "Type List", SetToMainMenu, types.Count()));
            activeMenu = nyMeny;
            active = activeMenu.First();
            activeindex = 0;
        }
        private void ListaTyper()
        {
            List<UIItem> nyMeny = new List<UIItem>();
            var types = from vehicle in garage
                        group vehicle by vehicle.Type into type
                        select type;
            foreach (var type in types)
            {
                nyMeny.Add(new UIItem(type.Key, "Type List", type.Count().ToString(), ShowType, 1));
            }
            nyMeny.Add(new UIItem("Return to main menu", "Type List", SetToMainMenu, types.Count()));
            activeMenu = nyMeny;
            active = activeMenu.First();
            activeindex = 0;
        }

        private void ShowType()
        {
            var vehiclelista = garage.Where(f => f.Type == active.Command).OrderBy(f => f.REG_NR).ToList<Vehicle>();

            PrintaVehicle(vehiclelista);
            SetToMainMenu();
        }

        private void CreateAirplane()
        {
            UIItem typ = kommandolista.Where(item => item.Command == "Type" && item.Category == "Create Vehicle")
                //.Select (item => item)
                //.ToArray()
                                      .First();
            typ.Additional = "Airplane";
            var query = from item in kommandolista
                        where item.Category == "Create Airplane" || item.Category == "Create Vehicle" || item.Category == "Return"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();

            active = activeMenu.ElementAt(0);

            activeindex = 0;
        }

        private void CreateBoat()
        {
            UIItem typ = kommandolista.Where(item => item.Command == "Type" && item.Category == "Create Vehicle")
                //.Select (item => item)
                //.ToArray()
                                      .First();
            typ.Additional = "Boat";
            var query = from item in kommandolista
                        where item.Category == "Create Boat" || item.Category == "Create Vehicle" || item.Category == "Return"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();

            active = activeMenu.ElementAt(0);

            activeindex = 0;
        }

        private void CreateMC()
        {
            UIItem typ = kommandolista.Where(item => item.Command == "Type" && item.Category == "Create Vehicle")
                //.Select (item => item)
                //.ToArray()
                                      .First();
            typ.Additional = "Motorcycle";
            var query = from item in kommandolista
                        where item.Category == "Create Motorcycle" || item.Category == "Create LandVehicle" || item.Category == "Create Vehicle" || item.Category == "Return"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();

            active = activeMenu.ElementAt(0);

            activeindex = 0;
        }

        private void CreateBuss()
        {
            UIItem typ = kommandolista.Where(item => item.Command == "Type" && item.Category == "Create Vehicle")
                //.Select (item => item)
                //.ToArray()
                                      .First();
            typ.Additional = "Buss";
            var query = from item in kommandolista
                        where item.Category == "Create Buss" || item.Category == "Create LandVehicle" || item.Category == "Create Vehicle" || item.Category == "Return"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();

            active = activeMenu.ElementAt(0);

            activeindex = 0;
        }

        private void CreateCar()
        {
            UIItem typ = kommandolista.Where(item => item.Command == "Type" && item.Category == "Create Vehicle")
                                      //.Select (item => item)
                                      //.ToArray()
                                      .First();
            typ.Additional = "Car";

            var query = from item in kommandolista
                        where item.Category == "Create Car" || item.Category == "Create LandVehicle" || item.Category == "Create Vehicle" || item.Category == "Return"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();

            active = activeMenu.ElementAt(0);

            activeindex = 0;
        }

        private void FyllTyp()
        {
            var query = from item in kommandolista
                        where item.Category == "Fill Type" || item.Category == "Return"
                        orderby item.ID
                        select item;
            activeMenu = query.ToList<UIItem>();

            active = activeMenu.ElementAt(0);

            activeindex = 0;
        }

        private void Create()
        {
            string type = activeMenu.Where(item => item.Command == "Type" 
                                                && item.Category == "Create Vehicle")    
                                    .ToArray()
                                    .First()
                                    .Additional;
            string[] vehicleValues = kommandolista.Where(item => item.Category == "Create Vehicle")
                                               .OrderBy(item => item.ID)
                                               .Select(item => item.Additional)
                                               .ToArray();
            string reg = (vehicleValues[1] == " ")? "AAA000" : vehicleValues[1];
            string col = (vehicleValues[2] == " ")? "Colorless" : vehicleValues[2];
            int nowhels;
            if (vehicleValues[3] == " ")
            {
                switch (type)
                {
                    case "Car":
                        nowhels = 4;
                        break;
                    case "Buss":
                        nowhels = 6;
                        break;
                    case "Motorcycle":
                        nowhels = 2;
                        break;
                    case "Boat":
                        nowhels = 0;
                        break;
                    case "Airplane":
                        nowhels = 3;
                        break;
                    default:
                        nowhels = 0;
                        break;
                }
            }
            else
            {
                nowhels = int.Parse(vehicleValues[3]);
            }
            int conyear = (vehicleValues[4] == " ")? 0 : int.Parse(vehicleValues[4]);
            
            string [] landVehicleValues = kommandolista.Where(item => item.Category == "Create LandVehicle")
                                                    .OrderBy(item => item.ID)
                                                    .Select(item => item.Additional)
                                                    .ToArray();
            int miles = (landVehicleValues[0] == " ")? 0 : int.Parse(landVehicleValues[0]);
            string lic = (landVehicleValues[1] == " ")? "A" : landVehicleValues[1];
            
            string [] carValues = kommandolista.Where(item => item.Category == "Create Car")
                                               .OrderBy(item => item.ID)
                                               .Select(item => item.Additional)
                                               .ToArray();
            double bagvol = (carValues[0] == " ")? 0: double.Parse(carValues[0]);
            string fuel = (carValues[1] == " ")? "None" : carValues[1];

            string [] bussValues = kommandolista.Where(item => item.Category == "Create Buss")
                                                .OrderBy(item => item.ID)
                                                .Select(item => item.Additional)
                                                .ToArray();

            int noseats = (bussValues[0] == " ")? 0: int.Parse(bussValues[0]);
            int line = (bussValues[1] == " ")? 0: int.Parse(bussValues[1]);

            string [] mcValues = kommandolista.Where(item => item.Category == "Create Motorcycle")
                                                .OrderBy(item => item.ID)
                                                .Select(item => item.Additional)
                                                .ToArray();
            string brand =(mcValues[0] == " ")? "Brandless" : mcValues[0];
            string cat = (mcValues[1] == " ")? "N/A" : mcValues[1];

            string [] boatValues = kommandolista.Where(item => item.Category == "Create Boat")
                                                .OrderBy(item => item.ID)
                                                .Select(item => item.Additional)
                                                .ToArray();
            int buoy = (boatValues[0] == " " )? 0 : int.Parse(boatValues[0]);
            int len = (boatValues[1] == " " )? 0 : int.Parse(boatValues[1]);

            string [] airplaneValues = kommandolista.Where(item => item.Category == "Create Airplane")
                                                .OrderBy(item => item.ID)
                                                .Select(item => item.Additional)
                                                .ToArray();
            int max = (airplaneValues[0] == " ")? 0: int.Parse(airplaneValues[0]);
            string airline = (airplaneValues[1] == " ")? "N/A": airplaneValues[1];

            try
            {
                switch (type)
                {
                    case "Car":
                        garage.Add(new Car(reg, col, nowhels, conyear, miles, lic, bagvol, fuel));
                        break;
                    case "Buss":
                        garage.Add(new Buss(reg, col, nowhels, conyear, miles, lic, noseats, line));
                        break;
                    case "Motorcycle":
                        garage.Add(new Motorcycle(reg, col, nowhels, conyear, miles, lic, brand, cat));
                        break;
                    case "Boat":
                        garage.Add(new Boat(reg, col, nowhels, conyear, buoy, len));
                        break;
                    case "Airplane":
                        garage.Add(new Airplane(reg, col, nowhels, conyear, max, airline));
                        break;
                    default:
                        break;
                }
                Console.Clear();
                Console.WriteLine("New {0} added.", type);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            
            SetToMainMenu();
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
                foreach (var vehicle in gar)
                {
                    var reg = new XElement("REG_NR", vehicle.REG_NR);
                    var col = new XElement("Color", vehicle.Color);
                    var nowhels = new XElement("NumberofWheels", vehicle.NumberofWheels);
                    var conyear = new XElement("ConstructionYear", vehicle.ConstructionYear);
                    if (vehicle is LandVehicle)
                    {
                        LandVehicle landvehicle = vehicle as LandVehicle;
                        var mil = new XElement("Mileage", landvehicle.Mileage);
                        var lic = new XElement("LicenseRequirement", landvehicle.LicenseRequirement);
                        if (vehicle is Buss)
                        {
                            Buss buss = vehicle as Buss;
                            var noseats = new XElement("NumberofSeats", buss.NumberofSeats);
                            var line = new XElement("Line", buss.Line);
                            garra.Add(new XElement(vehicle.Type, reg, col, nowhels, conyear, mil, lic, noseats, line));
                        }
                        else if (vehicle is Car)
                        {
                            Car car = vehicle as Car;
                            var bagvol = new XElement("BaggageVolume", car.BaggageVolume);
                            var fuel = new XElement("FuelType", car.FuelType);
                            garra.Add(new XElement(vehicle.Type, reg, col, nowhels, conyear, mil, lic, bagvol, fuel));
                        }
                        else if (vehicle is Motorcycle)
                        {
                            Motorcycle motorcycle = vehicle as Motorcycle;
                            var brand = new XElement("Brand", motorcycle.Brand);
                            var cat = new XElement("Category", motorcycle.Category);
                            garra.Add(new XElement(vehicle.Type, reg, col, nowhels, conyear, mil, lic, brand, cat));
                        }
                    }
                    else if (vehicle is Boat)
                    {
                        Boat boat = vehicle as Boat;
                        var buoy = new XElement("Buoyancy", boat.Buoyancy);
                        var len = new XElement("Length", boat.Length);
                        garra.Add(new XElement(vehicle.Type, reg, col, nowhels, conyear, buoy, len));
                    }
                    else if(vehicle is Airplane)
                    {
                        Airplane airplane = vehicle as Airplane;
                        var max = new XElement("MaxAltitude", airplane.MaxAltitude);
                        var line = new XElement("AirLine", airplane.AirLine);
                        garra.Add(new XElement(vehicle.Type, reg, col, nowhels, conyear, max, line));
                    }
                    
                }
                allagarage.Add(garra);
            }
            document.Add(allagarage);
            document.Save("vehicles.xml");
        }

        private void TaBortVehicle()
        {
            List<UIItem> tempmeny = new List<UIItem>();
            foreach (Vehicle vehicle in garage)
            {
                tempmeny.Add(new UIItem(vehicle.Type, "TaBortMig", vehicle.REG_NR, TaBortMig, 0));
            }
            tempmeny.Add(new UIItem("Return to main menu", "Return", SetToMainMenu, tempmeny.Count()));
            activeMenu = tempmeny.OrderBy(b => b.Command).ToList();
            active = activeMenu.First();
            activeindex = 0;
        }

        private void TaBortMig()
        {
            string name = active.Command;
            string reg = active.Additional;
            Vehicle bort = garage.Where(b => b.Type == name && b.REG_NR == reg).First();

            if (!garage.Remove(bort))
            {
                Console.WriteLine("Could not remove {0} {1}", name, reg);

            }
            else
            {
                Console.WriteLine("Are you sure you want to remove {0} {1}? Type 'REMOVE' to continue", name, reg);
                string svar = Console.ReadLine();
                if (svar == "REMOVE")
                {
                    Console.WriteLine("{0} {1} has been removed!", name, reg);
                }
                else
                {
                    Console.WriteLine("{0} {1} has not been removed.", name, reg);
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
                tempmeny.Add(new UIItem(garages.ElementAt(i).Name, "Alla garage", garages.ElementAt(i).Max.ToString(), AllaGarage, i + 1));
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
            Vehicle temp = SkapaGenerisktVehicle("Airplane", 3);
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
            LandVehicle temp = SkapaLandVehicle("Motorcycle", 2);
            Console.WriteLine("Input brand:");
            string brand = Console.ReadLine();
            Console.WriteLine("Input category of bike:");
            string cat = Console.ReadLine();
            garage.Add(new Motorcycle(temp.REG_NR, temp.Color, temp.NumberofWheels, temp.ConstructionYear, temp.Mileage, temp.LicenseRequirement, brand, cat));
            SetToMainMenu();
        }

        private void SkapaBuss()
        {
            LandVehicle temp = SkapaLandVehicle("Buss", 8);
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

        private LandVehicle SkapaLandVehicle(string type, int nowheels)
        {
            Vehicle temp = SkapaGenerisktVehicle(type, nowheels);
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

        private Vehicle SkapaGenerisktVehicle(string type, int nowheels)
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
            Vehicle temp = SkapaGenerisktVehicle("Boat", 0);
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
            LandVehicle temp = SkapaLandVehicle("Car", 4);
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
            Console.SetWindowSize(80, activeMenu.Count()+5);
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
                    if (active.Command == "Exit")
                        avsluta = true;
                    else
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



        public void ListaVehicle()
        {
            var vehiclelista = garage.OrderBy(f => f.Type).ToList<Vehicle>();
            
            PrintaVehicle(vehiclelista);
        }

        public void PrintaVehicle(List<Vehicle> vehiclelista)
        {
            Console.Clear();
            Vehicle tmp = null;
            Console.SetWindowSize(80, vehiclelista.Count() + 20);
            if (vehiclelista.Count == 0)
            {
                Console.WriteLine("No results to your search");
            }
            foreach (Vehicle vehicle in vehiclelista)
            {
                if (tmp == null || vehicle.Type != tmp.Type)
                {
                    PrintHeader(vehicle.Type);
                    tmp = vehicle;
                }
                Console.WriteLine(vehicle);
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

        public void SkapaVehicle()
        {
            var query = from item in kommandolista
                        where item.Category == "Skapa Vehicle"
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
            PrintaVehicle(query);
        }

        public void SokPaOlikaVariabler()
        {
            var typeQuery = (from item in kommandolista
                            where item.Command == "Type" && item.Category == "Sök på olika variabler"
                            select item).First();
            typeQuery.Additional = (active.Category == "Search Type")? active.Command : " ";

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
            Console.WriteLine("Input {0}", active.Command);
            string svar = "";
            bool correct = false;
            switch (active.SearchFieldType)
            {
                case "string":
                    svar = Console.ReadLine();
                    break;
                case "int":
                    int i = 0;
                    while (!correct)
                    {
                        correct = int.TryParse(Console.ReadLine(), out i);
                        if (!correct)
                        {
                            Console.Clear();
                            Console.WriteLine("Input an integer");
                        }
                        else
                        {
                            svar = i.ToString();
                        }
                    }
                    svar = i.ToString();
                    break;
                case "double":
                    double d = 0;
                    while (!correct)
                    {
                        correct = double.TryParse(Console.ReadLine(), out d);
                        if (!correct)
                        {
                            Console.Clear();
                            Console.WriteLine("Input a double");
                        }
                        else
                        {
                            svar = d.ToString();
                        }
                    }
                    svar = d.ToString();
                    break;
                default:
                    break;
            }
            
            active.Additional = (svar == "")? " " : svar;
        }
        private void Fyll()
        {
            Console.WriteLine("Input {0}", active.Command);
            string svar = "";
            bool correct = false;
            switch (active.SearchFieldType)
            {
                case "string":
                    svar = Console.ReadLine();
                    break;
                case "int":
                    int i = 0;
                    while (!correct)
                    {
                        correct = int.TryParse(Console.ReadLine(), out i);
                        if (!correct)
                        {
                            Console.Clear();
                            Console.WriteLine("Input an integer");
                        }
                        else
                        {
                            svar = i.ToString();
                        }
                    }
                    svar = i.ToString();
                    break;
                case "double":
                    double d = 0;
                    while (!correct)
                    {
                        correct = double.TryParse(Console.ReadLine(), out d);
                        if (!correct)
                        {
                            Console.Clear();
                            Console.WriteLine("Input a double");
                        }
                        else
                        {
                            svar = d.ToString();
                        }
                    }
                    svar = d.ToString();
                    break;
                default:
                    break;
            }

            active.Additional = (svar == "") ? " " : svar;
        }

        private void SokMig()
        {
            var query = from item in kommandolista
                        where item.Category == "Sök på olika variabler" && !(item.Additional == " ")
                        orderby item.ID
                        select item;
            List<UIItem> sokFrasLista = query.ToList<UIItem>();
            var result1 = from vehicle in garage
                         select vehicle;
            var result2 = from vehicle in garage
                          select vehicle;
            List<Vehicle> vehicleLista = result1.ToList<Vehicle>();
            List<Vehicle> resultLista = result2.ToList<Vehicle>();
            foreach (var item in sokFrasLista)
            {

                switch (item.Command)
                {
                    case "REG_NR":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b.REG_NR.Contains(item.Additional));
                        break;
                    case "Type":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b.Type.Contains(item.Additional));
                        break;
                    case "Color":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b.Color.Contains(item.Additional));
                        break;
                    case "Number of wheels":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b.NumberofWheels.Equals(int.Parse(item.Additional)));
                        break;
                    case "Construction year":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b.ConstructionYear.Equals(int.Parse(item.Additional)));
                        break;
                    case "Mileage":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b is LandVehicle && (b as LandVehicle).Mileage.Equals(int.Parse(item.Additional)));
                        break;
                    case "License requirement":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b is LandVehicle && (b as LandVehicle).LicenseRequirement.Contains(item.Additional));
                        break;
                    case "Brand":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b is Motorcycle && (b as Motorcycle).Brand.Contains(item.Additional));
                        break;
                    case "Category":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b is Motorcycle && (b as Motorcycle).Category.Contains(item.Additional));
                        break;
                    case "Baggage volume":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b is Car && (b as Car).BaggageVolume.Equals(double.Parse(item.Additional)));
                        break;
                    case "Fuel type":
                        result1 = garage.Where(b => vehicleLista.Contains(b) && b is Car && (b as Car).FuelType.Contains(item.Additional));
                        break;
                    default:
                        result1 = from vehicle in vehicleLista
                                  select vehicle;
                        break;

                }
                //if (item.Command != "Sök")
                //{
                    vehicleLista = vehicleLista.Intersect(result1.ToList<Vehicle>()).ToList<Vehicle>();
                    item.Additional = " ";
                //}
            }
            
            PrintaVehicle(vehicleLista);
            SetToMainMenu();
        }
    }
}
