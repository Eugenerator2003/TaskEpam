using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoparkLibrary.Fabric;
using AutoparkLibrary.Products;
using AutoparkLibrary.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkLibrary.Fabric.Tests
{
    [TestClass()]
    public class AutoparkFabricTests
    {
        [TestMethod()]
        public void GetProductListFromStringTest()
        {
            Product product = new Product("Milk", Product.ProductType.Food, Product.ConditionOfStorage.Box, 10, 10);
            Console.WriteLine(product);
            Product productNew = AutoparkFabric.GetProductFromString(product.ToString());
            Console.WriteLine(productNew);
            Assert.AreEqual(product, productNew);
        }
    }
}