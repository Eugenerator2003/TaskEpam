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
    /// Truck tractor.
    /// </summary>
    public class TruckTractor : ICloneable
    {
        /// <summary>
        /// Fuel cinsumprion of the truck.
        /// </summary>
        public readonly double FuelConsumption;

        /// <summary>
        /// Garage ID of the the truck.
        /// </summary>
        public string GarageID { get; }

        /// <summary>
        /// Model of the truck.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Semitrailer hitched to the truck.
        /// </summary>
        public Semitrailer Semitrailer { get; private set; }

        /// <summary>
        /// Carrying capacity of the truck.
        /// </summary>
        public double CarryingCapacity { get; }

        /// <summary>
        /// Hitching the semi-trailer to the truck.
        /// </summary>
        /// <param name="semitrailer">The semi-trailer.</param>
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
                throw new TruckCarryingCapacityOverflowException("Truck cannot be hooked to heavier semitrailer");
        }

        /// <summary>
        /// Unhitching the semi-trailer from the truck.
        /// </summary>
        public void UnhookSemitrailer()
        {
            Semitrailer semitrailer = Semitrailer;
            Semitrailer = null;
            if (semitrailer.Truck != null)
                semitrailer.UnhookTruck();
        }

        /// <summary>
        /// Loading product to the semi-trailer hitched to the truck.
        /// </summary>
        /// <param name="product">Product</param>
        public void Upload(Product product)
        {
            if (Semitrailer == null)
                throw new NoSemitrailerException("Truck doesn't have a trailer");
            Semitrailer.Upload(product); 
        }

        /// <summary>
        /// Unloading a specific priduct from the semi-trailer hitched to the truck.
        /// </summary>
        /// <param name="product">Specific product.</param>
        /// <param name="productUnloaded">Unloaded product.</param>
        public void Unload(Product product, out Product productUnloaded)
        {
            if (Semitrailer == null)
                throw new NoSemitrailerException("Truck doesn't have a trailer");
            Semitrailer.Unload(product, out productUnloaded);
        }

        /// <summary>
        /// Unloading a part of the specific product from the semi-trailer hitched to the truck.
        /// </summary>
        /// <param name="product">Specific product.</param>
        /// <param name="percentPart">A part of specific product which will unloaded.</param>
        /// <param name="productUnloaded">Unloaded product.</param>
        public void Unload(Product product, double percentPart, out Product productUnloaded)
        {
            if (Semitrailer == null)
                throw new NoSemitrailerException("Truck doesn't have a trailer");
            Semitrailer.Unload(product, percentPart, out productUnloaded);
        }

        /// <summary>
        /// Unloading products from the semi-trailer hitched to the truck.
        /// </summary>
        /// <param name="productsUnloaded">List of unloaded produtcs.</param>
        public void Unload(out List<Product> productsUnloaded)
        {
            if (Semitrailer == null)
                throw new NoSemitrailerException("Truck doesn't have a trailer");
            Semitrailer.Unload(out productsUnloaded);
        }

        /// <summary>
        /// Getting fuel consumption of the truck.
        /// </summary>
        /// <returns>Fuel consumption of the truck.</returns>
        public double GetFuelConsumption()
        {
            double semitrailerWeight = (Semitrailer == null) ? 0 : Semitrailer.SemitrailerWeight + Semitrailer.GetProductsWeight();
            return FuelConsumption * semitrailerWeight;
        }


        /// <summary>
        /// Constructor of truck tractor.
        /// </summary>
        /// <param name="garageId">Garage ID.</param>
        /// <param name="model">Model name.</param>
        /// <param name="carryingCapacity">Carrying capacity of the truck.</param>
        /// <param name="fuelConsumption">Fuel consumption of the truck.</param>
        public TruckTractor(string garageId, string model, double carryingCapacity, double fuelConsumption)
        {
            GarageID = garageId;
            Model = model;
            this.FuelConsumption = fuelConsumption;
            CarryingCapacity = carryingCapacity;
        }

        /// <summary>
        /// Constructor of truck tractor.
        /// </summary>
        /// <param name="garageId">Garage ID.</param>
        /// <param name="model">Model name.</param>
        /// <param name="maxLoadedSemitrailerWeight">Maximum loaded semi-trailer weight.</param>
        /// <param name="fuelConsumption">Fuel consumption of the truck.</param>
        /// <param name="semitrailer">The semitrailer hitched to the truck.</param>
        public TruckTractor(string garageId, string model, double maxLoadedSemitrailerWeight, double fuelConsumption, Semitrailer semitrailer)
                            : this(garageId, model, maxLoadedSemitrailerWeight, fuelConsumption)
        {
            AttachSemitrailer(semitrailer);
        }

        /// <summary>
        /// Comparing the truck with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is eqaul to the truck.</returns>
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

        /// <summary>
        /// Getting clone of the truck.
        /// </summary>
        /// <returns>Clone of the truck.</returns>
        public object Clone()
        {
            TruckTractor truckClone = new TruckTractor(GarageID, Model, CarryingCapacity, FuelConsumption);
            if (this.Semitrailer != null)
            {
                truckClone.AttachSemitrailer((Semitrailer)Semitrailer.Clone());
            }
            return (object)truckClone;
        }

        /// <summary>
        /// Getting the truck converted to String.
        /// </summary>
        /// <returns>Truck converted to String.</returns>
        public override string ToString()
        {
            string semitrailerStr = (Semitrailer != null) ? $" Semi-trailer: {Semitrailer.GarageID}" : "";
            return $"Truck - ID: {GarageID}; Model: {Model}; Carrying capacity: {CarryingCapacity}; Fuel Consumption: {FuelConsumption};"
                   + semitrailerStr;
        }

        /// <summary>
        /// Getting hash code of the truck.
        /// </summary>
        /// <returns>Hash code of the truck.</returns>
        public override int GetHashCode()
        {
            int hashCode = -1879307251;
            hashCode = hashCode * -1521134295 + FuelConsumption.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GarageID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Model);
            hashCode = hashCode * -1521134295 + EqualityComparer<Semitrailer>.Default.GetHashCode(Semitrailer);
            hashCode = hashCode * -1521134295 + CarryingCapacity.GetHashCode();
            return hashCode;
        }
    }
}
