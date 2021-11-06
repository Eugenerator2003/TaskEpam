using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoparkLibrary.Fabric;
using AutoparkLibrary.Products;
using AutoparkLibrary.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkLibrary.Transport.Tests
{
    [TestClass()]
    public class TruckTractorTests
    {
        [TestMethod()]
        public void AttachSemitrailerTest()
        {
            TruckTractor truck = new TruckTractor(AutoparkFabric.GetUniquRandomID(), "NCR-77", 10000, 25);
            Semitrailer.SemitrailerType type = Semitrailer.SemitrailerType.TiltSemitrailer;
            Semitrailer semitrailer = AutoparkFabric.GetSemitrailer(type, AutoparkFabric.GetUniquRandomID(), 1500, 7000, 3400);
            truck.AttachSemitrailer(semitrailer);
            Assert.AreEqual(semitrailer, truck.Semitrailer);
        }

        [TestMethod()]
        public void UnhookSemitrailerTest()
        {
            TruckTractor truck = new TruckTractor(AutoparkFabric.GetUniquRandomID(), "OMG-10000", 12350, 29);
            Semitrailer.SemitrailerType type = Semitrailer.SemitrailerType.TiltSemitrailer;
            Semitrailer semitrailer = AutoparkFabric.GetSemitrailer(type, AutoparkFabric.GetUniquRandomID(), 2000, 9000, 4000);
            truck.AttachSemitrailer(semitrailer);
            truck.UnhookSemitrailer();
            Assert.AreEqual(null, truck.Semitrailer);
        }

        [DataTestMethod()]
        [DataRow(23, 1599)]
        [DataRow(29, 1789)]
        [DataRow(25, 2003)]
        public void GetFuelConsumptionTest(double consumption, double semitrailerWeight)
        {
            TruckTractor truck = new TruckTractor(AutoparkFabric.GetUniquRandomID(), "NCR-77", 10000, 25);
            Semitrailer.SemitrailerType type = Semitrailer.SemitrailerType.TiltSemitrailer;
            Semitrailer semitrailer = AutoparkFabric.GetSemitrailer(type, AutoparkFabric.GetUniquRandomID(), semitrailerWeight, 7000, 3400);
            truck.AttachSemitrailer(semitrailer);
            Random random = new Random();
            Product.ConditionOfStorage storageCondition = Product.ConditionOfStorage.Box;
            Product.ProductType productType = Product.ProductType.HouseAppliences;
            double productWeight = 0;
            for (int i = 0; i < 40; i++)
            {
                double weight = random.NextDouble() * 3 + 4;
                productWeight += weight;
                double volume = random.NextDouble() * 2 + 2;
                Product product = (new Product("Vacuum cleaner", productType, storageCondition, weight, volume));
                semitrailer.Upload(product);
            }
            double consumptionExpected = truck.FuelConsumption * (semitrailerWeight + productWeight);
            Assert.AreEqual(consumptionExpected, truck.GetFuelConsumption(), 0.001);
        }
    }
}