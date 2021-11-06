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
    /// <summary>
    /// Autopark.
    /// </summary>
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

        /// <summary>
        /// List of trucks.
        /// </summary>
        public List<TruckTractor> Trucks { get => trucks; }

        /// <summary>
        /// List of semi-trailers.
        /// </summary>
        public List<Semitrailer> Semitrailers { get => semitrailers; }

        /// <summary>
        /// List of products.
        /// </summary>
        public List<Product> Products { get => products; }


        /// <summary>
        /// Getting lists of trucks, semi-trailers and products.
        /// </summary>
        /// <param name="trucks">Trucks.</param>
        /// <param name="semitrailers">Semi-traielrs.</param>
        /// <param name="products">Products.</param>
        public void ShowAutopark(out List<TruckTractor> trucks, out List<Semitrailer> semitrailers, out List<Product> products)
        {
            trucks = this.trucks;
            semitrailers = this.semitrailers;
            products = this.products;
        }

        /// <summary>
        /// Finding semi-trailer by type.
        /// </summary>
        /// <param name="type">Type of semi-trailer.</param>
        /// <returns>Semi-trailer if is was found or null if not.</returns>
        public Semitrailer GetSemitrailerByType(Semitrailer.SemitrailerType type)
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

        /// <summary>
        /// Finding semi-trailer by characteristics.
        /// </summary>
        /// <param name="semitrailerWeight">Weight of the semitrailer.</param>
        /// <param name="semitrailerMaxWeight">Maximum loaded products weight.</param>
        /// <param name="semitrailerMaxVolume">Maximum loaded products volume.</param>
        /// <returns>Semitrailer if it was found or null if not.</returns>
        public Semitrailer GetSemitrailerByCharacteristics(double semitrailerWeight, double semitrailerMaxWeight, double semitrailerMaxVolume)
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

        /// <summary>
        /// Finding hitches by product type.
        /// </summary>
        /// <param name="type">Product type.</param>
        /// <returns>List of trucks attached to semi-trailers uploaded with specific product type.</returns>
        public List<TruckTractor> GetHitchesByProductType(Product.ProductType type)
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

        /// <summary>
        /// Finding hitches which can be loaded.
        /// </summary>
        /// <returns>List of truck attached to semi-trailers which can be loaded.</returns>
        public List<TruckTractor> GetHitchesCanBeLoaded()
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

        /// <summary>
        /// Finding hitches which can be loaded fully.
        /// </summary>
        /// <returns>List of truck attached to semi-trailers which can be loaded fully.</returns>
        public List<TruckTractor> GetHitchesCanBeLoadedFully()
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
                        catch (TruckCarryingCapacityOverflowException)
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
                        || truckClone.Semitrailer.MaxProductsVolume == truckClone.Semitrailer.GetProductsVolume()
                        || truckClone.CarryingCapacity == truckClone.Semitrailer.GetProductsWeight())
                    {
                        foundHitches.Add(truck);
                    }
                }
            }
            return foundHitches;
        }

        /// <summary>
        /// Uploading specific product to transport by its garage ID.
        /// </summary>
        /// <param name="garageId">Garage ID.</param>
        /// <param name="product">Specific product.</param>
        public void Upload(string garageId, Product product)
        {
            if (Product.FindProductBySpecificProduct(product, products, out int indexOfFoundProduct))
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

        /// <summary>
        /// Unloading products from transport by its garage Id.
        /// </summary>
        /// <param name="garageId">Garage ID.</param>
        /// <returns>Products which wan unloaded.</returns>
        public List<Product> Unload(string garageId)
        {
            SearchByGarageID(garageId, out TruckTractor truck, out Semitrailer semitrailer);
            if (truck != null || semitrailer != null)
            {
                Semitrailer semitrailerUnloaded = (truck != null) ? truck.Semitrailer : semitrailer;
                semitrailerUnloaded.Unload(out List<Product> productsUnloaded);
                products.AddRange(productsUnloaded);
                return productsUnloaded;
            }
            else
                throw new InvalidGarageIDException($"There is no transpot with \"{garageId}\" ID");
        }

        /// <summary>
        /// Unloading part of product from transport by its garage ID.
        /// </summary>
        /// <param name="garageId">Garage ID.</param>
        /// <param name="percentPart">Percent part.</param>
        /// <param name="product">Specific produc.t</param>
        /// <returns>Product which was unloaded.</returns>
        public Product Unload(string garageId, double percentPart, Product product)
        {
            SearchByGarageID(garageId, out TruckTractor truck, out Semitrailer semitrailer);
            if (truck != null || semitrailer != null)
            {
                Semitrailer semitrailerUnloaded = (truck != null) ? truck.Semitrailer : semitrailer;
                semitrailerUnloaded.Unload(product, percentPart, out Product productUnloaded);
                products.Add(productUnloaded);
                return productUnloaded;
            }
            else
                throw new InvalidGarageIDException($"There is no transpot with \"{garageId}\" ID");
        }

        /// <summary>
        /// Unload product from transport by its garage ID.
        /// </summary>
        /// <param name="garageId">Garage ID</param>
        /// <param name="product">Specific product</param>
        public Product Unload(string garageId, Product product)
        {
            return Unload(garageId, 100, product);
        }

        /// <summary>
        /// Hitching two units of transport by their garage ID's.
        /// </summary>
        /// <param name="garageId1">First garage ID.</param>
        /// <param name="garageId2">Second garage ID.</param>
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

        /// <summary>
        /// Unhook transport by its garage ID.
        /// </summary>
        /// <param name="garageId">Garage ID.</param>
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

        /// <summary>
        /// Adding truck to autopark trucks collection.
        /// </summary>
        /// <param name="truck">Truck</param>
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

        /// <summary>
        /// Adding semi-trailer to autopark semitrailers collections.
        /// </summary>
        /// <param name="semitrailer">Semi-trailer</param>
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

        /// <summary>
        /// Adding product to autopark products collection.
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        /// <summary>
        /// Saving autopark status by StreamWriter.
        /// </summary>
        public void SaveAutoparkStream()
        {
            streamWriter.Write(trucks, semitrailers, products);
        }

        /// <summary>
        /// Saving autopark status by XmlWriter.
        /// </summary>
        public void SaveAutoparkXML()
        {
            xmlWriter.Write(trucks, semitrailers, products);
        }

        /// <summary>
        /// Loading autopark status by StreamReader.
        /// </summary>
        public void LoadAutoparkStream()
        {
            streamReader.Read(out trucks, out semitrailers, out products);
        }

        /// <summary>
        /// Loadding autopark status by XmlReader. 
        /// </summary>
        public void LoadAutoparkXML()
        {
            xmlReader.Read(out trucks, out semitrailers, out products);
        }

        /// <summary>
        /// Searching transport by its garage ID.
        /// </summary>
        /// <param name="garageId">Garage ID</param>
        /// <param name="truckSearched">Searched truck.</param>
        /// <param name="semitrailerSearched">Searhed semi-trailer.</param>
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

        /// <summary>
        /// Constructor of Autopark.
        /// </summary>
        /// <param name="filePath">File path for saving ang loading.</param>
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


        /// <summary>
        /// Constructor of autopark. File path is "E:\autopark.xml".
        /// </summary>
        public Autopark() : this(@"E:\autopark.xml")
        {
        }

        /// <summary>
        /// Comparing the autopark with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to the autopark.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Autopark autopark = obj as Autopark;
            if (autopark == null)
                return false;

            bool isEqual = false;
            int trucksCountThis = trucks.Count;
            int semitrailerCountThis = semitrailers.Count;
            int productsCountThis = products.Count;

            int trucksCount = autopark.trucks.Count;
            int semitrailerCount = autopark.semitrailers.Count;
            int productsCount = autopark.products.Count;

            if (trucksCountThis == trucksCount && semitrailerCountThis == semitrailerCount && productsCountThis == productsCount)
            {
                isEqual = true;
                for (int i = 0; i < trucksCount && isEqual; i++)
                {
                    if (!trucks[i].Equals(autopark.trucks[i]))
                    {
                        isEqual = false;
                    }
                }
                for(int i = 0; i < semitrailerCount && isEqual; i++)
                {
                    if (!semitrailers[i].Equals(autopark.semitrailers[i]))
                    {
                        isEqual = false;
                    }
                }
                for (int i = 0; i < productsCount && isEqual; i++)
                {
                    if (!products[i].Equals(autopark.products[i]))
                    {
                        isEqual = false;
                    }
                }
            }

            return isEqual;

        }

        /// <summary>
        /// Getting hash code of the autopark.
        /// </summary>
        /// <returns>Hash code of the autopark.</returns>
        public override int GetHashCode()
        {
            int hashCode = -193778087;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<TruckTractor>>.Default.GetHashCode(trucks);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Semitrailer>>.Default.GetHashCode(semitrailers);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Product>>.Default.GetHashCode(products);
            return hashCode;
        }

        /// <summary>
        /// Getting the autopark converting to String.
        /// </summary>
        /// <returns>The autopark converting to String.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
