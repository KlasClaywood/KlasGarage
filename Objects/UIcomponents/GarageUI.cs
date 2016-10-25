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

        public GarageUI(Garage<Vehicle> gar)
        {
            garages = new List<Garage<Vehicle>>();
            garages.Add(gar);
            garage = gar;
            kommandolista = new List<UIItem>();
        }

        public void SetToMainMenu()
        {
            kommandolista.Clear();
            kommandolista.Add(new UIItem("Lista fordon", ListaFordon));
            kommandolista.Add(new UIItem("Skapa ett fordon", SkapaFordon));
            kommandolista.Add(new UIItem("Byt garage", BytGarage));
            kommandolista.Add(new UIItem("Sök på Regnr", SokPaRegnr));
            kommandolista.Add(new UIItem("Sök på olika variabler", SokPaOlikaVariabler));
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
