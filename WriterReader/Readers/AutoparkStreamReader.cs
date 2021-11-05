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
    public class AutoparkStreamReader : IReader
    {
        string filePath;

        StreamReader reader;

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


        public AutoparkStreamReader(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
