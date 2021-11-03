using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoparkLibrary.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Products;

namespace AutoparkLibrary.Transport.Tests
{
    [TestClass()]
    public class TruckTractorTests
    {
        [TestMethod()]
        public void TentTruckTest()
        {
            TruckTractor truck = new TruckTractor("11", 2500, 5000, 5);
            Semitrailer semitrailer = new TiltSemitrailer("22", 1000, 5000, 3000);
            truck.AttachSemitrailer(semitrailer);
            Product product = new Product("МЯСО", Product.ProductType.Box, 2000, 100);
            truck.Upload(product);
            truck.Upload((Product)product.Clone());
            Product product2 = new Product("ГАВНО", Product.ProductType.Box, 100, 100);
            truck.Unload(product, out Product productUnloaded);
            truck.Upload(product2);
            Console.WriteLine($"Свободный вес: {truck.Semitrailer.FreeProductsWeight}\n" +
                              $"Свободный объём: {truck.Semitrailer.FreeProductsVolume}");
        }

        [TestMethod()]

        public void TankTruckTest()
        {
            TruckTractor truck = new TruckTractor("11", 2500, 6000, 6);
            Semitrailer semitrailer = new TankSemitrailer("119", 1000, 5000, 3000);
            semitrailer.AttachTruck(truck);
            Product vodka1 = new Product("ВОДКА", Product.ProductType.Liquid, 100, 100);
            semitrailer.Upload(vodka1);
            //Product voda = new Product("ВОДА", Product.ProductType.Liquid, 100, 100);
            //semitrailer.Upload(voda);
            Product vodka2 = new Product("ВОДКА", Product.ProductType.Liquid, 2900, 2900);
            semitrailer.Upload(vodka2);
            semitrailer.Unload(out List<Product> productsUnloaded);
            Console.WriteLine($"Свободный вес: {truck.Semitrailer.FreeProductsWeight}\n" +
                  $"Свободный объём: {truck.Semitrailer.FreeProductsVolume}");
        }
    }
}