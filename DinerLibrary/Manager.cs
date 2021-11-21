﻿using System;
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
        /// Adding order to orders collection.
        /// </summary>
        /// <param name="order">Client order.</param>
        public void AddOrder(Order<int> order)
        {
            _orders.Add(order);
        }

        /// <summary>
        /// Getting collection of orders.
        /// </summary>
        /// <returns>Collection of orders.</returns>
        public List<Order<int>> GetOrders()
        {
            return _orders;
        }

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
                throw new OrderException("Order can't to add.");
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
        /// Renewing kitchen and orders informations.
        /// </summary>
        public void Renew()
        {
            foreach (Recipe recipe in this.kitchen.RecipesBook)
            {
                Menu.Add((recipe.Name, recipe.DishType, recipe.Cost));
            }
            bool isHave = false;
            foreach(Order<int> order in _orders)
            {
                if (kitchen.OrdersWaiting.Contains(order))
                {
                    isHave = true;
                }
            }
            if (!isHave)
            {
                foreach(Order<int> order in kitchen.OrdersWaiting)
                {
                    _orders.Add(order);
                }
            }
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
            Renew();
        }

        /// <summary>
        /// Converting Manager to String.
        /// </summary>
        /// <returns>Manager converted to String.</returns>
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Comparing the manager with the object.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>True if the object is equal to the manager.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Manager manager = obj as Manager;
            if (manager == null)
                return false;
            bool isEqual = Menu.SequenceEqual(manager.Menu) && _orders.SequenceEqual(manager._orders);
            return isEqual;
        }

        /// <summary>
        /// Getting hash code of the manager.
        /// </summary>
        /// <returns>Hash code of the manager.</returns>
        public override int GetHashCode()
        {
            int hashCode = -1248103559;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<(string, Dish.DishType, double)>>.Default.GetHashCode(Menu);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Order<int>>>.Default.GetHashCode(_orders);
            return hashCode;
        }
    }
}
