using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects.UIcomponents
{
    class GarageUI
    {
        private List<UIItem> kommandolista;
        private List<Garage<Vehicle>> garages;
        private Garage<Vehicle> garage;
        private UIItem active;
        private int activeindex;

        public GarageUI(Garage<Vehicle> gar)
        {
            garages = new List<Garage<Vehicle>>();
            garages.Add(gar);
            garage = gar;
            kommandolista = new List<UIItem>();
            if (kommandolista.Count() != 0)
            {
                
                active = kommandolista.ElementAt(0);
            }
            activeindex = 0;
        }


        public bool RunMenu ()
        {
            Console.Clear();
            bool avsluta = false;
            foreach (UIItem punkt in kommandolista)
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
                        activeindex = kommandolista.Count - 1;
                    }
                    active = kommandolista.ElementAt(activeindex);
                    break;
                case ConsoleKey.DownArrow:
                    if (activeindex < kommandolista.Count - 1)
                    {
                        activeindex += 1;  
                    }
                    else
                    {
                        activeindex = 0;
                    }
                    active = kommandolista.ElementAt(activeindex);
                    break;
                case ConsoleKey.Enter:
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

        public void SetToMainMenu()
        {
            kommandolista.Clear();
            kommandolista.Add(new UIItem("Lista fordon", ListaFordon));
            kommandolista.Add(new UIItem("Skapa ett fordon", SkapaFordon));
            kommandolista.Add(new UIItem("Byt garage", BytGarage));
            kommandolista.Add(new UIItem("Sök på Regnr", SokPaRegnr));
            kommandolista.Add(new UIItem("Sök på olika variabler", SokPaOlikaVariabler));
            active = kommandolista.ElementAt(0);
            activeindex = 0;
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

        }

        public void SokPaOlikaVariabler()
        {

        }
    }
}
