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

        /// <summary>
        /// Считывает целочисленное значение
        /// </summary>
        /// <param name="prompt">Подсказка</param>
        /// <returns>Введённое значение</returns>
        public static int ReadInt(string prompt)
        {
            int n;
            do
            {
                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out n) || n <= 0);
            return n;
        }

        /// <summary>
        /// Создаёт объект Box
        /// </summary>
        /// <returns>Созданный объект</returns>
        public static Box GenerateBox()
        {
            return new Box(NextDoubleInInterval(-3, 10),
                NextDoubleInInterval(-3, 10),
                NextDoubleInInterval(-3, 10),
                NextDoubleInInterval(-3, 10));
        }

        /// <summary>
        /// Генерирует вещественное число в полуинтервале [min, max)
        /// </summary>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns>Созданное число</returns>
        public static double NextDoubleInInterval(int min, int max)
        {
            return rnd.NextDouble() + rnd.Next(min, max - 1);
        }

        /// <summary>
        /// Заполняет список объектов Box
        /// </summary>
        /// <param name="boxes">Ссылка на список</param>
        /// <param name="n">Количество элементов для добавления</param>
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

        /// <summary>
        /// Сериализует список объектов Box в Json
        /// </summary>
        /// <param name="boxes">Список объектов</param>
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
