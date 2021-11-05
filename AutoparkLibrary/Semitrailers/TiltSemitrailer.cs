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
            if (product.StorageCondition != Product.ConditionOfStorage.Box)
                throw new InvalidProductStorageConditionException("The semi-trailer can be loaded only with product with box storage condition");
            if (Products.Count > 0 && product.Type != type)
                throw new InvalidProductTypeException("The semi-trailer has products with another product type");
            if (product.Volume <= FreeVolume && product.Weight <= FreeWeight)
            {
                if (Truck == null)
                {
                    AddProduct(product);
                    type = product.Type;
                }
                else if (GetProductsWeight() + SemitrailerWeight + product.Weight <= Truck.CarryingCapacity)
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

        public override void Unload(Product product, double partPercent, out Product productUnloaded)
        {
            if (Product.FindProductByProductClone(product, Products, out int indexOfFoundProduct))
            {
                RemoveProduct(indexOfFoundProduct);
                productUnloaded = null;
                if (partPercent == 100)
                {
                    productUnloaded = (Product)product.Clone();
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

        public override void Unload(Product product, out Product productUnloaded)
        {
            Unload(product, 100, out productUnloaded);
        }

        public TiltSemitrailer(string garageId, double semitrailerWeight, double maxProductWeight, double maxProductVolume)
               : base(garageId, semitrailerWeight, maxProductWeight, maxProductVolume)
        {
            Type = SemitrailerType.TiltSemitrailer;
        }

        public override object Clone()
        {
            TiltSemitrailer tiltClone = new TiltSemitrailer(GarageID, SemitrailerWeight, MaxProductsWeight, MaxProductsVolume);
            foreach(Product product in Products)
            {
                tiltClone.Upload((Product)product.Clone());
            }
            return tiltClone;
        }
    }
}
