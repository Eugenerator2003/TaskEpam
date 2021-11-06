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
    /// <summary>
    /// Autopark StreamWriter. 
    /// </summary>
    public class AutoparkStreamWriter : IWriter
    {

        private string filePath;

        private StreamWriter writer;

        /// <summary>
        /// Writing information about trucks, semi-trailers and products. 
        /// </summary>
        /// <param name="trucks">Trucks</param>
        /// <param name="semitrailers">Semi-trailers</param>
        /// <param name="products">Products</param>
        public void Write(List<TruckTractor> trucks, List<Semitrailer> semitrailers, List<Product> products)
        {
            try
            {
                writer = new StreamWriter(filePath, false, Encoding.UTF8);
                XmlWriter xmlWriter = XmlWriter.Create(writer);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Autopark");
                WriteTrucks(xmlWriter, trucks);
                WriteSemitrailers(xmlWriter, semitrailers);
                WriteProducts(xmlWriter, products);
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
                writer.Dispose();
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Writing information abouts trucks by using XmlWriter.
        /// </summary>
        /// <param name="writer">XmlWriter</param> 
        /// <param name="trucks">Trucks</param>
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

        /// <summary>
        /// Writing information about semi-trailers by using XmlWriter.
        /// </summary>
        /// <param name="writer">XmlWriter</param>
        /// <param name="semitrailers">Semi-trailers.</param>
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
                StringBuilder productInfo = new StringBuilder("");
                if (semitrailer.Products.Count != 0)
                {
                    foreach (Product product in semitrailer.Products)
                    {
                        productInfo.Append(product + ";");
                    }
                }
                writer.WriteAttributeString("ProductsInfo", productInfo.ToString());
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Writing information about products by using XmlWriter.
        /// </summary>
        /// <param name="writer">XmlWriter</param>
        /// <param name="products">Products</param>
        private void WriteProducts(XmlWriter writer, List<Product> products)
        {
            foreach (Product product in products)
            {
                writer.WriteStartElement("Product");
                writer.WriteAttributeString("Name", product.Name);
                writer.WriteAttributeString("Type", Convert.ToString(product.Type));
                writer.WriteAttributeString("StorageCondition", Convert.ToString(product.StorageCondition));
                writer.WriteAttributeString("Weight", Convert.ToString(product.Weight));
                writer.WriteAttributeString("Volume", Convert.ToString(product.Volume));
                if (product.StorageCondition == Product.ConditionOfStorage.Thermal)
                {
                    writer.WriteAttributeString("MinimalStorageTemperature", Convert.ToString(product.TemperatureMin));
                    writer.WriteAttributeString("MaximalStorageTemperature", Convert.ToString(product.TemperatureMax));
                }
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Constructor of AutoparkStreamWriter.
        /// </summary>
        /// <param name="filePath">File path.</param>
        public AutoparkStreamWriter(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Comparing AutoparkStreamWriter with other object.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>True if object is equal to AutoparkStreamWriter.</returns>
        public override bool Equals(object obj)
        {
            return obj is AutoparkStreamWriter writer &&
                   filePath == writer.filePath &&
                   EqualityComparer<StreamWriter>.Default.Equals(this.writer, writer.writer);
        }

        /// <summary>
        /// Getting hash cod of the AutoparkStreamWriter.
        /// </summary>
        /// <returns>Hash code of the AutoparkStreamWriter</returns>
        public override int GetHashCode()
        {
            int hashCode = -1946460658;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(filePath);
            hashCode = hashCode * -1521134295 + EqualityComparer<StreamWriter>.Default.GetHashCode(writer);
            return hashCode;
        }

        /// <summary>
        /// Getting the AutoparkStreamWriter converted to String.
        /// </summary>
        /// <returns>The AutoparkStreamWriter converted to String.</returns>
        public override string ToString()
        {
            return base.ToString(); ;
        }

    }
}
