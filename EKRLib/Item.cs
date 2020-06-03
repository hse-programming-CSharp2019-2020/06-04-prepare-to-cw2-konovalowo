using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    [DataContract]
    public class Item : IComparable<Item>
    {
        double weight;

        [DataMember]
        public double Weight
        {
            get => weight;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Вес должен быть неотрицательным значением");
                }
                weight = value;
            }
        }

        public Item(double weight)
        {
            Weight = weight;
        }

        public static explicit operator double(Item item) => item.Weight;

        public override string ToString() => $"Weight: {weight:f3}";

        public int CompareTo(Item other) => this.Weight.CompareTo(other.Weight);
    }
}
