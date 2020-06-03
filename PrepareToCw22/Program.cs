using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using EKRLib;

namespace PrepareToCw22
{
    class Program
    {
        const string readPath = "../../../PrepareToCw2/bin/Debug/boxes.json";

        static void Main(string[] args)
        {
            // Цикл повтора решения.
            do
            {
                var boxes = new EKRLib.Collection<Box>();
                boxes = DeserializeKorobochki(boxes);

                //linq1
                Console.WriteLine("\t = LINQ 1 =");
                LinqQueryMaxDimensionOverThreeOrderByMaxDimension(boxes);

                //linq2
                Console.WriteLine("\t = LINQ 2 =");
                LinqQueryGroupByWeight(boxes);

                //linq3
                Console.WriteLine("\t = LINQ 3 =");
                LinqQueryListOfMaxWeightItems(boxes);

                Console.WriteLine("Для выхода нажмите ESC...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void LinqQueryListOfMaxWeightItems(Collection<Box> boxes)
        {
            var linq3 = boxes.Where(b => b.Weight == boxes.Max(i => i.Weight));
            Console.WriteLine($"Количество: {linq3.Count()}");
            foreach (var item in linq3)
            {
                Console.WriteLine(item);
            }
        }

        private static void LinqQueryGroupByWeight(Collection<Box> boxes)
        {
            var linq2 = boxes.GroupBy(i => i.Weight);
            foreach (var list in linq2)
            {
                Console.WriteLine($"Maccа: {list.Key:f3}");
                foreach (var box in list)
                {
                    Console.WriteLine("\t" + box.ToString());
                }
            }
        }

        private static void LinqQueryMaxDimensionOverThreeOrderByMaxDimension(Collection<Box> boxes)
        {
            var linq1 = boxes.Where(i => i.GetLongestSideSize() > 3)
                            .OrderByDescending(i => i.GetLongestSideSize())
                            .Select(i => i);
            foreach (var item in linq1)
            {
                Console.WriteLine(item);
            }
        }

        private static Collection<Box> DeserializeKorobochki(Collection<Box> boxes)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(boxes.GetType());
            try
            {
                using (var sw = new FileStream(readPath, FileMode.Open))
                {
                    boxes = (EKRLib.Collection<Box>)serializer.ReadObject(sw);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ошибка ввода/вывода при чтении файла: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при чтении файла: {e.Message}");
            }

            return boxes;
        }
    }
}
