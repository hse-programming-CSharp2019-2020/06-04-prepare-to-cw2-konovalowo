using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    [DataContract]
    public class Collection<T> where T: Item 
    {
        [DataMember]
        List<T> items;

        public int Count => items.Count();

        public List<T> Items => items;

        public Collection()
        {
            items = new List<T>();
        }

        public void Add(T item) => items.Add(item);

        public IEnumerator<T> GetEnumerator()
        {
            return new CollectionEnumerator<T>(items.ToArray());
        }
    }
}
