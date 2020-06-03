using System;
using System.Collections.Generic;
using System.Threading;

namespace lab14
{
    public class TestCollections
    {
        public List<Transport> listKey = new List<Transport>();
        public Dictionary<Transport, Transport> dictKeyValue = new Dictionary<Transport, Transport>();

        public TestCollections(int count)
        {
            Thread.Sleep(50);
            for (int i = 0; i < count; i++)
            {
                Transport t = new Transport();
                switch (i%3)
                {
                    case 0:
                        t = new Car(i+200, i+300);
                        break;
                    case 1:
                        t = new Train(i*2+300, i*2+380);
                        break;
                    case 2:
                        t = new Express(i*3+400, i*3+290);
                        break;
                }
                listKey.Add(t);
                dictKeyValue.Add(t, t);
            }
        }

        public void Show()
        {
            Console.WriteLine("collection:");
            foreach (Transport item in listKey)
            {
                Console.WriteLine(item.Print());
            }
        }
    }
}