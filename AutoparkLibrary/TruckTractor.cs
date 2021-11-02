using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autopark.Products;
using Autopark.Exceptions;

namespace Autopark.Transport
{
    public class TruckTractor
    {
        public string GarageID { get; private set; }
        public static int TruckQuantity{ get; private set; }
        public Semitrailer Semitrailer { get; private set; }
        public int SemitrailerCapacity { get; private set; }

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

        public bool Upload(Product product)
        {
            if (Semitrailer == null)
                throw new NoTrailerException("Truck doesn't have a trailer");
            return Semitrailer.Upload(product);
        }

        public bool Unload(Product product)
        {
            if (Semitrailer == null)
                throw new NoTrailerException("Truck doesn't have a trailer");
            return Semitrailer.Upload(product);
        }

        public TruckTractor(string garageId)
        {
            this.GarageID = garageId;
            TruckQuantity++;
        }

        public TruckTractor(string garageId, Semitrailer semitrailer) : this(garageId)
        {
            AttachSemitrailer(semitrailer);
        }

        public TruckTractor(string garageId, Semitrailer semitrailer, Product product) : this(garageId, semitrailer)
        {
            Semitrailer.Upload(product);
        }
    }
}
