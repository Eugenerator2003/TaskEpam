using Microsoft.VisualStudio.TestTools.UnitTesting;
using DinerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerLibrary.Tests
{
    [TestClass()]
    public class DinerTests
    {
        [TestMethod()]
        public void SaveLoadTest()
        {
            Diner dinerOld = DinerFabric.GetStandartDiner();
            DinerKitchen kitchen = new DinerKitchen();
            Diner dinerNew = new Diner(kitchen);
            dinerOld.SaveRecipe();
            dinerNew.LoadRecipe();
            CollectionAssert.AreEqual(dinerOld.Menu, dinerNew.Menu);
        }
    }
}