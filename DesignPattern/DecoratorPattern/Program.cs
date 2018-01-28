using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Beverage beverage = new Expresso();
            Console.WriteLine(beverage.getDescription() + " $" + beverage.Cost());
            //
            Beverage beverage2 = new DarkRoast();
            beverage2 = new Mocha(beverage2);
            beverage2 = new Mocha(beverage2);
            beverage2 = new Whip(beverage2);
            Console.WriteLine(beverage2.getDescription() + " $" + beverage2.Cost());
            //
            Beverage beverage3 = new HouseBlend();
            beverage3 = new Soy(beverage3);
            beverage3 = new Mocha(beverage3);
            beverage3 = new Whip(beverage3);
            Console.WriteLine(beverage3.getDescription() + " $" + beverage3.Cost());

            Stream a = new MemoryStream();
            Stream b = new GZipStream(a, CompressionMode.Decompress);
        }
    }

    public abstract class Beverage
    {
        protected string description = "Unknown Beverage";

        public abstract string getDescription();

        public abstract double Cost();
    }

    public abstract class CondimentDecorator : Beverage
    {
        public override string getDescription()
        {
            return string.Empty;
        }
    }

    public class Expresso : Beverage
    {
        public Expresso()
        {
            description = "Expresso";
        }

        public override double Cost()
        {
            return 1.99d;
        }

        public override string getDescription()
        {
            return description;
        }
    }

    public class HouseBlend : Beverage
    {
        public HouseBlend()
        {
            description = "House Blend Coffee";
        }

        public override double Cost()
        {
            return .89d;
        }

        public override string getDescription()
        {
            return description;
        }
    }

    public class DarkRoast : Beverage
    {
        public DarkRoast()
        {
            description = "Dark Roast Coffee";
        }

        public override double Cost()
        {
            return 1.22d;
        }

        public override string getDescription()
        {
            return description;
        }
    }

    public class Decaf : Beverage
    {
        public Decaf()
        {
            description = "Decaf";
        }

        public override double Cost()
        {
            return .78d;
        }

        public override string getDescription()
        {
            return description;
        }

    }

    public class Mocha : CondimentDecorator
    {
        public Beverage Beverage { get; private set; }

        public Mocha(Beverage beverage)
        {
            Beverage = beverage;
        }

        public override string getDescription()
        {
            return Beverage.getDescription() + ", Mocha";
        }

        public override double Cost()
        {
            return .20 + Beverage.Cost();
        }
    }

    public class Whip : CondimentDecorator
    {
        public Beverage Beverage { get; set; }

        public Whip(Beverage beverage)
        {
            Beverage = beverage;
        }

        public override string getDescription()
        {
            return Beverage.getDescription() + ", Whip";
        }
        public override double Cost()
        {
            return .15 + Beverage.Cost();
        }
    }

    public class Soy : CondimentDecorator
    {
        public Beverage Beverage { get; set; }

        public Soy(Beverage beverage)
        {
            Beverage = beverage;
        }

        public override string getDescription()
        {
            return Beverage.getDescription() + ", Soy";
        }

        public override double Cost()
        {
            return .17 + Beverage.Cost();
        }
    }
}
