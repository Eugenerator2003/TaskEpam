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
    public class RefrigiratorSemitrailerTests
    {
        [TestMethod]
        public void AttachUnhookTest()
        {
            TruckTractor truck = AutoparkFabric.GetTruck(AutoparkFabric.GetUniquRandomID(), "Model", 7500, 23);
            RefrigiratorSemitrailer semitrailer = new RefrigiratorSemitrailer(AutoparkFabric.GetUniquRandomID(), 2155, 4788, 1900);
            semitrailer.AttachTruck(truck);
            semitrailer.UnhookTruck();
            TruckTractor truckExpected = null;
            TruckTractor truckGotten = semitrailer.Truck;
            Assert.AreEqual(truckExpected, truckGotten);
        }

        [TestMethod()]
        public void UploadUnloadTest()
        {
            RefrigiratorSemitrailer semitrailer = new RefrigiratorSemitrailer(AutoparkFabric.GetUniquRandomID(), 2500, 5000, 2300);
            List<Product> products = new List<Product>();
            Random random = new Random();
            for (int i = 0; i < 40; i++)
            {
                Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Thermal;
                Product.ProductType type = Product.ProductType.Food;
                double weight = random.NextDouble() * 3 + 3;
                double volume = random.NextDouble() * 2 + 1;
                double temp_min = -30 - random.NextDouble() * 5;
                double temp_max = -7 + random.NextDouble() * 5;
                products.Add(new Product("Meat", type, storageCondition, weight, volume, temp_min, temp_max));
                semitrailer.Upload(products[i]);
            }
            semitrailer.Unload(out List<Product> productsUnloaded);
            CollectionAssert.AreEqual(productsUnloaded, products);
        }


        [TestMethod()]
        public void UploadUnloadOneProductTest()
        {
            RefrigiratorSemitrailer semitrailer = new RefrigiratorSemitrailer(AutoparkFabric.GetUniquRandomID(), 2500, 5000, 2300);
            List<Product> products = new List<Product>();
            Random random = new Random();
            for (int i = 0; i < 40; i++)
            {
                Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Thermal;
                Product.ProductType type = Product.ProductType.Food;
                double weight = random.NextDouble() * 3 + 3;
                double volume = random.NextDouble() * 2 + 1;
                double temp_min = -30 - random.NextDouble() * 5;
                double temp_max = -7 + random.NextDouble() * 5;
                products.Add(new Product("Meat", type, storageCondition, weight, volume, temp_min, temp_max));
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
            RefrigiratorSemitrailer semitrailer = new RefrigiratorSemitrailer(AutoparkFabric.GetUniquRandomID(), 2500, 5000, 2300);
            List<Product> products = new List<Product>();
            Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Thermal;
            Product.ProductType type = Product.ProductType.Food;
            Random random = new Random();
            for (int i = 0; i < 40; i++)
            {
                double weight = random.NextDouble() * 3 + 3;
                double volume = random.NextDouble() * 2 + 1;
                double temp_min = -30 - random.NextDouble() * 5;
                double temp_max = -7 + random.NextDouble() * 5;
                products.Add(new Product("Meat", type, storageCondition, weight, volume, temp_min, temp_max));
                semitrailer.Upload(products[i]);
            }
            int indexExpected = random.Next(0, 40);
            double percent = Math.Floor(random.NextDouble() * 100);
            Product product = products[indexExpected];
            Product productExpected = new Product("Meat", type, storageCondition, product.Weight * percent / 100,
                                              product.Volume * percent / 100, product.TemperatureMin, product.TemperatureMax);
            semitrailer.Unload(product, percent, out Product productGotten);
            Assert.AreEqual(productExpected, productGotten);
            }

        [TestMethod]
        public void GetProductsInfoTest()
        {
            RefrigiratorSemitrailer semitrailer = new RefrigiratorSemitrailer(AutoparkFabric.GetUniquRandomID(), 2500, 5000, 2300);
            Random random = new Random();
            double productsWeightExpected = 0;
            double productsVolumeExpected = 0;
            for (int i = 0; i < 40; i++)
            {
                Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Thermal;
                Product.ProductType type = Product.ProductType.Food;
                double weight = random.NextDouble() * 3 + 3;
                double volume = random.NextDouble() * 2 + 1;
                double temp_min = -30 - random.NextDouble() * 5;
                double temp_max = -7 + random.NextDouble() * 5;
                Product product = new Product("Meat", type, storageCondition, weight, volume, temp_min, temp_max);
                semitrailer.Upload(product);
                productsVolumeExpected += product.Volume;
                productsWeightExpected += product.Weight;
            }
            Assert.AreEqual(productsWeightExpected, semitrailer.GetProductsWeight(), 0.0001);
            Assert.AreEqual(productsVolumeExpected, semitrailer.GetProductsVolume(), 0.0001);

        }
    }
}