using System;
using System.Xml;
using System.Collections.Generic;
using AutoparkLibrary.Products;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Fabric;
using System.Text;
using System.Threading.Tasks;


namespace AutoparkLibrary.IO
{
    public class AutoparkXmlReader : IReader
    {
        private string filePath;

        private XmlReader reader;

        public void Read(out List<TruckTractor> trucks, out List<Semitrailer> semitrailers, out List<Product> products)
        {
            trucks = new List<TruckTractor>();
            semitrailers = new List<Semitrailer>();
            products = new List<Product>();
            reader = XmlReader.Create(filePath);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch(reader.Name)
                    {
                        case "Truck":
                            ReadTruck(trucks);
                            break;
                        case "Semitrailer":
                            ReadSemitrailer(semitrailers);
                            break;
                        case "Product":
                            ReadProduct(products);
                            break;
                    }
                }
            }
        }

        private void ReadTruck(List<TruckTractor> trucks)
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

        private void ReadSemitrailer(List<Semitrailer> semitrailers)
        {
            if (reader.HasAttributes)
            {
                string garageId = reader.GetAttribute("GarageID");
                double semitrailerWeight = Convert.ToDouble(reader.GetAttribute("SemitrailerWeight"));
                double maxProductWeight = Convert.ToDouble(reader.GetAttribute("MaximumProductWeight"));
                double maxProductVolume = Convert.ToDouble(reader.GetAttribute("MaximimProductVolume"));
                string productInfo = reader.GetAttribute("ProductInfo");
                List<Product> products = AutoparkFabric.GetProductListFromString(productInfo);
            }
        }

        private void ReadProduct(List<Product> products)
        {
        }

        public AutoparkXmlReader(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
