using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;

namespace AutoparkLibrary.IO
{
    public interface IReader
    {
        void Read(out List<TruckTractor> trucks, out List<Semitrailer> semitrailers, out List<Product> products);
    }
}
