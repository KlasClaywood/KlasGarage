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
            throw new NotImplementedException();
        }

        private void SkapaMC()
        {
            throw new NotImplementedException();
        }

        private void SkapaBuss()
        {
            throw new NotImplementedException();
        }

        private void SkapaBat()
        {
            throw new NotImplementedException();
        }

        private void SkapaBil()
        {
            throw new NotImplementedException();
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
