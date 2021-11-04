using AutoparkLibrary.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Exceptions;

namespace AutoparkLibrary.Transport
{
    public class TankSemitrailer : Semitrailer
    {
        private string liquidName;

        public override void Upload(Product product)
        {
            if (product.StorageCondition != Product.ConditionOfStorage.Liquid)
                throw new InvalidProductStorageConditionException("The semi-trailer can be loaded only with liquid product");
            if (product.Volume <= FreeProductsVolume && product.Weight <= FreeProductsWeight)
            {
                if (liquidName == "")
                {
                    if (Truck == null)
                    {
                        AddProduct(product);
                        liquidName = product.Name;
                    }
                    else if (GetProductsWeight() + SemitrailerWeight + product.Weight <= Truck.CarryingCapacity)
                    {
                        AddProduct(product);
                        liquidName = product.Name;
                    }
                    else
                        throw new TruckMaxWeightOverflowException("Adding a product will overload the attached truck");
                }
                else
                {
                    if (product.Name == liquidName)
                    {
                        Product productWhole = new Product(product.Name, product.Type, product.StorageCondition, Products[0].Weight + product.Weight, Products[0].Volume + product.Volume);
                        if (Truck == null)
                        {
                            RemoveProduct(Products[0]);
                            AddProduct(productWhole);
                        }
                        else if (GetProductsWeight() + SemitrailerWeight + product.Weight <= Truck.CarryingCapacity)
                        {
                            RemoveProduct(Products[0]);
                            AddProduct(productWhole);
                        }
                        else
                            throw new TruckMaxWeightOverflowException("Adding a product will overload the attached truck");
                    }
                    else
                        throw new InvalidProductStorageConditionException("Semi-trailer already loaded with another liquid");
                }
            }
            else
                throw new SemitrailleMaxDimensionsOverflowException("The semi-trailer cannot be loaded with the weight or volume of this load");
        }

        public override void Unload(out List<Product> productsUnloaded)
        {
            if (Products.Count > 0)
            {
                productsUnloaded = new List<Product>();
                productsUnloaded.Add(Products[0]);
                Products.Clear();
                FreeProductsVolume = MaxProductsVolume;
                FreeProductsWeight = MaxProductsWeight;
                liquidName = "";
            }
            else
                throw new NoProductsLoadedException("There is no liquid in the semi-trailer");
        }

        public override void Unload(Product product, out Product productUnloaded)
        {
            Unload(product, 100, out productUnloaded);
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
                    liquidName = "";
                }
                else if (partPercent > 100 || partPercent <= 0)
                {
                    productUnloaded = new Product(product.Name, product.Type, product.StorageCondition, product.Weight * partPercent / 100, product.Volume * partPercent / 100);
                    Product productLoded = new Product(product.Name, product.Type, product.StorageCondition, product.Weight * (100 - partPercent) / partPercent, product.Volume * (100 - partPercent) / 100);
                    AddProduct(productLoded);
                }
                else
                    throw new ArgumentException("Invalid part percent of product");
            }
            else
                throw new NoProductsLoadedException($"There is no {product.Name} in semi-trailer");
        }


        public TankSemitrailer(string ID, double semitrailerWeight, double maxProductWeight, double maxProductVolume) : base(ID, semitrailerWeight, maxProductWeight, maxProductVolume)
        {
            Type = SemitrailerType.TankSemitrailer;   
        }

    }
}
