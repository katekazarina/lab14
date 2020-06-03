using System;
using System.Collections.Generic;
using System.Threading;

namespace lab14
{
    public class Collection
    {
        public List<Transport> City { get; set; }
        public List<Transport> Station { get; set; }

        public Collection(int length)
        {
            City = new List<Transport>();
            Station = new List<Transport>();
            
            for (int i = 0; i < length; i++)
            {
                Thread.Sleep(50);
                switch (i%3)
                {
                    case 0:
                        Car car = new Car();
                        City.Add(car);
                        break;
                    case 1:
                        Train train = new Train();
                        Station.Add(train);
                        break;
                    case 2:
                        Express express = new Express();
                        Station.Add(express);
                        break;
                }
            }
        }

        public void ShowCity()
        {
            foreach (Car item in City)
            {
                Console.WriteLine(item.Print());
            }
        }
        
        public void ShowStation()
        {
            foreach (Transport item in Station)
            {
                Console.WriteLine(item.Print());
            }
        }

        public void ShowCollection()
        {
            ShowCity();
            ShowStation();
        }
    }
}