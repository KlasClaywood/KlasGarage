using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasGarage.Objects
{
    class Garage<T>:IEnumerable<T>, ICollection<T> where T : Vehicle
    {
        private List<T> vehicleList;
        public int Max { get; private set; }
        public string Name { get; private set; }
        public bool IsReadOnly
        {
            get;
            private set;
        }
        public int Count
        {
            get { return vehicleList.Count; }
        }

        public Garage(string namn, int max)
        {
            Name = namn;
            Max = max;
            vehicleList = new List<T>();
            IsReadOnly = false;
        }
        public void Add(T item)
        {
            if (this.Count < this.Max)
            {
                vehicleList.Add(item);
            }
            else throw new Exception("Not enough space in garage!");
        }

        public void Clear()
        {
            vehicleList.Clear();
        }

        public bool Contains(T item)
        {
            return vehicleList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            vehicleList.CopyTo(array, arrayIndex);
        }

        
        

        public bool Remove(T item)
        {
            return vehicleList.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return vehicleList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return vehicleList.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return vehicleList.GetEnumerator();
        }
    }
}
