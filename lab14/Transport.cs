using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace lab14
{
    public class Transport
    {
        public int MaxSpeed { get; set; }

        public Transport()
        {
            Random rnd = new Random();
            MaxSpeed = rnd.Next(100, 401);
        }

        public Transport(int speed)
        {
            MaxSpeed = speed;
        }

        public virtual string Print()
        {
            return "Transport: max speed - " + MaxSpeed;
        }

        public object Clone()
        {
            return new Transport(this.MaxSpeed);
        }

        public override int GetHashCode()
        {
            return MaxSpeed.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Transport t = (Transport)obj;
            return this.MaxSpeed == t.MaxSpeed;
        }

        public override string ToString()
        {
            return MaxSpeed.ToString();
        }
    }

    public class Car: Transport
    {
        public int Power { get; set; }
        private string _carMaker;
        private List<string> _makers = new List<string>(){"BMW", "Ford", "Audi", "Porsche", "Nissan"};

        public string CarMaker
        {
            get => _carMaker;
            set => _carMaker = value;
        }

        public Car():base()
        {
            Random rnd = new Random();
            Power = rnd.Next(200, 1501);
            CarMaker = _makers[rnd.Next(0, 5)];
        }

        public Car(int speed, int power):base(speed)
        {
            Power = power;
            Random rnd = new Random();
            CarMaker = _makers[rnd.Next(0, 5)];
        }

        public override string Print()
        {
            return "Car: power - " + Power + ", max speed - " + MaxSpeed+" , maker - "+CarMaker;
        }
    }

    public class Train : Transport
    {
        public int Carriage { get; }

        public Train()
        {
            Random rnd = new Random();
            Carriage = rnd.Next(200, 1501);
        }

        public Train(int speed, int c) : base(speed)
        {
            Carriage = c;
        }

        public override string Print()
        {
            return "Train: carriage - " + Carriage + ", max speed - " + MaxSpeed;
        }
    }

    public class Express : Transport
    {
        public int Passengers { get; }

        public Express() : base()
        {
            Random rnd = new Random();
            Passengers = rnd.Next(200, 1501);
        }

        public Express(int speed, int p) : base(speed)
        {
            Passengers = p;
        }

        public override string Print()
        {
            return "Express: passengers - " + Passengers + ", max speed - " + MaxSpeed;
        }
    }

    public class SortByMaxSpeed : IComparer
    {
        int IComparer.Compare(object ob1, object ob2)
        {
            Transport t1 = (Transport)ob1;
            Transport t2 = (Transport)ob2;
            if (t1.MaxSpeed == t2.MaxSpeed)
            {
                return 0;
            }

            return t1.MaxSpeed > t2.MaxSpeed ? 1 : -1;
        }
    }
}
