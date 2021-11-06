using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Fabric;
using AutoparkLibrary.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkLibrary.Transport.Tests
{
    [TestClass()]
    public class TankSemitrailerTests
    {
        [TestMethod]
        public void AttachUnhookTest()
        {
            TruckTractor truck = AutoparkFabric.GetTruck(AutoparkFabric.GetUniquRandomID(), "Model", 7500, 23);
            TankSemitrailer semitrailer = new TankSemitrailer(AutoparkFabric.GetUniquRandomID(), 2155, 4788, 1900);
            semitrailer.AttachTruck(truck);
            semitrailer.UnhookTruck();
            TruckTractor truckExpected = null;
            TruckTractor truckGotten = semitrailer.Truck;
            Assert.AreEqual(truckExpected, truckGotten);
        }

        [TestMethod()]
        public void UploadUnloadTest()
        {
            TankSemitrailer semitrailer = new TankSemitrailer(AutoparkFabric.GetUniquRandomID(), 2340, 5000, 3300);
            List<Product> products = new List<Product>();
            Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Liquid;
            Product.ProductType type = Product.ProductType.Chemistry;
            Random random = new Random();
            double oilWeight = 0;
            double oilVolume = 0;
            for (int i = 0; i < 40; i++)
            {
                double weight = random.NextDouble() * 3 + 13;
                double volume = weight / 1040;
                oilWeight += weight;
                oilVolume += volume;
                products.Add(new Product("Oil", type, storageCondition, weight, volume));
                semitrailer.Upload(products[i]);
            }
            Product productExpected = new Product("Oil", type, storageCondition, oilWeight, oilVolume); 
            semitrailer.Unload(out List<Product> productsUnloaded);
            Assert.AreEqual(productExpected, productsUnloaded[0]);
        }


        [TestMethod()]
        public void UploadUnloadOneProductTest()
        {
            TankSemitrailer semitrailer = new TankSemitrailer(AutoparkFabric.GetUniquRandomID(), 2340, 5000, 3300);
            List<Product> products = new List<Product>();
            Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Liquid;
            Product.ProductType type = Product.ProductType.Chemistry;
            Random random = new Random();
            double oilWeight = 0;
            double oilVolume = 0;
            for (int i = 0; i < 40; i++)
            {
                double weight = random.NextDouble() * 3 + 13;
                double volume = weight / 1040;
                oilWeight += weight;
                oilVolume += volume;
                products.Add(new Product("Oil", type, storageCondition, weight, volume));
                semitrailer.Upload(products[i]);
            }
            Product productExpected = new Product("Oil", type, storageCondition, oilWeight, oilVolume);
            semitrailer.Unload(productExpected, out Product productGotten);
            Assert.AreEqual(productExpected, productGotten);
        }

        [TestMethod()]
        public void UploadUnloadOnePartProductTest()
        {
            TankSemitrailer semitrailer = new TankSemitrailer(AutoparkFabric.GetUniquRandomID(), 2340, 5000, 3300);
            List<Product> products = new List<Product>();
            Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Liquid;
            Product.ProductType type = Product.ProductType.Chemistry;
            Random random = new Random();
            double oilWeight = 0;
            double oilVolume = 0;
            for (int i = 0; i < 40; i++)
            {
                double weight = random.NextDouble() * 3 + 13;
                double volume = weight / 1040;
                oilWeight += weight;
                oilVolume += volume;
                products.Add(new Product("Oil", type, storageCondition, weight, volume));
                semitrailer.Upload(products[i]);
            }
            double percent = Math.Floor(random.NextDouble() * 100);
            Product product = new Product("Oil", type, storageCondition, oilWeight, oilVolume);
            Product productExpected = new Product("Oil", type, storageCondition, oilWeight * percent / 100, oilVolume * percent / 100);
            semitrailer.Unload(product, percent, out Product productGotten);
            Assert.AreEqual(productExpected, productGotten);
        }

        [TestMethod]
        public void GetProductsInfoTest()
        {
            TankSemitrailer semitrailer = new TankSemitrailer(AutoparkFabric.GetUniquRandomID(), 2340, 5000, 3300);
            List<Product> products = new List<Product>();
            Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Liquid;
            Product.ProductType type = Product.ProductType.Chemistry;
            Random random = new Random();
            double oilWeight = 0;
            double oilVolume = 0;
            for (int i = 0; i < 40; i++)
            {
                double weight = random.NextDouble() * 3 + 13;
                double volume = weight / 1040;
                oilWeight += weight;
                oilVolume += volume;
                products.Add(new Product("Oil", type, storageCondition, weight, volume));
                semitrailer.Upload(products[i]);
            }
            Assert.AreEqual(oilWeight, semitrailer.GetProductsWeight(), 0.0001);
            Assert.AreEqual(oilVolume, semitrailer.GetProductsVolume(), 0.0001);

        }
    }
}