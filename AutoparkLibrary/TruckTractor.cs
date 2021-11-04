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
        public readonly double FuelConsumption;
        public string GarageID { get; }
        public string Model { get; }
        public static int TruckQuantity{ get; private set; }
        public Semitrailer Semitrailer { get; private set; }
        public double CarryingCapacity { get; }

        public void AttachSemitrailer(Semitrailer semitrailer)
        {
            if (CarryingCapacity >= semitrailer.SemitrailerWeight + semitrailer.GetProductsWeight())
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

        public double GetFuelConsumption()
        {
            double semitrailerWeight = (Semitrailer == null) ? 0 : Semitrailer.SemitrailerWeight + Semitrailer.GetProductsWeight();
            return FuelConsumption * semitrailerWeight;
        }

        public TruckTractor(string garageId, string model, double maxLoadedSemitrailerWeight, double fuelConsumption)
        {
            GarageID = garageId;
            Model = model;
            this.FuelConsumption = fuelConsumption;
            CarryingCapacity = maxLoadedSemitrailerWeight;
            TruckQuantity++;
        }

        public TruckTractor(string garageId, string model, double maxLoadedSemitrailerWeight, double fuelConsumption, Semitrailer semitrailer)
                            : this(garageId, model, maxLoadedSemitrailerWeight, fuelConsumption)
        {
            AttachSemitrailer(semitrailer);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            TruckTractor truck = obj as TruckTractor;
            if (truck == null)
                return false;
            bool semitrailersEqual = this.Semitrailer == null && truck.Semitrailer == null ? true : this.Semitrailer.Equals(truck.Semitrailer);
            return this.Model == truck.Model
                   && this.CarryingCapacity == truck.CarryingCapacity
                   && this.FuelConsumption == truck.FuelConsumption
                   && semitrailersEqual;
        }
    }
}
