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
        public void SaveLoadRecipesTest()
        {
            Diner dinerOld = DinerFabric.GetStandartDiner();
            DinerKitchen kitchen = new DinerKitchen();
            Diner dinerNew = new Diner(kitchen);
            dinerOld.SaveRecipe();
            dinerNew.LoadRecipe();
            CollectionAssert.AreEqual(dinerOld.Menu, dinerNew.Menu);
        }

        [TestMethod()]
        public void SaveLoadTest()
        {
            Diner dinerOld = DinerFabric.GetStandartDiner();
            Dish dishSteak = new Dish("Steak", Dish.DishType.Dish, 3);
            Dish dishFriedPotatoes = new Dish("Fried potatoes", Dish.DishType.Dish, 2);
            Order<int> order1 = new Order<int>(1, new List<Dish> { dishSteak });
            Order<int> order2 = new Order<int>(2, new List<Dish> { dishFriedPotatoes });
            dinerOld.TakeOrder(order1);
            dinerOld.TakeOrder(order2);
            while (dinerOld.GetWaintingOrders().Count > 0)
            {
                dinerOld.Cook();
            }
            dinerOld.Save();
            DinerKitchen kitchen = new DinerKitchen();
            Diner dinerNew = new Diner(kitchen);
            dinerNew.Load();
            Assert.AreEqual(dinerOld, dinerNew);
        }
    }
}