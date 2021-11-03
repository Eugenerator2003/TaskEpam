using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Products;
using AutoparkLibrary.Exceptions;

namespace AutoparkLibrary.Transport
{
    public class TiltSemitrailer : Semitrailer
    {
        private Product.ProductType type;
        public override void Upload(Product product)
        {
            if (product.Condition != Product.StorageCondition.Box)
                throw new InvalidProductStorageConditionException("The semi-trailer can be loaded only with product with box storage condition");
            if (products.Count > 0 && product.Type != type)
                throw new InvalidProductTypeException("The semi-trailer has products with another product type");
            if (product.Volume <= FreeProductsVolume && product.Weight <= FreeProductsWeight)
            {
                if (Truck == null)
                {
                    AddProduct(product);
                    type = product.Type;
                }
                else if (GetProductsWeight() + SemitrailerWeight + product.Weight <= Truck.MaxLoadedSemitrailerWeight)
                {
                    AddProduct(product);
                    type = product.Type;
                }
                else
                    throw new TruckMaxWeightOverflowException("Adding a product will overload the attached truck");
            }
            else
                throw new SemitrailleMaxDimensionsOverflowException("The semi-trailer cannot be loaded with the weight or volume of this load");
        }

        public override void Unload(out List<Product> productsUnloaded)
        {
            if (products.Count > 0)
            {
                productsUnloaded = new List<Product>();
                foreach (Product product in products)
                    productsUnloaded.Add(product);
                products.Clear();
                FreeProductsVolume = MaxProductsVolume;
                FreeProductsWeight = MaxProductsWeight;
            }
            else
                throw new NoProductsLoadedException("There are no loaded products in the semi-trailer");
        }

        public override void Unload(Product product, double partPercent, out Product productUnloaded)
        {
            if (products.Contains(product))
            {
                RemoveProduct(product);
                productUnloaded = null;
                if (partPercent == 100)
                {
                    productUnloaded = (Product)product.Clone();
                }
                else if (partPercent > 100 || partPercent <= 0)
                {
                    productUnloaded = new Product(product.Name, product.Type, product.Condition, product.Weight * partPercent / 100, product.Volume * partPercent / 100);
                    Product productLoded = new Product(product.Name, product.Type, product.Condition, product.Weight * (100 - partPercent) / partPercent, product.Volume * (100 - partPercent) / 100);
                    AddProduct(productLoded);
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

        public TiltSemitrailer(string ID, double semitrailerWeight, double maxProductWeight, double maxProductVolume)
               : base(ID, semitrailerWeight, maxProductWeight, maxProductVolume)
        {
        }
    }
}
