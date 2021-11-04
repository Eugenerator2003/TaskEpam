using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using AutoparkLibrary.Products;
using AutoparkLibrary.Transport;

namespace AutoparkLibrary.IO
{
    public class AutoparkStreamWriter : IWriter
    {

        private string filePath;

        private StreamWriter writer;

        public void Write(List<TruckTractor> trucks, List<Semitrailer> semitrailers, List<Product> products)
        {
            try
            {

                writer = new StreamWriter(filePath);
                XmlWriter xmlWriter = XmlWriter.Create(writer);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Autopark");
                WriteTrucks(xmlWriter, trucks);
                WriteSemitrailers(xmlWriter, semitrailers);
                WriteProducts(xmlWriter, products);
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        private void WriteTrucks(XmlWriter writer, List<TruckTractor> trucks)
        {
            foreach (TruckTractor truck in trucks)
            {
                writer.WriteStartElement("Truck");
                writer.WriteAttributeString("GarageID", truck.GarageID);
                writer.WriteAttributeString("Model", truck.Model);
                writer.WriteAttributeString("CarryingCapacity", Convert.ToString(truck.CarryingCapacity));
                writer.WriteAttributeString("FuelConsumption", Convert.ToString(truck.FuelConsumption));
                writer.WriteEndElement();
            }
        }

        private void WriteSemitrailers(XmlWriter writer, List<Semitrailer> semitrailers)
        {
            foreach (Semitrailer semitrailer in semitrailers)
            {
                writer.WriteStartElement("Semitrailer");
                writer.WriteAttributeString("SemitrailerType", Convert.ToString(semitrailer.Type));
                writer.WriteAttributeString("GarageID", semitrailer.GarageID);
                writer.WriteAttributeString("SemitrailerWeight", Convert.ToString(semitrailer.SemitrailerWeight));
                writer.WriteAttributeString("MaximumProductWeight", Convert.ToString(semitrailer.MaxProductsWeight));
                writer.WriteAttributeString("MaximumProductVolume", Convert.ToString(semitrailer.MaxProductsVolume));
                writer.WriteEndElement();
            }
        }

        private void WriteProducts(XmlWriter writer, List<Product> products)
        {
            foreach (Product product in products)
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

        public AutoparkStreamWriter(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
