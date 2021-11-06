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
    /// Semi-trailer.
    /// </summary>
    public abstract class Semitrailer : ICloneable
    {
        /// <summary>
        /// Types of semi-trailer.
        /// </summary>
        public enum SemitrailerType
        {
            /// <summary>
            /// Semi-trailer type used for transportation different product types without special temperature set.
            /// </summary>
            TiltSemitrailer,
            /// <summary>
            /// Semi-trailer type used for transportation any liquid product.
            /// </summary>
            TankSemitrailer,
            /// <summary>
            /// Semi-trailer type used for transportation different product types with special temperature set.
            /// </summary>
            RefrigiratorSemitrailer
        }

        /// <summary>
        /// Type of the semi-trailer.
        /// </summary>
        public SemitrailerType Type { get; protected set; }

        /// <summary>
        /// Garage ID of the semi-trailer.
        /// </summary>
        public string GarageID { get; private set; }

        /// <summary>
        /// Truck hitched to the semi-trailer.
        /// </summary>
        public TruckTractor Truck { get; protected set; }

        /// <summary>
        /// Weight of the semi-trailer.
        /// </summary>
        public double SemitrailerWeight { get; }

        /// <summary>
        /// Maximum weight of products transported by the semi-trailer.
        /// </summary>
        public double MaxProductsWeight { get; }

        /// <summary>
        /// Maximum volume of product transported by the semi-trailer.
        /// </summary>
        public double MaxProductsVolume { get; }
        
        /// <summary>
        /// Free volume of the semi-trailer.
        /// </summary>
        public double FreeVolume { get; protected set; }
        
        /// <summary>
        /// Free weight of the semi-trailer.
        /// </summary>
        public double FreeWeight { get; protected set; }

        /// <summary>
        /// List of products transported by the semi-trailer.
        /// </summary>
        private List<Product> products;

        /// <summary>
        /// List of products transported by the semi-trailer.
        /// </summary>
        public List<Product> Products { get => products; }

        /// <summary>
        /// Hitching the truck to the semi-trailer.
        /// </summary>
        /// <param name="truck">Truck which will be attached to semi-trailer.</param>
        public void AttachTruck(TruckTractor truck)
        {
            if (SemitrailerWeight + GetProductsWeight() <= truck.CarryingCapacity)
            {
                if (Truck != null)
                {
                    Truck.UnhookSemitrailer();
                }
                Truck = truck;
                if (truck.Semitrailer != this)
                    truck.AttachSemitrailer(this);
            }
            else
                throw new TruckCarryingCapacityOverflowException("Truck cannot be hooked to heavier semitrailer");

        }

        /// <summary>
        /// Unhitching the truck from the semi-trailer.
        /// </summary>
        public void UnhookTruck()
        {
            if (Truck.Semitrailer != null)
                Truck.UnhookSemitrailer();
            Truck = null;
        }

        /// <summary>
        /// Loading products to the semi-trailer.
        /// </summary>
        /// <param name="product">Products which will be uploaded.</param>
        public abstract void Upload(Product product);

        /// <summary>
        /// Unloading products from the semi-trailer.
        /// </summary>
        /// <param name="productsUnloaded">List of product which will be unload.</param>
        public abstract void Unload(out List<Product> productsUnloaded);

        /// <summary>
        /// Unloading a specific product from the semi-trailer.
        /// </summary>
        /// <param name="product">Specific product.</param>
        /// <param name="productUnloaded">Unloaded product.</param>
        public abstract void Unload(Product product, out Product productUnloaded);

        /// <summary>
        /// Unloading a part of specific product from the semi-trailer.
        /// </summary>
        /// <param name="product">Specific product.</param>
        /// <param name="percentPart">A part of specific product which will unloaded.</param>
        /// <param name="productUnloaded">Unloaded product.</param>
        public abstract void Unload(Product product, double percentPart, out Product productUnloaded);

        /// <summary>
        /// Adding product to the list of products.
        /// </summary>
        /// <param name="product">Product.</param>
        protected void AddProduct(Product product)
        {
            Products.Add(product);
            FreeWeight -= product.Weight;
            FreeVolume -= product.Volume;
        }

        /// <summary>
        /// Removing product from the list of products.
        /// </summary>
        /// <param name="product"></param>
        protected void RemoveProduct(Product product)
        {
            Products.Remove(product);
            FreeWeight += product.Weight;
            FreeVolume += product.Volume;
        }

        /// <summary>
        /// Removing product from the list of products by it index.
        /// </summary>
        /// <param name="indexOfProduct">Index of product in the list of products.</param>
        protected void RemoveProduct(int indexOfProduct)
        {
            FreeWeight += Products[indexOfProduct].Weight;
            FreeVolume += Products[indexOfProduct].Volume;
            Products.RemoveAt(indexOfProduct);
        }

        /// <summary>
        /// Getting weight of the product which already uploaded to the semi-trailer.
        /// </summary>
        /// <returns></returns>
        public double GetProductsWeight()
        {
            return MaxProductsWeight - FreeWeight;
        }

        /// <summary>
        /// Getting volume of the product which already uploaded to the semi-trailer.
        /// </summary>
        /// <returns></returns>
        public double GetProductsVolume()
        {
            return MaxProductsVolume - FreeVolume;
        }

        /// <summary>
        /// Constructor of Semitrailer type.
        /// </summary>
        /// <param name="ID">The semi-trailer Garage ID</param>
        /// <param name="semitrailerWeight">The semi-trailer weight.</param>
        /// <param name="maxProductsWeight">Maximum weight of products transported by the semi-trailer.</param>
        /// <param name="maxProductsVolume">Maximum volume of products transported by the semi-trailer.</param>
        public Semitrailer(string ID, double semitrailerWeight, double maxProductsWeight, double maxProductsVolume)
        {
            this.GarageID = ID;
            SemitrailerWeight = semitrailerWeight;
            MaxProductsWeight = maxProductsWeight;
            MaxProductsVolume = maxProductsVolume;
            FreeVolume = maxProductsVolume;
            FreeWeight = maxProductsWeight;
            products = new List<Product>();
        }

        /// <summary>
        /// Getting clone of the semi-trailer.
        /// </summary>
        /// <returns>Clone of the semi-trailer.</returns>
        public abstract object Clone();

        /// <summary>
        /// Comparing the semi-trailer with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to the semi-trailer.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Semitrailer semitrailer = obj as Semitrailer;
            if (semitrailer == null)
                return false;
            bool productsEqual = true;
            if (Products.Count == semitrailer.Products.Count)
            {
                for (int i = 0; i < Products.Count; i++)
                {
                    if (!Products[i].Equals(semitrailer.Products[i]))
                    {
                        productsEqual = false;
                        break;
                    }
                }
            }
            else
                return false;
            return productsEqual
                   && this.Type == semitrailer.Type
                   && this.SemitrailerWeight == semitrailer.SemitrailerWeight
                   && this.MaxProductsVolume == semitrailer.MaxProductsVolume
                   && this.MaxProductsWeight == semitrailer.MaxProductsWeight;
        }

        /// <summary>
        /// Getting the semi-trailer converted to String.
        /// </summary>
        /// <returns>The semi-trailer converted to String.</returns>
        public override string ToString()
        {
            string truckStr = (Truck != null) ? $" Truck: {Truck.GarageID}" : "";
            return $"Semitrailer - ID {GarageID}; Type: {Type}; Weight: {SemitrailerWeight}; " +
                   $"Max products weight: {MaxProductsWeight}; Max products volume: {MaxProductsWeight}; " +
                   $"Free weight: {FreeWeight}; Free volume: {FreeVolume};" + truckStr;
        }

        /// <summary>
        /// Getting hash code of the semi-trailer.
        /// </summary>
        /// <returns>Hash code of the semi-trailer.</returns>
        public override int GetHashCode()
        {
            int hashCode = 939809021;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GarageID);
            hashCode = hashCode * -1521134295 + EqualityComparer<TruckTractor>.Default.GetHashCode(Truck);
            hashCode = hashCode * -1521134295 + SemitrailerWeight.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxProductsWeight.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxProductsVolume.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Product>>.Default.GetHashCode(products);
            return hashCode;
        }
    }
}
