using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;

namespace AutoparkLibrary.IO
{
    public interface IWriter
    {
        void Write(List<TruckTractor> trucks, List<Semitrailer> semitrailers, List<Product> products);
    }
}
