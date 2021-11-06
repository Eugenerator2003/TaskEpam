using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;
using AutoparkLibrary.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkLibrary.Transport.Tests
{
    [TestClass()]
    public class TiltSemitrailerTests
    {
        [TestMethod]
        public void AttachUnhookTruckTest()
        {
            TruckTractor truck = AutoparkFabric.GetTruck(AutoparkFabric.GetUniquRandomID(), "Model", 7500, 23);
            TiltSemitrailer semitrailer = new TiltSemitrailer(AutoparkFabric.GetUniquRandomID(), 2155, 4788, 1900);
            semitrailer.AttachTruck(truck);
            semitrailer.UnhookTruck();
            TruckTractor truckExpected = null;
            TruckTractor truckGotten = semitrailer.Truck;
            Assert.AreEqual(truckExpected, truckGotten);
        }

        [TestMethod()]
        public void UploadUnloadTest()
        {
            TiltSemitrailer semitrailer = new TiltSemitrailer(AutoparkFabric.GetUniquRandomID(), 2500, 5000, 2300);
            List<Product> products = new List<Product>();
            Random random = new Random();
            for(int i = 0; i < 40; i++)
            {
                Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Box;
                Product.ProductType type = Product.ProductType.HouseAppliences;
                double weight = random.NextDouble() * 3 + 4;
                double volume = random.NextDouble() * 2 + 2;
                products.Add(new Product("Vacuum cleaner", type, storageCondition, weight, volume));
                semitrailer.Upload(products[i]);
            }
            semitrailer.Unload(out List<Product> productsUnloaded);
            CollectionAssert.AreEqual(productsUnloaded, products);
        }


        [TestMethod()]
        public void UploadUnloadOneProductTest()
        {
            TiltSemitrailer semitrailer = new TiltSemitrailer(AutoparkFabric.GetUniquRandomID(), 2500, 5000, 2300);
            List<Product> products = new List<Product>();
            Random random = new Random();
            for (int i = 0; i < 40; i++)
            {
                Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Box;
                Product.ProductType type = Product.ProductType.HouseAppliences;
                double weight = random.NextDouble() * 3 + 4;
                double volume = random.NextDouble() * 2 + 2;
                products.Add(new Product("Vacuum cleaner", type, storageCondition, weight, volume));
                semitrailer.Upload(products[i]);
            }
            int indexExpected = random.Next(0, 40);
            Product productExpected = products[indexExpected];
            semitrailer.Unload(productExpected, out Product productGotten);
            Assert.AreEqual(productExpected, productGotten);
        }

        [TestMethod()]
        public void UploadUnloadOnePartProductTest()
        {
            TiltSemitrailer semitrailer = new TiltSemitrailer(AutoparkFabric.GetUniquRandomID(), 2500, 5000, 2300);
            List<Product> products = new List<Product>();
            Random random = new Random();
            Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Box;
            Product.ProductType type = Product.ProductType.HouseAppliences;
            for (int i = 0; i < 40; i++)
            {
                double weight = random.NextDouble() * 3 + 4;
                double volume = random.NextDouble() * 2 + 2;
                products.Add(new Product("Vacuum cleaner", type, storageCondition, weight, volume));
                semitrailer.Upload(products[i]);
            }
            int indexExpected = random.Next(0, 40);
            double percent = Math.Floor(random.NextDouble() * 100);
            Product product = products[indexExpected];
            Product productExpected = new Product("Vacuum cleaner", type, storageCondition, product.Weight * percent / 100,
                                                            product.Volume * percent / 100);
            semitrailer.Unload(product, percent, out Product productGotten);
            Assert.AreEqual(productExpected, productGotten);
        }

        [TestMethod]
        public void GetProductsInfoTest()
        {
            TiltSemitrailer semitrailer = new TiltSemitrailer(AutoparkFabric.GetUniquRandomID(), 2500, 5000, 2300);
            Random random = new Random();
            double productsWeightExpected = 0;
            double productsVolumeExpected = 0;
            for (int i = 0; i < 40; i++)
            {
                Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Box;
                Product.ProductType type = Product.ProductType.HouseAppliences;
                double weight = random.NextDouble() * 3 + 4;
                double volume = random.NextDouble() * 2 + 2;
                Product product = new Product("Vacuum cleaner", type, storageCondition, weight, volume);
                semitrailer.Upload(product);
                productsVolumeExpected += product.Volume;
                productsWeightExpected += product.Weight;
            }
            Assert.AreEqual(productsWeightExpected, semitrailer.GetProductsWeight(), 0.0001);
            Assert.AreEqual(productsVolumeExpected, semitrailer.GetProductsVolume(), 0.0001);

        }
        

    }
}