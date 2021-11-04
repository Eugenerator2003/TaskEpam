using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AutoparkLibrary.Products;
using AutoparkLibrary.Transport;

namespace AutoparkLibrary.IO
{
    public class AutoparkXmlWriter : IWriter
    {
        private string filePath;

        private XmlWriter writer;

        public void Write(List<TruckTractor> trucks, List<Semitrailer> semitrailers, List<Product> products)
        {
            writer = XmlWriter.Create(filePath);
            writer.WriteStartDocument();
            writer.WriteStartElement("Autopark");
            WriteTrucks(trucks);
            WriteSemitrailers(semitrailers);
            WriteProducts(products);
            writer.WriteEndElement();
            writer.Close();
        }

        private void WriteTrucks(List<TruckTractor> trucks)
        {
            foreach(TruckTractor truck in trucks)
            {
                writer.WriteStartElement("Truck");
                writer.WriteAttributeString("GarageID", truck.GarageID);
                writer.WriteAttributeString("Model", truck.Model);
                writer.WriteAttributeString("CarryingCapacity", Convert.ToString(truck.CarryingCapacity));
                writer.WriteAttributeString("FuelConsumption", Convert.ToString(truck.FuelConsumption));
                writer.WriteEndElement();
            }
        }

        private void WriteSemitrailers(List<Semitrailer> semitrailers)
        {
            foreach (Semitrailer semitrailer in semitrailers)
            {
                writer.WriteStartElement("Semitrailer");
                writer.WriteAttributeString("SemitrailerType", Convert.ToString(semitrailer.Type));
                writer.WriteAttributeString("GarageID", semitrailer.GarageID);
                writer.WriteAttributeString("SemitrailerWeight", Convert.ToString(semitrailer.SemitrailerWeight));
                writer.WriteAttributeString("MaximumProductWeight", Convert.ToString(semitrailer.MaxProductsWeight));
                writer.WriteAttributeString("MaximumProductVolume", Convert.ToString(semitrailer.MaxProductsVolume));
                StringBuilder productInfo = new StringBuilder("");
                if (semitrailer.Products.Count != 0)
                {
                    foreach(Product product in semitrailer.Products)
                    {
                        productInfo.Append(product + ";");
                    }
                }
                writer.WriteAttributeString("ProductsInfo", productInfo.ToString());
                writer.WriteEndElement();
            }
        }

        private void WriteProducts(List<Product> products)
        {
            foreach(Product product in products)
            {
                writer.WriteStartElement("Product");
                writer.WriteAttributeString("Name", product.Name);
                writer.WriteAttributeString("ProductType", Convert.ToString(product.Type));
                writer.WriteAttributeString("StorageCondition", Convert.ToString(product.StorageCondition));
                writer.WriteAttributeString("ProductWeight", Convert.ToString(product.Weight));
                writer.WriteAttributeString("ProductVolume", Convert.ToString(product.Volume));
                if (product.StorageCondition == Product.ConditionOfStorage.Thermal)
                {
                    writer.WriteAttributeString("MinimalStorageTemperature", Convert.ToString(product.TemperatureMin));
                    writer.WriteAttributeString("MaximalStorageTemperature", Convert.ToString(product.TemperatureMax));
                }
                writer.WriteEndElement();
            }
        }

        public AutoparkXmlWriter(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
