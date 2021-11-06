using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;

namespace AutoparkLibrary.Fabric
{
    /// <summary>
    /// Autopark fabric.
    /// </summary>
    public static class AutoparkFabric
    {
        private static List<string> existIds = new List<string>();

        /// <summary>
        /// Getting the truck tractor.
        /// </summary>
        /// <param name="garageId">Garage ID.</param>
        /// <param name="model">Model name.</param>
        /// <param name="carryingCapacity">Carruing capacity of the truck.</param>
        /// <param name="fuelConsumption">Fuel consumption of the truck.</param>
        /// <returns>The truck.</returns>
        public static TruckTractor GetTruck(string garageId, string model, double carryingCapacity, double fuelConsumption)
        {
            return new TruckTractor(garageId, model, carryingCapacity, fuelConsumption);
        }

        /// <summary>
        /// Getting the semi-trailer.
        /// </summary>
        /// <param name="type">Type of semi-trailer</param>
        /// <param name="garageId">The semi-trailer Garage ID</param>
        /// <param name="semitrailerWeight">The semi-trailer weight.</param>
        /// <param name="maxProductsWeight">Maximum weight of products transported by the semi-trailer.</param>
        /// <param name="maxProductsVolume">Maximum volume of products transported by the semi-trailer.</param>
        /// <returns>The semi-trailer</returns>
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

        /// <summary>
        /// Getting the product.
        /// </summary>
        /// <param name="name">Product name.</param>
        /// <param name="type">Product type.</param>
        /// <param name="storageCondition">Product storage condition.</param>
        /// <param name="weight">Prdouct weight.</param>
        /// <param name="volume">Product volume.</param>
        /// <returns>The product</returns>
        public static Product GetProduct(string name, Product.ProductType type, Product.ConditionOfStorage storageCondition,
                                                                                        double weight, double volume)
        {
            return new Product(name, type, storageCondition, weight, volume);
        }

        /// <summary>
        /// Getting the product with temperature set.
        /// </summary>
        /// <param name="name">Product name.</param>
        /// <param name="type">Product type.</param>
        /// <param name="storageCondition">Product storage condition.</param>
        /// <param name="weight">Prdouct weight.</param>
        /// <param name="volume">Product volume.</param>
        /// <param name="temperatureMin">Minimal storage temperature.</param>
        /// <param name="temperatureMax">Maximal storage temperature.</param>
        public static Product GetProduct(string name, Product.ProductType type, Product.ConditionOfStorage storageCondition,
                                                double weight, double volume, double temperatureMin, double temperatureMax)
        {
            return new Product(name, type, storageCondition, weight, volume, temperatureMin, temperatureMax);
        }

        /// <summary>
        /// Getting the product from string with product info.
        /// </summary>
        /// <param name="str">String with product info.</param>
        /// <returns>Product</returns>
        public static Product GetProductFromString(string str)
        {
            Product product = null;
            List<string> attributes = new List<string>(str.Split(','));
            string name = attributes[0];
            Enum.TryParse(attributes[1], out Product.ConditionOfStorage storageCondition);
            Enum.TryParse(attributes[2], out Product.ProductType type);
            double weight = Convert.ToDouble(attributes[3]);
            double volume = Convert.ToDouble(attributes[4]);
            if (attributes.Count > 5)
            {
                double temperatureMin = Convert.ToDouble(attributes[5]);
                double temperatureMax = Convert.ToDouble(attributes[6]);
                product = GetProduct(name, type, storageCondition, weight, volume, temperatureMin, temperatureMax);
            }
            else
                product = GetProduct(name, type, storageCondition, weight, volume);
            return product;
        }

        /// <summary>
        /// Getting the list of products from semicolon-delimited product information string.
        /// </summary>
        /// <param name="str">Semicolon-delimited product information string.</param>
        /// <returns>List of products.</returns>
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

        public static string GetUniquRandomID()
        {
            StringBuilder id = new StringBuilder();
            Random random = new Random();
            bool unique = false;
            while(!unique)
            {
                id.Clear();
                id.Append(((char)(random.Next(65, 90))) + ((char)(random.Next(65, 95))) + "-");
                id.Append(random.Next(1000, 9999));
                int i;
                for (i = 0; i < existIds.Count; i++)
                {
                    if (id.ToString() == existIds[i])
                    {
                        break;
                    }
                }
                if (i > existIds.Count - 1 || existIds.Count == 0)
                {
                    unique = true;
                }    
            }
            existIds.Add(id.ToString());
            return id.ToString();
        }

    }
}
