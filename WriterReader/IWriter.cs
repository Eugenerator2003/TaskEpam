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
    /// Interface for writer.
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Writing information about trucks, semi-trailers and products. 
        /// </summary>
        /// <param name="trucks">Trucks</param>
        /// <param name="semitrailers">Semi-trailers</param>
        /// <param name="products">Products</param>
        void Write(List<TruckTractor> trucks, List<Semitrailer> semitrailers, List<Product> products);
    }
}
