using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerLibrary
{
    /// <summary>
    /// Class of client order.
    /// </summary>
    /// <typeparam name="T">Type of client number field.</typeparam>
    public class Order<T>
    {
        /// <summary>
        /// Client number.
        /// </summary>
        public T ClientNumber { get; }

        /// <summary>
        /// Date of taking order.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Collection of dishes ordered by client.
        /// </summary>
        public List<Dish> Dishes;

        /// <summary>
        /// Costructor of Order.
        /// </summary>
        /// <param name="clientNumber">Client number.</param>
        /// <param name="dishes">Collection of dishes ordered by client.</param>
        public Order(T clientNumber, List<Dish> dishes)
        {
            ClientNumber = clientNumber;
            Dishes = new List<Dish>();
            Dishes.AddRange(dishes);
            Date = DateTime.Now;
        }

        /// <summary>
        /// Costructor of Order.
        /// </summary>
        /// <param name="clientNumber">Client number.</param>
        /// <param name="dishes">Collection of dishes ordered by client.</param>
        /// <param name="date">Date when order was taken.</param>
        public Order(T clientNumber, List<Dish> dishes, DateTime date) : this(clientNumber, dishes)
        {
            Date = date;
        }



    }
}
