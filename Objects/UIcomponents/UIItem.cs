using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects.UIcomponents
{
    class UIItem
    {
        public string Command { get; private set; }
        private Action kommando;
        public string Category { get; private set; }
        public int ID { get; private set; }
        public string Additional { get; set; }
        
 
        public UIItem (string com, string cat, Action kom, int id)
        {
            Command = com;
            kommando = kom;
            Category = cat;
            ID = id;
            Additional = "";
        }
        public UIItem(string com, string cat, string additional, Action kom, int id)
        {
            Command = com;
            kommando = kom;
            Category = cat;
            ID = id;
            Additional = additional;
        }
        public void Invoke()
        {
            kommando.Invoke();
        }
    }
}
