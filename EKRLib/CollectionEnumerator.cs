using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    class CollectionEnumerator<T> : IEnumerator<T> where T: Item
    {
        T[] items;
        int i;

        public CollectionEnumerator(T[] items)
        {
            this.items = items;
        }

        public T Current => items[i];

        object IEnumerator.Current => items[i];

        public void Dispose()
        {
            return;
        }

        public bool MoveNext()
        {
            while (items[i].Weight <= 0)
            {
                i++;
            }
            return ++i < items.Length;
        }

        public void Reset()
        {
            i = 0;
        }
    }
}
