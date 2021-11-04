using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;
using AutoparkLibrary.IO;

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
            
        public string ShowAutopark()
        {
            StringBuilder info = new StringBuilder();
            info.Append("Trucks:\n");
            foreach(TruckTractor truck in trucks)
            {
                info.Append($"some inf\n");
            }
            info.Append("Semitrailers:\n");
            foreach(Semitrailer semitrailer in semitrailers)
            {
                info.Append($"some inf\n");
            }
            return Convert.ToString(info);
        }

        public string FindHitchByProductType()
        {
            

            return "";
        }

        public void SaveAutoparpStream()
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

        public Autopark(string filePath)
        {
            _filePath = filePath;
            streamReader = new AutoparkStreamReader(_filePath);
            streamWriter = new AutoparkStreamWriter(_filePath);
            xmlWriter = new AutoparkXmlWriter(_filePath);
            xmlReader = new AutoparkXmlReader(_filePath);
            xmlReader.Read(out trucks, out semitrailers, out products);
        }

        public Autopark() : this("autopark.xml")
        {
        }
    }
}
