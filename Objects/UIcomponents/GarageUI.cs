using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects.UIcomponents
{
    class GarageUI
    {
        private List<UIItem> kommandolista;
        private Garage<Vehicle> garage;

        public GarageUI(Garage<Vehicle> gar)
        {
            garage = gar;
            kommandolista = new List<UIItem>();
        }

        public void SetToMainMenu()
        {
            kommandolista.Clear();
            kommandolista.Add(new UIItem("Lista fordon", ListaFordon));
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
        
    }
}
