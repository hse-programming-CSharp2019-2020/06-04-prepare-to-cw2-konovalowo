using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    /// <summary>
    /// Класс, описывающий коллекцию объектов Item
    /// </summary>
    /// <typeparam name="T">Объект Item или производный</typeparam>
    [DataContract]
    public class Collection<T> : IEnumerable<T> where T: Item 
    {
        [DataMember]
        List<T> items;

        public int Count => items.Count();

        public Collection()
        {
            items = new List<T>();
        }

        /// <summary>
        /// Добавляет Item в коллекцию
        /// </summary>
        /// <param name="item">Новый объект</param>
        public void Add(T item) => items.Add(item);

        public IEnumerator<T> GetEnumerator()
        {
            return new CollectionEnumerator<T>(items.ToArray());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
