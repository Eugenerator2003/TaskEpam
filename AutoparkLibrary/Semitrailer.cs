using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autopark.Products;
using Autopark.Exceptions;

namespace Autopark.Transport
{
    public abstract class Semitrailer
    {
        public string ID { get; private set; }
        public int SemitrailerQuantity { get; private set; }
        public TruckTractor Truck { get; private set; }
        public double SemitrailerWeight { get; }
        public double MaxProductsWeight { get; }
        public double MaxProductsVolume { get; }
        public double FreeProductsVolume { get; private set; }
        public double FreeProductWeight { get; private set; }

        private List<Product> products = new List<Product>();

        public void AttachTruck(TruckTractor truck)
        {
            if (SemitrailerWeight + GetProductsWeight() <= truck.MaxLoadedSemitrailerWeight)
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

        public abstract bool Upload(Product product);

        public abstract bool Unload();

        public double GetProductsWeight()
        {
            double weight = 0;
            foreach(Product product in products)
            {
                weight += product.Weight;
            }
            return weight;
        }

    }
}
