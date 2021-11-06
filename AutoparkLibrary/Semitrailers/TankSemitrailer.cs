using AutoparkLibrary.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Exceptions;

namespace AutoparkLibrary.Transport
{
    /// <summary>
    /// Tank semi-trailer.
    /// </summary>
    public class TankSemitrailer : Semitrailer
    {
        /// <summary>
        /// Name of liquid already uploaded to the semi-trailer.
        /// </summary>
        public string LiquidName { get; private set; }

        /// <summary>
        /// Loading products to the semi-trailer.
        /// </summary>
        /// <param name="product">Products which will be uploaded.</param>
        public override void Upload(Product product)
        {
            if (product.StorageCondition != Product.ConditionOfStorage.Liquid)
                throw new InvalidProductStorageConditionException("The semi-trailer can be loaded only with liquid product");
            if (product.Volume <= FreeVolume && product.Weight <= FreeWeight)
            {
                if (LiquidName == "")
                {
                    if (Truck == null)
                    {
                        AddProduct(product);
                        LiquidName = product.Name;
                    }
                    else if (GetProductsWeight() + SemitrailerWeight + product.Weight <= Truck.CarryingCapacity)
                    {
                        AddProduct(product);
                        LiquidName = product.Name;
                    }
                    else
                        throw new TruckCarryingCapacityOverflowException("Adding a product will overload the attached truck");
                }
                else
                {
                    if (product.Name == LiquidName)
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
                            throw new TruckCarryingCapacityOverflowException("Adding a product will overload the attached truck");
                    }
                    else
                        throw new InvalidProductStorageConditionException("Semi-trailer already loaded with another liquid");
                }
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
                productsUnloaded.Add(Products[0]);
                Products.Clear();
                FreeVolume = MaxProductsVolume;
                FreeWeight = MaxProductsWeight;
                LiquidName = "";
            }
            else
                throw new NoProductsLoadedException("There is no liquid in the tank semi-trailer");
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
                    LiquidName = "";
                }
                else if (percentPart > 100 || percentPart <= 0)
                {
                    productUnloaded = new Product(product.Name, product.Type, product.StorageCondition, product.Weight * percentPart / 100, product.Volume * percentPart / 100);
                    Product productLoded = new Product(product.Name, product.Type, product.StorageCondition, product.Weight * (100 - percentPart) / percentPart, product.Volume * (100 - percentPart) / 100);
                    AddProduct(productLoded);
                }
                else
                    throw new ArgumentException("Invalid part percent of product");
            }
            else
                throw new NoProductsLoadedException($"There is no {product} in semi-trailer");
        }



        public TankSemitrailer(string ID, double semitrailerWeight, double maxProductWeight, double maxProductVolume) : base(ID, semitrailerWeight, maxProductWeight, maxProductVolume)
        {
            Type = SemitrailerType.TankSemitrailer;   
        }

        public override object Clone()
        {
            TankSemitrailer tankClone = new TankSemitrailer(GarageID, SemitrailerWeight, MaxProductsWeight, MaxProductsVolume);
            foreach(Product product in Products)
            {
                tankClone.Upload((Product)product.Clone());
            }
            return tankClone;
        }

        /// <summary>
        /// Getting the semi-trailer converted to String.
        /// </summary>
        /// <returns>The semi-trailer converted to String.</returns>
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Comparing the tlit semi-trailer with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to the tilt semi-trailer.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Getting hash code of the tilt semi-trailer.
        /// </summary>
        /// <returns>Hash code of the tilt semi-trailer.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
