using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Products;
using AutoparkLibrary.Exceptions;

namespace AutoparkLibrary.Transport
{
    public abstract class Semitrailer : ICloneable
    {
        public enum SemitrailerType
        {
            TiltSemitrailer,
            TankSemitrailer,
            RefrigiratorSemitrailer
        }

        public SemitrailerType Type { get; protected set; }

        public string GarageID { get; private set; }
        public int SemitrailerQuantity { get; protected set; }
        public TruckTractor Truck { get; protected set; }
        public double SemitrailerWeight { get; }
        public double MaxProductsWeight { get; }
        public double MaxProductsVolume { get; }
        public double FreeVolume { get; protected set; }
        public double FreeWeight { get; protected set; }

        public List<Product> Products = new List<Product>();

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
                throw new TruckMaxWeightOverflowException("Truck cannot be hooked to heavier semitrailer");

        }

        public void UnhookTruck()
        {
            if (Truck.Semitrailer != null)
                Truck.UnhookSemitrailer();
            Truck = null;
        }

        public abstract void Upload(Product product);

        public abstract void Unload(out List<Product> productsUnloaded);

        public abstract void Unload(Product product, out Product productUnloaded);

        public abstract void Unload(Product product, double partPercent, out Product productUnloaded);

        protected void AddProduct(Product product)
        {
            Products.Add(product);
            FreeWeight -= product.Weight;
            FreeVolume -= product.Volume;
        }

        protected void RemoveProduct(Product product)
        {
            Products.Remove(product);
            FreeWeight += product.Weight;
            FreeVolume += product.Volume;
        }

        protected void RemoveProduct(int indexOfProduct)
        {
            FreeWeight += Products[indexOfProduct].Weight;
            FreeVolume += Products[indexOfProduct].Volume;
            Products.RemoveAt(indexOfProduct);
        }

        public double GetProductsWeight()
        {
            return MaxProductsWeight - FreeWeight;
        }

        public double GetProductsVolume()
        {
            return MaxProductsVolume - FreeVolume;
        }

        public Semitrailer(string ID, double semitrailerWeight, double maxProductsWeight, double maxProductsVolume)
        {
            this.GarageID = ID;
            SemitrailerWeight = semitrailerWeight;
            MaxProductsWeight = maxProductsWeight;
            MaxProductsVolume = maxProductsVolume;
            FreeVolume = maxProductsVolume;
            FreeWeight = maxProductsWeight;
        }

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
                   && this.SemitrailerWeight == SemitrailerWeight
                   && this.MaxProductsVolume == semitrailer.MaxProductsVolume
                   && this.MaxProductsWeight == semitrailer.MaxProductsWeight;
        }

        public abstract object Clone();

        public override string ToString()
        {
            string truckStr = (Truck != null) ? $" Truck: {Truck.GarageID}" : "";
            return $"Semitrailer - ID {GarageID}; Type: {Type}; Weight: {SemitrailerWeight}; " +
                   $"Max products weight: {MaxProductsWeight}; Max products volume: {MaxProductsWeight}; " +
                   $"Free weight: {FreeWeight}; Free volume: {FreeVolume};" + truckStr;
        }
    }
}
