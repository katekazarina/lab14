using System;
using System.ComponentModel.Design;
using System.Linq;

namespace lab14
{
    internal class Program
    {
        public static readonly Collection[] Collections = new [] { new Collection(3), new Collection(4), new Collection(3)};
        
        public static void Main(string[] args)
        {
            Console.WriteLine("коллекция 1:");
            Collections[0].ShowCollection();
            Console.WriteLine("коллекция 2:");
            Collections[1].ShowCollection();
            Console.WriteLine("коллекция 3:");
            Collections[2].ShowCollection();
            
            Selection();
            CountTypePassengers(300);
            CountType("Car");
            GroupCar(500);
            Aggregate("Train");
        }
        
        public static void Selection()
        {
            Console.WriteLine("мощность всех машин марки BMW");
            //linq
            var linq = from coll in Collections 
                from car in coll.City
                where ((Car)car).CarMaker is "BMW" select ((Car)car).Power;
            
            Console.WriteLine("результат linq запроса: ");
            foreach (int power in linq)
            {
                Console.Write(power + " ");
            }
            Console.WriteLine();
            
            //метод расширения
            var method = Collections.SelectMany(coll=>coll.City)
                .Where(car =>((Car)car).CarMaker is "BMW")
                .Select(car => ((Car)car).Power);
            
            Console.WriteLine("результат метода расширения: ");
            foreach (int power in method)
            {
                Console.Write(power + " ");
            }
        }

        public static void CountTypePassengers(int passengers)
        {
            Console.WriteLine("кол-во экспрессов с кол-вом пассажиров больше "+ passengers);

            var linq = (from coll in Collections
                from express in coll.Station
                where express is Express && express.MaxSpeed > passengers
                select express).Count();
            Console.WriteLine("результат linq-запроса - " + linq);

            var method = Collections.SelectMany(coll=>coll.Station)
                .Where(express => express is Express && express.MaxSpeed > passengers)
                .Select(express => express).Count();
            Console.WriteLine("результат метода расширения - " + method);
        }

        public static void CountType(string type)
        {
            Console.WriteLine("кол-во указанного транспортного средства");

            int linq;
            int method;
            if (type == "Car")
            {
                linq = (from coll in Collections from car in coll.City
                    select car).Count();
                method = Collections.SelectMany(coll=>coll.City)
                    .Select(tr => tr).Count();
            }

            else 
            {
                linq = (from coll in Collections from tr in coll.Station
                where tr.GetType().Name.ToString()==type
                select tr).Count();
                method = Collections.SelectMany(coll=>coll.Station)
                    .Where(tr => tr.GetType().Name.ToString() == type)
                    .Select(tr => tr)
                    .Count();
            }

            Console.WriteLine("результат linq-запроса - " + linq);
            Console.WriteLine("результат метода расширения - " + method);
        }

        public static void GroupCar(int speed)
        {
            Console.WriteLine("Вывод всех машин с максимальной скоростью больше " + speed);
            var linq = from coll in Collections 
                from tr in coll.City 
                orderby tr.MaxSpeed 
                group tr by tr.MaxSpeed > speed;
            Console.WriteLine("результат linq-запроса:");
            foreach (var group in linq)
            {
                foreach(var item in group)
                {
                    Console.WriteLine(item.Print());
                }
            }

            var method = Collections.SelectMany(coll=>coll.City)
                .OrderBy(tr => tr.MaxSpeed)
                .GroupBy(tr => tr.MaxSpeed > speed);
            Console.WriteLine("результат метода расширения:");
            foreach (var group in method)
            {
                foreach(var item in group)
                {
                    Console.WriteLine(item.Print());
                }
            }
        }

        public static void Aggregate(string type)
        {
            Console.WriteLine("средняя мощность всех (заданного типа) транспортных средств в организации");

            int linqSum, linqCount, methodSum, methodCount;
            
            if (type == "Car")
            {
                linqSum = (from coll in Collections from car in coll.City
                    select car.MaxSpeed).Sum();
                linqCount = (from coll in Collections from car in coll.City
                    select car).Count();
                
                methodSum = Collections.SelectMany(coll=>coll.City)
                    .Select(transport => transport.MaxSpeed).Sum();
                methodCount = Collections.SelectMany(coll=>coll.City)
                    .Select(transport => transport).Count();
            }
            else
            {
                linqSum = (from coll in Collections from transport in coll.Station
                    where transport.GetType().Name.ToString() == type
                    select transport.MaxSpeed).Sum();
                linqCount = (from coll in Collections from transport in coll.Station
                    where transport.GetType().Name.ToString() == type
                    select transport).Count();
                
                methodSum = Collections.SelectMany(coll=>coll.Station)
                    .Where(transport => transport.GetType().Name.ToString() == type)
                    .Select(transport => transport.MaxSpeed).Sum();
                methodCount = Collections.SelectMany(coll=>coll.Station)
                    .Where(transport => transport.GetType().Name.ToString() == type)
                    .Select(transport => transport).Count();
            }
            
            Console.WriteLine("результат linq-запроса - " + linqSum/linqCount);
            Console.WriteLine("результат метода расширения - " + methodSum/methodCount);
        }
    }
}