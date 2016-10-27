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
        public string SearchFieldType { set; get; }
        
 
        public UIItem (string com, string cat, Action kom, int id)
        {
            Command = com;
            kommando = kom;
            Category = cat;
            ID = id;
            Additional = "";
            SearchFieldType = "";
        }
        public UIItem(string com, string cat, string additional, Action kom, int id)
        {
            Command = com;
            kommando = kom;
            Category = cat;
            ID = id;
            Additional = additional;
            SearchFieldType = "";
        }
        public UIItem (string com, string cat, string additional, string fieldtype, Action kom, int id)
        {
            Command = com;
            Category = cat;
            kommando = kom;
            ID = id;
            Additional = additional;
            SearchFieldType = fieldtype;
        }
        public void Invoke()
        {
            kommando.Invoke();
        }
    }
}
