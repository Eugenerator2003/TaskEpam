using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;

namespace AutoparkLibrary.IO
{
    public class AutoparkStreamReader : IReader
    {
        string filePath;

        System.IO.StreamReader reader;

        public void Read(out List<TruckTractor> trucks, out List<Semitrailer> semitrailers, out List<Product> products)
        {
            trucks = new List<TruckTractor>();

            semitrailers = new List<Semitrailer>();

            products = new List<Product>();
        }

        //private TruckTractor GetTruck()
        //{
        //    return AutoparkFabric.GetTruck();
        //}

        public AutoparkStreamReader(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
