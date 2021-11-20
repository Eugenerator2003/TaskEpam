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
        /// Diner menu. Contains name of dish and its type and price.
        /// </summary>
        public List<(string, Dish.DishType, double)> Menu;

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
            try
            {
                kitchen.AddOrder(order);
                _orders.Add(order);
            }
            catch 
            {
                throw new OrderException("Order contains unknown dish name.");
            }
        }

        /// <summary>
        /// Getting collection of orders which taken in given period.
        /// </summary>
        /// <param name="startDate">Period start date.</param>
        /// <param name="endDate">Period end date.</param>
        /// <returns>Collection of orders which taken in given period.</returns>
        public List<Order<int>> GetOrdersForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Order<int>> ordersInPeriod = new List<Order<int>>();
            foreach(Order<int> order in _orders)
            {
                if (startDate <= order.Date && order.Date <= endDate)
                {
                    ordersInPeriod.Add(order);
                }
            }
            return ordersInPeriod;
        }

        /// <summary>
        /// Getting expenses of cooking dishes in given period and by given dish type.
        /// </summary>
        /// <param name="startDate">Period start date.</param>
        /// <param name="endDate">Perios end date.</param>
        /// <param name="type">Dish type.</param>
        /// <returns>Expenses of cooking dishes in given period and by given dish type.</returns>
        public double GetExpensesForPeriod(DateTime startDate, DateTime endDate, Dish.DishType type)
        {
            double expenses = 0;
            foreach(Order<int> order in GetOrdersForPeriod(startDate, endDate))
            {
                foreach(Dish dish in order.Dishes)
                {
                    foreach((string, Dish.DishType, double) menuNote in Menu)
                    {
                        if (dish.Name == menuNote.Item1 && dish.Type == menuNote.Item2 && dish.Type == type)
                        {
                            expenses += menuNote.Item3 * dish.PortionCount;
                        }
                    }
                }
            }
            return expenses;
        }

        /// <summary>
        /// Constructor of Manager.
        /// </summary>
        /// <param name="kitchen">The diner kitchen.</param>
        public Manager(DinerKitchen kitchen)
        {
            this.kitchen = kitchen;
            Menu = new List<(string, Dish.DishType, double)>();
            _orders = new List<Order<int>>();
            foreach(Recipe recipe in this.kitchen.RecipesBook)
            {
                Menu.Add((recipe.Name, recipe.DishType, recipe.Cost));
            }
        }
    }
}
