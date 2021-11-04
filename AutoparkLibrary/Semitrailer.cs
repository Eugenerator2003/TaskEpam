using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Products;
using AutoparkLibrary.Exceptions;

namespace AutoparkLibrary.Transport
{
    public abstract class Semitrailer
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
        public double FreeProductsVolume { get; protected set; }
        public double FreeProductsWeight { get; protected set; }

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
            FreeProductsWeight -= product.Weight;
            FreeProductsVolume -= product.Volume;
        }

        protected void RemoveProduct(Product product)
        {
            Products.Remove(product);
            FreeProductsWeight += product.Weight;
            FreeProductsVolume += product.Volume;
        }

        public double GetProductsWeight()
        {
            return MaxProductsWeight - FreeProductsWeight;
        }

        public double GetProductsVolume()
        {
            return MaxProductsVolume - FreeProductsVolume;
        }

        public Semitrailer(string ID, double semitrailerWeight, double maxProductsWeight, double maxProductsVolume)
        {
            this.GarageID = ID;
            SemitrailerWeight = semitrailerWeight;
            MaxProductsWeight = maxProductsWeight;
            MaxProductsVolume = maxProductsVolume;
            FreeProductsVolume = maxProductsVolume;
            FreeProductsWeight = maxProductsWeight;
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
    }
}
