using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;

namespace AutoparkLibrary.Fabric
{
    public static class AutoparkFabric
    {
        public static TruckTractor GetTruck(string garageId, string model, double carryingCapacity, double fuelConsumption)
        {
            return new TruckTractor(garageId, model, carryingCapacity, fuelConsumption);
        }

        public static Semitrailer GetSemitrailer(Semitrailer.SemitrailerType type, string garageId, double semitrailerWeight, double maxProductsWeight, double maxProductsVolume)
        {
            Semitrailer semitrailer = null;
            switch(type)
            {
                case (Semitrailer.SemitrailerType.TiltSemitrailer):
                    semitrailer = new TiltSemitrailer(garageId, semitrailerWeight, maxProductsWeight, maxProductsVolume);
                    break;
                case (Semitrailer.SemitrailerType.TankSemitrailer):
                    semitrailer = new TankSemitrailer(garageId, semitrailerWeight, maxProductsWeight, maxProductsVolume);
                    break;
                case (Semitrailer.SemitrailerType.RefrigiratorSemitrailer):
                    semitrailer = new RefrigiratorSemitrailer(garageId, semitrailerWeight, maxProductsWeight, maxProductsVolume);
                    break;
            }
            return semitrailer;
        }

        public static Product GetProduct(string name, Product.ProductType type, Product.ConditionOfStorage storageCondition,
                                                                                        double weight, double volume)
        {
            return new Product(name, type, storageCondition, weight, volume);
        }

        public static Product GetProduct(string name, Product.ProductType type, Product.ConditionOfStorage storageCondition,
                                                double weight, double volume, double temperatureMin, double temperatureMax)
        {
            return new Product(name, type, storageCondition, weight, volume, temperatureMin, temperatureMax);
        }

        public static Product GetProductFromString(string str)
        {
            Product product = null;
            List<string> attributes = new List<string>(str.Split(','));
            string name = attributes[0].Trim();
            Enum.TryParse(attributes[1].Trim(), out Product.ConditionOfStorage storageCondition);
            Enum.TryParse(attributes[2].Trim(), out Product.ProductType type);
            double weight = Convert.ToDouble(attributes[3].Trim());
            double volume = Convert.ToDouble(attributes[4].Trim());
            if (attributes.Count > 5)
            {
                double temperatureMin = Convert.ToDouble(attributes[5].Trim());
                double temperatureMax = Convert.ToDouble(attributes[6].Trim());
                product = GetProduct(name, type, storageCondition, weight, volume, temperatureMin, temperatureMax);
            }
            product = new Product(name, type, storageCondition, weight, volume);
            return product;
        }

        public static List<Product> GetProductListFromString(string str)
        {
            List<Product> products = new List<Product>();
            List<string> productsInfo = new List<string>(str.Split(';'));
            foreach(string product_info in productsInfo)
            {
                products.Add(GetProductFromString(product_info));
            }
            return products;
        }


    }
}
