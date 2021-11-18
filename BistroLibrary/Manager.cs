using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerLibrary
{
    /// <summary>
    /// Class of manager.
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// Diner menu.
        /// </summary>
        public List<string> Menu;

        private DinerKitchen kitchen;

        private List<Order<int>> _orders;

        /// <summary>
        /// Getting done orders from the kitchen.
        /// </summary>
        /// <returns>The list of done orders.</returns>
        public List<Order<int>> GetDoneOrders()
        {
            return kitchen.GetDoneOrders();
        }

        /// <summary>
        /// Taking order from client.
        /// </summary>
        /// <param name="order">Client order.</param>
        public void TakeOrder(Order<int> order)
        {
            kitchen.AddOrder(order);
            _orders.Add(order);
        }

        /// <summary>
        /// Getting collection of orders which taken in given period.
        /// </summary>
        /// <param name="startTime">Period start date.</param>
        /// <param name="endTime">Period end date.</param>
        /// <returns>Collection of orders which taken in given period.</returns>
        public List<Order<int>> GetOrdersForPeriod(DateTime startTime, DateTime endTime)
        {
            List<Order<int>> ordersInPeriod = new List<Order<int>>();
            foreach(Order<int> order in _orders)
            {
                if (startTime < order.Date && order.Date < endTime)
                {
                    ordersInPeriod.Add(order);
                }
            }
            return ordersInPeriod;
        }

        /// <summary>
        /// Constructor of Manager.
        /// </summary>
        /// <param name="kitchen">The diner kitchen.</param>
        public Manager(DinerKitchen kitchen)
        {
            this.kitchen = kitchen;
            Menu = new List<string>();
            _orders = new List<Order<int>>();
            foreach(Recipe recipe in this.kitchen.RecipesBook)
            {
                Menu.Add(recipe.Name);
            }
        }
    }
}
