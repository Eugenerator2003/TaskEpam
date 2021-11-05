using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;
using AutoparkLibrary.IO;
using AutoparkLibrary.Exceptions;

namespace AutoparkLibrary
{
    public class Autopark
    {
        private readonly string _filePath;
        private IReader xmlReader;
        private IReader streamReader;
        private IWriter xmlWriter;
        private IWriter streamWriter;

        private List<TruckTractor> trucks;
        private List<Semitrailer> semitrailers;
        private List<Product> products;

        public List<TruckTractor> Trucks { get => trucks; }
        public List<Semitrailer> Semitrailers { get => semitrailers; }
        public List<Product> Products { get => products; }

        public void ShowAutopark(out List<TruckTractor> trucks, out List<Semitrailer> semitrailers, out List<Product> products)
        {
            trucks = this.trucks;
            semitrailers = this.semitrailers;
            products = this.products;
        }

        public Semitrailer FindSemitrailerByType(Semitrailer.SemitrailerType type)
        {
            Semitrailer found = null;
            foreach (Semitrailer semitrailer in semitrailers)
            {
                if (semitrailer.Type == type)
                {
                    found = semitrailer;
                    break;
                }
            }
            return found;
        }

        public Semitrailer FindSemitrailerByCharacteristics(double semitrailerWeight, double semitrailerMaxWeight, double semitrailerMaxVolume)
        {
            if (semitrailerWeight <= 0 || semitrailerMaxWeight <= 0 || semitrailerMaxVolume <= 0)
                throw new ArgumentException("Invalid semi-trailer characteristics");
            else
            {
                Semitrailer found = null;
                foreach (Semitrailer semitrailer in semitrailers)
                {
                    if (semitrailer.SemitrailerWeight == semitrailerWeight && semitrailer.MaxProductsWeight == semitrailerMaxWeight
                        && semitrailer.MaxProductsVolume == semitrailerMaxVolume)
                    {
                        found = semitrailer;
                        break;
                    }
                }
                return found;
            }
        }

        public List<TruckTractor> FindHitchesByProductType(Product.ProductType type)
        {
            List<TruckTractor> foundHitches = new List<TruckTractor>();
            foreach (TruckTractor truck in trucks)
            {
                if (truck.Semitrailer != null)
                {
                    foreach (Product product in truck.Semitrailer.Products)
                    {
                        if (product.Type == type)
                        {
                            foundHitches.Add(truck);
                            break;
                        }
                    }
                }
            }
            return foundHitches;
        }

        public List<TruckTractor> FindHitchesCanBeLoaded()
        {
            List<TruckTractor> foundHitches = new List<TruckTractor>();
            foreach (TruckTractor truck in trucks)
            {
                if (truck.Semitrailer != null && truck.Semitrailer.Products.Count == 0)
                {
                    foreach (Product product in products)
                    {
                        try
                        {
                            truck.Upload(product);
                            truck.Unload(product, out Product productUnloaded);
                            foundHitches.Add(truck);
                            break;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            return foundHitches;
        }

        public List<TruckTractor> FindHitchesCanBeLoadedFully()
        {
            List<TruckTractor> foundHitches = new List<TruckTractor>();
            foreach (TruckTractor truck in trucks)
            {
                if (truck.Semitrailer != null && truck.Semitrailer.Products.Count == 0)
                {
                    TruckTractor truckClone = (TruckTractor)truck.Clone();
                    foreach (Product product in products)
                    {
                        try
                        {
                            truckClone.Upload(product);
                        }
                        catch (TruckMaxWeightOverflowException)
                        {
                            break;
                        }
                        catch (SemitrailleMaxDimensionsOverflowException)
                        {
                            break;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    if (truckClone.Semitrailer.MaxProductsWeight == truckClone.Semitrailer.GetProductsWeight()
                        || truckClone.Semitrailer.MaxProductsVolume == truckClone.Semitrailer.GetProductsVolume())
                    {
                        foundHitches.Add(truck);
                    }
                }
            }
            return foundHitches;
        }

        public void Upload(string garageId, Product product)
        {
            if (Product.FindProductByProductClone(product, products, out int indexOfFoundProduct))
            {
                Product productCanBeUploaded = products[indexOfFoundProduct];
                SearchByGarageID(garageId, out TruckTractor truck, out Semitrailer semitrailer);
                if (truck != null || semitrailer != null)
                {
                    Semitrailer semitrailerUploaded = (truck != null) ? truck.Semitrailer : semitrailer;
                    semitrailerUploaded.Upload(productCanBeUploaded);
                    products.Remove(productCanBeUploaded);
                }
                else
                    throw new InvalidGarageIDException($"There is no transpot with \"{garageId}\" ID");
            }
            else
                throw new InvalidProductException($"There is no \"{product}\" in autopark");
        }

        public void Unload(string garageId)
        {
            SearchByGarageID(garageId, out TruckTractor truck, out Semitrailer semitrailer);
            if (truck != null || semitrailer != null)
            {
                Semitrailer semitrailerUnloaded = (truck != null) ? truck.Semitrailer : semitrailer;
                semitrailerUnloaded.Unload(out List<Product> productsUnloaded);
                products.AddRange(productsUnloaded);
            }
            else
                throw new InvalidGarageIDException($"There is no transpot with \"{garageId}\" ID");
        }

        public void Unload(string garageId, double partPercent, Product product)
        {
            SearchByGarageID(garageId, out TruckTractor truck, out Semitrailer semitrailer);
            if (truck != null || semitrailer != null)
            {
                Semitrailer semitrailerUnloaded = (truck != null) ? truck.Semitrailer : semitrailer;
                semitrailerUnloaded.Unload(product, partPercent, out Product productUnloaded);
                products.Add(productUnloaded);

            }
            else
                throw new InvalidGarageIDException($"There is no transpot with \"{garageId}\" ID");
        }

        public void Unload(string garageId, Product product)
        {
            Unload(garageId, 100, product);
        }

        public void Attach(string garageId1, string garageId2)
        {
            SearchByGarageID(garageId1, out TruckTractor truckSearched1, out Semitrailer semitrailerSearched1);
            SearchByGarageID(garageId2, out TruckTractor truckSearched2, out Semitrailer semitrailerSearched2);
            if (truckSearched1 != null && truckSearched2 != null || semitrailerSearched1 != null && semitrailerSearched2 != null)
                throw new InvalidGarageIDException("It's impossible to attach transport of the same type");
            if (truckSearched1 != null)
                truckSearched1.AttachSemitrailer(semitrailerSearched2);
            else
                semitrailerSearched1.AttachTruck(truckSearched2);
        }

        public void Unhook(string garageId)
        {
            SearchByGarageID(garageId, out TruckTractor truckSerached, out Semitrailer semitrailerSearched);
            if (truckSerached != null)
                truckSerached.UnhookSemitrailer();
            else if (semitrailerSearched != null)
                semitrailerSearched.UnhookTruck();
            else
                throw new InvalidGarageIDException($"There is no transport with \"{garageId}\" ID");

        }

        public void AddTruck(TruckTractor truck)
        {
            SearchByGarageID(truck.GarageID, out TruckTractor truckSearched, out Semitrailer semitrailerSerached);
            if (truckSearched != null || semitrailerSerached != null)
            {
                throw new InvalidGarageIDException($"Transport with \"{truck.GarageID}\" ID is already exist");
            }
            trucks.Add(truck);
            if (truck.Semitrailer != null)
            {
                try
                {
                    AddSemitrailer(truck.Semitrailer);
                }
                catch
                {

                }
            }
        }

        public void AddSemitrailer(Semitrailer semitrailer)
        {
            SearchByGarageID(semitrailer.GarageID, out TruckTractor truckSearched, out Semitrailer semitrailerSerached);
            if (truckSearched != null || semitrailerSerached != null)
            {
                throw new InvalidGarageIDException($"Transport with \"{semitrailer.GarageID}\" ID is already exist");
            }
            semitrailers.Add(semitrailer);
            if (semitrailer.Truck != null)
            {
                try
                {
                    AddTruck(semitrailer.Truck);
                }
                catch
                {

                }
            }
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }
        public void SaveAutoparkStream()
        {
            streamWriter.Write(trucks, semitrailers, products);
        }

        public void SaveAutoparkXML()
        {
            xmlWriter.Write(trucks, semitrailers, products);
        }

        public void LoadAutoparkStream()
        {
            streamReader.Read(out trucks, out semitrailers, out products);
        }

        public void LoadAutoparkXML()
        {
            xmlReader.Read(out trucks, out semitrailers, out products);
        }

        private void SearchByGarageID(string garageId, out TruckTractor truckSearched, out Semitrailer semitrailerSearched)
        {
            truckSearched = null;
            semitrailerSearched = null;
            foreach (TruckTractor truck in trucks)
            {
                if (truck.GarageID == garageId)
                {
                    truckSearched = truck;
                }
            }

            foreach (Semitrailer semitrailer in semitrailers)
            {
                if (semitrailer.GarageID == garageId)
                {
                    semitrailerSearched = semitrailer;
                }
            }
        }

        public Autopark(string filePath)
        {
            _filePath = filePath;
            streamReader = new AutoparkStreamReader(_filePath);
            streamWriter = new AutoparkStreamWriter(_filePath);
            xmlWriter = new AutoparkXmlWriter(_filePath);
            xmlReader = new AutoparkXmlReader(_filePath);
            trucks = new List<TruckTractor>();
            semitrailers = new List<Semitrailer>();
            products = new List<Product>();
        }

        public Autopark() : this(@"E:\autopark.xml")
        {
        }
    }
}
