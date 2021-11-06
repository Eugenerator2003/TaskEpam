using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkLibrary.Products
{
    /// <summary>
    /// Product.Storage like a liquid.
    /// </summary>
    public class Product : ICloneable
    {
        /// <summary>
        /// Conditios of storage.
        /// </summary>
        public enum ConditionOfStorage
        {
            /// <summary>
            /// Stored in boxes.
            /// </summary>
            Box,
            /// <summary>
            /// Stored like a liquid.
            /// </summary>
            Liquid,
            /// <summary>
            /// Stored with temperature set.
            /// </summary>
            Thermal
        }

        /// <summary>
        /// Type of product.
        /// </summary>
        public enum ProductType
        {
            /// <summary>
            /// Food product.
            /// </summary>
            Food,
            /// <summary>
            /// Chemistry product
            /// </summary>
            Chemistry,
            /// <summary>
            /// House applience product.
            /// </summary>
            HouseAppliences,
            /// <summary>
            /// Fuel product.
            /// </summary>
            Fuel
        }

        /// <summary>
        /// Storage condition of the product.
        /// </summary>
        public ConditionOfStorage StorageCondition { get; }

        /// <summary>
        /// Type of the product.
        /// </summary>
        public ProductType Type { get; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Weight of the product.
        /// </summary>
        public double Weight { get; }

        /// <summary>
        /// Volume of the product.
        /// </summary>
        public double Volume { get; }

        /// <summary>
        /// Minimal temperature storage.
        /// </summary>
        public double TemperatureMin { get; }

        /// <summary>
        /// Maximal temperature storage.
        /// </summary>
        public double TemperatureMax { get; }

        /// <summary>
        /// Finding the product by specific product in list of product.
        /// </summary>
        /// <param name="productSpecific">Specific product.</param>
        /// <param name="products">List of products.</param>
        /// <param name="indexOfFoundProduct">Index of found product.</param>
        /// <returns>True if product was found.</returns>
        public static bool FindProductBySpecificProduct(Product productSpecific, List<Product> products, out int indexOfFoundProduct)
        {
            bool isFound = false;
            indexOfFoundProduct = -1;
            for (int i = 0; i < products.Count && !isFound; i++)
            {
                if (products[i].Equals(productSpecific))
                {
                    isFound = true;
                    indexOfFoundProduct = i;
                }
            }
            return isFound;
        }

        /// <summary>
        /// Constructor of product.
        /// </summary>
        /// <param name="name">Product name.</param>
        /// <param name="type">Product type.</param>
        /// <param name="condition">Product storage condition.</param>
        /// <param name="weight">Prdouct weight.</param>
        /// <param name="volume">Product volume.</param>
        public Product(string name, ProductType type, ConditionOfStorage condition, double weight, double volume)
        {
            Name = name;
            Weight = weight;
            Volume = volume;
            StorageCondition = condition;
            Type = type;
        }

        /// <summary>
        /// Constructor of product with temperature set..
        /// </summary>
        /// <param name="name">Product name.</param>
        /// <param name="type">Product type.</param>
        /// <param name="condition">Product storage condition.</param>
        /// <param name="weight">Prdouct weight.</param>
        /// <param name="volume">Product volume.</param>
        /// <param name="temp_min">Minimal storage temperature.</param>
        /// <param name="temp_max">Maximal storage temperature.</param>
        public Product(string name, ProductType type, ConditionOfStorage condition, double weight, double volume, double temp_min, double temp_max)
                 : this(name, type, condition, weight, volume)
        {
            TemperatureMin = temp_min;
            TemperatureMax = temp_max;
        }

        /// <summary>
        /// Convetring the product to String.
        /// </summary>
        /// <returns>Product convetring to the string.</returns>
        public override string ToString()
        {
            string result = $"{Name}, {Type}, {StorageCondition}, {Weight}, {Volume}";
            if (StorageCondition == ConditionOfStorage.Thermal)
            {
                result += $", {TemperatureMin}, {TemperatureMax}";
            }
            return result;
        }

        /// <summary>
        /// Getting clone of the product.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Product product = new Product(Name, Type, StorageCondition, Weight, Volume, TemperatureMin, TemperatureMax);
            return (object)product;
        }

        /// <summary>
        /// Comparing the product with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to product.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Product product = obj as Product;
            if (product == null)
                return false;
            bool storageEqual = false;
            if (StorageCondition == product.StorageCondition)
            {
                if (StorageCondition != ConditionOfStorage.Thermal)
                { 
                    storageEqual = true;
                }
                else
                {
                    if (TemperatureMin == product.TemperatureMin && TemperatureMax == product.TemperatureMax)
                    {
                        storageEqual = true;
                    }
                }
            }
            return Type == product.Type &&
                   Name == product.Name &&
                   Weight == product.Weight &&
                   Volume == product.Volume &&
                   storageEqual;
        }

        /// <summary>
        /// Getting hash code of the product.
        /// </summary>
        /// <returns>Hash code of the product.</returns>
        public override int GetHashCode()
        {
            int hashCode = 1123610502;
            hashCode = hashCode * -1521134295 + StorageCondition.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            hashCode = hashCode * -1521134295 + Volume.GetHashCode();
            hashCode = hashCode * -1521134295 + TemperatureMin.GetHashCode();
            hashCode = hashCode * -1521134295 + TemperatureMax.GetHashCode();
            return hashCode;
        }
    }
}
