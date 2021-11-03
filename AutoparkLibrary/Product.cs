using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkLibrary.Products
{
    public class Product : ICloneable
    {
        public enum StorageCondition
        {
            Box,
            Liquid,
            Thermal
        }

        public enum ProductType
        {
            Food,
            Chemistry,
            HouseAppliences,
            Fuel
        }

        public StorageCondition Condition { get; }

        public ProductType Type { get; }

        public string Name { get; }

        public double Weight { get; }

        public double Volume { get; }

        public double TemperatureMin { get; }

        public double TemperatureMax { get; }

        public Product(string name, ProductType type, StorageCondition condition, double weight, double volume)
        {
            Name = name;
            Weight = weight;
            Volume = volume;
            Condition = condition;
            Type = type;
        }

        public Product(string name, ProductType type, StorageCondition condition, double weight, double volume, double temp_min, double temp_max)
                 : this(name, type, condition, weight, volume)
        {
            TemperatureMin = temp_max;
            TemperatureMax = temp_min;
        }

        public object Clone()
        {
            Product product = new Product(Name, Type, Condition, Weight, Volume);
            return (object)product;
        }
    }
}
