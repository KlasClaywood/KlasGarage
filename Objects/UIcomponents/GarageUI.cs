using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public GarageUI(Garage<Vehicle> gar)
        {
            arguments = new string[0];
            garages = new List<Garage<Vehicle>>();
            garages.Add(gar);
            garage = gar;
            kommandolista = new List<UIItem>();
            kommandolista.Add(new UIItem("Lista fordon", "Huvudmeny", ListaFordon, 1));
            kommandolista.Add(new UIItem("Skapa ett fordon", "Huvudmeny", SkapaFordon, 2));
            kommandolista.Add(new UIItem("Byt garage", "Huvudmeny", BytGarage, 3));
            kommandolista.Add(new UIItem("Sök på Regnr", "Huvudmeny", SokPaRegnr, 4));
            kommandolista.Add(new UIItem("Sök på olika variabler", "Huvudmeny", SokPaOlikaVariabler, 5));
            kommandolista.Add(new UIItem("Skapa bil", "Skapa fordon", SkapaBil, 1));
            kommandolista.Add(new UIItem("Skapa båt", "Skapa fordon", SkapaBat, 2));
            kommandolista.Add(new UIItem("Skapa buss", "Skapa fordon", SkapaBuss, 3));
            kommandolista.Add(new UIItem("Skapa motorcykel", "Skapa fordon", SkapaMC, 4));
            kommandolista.Add(new UIItem("Skapa flygplan", "Skapa fordon", SkapaFlygplan, 5));
            var query = from item in kommandolista
                         where item.Category == "Huvudmeny"
                         orderby item.ID
                         select item;
            activeMenu = query.ToList<UIItem>();
                
            active = activeMenu.ElementAt(0);
            
            activeindex = 0;
        }

        private void SkapaFlygplan()
        {
            Vehicle temp = SkapaGenerisktFordon("Airplane", 3);
            Console.WriteLine("Input maximum altitude:");
            int max = int.Parse(Console.ReadLine());
            Console.WriteLine("Input airline name:");
            string airl = Console.ReadLine();

            garage.Add(new Airplane(temp.REG_NR, temp.Color, temp.NumberofWheels, temp.ConstructionYear, max, airl));
            SetToMainMenu();
        }

        private void SetToMainMenu()
        {
            var query = from item in kommandolista
                        where item.Category == "Huvudmeny"
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
            int noseats = int.Parse(Console.ReadLine());
            Console.WriteLine("Input line number:");
            int line = int.Parse(Console.ReadLine());
            garage.Add(new Buss(temp.REG_NR, temp.Color, temp.NumberofWheels, temp.ConstructionYear, temp.Mileage, temp.LicenseRequirement, noseats, line));
            SetToMainMenu();
        }

        private LandVehicle SkapaLandfordon(string type, int nowheels)
        {
            Vehicle temp = SkapaGenerisktFordon(type, nowheels);
            Console.WriteLine("Input mileage:");
            int miles = int.Parse(Console.ReadLine());
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
            int conyear = int.Parse(Console.ReadLine());
            return new Vehicle(type, regnr, color, nowheels, conyear);
        }

        private void SkapaBat()
        {
            Vehicle temp = SkapaGenerisktFordon("Boat", 0);
            Console.WriteLine("Input buoyancy:");
            int buoy = int.Parse(Console.ReadLine());
            Console.WriteLine("Input length:");
            int len = int.Parse(Console.ReadLine());
            garage.Add(new Boat(temp.REG_NR, temp.Color, temp.NumberofWheels, temp.ConstructionYear, buoy, len));
            SetToMainMenu();
        }

        private void SkapaBil()
        {
            LandVehicle temp = SkapaLandfordon("Car", 4);
            Console.WriteLine("Input baggage volume:");
            double bagvol = double.Parse(Console.ReadLine());
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
                    Console.WriteLine(punkt.Command);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(punkt.Command);
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
            Console.Clear();
            foreach (Vehicle fordon in garage)
            {
                Console.WriteLine(fordon);
            }
            Console.ReadKey();
        }

        public void SkapaFordon()
        {
            var query = from item in kommandolista
                        where item.Category == "Skapa fordon"
                        select item;
            activeMenu = query.ToList<UIItem>();
            active = activeMenu.ElementAt(0);
            activeindex = 0;
        }

        public void BytGarage()
        {
            
        }

        public void SokPaRegnr()
        {
            
            string reg = Console.ReadLine();
            var query = from vehicle in garage
                        where vehicle.REG_NR.Contains(reg)
                        select vehicle;
            foreach (Vehicle vehicle in query)
            {
                Console.WriteLine(vehicle);
            }
            Console.ReadKey();
        }

        public void SokPaOlikaVariabler()
        {
            
        }
    }
}
