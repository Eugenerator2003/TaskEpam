using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autopark.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autopark.Tests
{
    [TestClass()]
    public class TruckTractorTests
    {
        [TestMethod()]
        public void AttachSemitrailerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UnhookSemitrailerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UploadTest()
        {
            TruckTractor truck = new TruckTractor("001");
            truck.Upload();
        }

        [TestMethod()]
        public void UnloadTest()
        {
            TruckTractor truck = new TruckTractor("001");
            truck.Unload();
        }

        [TestMethod()]
        public void TruckTractorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TruckTractorTest1()
        {
            Assert.Fail();
        }
    }
}