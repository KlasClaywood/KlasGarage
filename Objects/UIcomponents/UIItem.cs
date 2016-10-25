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
        
 
        public UIItem (string com, Action kom)
        {
            Command = com;
            kommando = kom;
            
        }
        public void Invoke()
        {
            kommando.Invoke();
        }
    }
}
