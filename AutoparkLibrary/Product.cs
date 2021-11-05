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

        public static bool FindProductByProductClone(Product productClone, List<Product> products, out int indexOfFoundProduct)
        {
            bool isFound = false;
            indexOfFoundProduct = -1;
            for (int i = 0; i < products.Count && !isFound; i++)
            {
                if (products[i].Equals(productClone))
                {
                    isFound = true;
                    indexOfFoundProduct = i;
                }
            }
            return isFound;
        }

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
            string result = $"{Name}, {Type}, {StorageCondition}, {Weight}, {Volume}";
            if (StorageCondition == ConditionOfStorage.Thermal)
            {
                result += $", {Weight}, {Volume}";
            }
            return result;
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
