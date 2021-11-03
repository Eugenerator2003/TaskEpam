using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Products;
using AutoparkLibrary.Exceptions;

namespace AutoparkLibrary.Transport
{
    public class TruckTractor
    {
        private readonly double fuelConsumptionPerKG;
        private readonly double weight;
        public string GarageID { get; private set; }
        public static int TruckQuantity{ get; private set; }
        public Semitrailer Semitrailer { get; private set; }
        public double MaxLoadedSemitrailerWeight { get; }
        

        public void AttachSemitrailer(Semitrailer semitrailer)
        {
            if (MaxLoadedSemitrailerWeight >= semitrailer.SemitrailerWeight + semitrailer.GetProductsWeight())
            {
                if (Semitrailer != null)
                {
                    Semitrailer.UnhookTruck();
                }
                Semitrailer = semitrailer;
                if (Semitrailer.Truck != this)
                    Semitrailer.AttachTruck(this);
            }
            else
                throw new TruckMaxWeightOverflowException("Truck cannot be hooked to heavier semitrailer");
        }

        public void UnhookSemitrailer()
        {
            if (Semitrailer.Truck != null)
                Semitrailer.UnhookTruck();
            Semitrailer = null;
        }

        public void Upload(Product product)
        {
            if (Semitrailer == null)
                throw new NoTrailerException("Truck doesn't have a trailer");
            Semitrailer.Upload(product); 
        }

        public void Unload(Product product, out Product productUnloaded)
        {
            if (Semitrailer == null)
                throw new NoTrailerException("Truck doesn't have a trailer");
            Semitrailer.Unload(product, out productUnloaded);
        }

        public TruckTractor(string garageId, double weight, double maxLoadedSemitrailerWeight, double fuelConsumptionPerKG)
        {
            GarageID = garageId;
            this.fuelConsumptionPerKG = fuelConsumptionPerKG;
            this.weight = weight;
            MaxLoadedSemitrailerWeight = maxLoadedSemitrailerWeight;
            TruckQuantity++;
        }

        public TruckTractor(string garageId, double weight, double maxLoadedSemitrailerWeight, double fuelConsumptionPerKG, Semitrailer semitrailer)
                            : this(garageId, weight, maxLoadedSemitrailerWeight, fuelConsumptionPerKG)
        {
            AttachSemitrailer(semitrailer);
        }

        public TruckTractor(string garageId, double weight, double maxLoadedSemitrailerWeight, double fuelConsumptionPerKG, Semitrailer semitrailer, Product product)
            : this(garageId, weight, maxLoadedSemitrailerWeight, fuelConsumptionPerKG, semitrailer)
        {
            Semitrailer.Upload(product);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            TruckTractor truck = obj as TruckTractor;
            if (truck == null)
                return false;
            bool semitrailersEqual = this.Semitrailer == null && truck.Semitrailer == null ? true : this.Semitrailer.Equals(truck.Semitrailer);
            return this.weight == truck.weight
                   && this.MaxLoadedSemitrailerWeight == truck.MaxLoadedSemitrailerWeight
                   && this.fuelConsumptionPerKG == truck.fuelConsumptionPerKG
                   && semitrailersEqual;
        }
    }
}
