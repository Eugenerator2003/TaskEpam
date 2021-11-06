using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;
using AutoparkLibrary.Fabric;

namespace AutoparkLibrary.IO
{
    /// <summary>
    /// Autopark StreamReader.
    /// </summary>
    public class AutoparkStreamReader : IReader
    {
        string filePath;

        StreamReader reader;

        /// <summary>
        /// Reading information about trucks, semi-trailers and products.
        /// </summary>
        /// <param name="trucks">Trucks.</param>
        /// <param name="semitrailers">Semi-trailers.</param>
        /// <param name="products">Products.</param>
        public void Read(out List<TruckTractor> trucks, out List<Semitrailer> semitrailers, out List<Product> products)
        {
            trucks = new List<TruckTractor>();
            semitrailers = new List<Semitrailer>();
            products = new List<Product>();
            reader = new StreamReader(filePath, Encoding.UTF8);
            XmlReader xmlReader = XmlReader.Create(reader);
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    switch (xmlReader.Name)
                    {
                        case "Truck":
                            ReadTruck(xmlReader, trucks);
                            break;
                        case "Semitrailer":
                            ReadSemitrailer(xmlReader, semitrailers);
                            break;
                        case "Product":
                            ReadProduct(xmlReader, products);
                            break;
                    }
                }
            }
            reader.Dispose();

        }


        /// <summary>
        /// Reading information abouts trucks by using XmlReader.
        /// </summary>
        /// <param name="reader">XmlReeader</param>
        /// <param name="trucks">Trucks</param>
        private void ReadTruck(XmlReader reader, List<TruckTractor> trucks)
        {
            if (reader.HasAttributes)
            {
                string garageId = reader.GetAttribute("GarageID");
                string model = reader.GetAttribute("Model");
                double carryingCapacity = Convert.ToDouble(reader.GetAttribute("CarryingCapacity"));
                double fuelConsumption = Convert.ToDouble(reader.GetAttribute("FuelConsumption"));
                trucks.Add(AutoparkFabric.GetTruck(garageId, model, carryingCapacity, fuelConsumption));
            }
        }

        /// <summary>
        /// Reading information about semi-trailers by using XmlReader.
        /// </summary>
        /// <param name="reader">XmlReader</param>
        /// <param name="semitrailers">Semi-trailers.</param>
        private void ReadSemitrailer(XmlReader reader, List<Semitrailer> semitrailers)
        {
            if (reader.HasAttributes)
            {
                string garageId = reader.GetAttribute("GarageID");
                Enum.TryParse(reader.GetAttribute("SemitrailerType"), out Semitrailer.SemitrailerType type);
                double semitrailerWeight = Convert.ToDouble(reader.GetAttribute("SemitrailerWeight"));
                double maxProductWeight = Convert.ToDouble(reader.GetAttribute("MaximumProductWeight"));
                double maxProductVolume = Convert.ToDouble(reader.GetAttribute("MaximimProductVolume"));
                Semitrailer semitrailer = AutoparkFabric.GetSemitrailer(type, garageId, semitrailerWeight, maxProductWeight, maxProductVolume);

                string productInfo = reader.GetAttribute("ProductInfo");
                if (productInfo != null)
                {
                    List<Product> products = AutoparkFabric.GetProductListFromString(productInfo);
                    foreach (Product product in products)
                    {
                        semitrailer.Upload(product);
                    }
                }
                semitrailers.Add(semitrailer);

            }
        }

        /// <summary>
        /// Reading information about products by using XmlReader.
        /// </summary>
        /// <param name="reader">XmlReader</param>
        /// <param name="products">Products</param>
        private void ReadProduct(XmlReader reader, List<Product> products)
        {
            if (reader.HasAttributes)
            {
                Product product = null;
                string name = reader.GetAttribute("Name");
                Enum.TryParse(reader.GetAttribute("ProductType"), out Product.ProductType type);
                Enum.TryParse(reader.GetAttribute("StorageCondition"), out Product.ConditionOfStorage storageCondition);
                double weight = Convert.ToDouble(reader.GetAttribute("Weight"));
                double volume = Convert.ToDouble(reader.GetAttribute("Volume"));
                if (reader.AttributeCount == 7)
                {
                    double tempMin = Convert.ToDouble(reader.GetAttribute("MinimalStorageTemperature"));
                    double tempMax = Convert.ToDouble(reader.GetAttribute("MaximalStorageTemperature"));
                    product = AutoparkFabric.GetProduct(name, type, storageCondition, weight, volume, tempMin, tempMax);
                }
                else
                    product = AutoparkFabric.GetProduct(name, type, storageCondition, weight, volume);
                products.Add(product);
            }
        }

        /// <summary>
        /// Constructor of AutoparkStreamReader.
        /// </summary>
        /// <param name="filePath">File path.</param>
        public AutoparkStreamReader(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Comparing AutoparkStreamReader with other object.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>True if object is equal to AutoparkStreamReader.</returns>
        public override bool Equals(object obj)
        {
            return obj is AutoparkStreamReader reader &&
                   filePath == reader.filePath &&
                   EqualityComparer<StreamReader>.Default.Equals(this.reader, reader.reader);
        }

        /// <summary>
        /// Getting hash cod of the AutoparkStreamReader.
        /// </summary>
        /// <returns>Hash code of the AutoparkStreamReader</returns>
        public override int GetHashCode()
        {
            int hashCode = 774023802;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(filePath);
            hashCode = hashCode * -1521134295 + EqualityComparer<StreamReader>.Default.GetHashCode(reader);
            return hashCode;
        }

        /// <summary>
        /// Getting the AutoparkStreamReader converted to String.
        /// </summary>
        /// <returns>The AutoparkStreamReader converted to String.</returns>
        public override string ToString()
        {
            return base.ToString(); ;
        }

    }
}
