using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Products;
using AutoparkLibrary.Exceptions;

namespace AutoparkLibrary.Transport
{
    /// <summary>
    /// Tilt semi-trailer.
    /// </summary>
    public class TiltSemitrailer : Semitrailer
    {
        private Product.ProductType type;

        /// <summary>
        /// Loading products to the semi-trailer.
        /// </summary>
        /// <param name="product">Products which will be uploaded.</param>
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
                }
                else if (percentPart < 100 && percentPart > 0)
                {
                    productUnloaded = new Product(product.Name, product.Type, product.StorageCondition, product.Weight * percentPart / 100, product.Volume * percentPart / 100);
                    Product productLoded = new Product(product.Name, product.Type, product.StorageCondition, product.Weight * (100 - percentPart) / percentPart, product.Volume * (100 - percentPart) / 100);
                    AddProduct(productLoded);
                }
                else
                    throw new ArgumentException($"Invalid part percent (\"{percentPart}\") of product");
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
        /// Constructor of TiltSemitrailer type.
        /// </summary>
        /// <param name="garageId">The semi-trailer garage ID</param>
        /// <param name="semitrailerWeight">The semi-trailer weight.</param>
        /// <param name="maxProductsWeight">Maximum weight of products transported by the semi-trailer.</param>
        /// <param name="maxProductsVolume">Maximum volume of products transported by the semi-trailer.</param>
        public TiltSemitrailer(string garageId, double semitrailerWeight, double maxProductsWeight, double maxProductsVolume)
               : base(garageId, semitrailerWeight, maxProductsWeight, maxProductsVolume)
        {
            Type = SemitrailerType.TiltSemitrailer;
        }

        /// <summary>
        /// Getting clone of the tilt semi-trailer.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            TiltSemitrailer tiltClone = new TiltSemitrailer(GarageID, SemitrailerWeight, MaxProductsWeight, MaxProductsVolume);
            foreach(Product product in Products)
            {
                tiltClone.Upload((Product)product.Clone());
            }
            return tiltClone;
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
