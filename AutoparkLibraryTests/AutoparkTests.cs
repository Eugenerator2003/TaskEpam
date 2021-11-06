using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoparkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoparkLibrary.Transport;
using AutoparkLibrary.Products;
using AutoparkLibrary.Fabric;

namespace AutoparkLibrary.Tests
{
    [TestClass()]
    public class AutoparkTests
    {
        [TestMethod()]
        public void SaveLoadTest()
        {
            Autopark autopartExpected = new Autopark();
            List<string> garageIds = new List<string>();
            for(int i = 0; i < 10; i++)
            {
                garageIds.Add(AutoparkFabric.GetUniquRandomID());
            }
            for(int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    TruckTractor truck = AutoparkFabric.GetTruck(garageIds[i], $"MK-{(i + 2) * Math.Abs(i)}", 
                                                                                            7500 + 750 * (i / 2), 20 + i);
                    autopartExpected.AddTruck(truck);
                }
                else
                {
                    Semitrailer semitrailer = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer,
                                     garageIds[i], 1500 + 100 * (i / 2), 5000 + i * 230, 2500 + i * 150);
                    autopartExpected.AddSemitrailer(semitrailer);
                }
            }
            autopartExpected.SaveAutoparkXML();
            Autopark autopark = new Autopark();
            autopark.LoadAutoparkXML();
            Assert.AreEqual(autopartExpected, autopark);
        }

        [TestMethod()]
        public void AttachTest()
        {
            Autopark autopark = new Autopark();
            string Id1 = AutoparkFabric.GetUniquRandomID();
            string Id2 = AutoparkFabric.GetUniquRandomID();
            TruckTractor truck = AutoparkFabric.GetTruck(Id1, "MUD3", 5000, 5);
            Semitrailer semitrailer = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, Id2, 2000, 5000, 1500);
            autopark.AddSemitrailer(semitrailer);
            autopark.AddTruck(truck);
            autopark.Attach(Id1, Id2);
            Assert.AreEqual(semitrailer, truck.Semitrailer);
        }

        [TestMethod()]
        public void UnhookTest()
        {
            Autopark autopark = new Autopark();
            string Id1 = AutoparkFabric.GetUniquRandomID();
            string Id2 = AutoparkFabric.GetUniquRandomID();
            TruckTractor truck = AutoparkFabric.GetTruck(Id1, "MUD3", 5000, 5);
            Semitrailer semitrailer = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, Id2, 2000, 5000, 1500);
            autopark.AddSemitrailer(semitrailer);
            autopark.AddTruck(truck);
            autopark.Attach(Id1, Id2);
            autopark.Unhook(Id2);
            Assert.AreEqual(null, truck.Semitrailer);
        }

        [TestMethod()]
        public void UploadUnloadTest()
        {
            Autopark autopark = new Autopark();
            string id1 = AutoparkFabric.GetUniquRandomID();
            string id2 = AutoparkFabric.GetUniquRandomID();
            TruckTractor truck = AutoparkFabric.GetTruck(id1, "MUD3", 5000, 5);
            Semitrailer semitrailer = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id2, 2000, 5000, 1500);
            autopark.AddSemitrailer(semitrailer);
            autopark.AddTruck(truck);
            autopark.Attach(id1, id2);
            Product productExpected = AutoparkFabric.GetProduct("Vacuum cleaner", Product.ProductType.HouseAppliences, Product.ConditionOfStorage.Box, 4, 3);
            autopark.AddProduct(productExpected);
            autopark.Upload(id1, autopark.Products[0]);
            Product product = autopark.Unload(id2, productExpected);
            Assert.AreEqual(productExpected, product);
        }

        [TestMethod]
        public void GetHitchesCanBeUploadedTest()
        {
            Autopark autopark = new Autopark();
            string id1 = AutoparkFabric.GetUniquRandomID();
            string id2 = AutoparkFabric.GetUniquRandomID();
            string id3 = AutoparkFabric.GetUniquRandomID();
            string id4 = AutoparkFabric.GetUniquRandomID();
            TruckTractor truck1 = AutoparkFabric.GetTruck(id1, "MUD3", 5000, 5);
            Semitrailer semitrailer1 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id2, 2000, 5000, 1500);
            TruckTractor truck2 = AutoparkFabric.GetTruck(id3, "MUD3", 6000, 5);
            Semitrailer semitrailer2 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id4, 2000, 5000, 1500);
            autopark.AddSemitrailer(semitrailer1);
            autopark.AddTruck(truck1);
            autopark.AddTruck(truck2);
            autopark.AddSemitrailer(semitrailer2);
            autopark.Attach(id1, id2);
            autopark.Attach(id3, id4);
            Product product = AutoparkFabric.GetProduct("Vacuum cleaner", Product.ProductType.HouseAppliences, Product.ConditionOfStorage.Box, 4, 3);
            autopark.AddProduct(product);
            List<TruckTractor> trucksExpected = new List<TruckTractor>();
            trucksExpected.Add(truck1);
            trucksExpected.Add(truck2);
            List<TruckTractor> trucks = autopark.GetHitchesCanBeLoaded();
            CollectionAssert.AreEqual(trucksExpected, trucks);
        }

        [TestMethod]
        public void GetHitchesCanBeUploadedFullyTest()
        {
            Autopark autopark = new Autopark();
            string id1 = AutoparkFabric.GetUniquRandomID();
            string id2 = AutoparkFabric.GetUniquRandomID();
            string id3 = AutoparkFabric.GetUniquRandomID();
            string id4 = AutoparkFabric.GetUniquRandomID();
            TruckTractor truck1 = AutoparkFabric.GetTruck(id1, "MUD3", 5000, 5);
            Semitrailer semitrailer1 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id2, 2000, 5000, 1500);
            TruckTractor truck2 = AutoparkFabric.GetTruck(id3, "MUD3", 6000, 5);
            Semitrailer semitrailer2 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id4, 2000, 2500, 1200);
            autopark.AddSemitrailer(semitrailer1);
            autopark.AddTruck(truck1);
            autopark.AddTruck(truck2);
            autopark.AddSemitrailer(semitrailer2);
            autopark.Attach(id1, id2);
            autopark.Attach(id3, id4);
            Product product = AutoparkFabric.GetProduct("Smth", Product.ProductType.Food, Product.ConditionOfStorage.Box, 2500, 1000);
            autopark.AddProduct(product);
            List<TruckTractor> trucksExpected = new List<TruckTractor>();
            trucksExpected.Add(truck2);
            List<TruckTractor> trucks = autopark.GetHitchesCanBeLoadedFully();
            CollectionAssert.AreEqual(trucksExpected, trucks);
        }

        [TestMethod()]
        public void GetSemitrailerByTypeTest()
        {
            Autopark autopark = new Autopark();
            string id1 = AutoparkFabric.GetUniquRandomID();
            string id2 = AutoparkFabric.GetUniquRandomID();
            string id3 = AutoparkFabric.GetUniquRandomID();
            Semitrailer semitrailer1 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.RefrigiratorSemitrailer, id1, 2000, 5000, 1500);
            Semitrailer semitrailer2 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TankSemitrailer, id2, 2000, 5000, 1500);
            Semitrailer semitrailer3 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id3, 2000, 5000, 1500);
            autopark.AddSemitrailer(semitrailer1);
            autopark.AddSemitrailer(semitrailer2);
            autopark.AddSemitrailer(semitrailer3);
            Assert.AreEqual(semitrailer2, autopark.GetSemitrailerByType(Semitrailer.SemitrailerType.TankSemitrailer));
        }

        [TestMethod()]
        public void GetSemitrailerByCharacteristicsTest()
        {
            Autopark autopark = new Autopark();
            string id1 = AutoparkFabric.GetUniquRandomID();
            string id2 = AutoparkFabric.GetUniquRandomID();
            string id3 = AutoparkFabric.GetUniquRandomID();
            double semitrailerWeightExpected = 2000;
            double maxLoadedProductsWeightExpected = 5455;
            double maxLoadedProductsVolumeExpected = 2500;
            Semitrailer semitrailer1 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id1, 1900, 4500, 1500);
            Semitrailer semitrailer2 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id2, 2105, 5555, 1400);
            Semitrailer semitrailer3 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id3,
                                        semitrailerWeightExpected, maxLoadedProductsWeightExpected, maxLoadedProductsVolumeExpected);
            autopark.AddSemitrailer(semitrailer1);
            autopark.AddSemitrailer(semitrailer2);
            autopark.AddSemitrailer(semitrailer3);
            Semitrailer semitrailer = autopark.GetSemitrailerByCharacteristics(semitrailerWeightExpected, maxLoadedProductsWeightExpected, maxLoadedProductsVolumeExpected);
            Assert.AreEqual(semitrailer3, semitrailer);
        }

        [TestMethod()]
        public void GetHitchesByProductTypeTest()
        {
            Autopark autopark = new Autopark();
            string id1 = AutoparkFabric.GetUniquRandomID();
            string id2 = AutoparkFabric.GetUniquRandomID();
            string id3 = AutoparkFabric.GetUniquRandomID();
            string id4 = AutoparkFabric.GetUniquRandomID();
            TruckTractor truck1 = AutoparkFabric.GetTruck(id1, "MUD3", 5000, 5);
            Semitrailer semitrailer1 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id2, 2000, 5000, 1500);
            TruckTractor truck2 = AutoparkFabric.GetTruck(id3, "MUD3", 6000, 5);
            Semitrailer semitrailer2 = AutoparkFabric.GetSemitrailer(Semitrailer.SemitrailerType.TiltSemitrailer, id4, 2000, 5000, 1500);
            autopark.AddSemitrailer(semitrailer1);
            autopark.AddTruck(truck1);
            autopark.AddTruck(truck2);
            autopark.AddSemitrailer(semitrailer2);
            autopark.Attach(id1, id2);
            autopark.Attach(id3, id4);
            Product product1 = AutoparkFabric.GetProduct("Vacuum cleaner", Product.ProductType.HouseAppliences, Product.ConditionOfStorage.Box, 4, 3);
            autopark.AddProduct(product1);
            autopark.Upload(id4, product1);
            Product product2 = AutoparkFabric.GetProduct("Pasta", Product.ProductType.Food, Product.ConditionOfStorage.Box, 4, 3);
            autopark.AddProduct(product2);
            autopark.Upload(id2, product2);
            List<TruckTractor> trucksExpected = new List<TruckTractor>();
            trucksExpected.Add(truck1);
            CollectionAssert.AreEqual(trucksExpected, autopark.GetHitchesByProductType(Product.ProductType.Food));
        }
    }
}