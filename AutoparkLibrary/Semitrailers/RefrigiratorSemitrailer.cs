using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Exceptions;
using AutoparkLibrary.Products;

namespace AutoparkLibrary.Transport
{
    public class RefrigiratorSemitrailer : Semitrailer
    {
        private Product.ProductType type;
        private double temp_min, temp_max;

        public override void Upload(Product product)
        {
            if (product.StorageCondition != Product.ConditionOfStorage.Thermal)
                throw new InvalidProductStorageConditionException("The semi-trailer can be loaded only with products with temperature conditions");
            if (Products.Count > 0 && product.Type != type)
                throw new InvalidProductTypeException("The semi-trailer has products with another product type");
            if (Products.Count > 0 && (product.TemperatureMin <= temp_min || product.TemperatureMax >= temp_max))
                throw new InvalidProductStorageConditionException("Product not suitable for semi-trailer with already setted temperature condition");
            if (product.Volume <= FreeProductsVolume && product.Weight <= FreeProductsWeight)
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
                    throw new TruckMaxWeightOverflowException("Adding a product will overload the attached truck");
            }
            else
                throw new SemitrailleMaxDimensionsOverflowException("The semi-trailer cannot be loaded with the weight or volume of this load");
        }

        public override void Unload(out List<Product> productsUnloaded)
        {
            if (Products.Count > 0)
            {
                productsUnloaded = new List<Product>();
                foreach (Product product in Products)
                    productsUnloaded.Add(product);
                Products.Clear();
                FreeProductsVolume = MaxProductsVolume;
                FreeProductsWeight = MaxProductsWeight;
            }
            else
                throw new NoProductsLoadedException("There are no loaded products in the semi-trailer");
        }

        public override void Unload(Product product, double partPercent, out Product productUnloaded)
        {
            if (Products.Contains(product))
            {
                RemoveProduct(product);
                productUnloaded = null;
                if (partPercent == 100)
                {
                    productUnloaded = (Product)product.Clone();
                    _SetTemperature();
                }
                else if (partPercent > 100 || partPercent <= 0)
                {
                    productUnloaded = new Product(product.Name, product.Type, product.StorageCondition, product.Weight * partPercent / 100, product.Volume * partPercent / 100);
                    Product productLoded = new Product(product.Name, product.Type, product.StorageCondition, product.Weight * (100 - partPercent) / partPercent, product.Volume * (100 - partPercent) / 100);
                    AddProduct(productLoded);
                    _SetTemperature();
                }
                else
                    throw new ArgumentException("Invalid part percent of product");
            }
            else
                throw new NoProductsLoadedException($"There is no {product.Name} in semi-trailer");
        }
        public override void Unload(Product product, out Product productUnloaded)
        {
            Unload(product, 100, out productUnloaded);
        }

        public RefrigiratorSemitrailer(string ID, double semitrailerWeight, double maxProductWeight, double maxProductVolume) : base(ID, semitrailerWeight, maxProductWeight, maxProductVolume)
        {
            Type = SemitrailerType.RefrigiratorSemitrailer;
        }

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

        public void GetTemperatureCondition(out double temperatureMin, out double temperatureMax)
        {
            temperatureMin = temp_min;
            temperatureMax = temp_max;
        }
    }
}
