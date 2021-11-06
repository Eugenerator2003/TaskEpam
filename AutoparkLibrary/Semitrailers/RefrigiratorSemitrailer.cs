using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Exceptions;
using AutoparkLibrary.Products;

namespace AutoparkLibrary.Transport
{
    /// <summary>
    /// Refrigirator semi-trailer.
    /// </summary>
    public class RefrigiratorSemitrailer : Semitrailer
    {
        private Product.ProductType type;
        private double temp_min, temp_max;

        /// <summary>
        /// Loading products to the semi-trailer.
        /// </summary>
        /// <param name="product">Products which will be uploaded.</param>
        public override void Upload(Product product)
        {
            if (product.StorageCondition != Product.ConditionOfStorage.Thermal)
                throw new InvalidProductStorageConditionException("The semi-trailer can be loaded only with products with temperature conditions");
            if (Products.Count > 0 && product.Type != type)
                throw new InvalidProductTypeException("The semi-trailer has products with another product type");
            if (Products.Count > 0 && (product.TemperatureMin <= temp_min || product.TemperatureMax >= temp_max))
                throw new InvalidProductStorageConditionException("Product not suitable for semi-trailer with already setted temperature condition");
            if (product.Volume <= FreeVolume && product.Weight <= FreeWeight)
            {
                if (Truck == null)
                {
                    AddProduct(product);
                    type = product.Type;
                    _SetTemperature();
                }
                else if (GetProductsWeight() + SemitrailerWeight + product.Weight <= Truck.CarryingCapacity)
                {
                    AddProduct(product);
                    type = product.Type;
                    _SetTemperature();
                }
                else
                    throw new TruckCarryingCapacityOverflowException("Adding a product will overload the attached truck");
            }
            else
                throw new SemitrailleMaxDimensionsOverflowException("The semi-trailer cannot be loaded with the weight or volume of this load");
        }

        /// <summary>
        /// Unloading products from the semi-trailer.
        /// </summary>
        /// <param name="productsUnloaded">List of product which will be unload.</param>
        public override void Unload(out List<Product> productsUnloaded)
        {
            if (Products.Count > 0)
            {
                productsUnloaded = new List<Product>();
                foreach (Product product in Products)
                    productsUnloaded.Add(product);
                Products.Clear();
                FreeVolume = MaxProductsVolume;
                FreeWeight = MaxProductsWeight;
            }
            else
                throw new NoProductsLoadedException("There are no loaded products in the semi-trailer");
        }

        /// <summary>
        /// Unloading a part of specific product from the semi-trailer.
        /// </summary>
        /// <param name="product">Specific product.</param>
        /// <param name="percentPart">A part of specific product which will unloaded.</param>
        /// <param name="productUnloaded">Unloaded product.</param>
        public override void Unload(Product product, double percentPart, out Product productUnloaded)
        {
            if (Product.FindProductBySpecificProduct(product, Products, out int indexOfFoundProduct))
            {
                RemoveProduct(indexOfFoundProduct);
                productUnloaded = null;
                if (percentPart == 100)
                {
                    productUnloaded = (Product)product.Clone();
                    _SetTemperature();
                }
                else if (percentPart > 100 || percentPart <= 0)
                {
                    productUnloaded = new Product(product.Name, product.Type, product.StorageCondition, product.Weight * percentPart / 100, product.Volume * percentPart / 100);
                    Product productLoded = new Product(product.Name, product.Type, product.StorageCondition, product.Weight * (100 - percentPart) / percentPart, product.Volume * (100 - percentPart) / 100);
                    AddProduct(productLoded);
                    _SetTemperature();
                }
                else
                    throw new ArgumentException("Invalid part percent of product");
            }
            else
                throw new NoProductsLoadedException($"There is no {product.Name} in semi-trailer");
        }

        /// <summary>
        /// Unloading a specific product from the semi-trailer.
        /// </summary>
        /// <param name="product">Specific product.</param>
        /// <param name="productUnloaded">Unloaded product.</param>
        public override void Unload(Product product, out Product productUnloaded)
        {
            Unload(product, 100, out productUnloaded);
        }

        /// <summary>
        /// Constructor of RefrigiratorSemitrailer type.
        /// </summary>
        /// <param name="garageId">The semi-trailer Garage ID</param>
        /// <param name="semitrailerWeight">The semi-trailer weight.</param>
        /// <param name="maxProductstWeight">Maximum weight of products transported by the semi-trailer.</param>
        /// <param name="maxProductsVolume">Maximum volume of products transported by the semi-trailer.</param>
        public RefrigiratorSemitrailer(string garageId, double semitrailerWeight, double maxProductstWeight, double maxProductsVolume) :
            base(garageId, semitrailerWeight, maxProductstWeight, maxProductsVolume)
        {
            Type = SemitrailerType.RefrigiratorSemitrailer;
        }

        /// <summary>
        /// Setting temperature set in the refrigirator.
        /// </summary>
        private void _SetTemperature()
        {
            int count = Products.Count;
            if (count > 0)
            {
                temp_max = Products[0].TemperatureMax;
                temp_min = Products[0].TemperatureMin;
                for (int i = 1; i < count; i++)
                {
                    if (Products[i].TemperatureMax < temp_max)
                    {
                        temp_max = Products[i].TemperatureMax;
                    }
                    else if (Products[i].TemperatureMin > temp_min)
                    {
                        temp_min = Products[i].TemperatureMin;
                    }
                }
            }
        }

        /// <summary>
        /// Getting temperature set of refrigirator.
        /// </summary>
        /// <param name="temperatureMin">Minimum temptreture.</param>
        /// <param name="temperatureMax">Maximum temperature</param>
        public void GetTemperatureCondition(out double temperatureMin, out double temperatureMax)
        {
            temperatureMin = temp_min;
            temperatureMax = temp_max;
        }

        /// <summary>
        /// Getting clone of the refigirator semi-trailer.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            RefrigiratorSemitrailer refrigiratorClone = new RefrigiratorSemitrailer(GarageID, SemitrailerWeight, MaxProductsWeight, MaxProductsVolume);
            foreach(Product product in Products)
            {
                refrigiratorClone.Upload((Product)product.Clone());
            }
            return refrigiratorClone;
        }

        /// <summary>
        /// Getting the refrigirator semi-trailer converted to String.
        /// </summary>
        /// <returns>The semi-trailer converted to String.</returns>
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Comparing the refigirator semi-trailer with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to the refrigirator semi-trailer.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Getting hash code of the refrigirator semi-trailer.
        /// </summary>
        /// <returns>Hash code of the refrigirator semi-trailer.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
