using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autopark.Products
{
    public class Product
    {

        public enum CargoType
        {

        }

        public CargoType Type { get; }

        public string Name { get; }

        public double Weight { get; private set; }

        public double Volume { get; private set; }

        public Product(string name, CargoType type, double weight, double volume)
        {
            Name = name;
            Weight = weight;
            Volume = volume;
            Type = type;
        }
    }
}
