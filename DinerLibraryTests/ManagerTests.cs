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
    public class ManagerTests
    {
        [DataTestMethod()]
        [DataRow(0, 3, 1, 5)]
        [DataRow(1, 3, 0, 8)]
        public void GetDoneOrdersTest(int menuIndex1, int count1, int menuIndex2, int count2)
        {
            DinerKitchen kitchen = DinerFabric.GetStandartDinerKitchen();
            Manager manager = new Manager(kitchen);
            Dish dish1 = new Dish(manager.Menu[menuIndex1].Item1, manager.Menu[menuIndex1].Item2, count1);
            Dish dish2 = new Dish(manager.Menu[menuIndex2].Item1, manager.Menu[menuIndex2].Item2, count2);
            Order<int> order1 = new Order<int>(1, new List<Dish> { dish1 });
            Order<int> order2 = new Order<int>(2, new List<Dish> { dish2 });
            List<Order<int>> orderExpected = new List<Order<int>>
            {
                order1,
                order2,
            };
            manager.TakeOrder(order1);
            manager.TakeOrder(order2);
            while (kitchen.OrdersWaiting.Count > 0)
            {
                kitchen.Cook();
            }
            List<Order<int>> ordersDone = manager.GetDoneOrders();
            CollectionAssert.AreEqual(orderExpected, ordersDone);
        }

        [DataTestMethod()]
        [DataRow(0, 0, 1, 3)]
        [DataRow(1, 1, 0, 0)]
        public void GetOrdersForPeriodTest(int menuIndex1, int count1, int menuIndex2, int count2)
        {
            DinerKitchen kitchen = DinerFabric.GetStandartDinerKitchen();
            Manager manager = new Manager(kitchen);
            Dish dish1 = new Dish(manager.Menu[menuIndex1].Item1, manager.Menu[menuIndex1].Item2, count1);
            Dish dish2 = new Dish(manager.Menu[menuIndex2].Item1, manager.Menu[menuIndex2].Item2, count2);
            DateTime dateStart = DateTime.Now;
            Order<int> order1 = new Order<int>(1, new List<Dish> { dish1 });
            Order<int> order2 = new Order<int>(2, new List<Dish> { dish2 });
            List<Order<int>> orderExpected = new List<Order<int>>
            {
                order1,
                order2,
            };
            manager.TakeOrder(order1);
            manager.TakeOrder(order2);
            DateTime dateEnd = DateTime.Now;
            List<Order<int>> ordersDone = manager.GetOrdersForPeriod(dateStart, dateEnd);
            CollectionAssert.AreEqual(orderExpected, ordersDone);
        }

        [DataTestMethod()]
        [DataRow(1, 4, 0, 5, Dish.DishType.Dish)]
        [DataRow(0, 0, 1, 1, Dish.DishType.Drink)]
        public void GetExpensesForPeriodTest(int menuIndex1, int count1, int menuIndex2, int count2, Dish.DishType type)
        {
            DinerKitchen kitchen = DinerFabric.GetStandartDinerKitchen();
            Manager manager = new Manager(kitchen);
            Dish dish1 = new Dish(manager.Menu[menuIndex1].Item1, manager.Menu[menuIndex1].Item2, count1);
            Dish dish2 = new Dish(manager.Menu[menuIndex2].Item1, manager.Menu[menuIndex2].Item2, count2);
            double totalCost = 0;
            if (dish1.Type == type)
            {
                totalCost += dish1.PortionCount * manager.Menu[menuIndex1].Item3;
            }
            if (dish2.Type == type)
            {
                totalCost += dish2.PortionCount * manager.Menu[menuIndex2].Item3; ;
            }
            DateTime dateStart = DateTime.Now;
            Order<int> order1 = new Order<int>(1, new List<Dish> { dish1 });
            Order<int> order2 = new Order<int>(2, new List<Dish> { dish2 });
            manager.TakeOrder(order1);
            manager.TakeOrder(order2);
            while (kitchen.OrdersWaiting.Count > 0)
            {
                kitchen.Cook();
            }
            List<Order<int>> ordersDone = manager.GetDoneOrders();
            DateTime dateEnd = DateTime.Now;
            Assert.AreEqual(totalCost, manager.GetExpensesForPeriod(dateStart, dateEnd, type));
        }
    }
}