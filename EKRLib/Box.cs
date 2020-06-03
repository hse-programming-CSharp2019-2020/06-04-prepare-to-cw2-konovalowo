using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    [DataContract]
    public class Box : Item
    {
        double a, b, c;

        [DataMember]
        public double A
        {
            get => a;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Измерение должно быть положительным");
                }
                a = value;
            }
        }

        [DataMember]
        public double B
        {
            get => b;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Измерение должно быть положительным");
                }
                b = value;
            }
        }

        [DataMember]
        public double C
        {
            get => c;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Измерение должно быть положительным");
                }
                c = value;
            }
        }

        public Box(double a, double b, double c, double weight) : base(weight)
        {
            A = a;
            B = b;
            C = c;
        }

        public double GetLongestSideSize() => Math.Max(a, Math.Max(b, c));

        public override string ToString()
        {
            return base.ToString() + $", A: {A:f3}, B: {B:f3}, C: {C:f3}";
        }
    }
}
