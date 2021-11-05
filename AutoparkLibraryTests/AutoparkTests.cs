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
        public void AutoparkTest()
        {
            Autopark autopark = new Autopark();
            //TruckTractor truck1 = AutoparkFabric.GetTruck("1", "MUD3", 5000, 5);
            //TruckTractor truck2 = AutoparkFabric.GetTruck("2", "MUD10", 4599, 6);
            //Semitrailer semitrailer1 = AutoparkFabric.GetSemitrailer(
            //                            Semitrailer.SemitrailerType.TiltSemitrailer, "3", 2000, 5000, 1500);
            //Semitrailer semitrailer2 = AutoparkFabric.GetSemitrailer(
            //                            Semitrailer.SemitrailerType.TankSemitrailer, "4", 2000, 5000, 1500);
            //truck1.AttachSemitrailer(semitrailer1);
            //autopark.AddTruck(truck1);
            //autopark.AddTruck(truck2);
            ////autopark.AddSemitrailer(semitrailer1);
            //autopark.AddSemitrailer(semitrailer2);
            //autopark.Attach(truck2.GarageID, semitrailer2.GarageID);
            autopark.LoadAutoparkStream();
            List<TruckTractor> trucks;
            List<Semitrailer> semitrailers;
            List<Product> products;
            autopark.ShowAutopark(out trucks, out semitrailers, out products);
            ConsoleWriteCollection(trucks);
            ConsoleWriteCollection(semitrailers);
            ConsoleWriteCollection(products);
            //autopark.SaveAutoparkXML();
        }

        public static void ConsoleWriteCollection<T>(List<T> list)
        {
            foreach(T t in list)
            {
                Console.WriteLine(t);
            }
        }
    }
}