using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;

namespace AutoparkLibrary.IO
{
    /// <summary>
    /// Interface for reader.
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Reading information about trucks, semi-trailers and products.
        /// </summary>
        /// <param name="trucks">Trucks.</param>
        /// <param name="semitrailers">Semi-trailers.</param>
        /// <param name="products">Products.</param>
        void Read(out List<TruckTractor> trucks, out List<Semitrailer> semitrailers, out List<Product> products);
    }
}
