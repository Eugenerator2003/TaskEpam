using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkLibrary.Products
{
    public class Product : ICloneable
    {
        public enum ConditionOfStorage
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

        public ConditionOfStorage StorageCondition { get; }

        public ProductType Type { get; }

        public string Name { get; }

        public double Weight { get; }

        public double Volume { get; }

        public double TemperatureMin { get; }

        public double TemperatureMax { get; }

        public Product(string name, ProductType type, ConditionOfStorage condition, double weight, double volume)
        {
            Name = name;
            Weight = weight;
            Volume = volume;
            StorageCondition = condition;
            Type = type;
        }

        public Product(string name, ProductType type, ConditionOfStorage condition, double weight, double volume, double temp_min, double temp_max)
                 : this(name, type, condition, weight, volume)
        {
            TemperatureMin = temp_max;
            TemperatureMax = temp_min;
        }

        public override string ToString()
        {
            return $"{Name}, {Type}, {StorageCondition}, {Weight}, {Volume}, " +
                   $"{TemperatureMin}, {TemperatureMax}";
        }


        public object Clone()
        {
            Product product = new Product(Name, Type, StorageCondition, Weight, Volume);
            return (object)product;
        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   StorageCondition == product.StorageCondition &&
                   Type == product.Type &&
                   Name == product.Name &&
                   Weight == product.Weight &&
                   Volume == product.Volume &&
                   TemperatureMin == product.TemperatureMin &&
                   TemperatureMax == product.TemperatureMax;
        }
    }
}
