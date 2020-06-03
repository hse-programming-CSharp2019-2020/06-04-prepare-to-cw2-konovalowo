using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using EKRLib;
using System.Runtime.Serialization.Json;
using System.Runtime.InteropServices;

namespace PrepareToCw2
{
    class Program
    {
        public static Random rnd = new Random();

        const string writePath = "boxes.json";

        static void Main(string[] args)
        {
            // Цикл повтора решения.
            do
            {
                EKRLib.Collection<Box> boxes = new EKRLib.Collection<Box>();

                int n = ReadInt("Введите количество элементов коллекции: ");

                GenerateListOfKorobochek(boxes, n);

                foreach (var box in boxes)
                {
                    Console.WriteLine(box);
                }

                SerializeKorobochki(boxes);

                Console.WriteLine("Для выхода нажмите ESC...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }


        public static int ReadInt(string prompt)
        {
            int n;
            do
            {
                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out n) || n <= 0);
            return n;
        }

        public static Box GenerateBox()
        {
            return new Box(NextDoubleInInterval(-3, 10),
                NextDoubleInInterval(-3, 10),
                NextDoubleInInterval(-3, 10),
                NextDoubleInInterval(-3, 10));
        }

        public static double NextDoubleInInterval(int min, int max)
        {
            return rnd.NextDouble() + rnd.Next(min, max - 1);
        }

        private static void GenerateListOfKorobochek(EKRLib.Collection<Box> boxes, int n)
        {
            while (boxes.Count != n + 1)
            {
                try
                {
                    boxes.Add(GenerateBox());
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Исключение при создании коробочки: {e.Message}");
                    continue;
                }
            }
        }

        private static void SerializeKorobochki(EKRLib.Collection<Box> boxes)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(boxes.GetType());
            try
            {
                using (var sw = new FileStream(writePath, FileMode.Create))
                {
                    serializer.WriteObject(sw, boxes);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ошибка ввода/вывода при записи в файл: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при записи в файл: {e.Message}");
            }
        }
    }
}
