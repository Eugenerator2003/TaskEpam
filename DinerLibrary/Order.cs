using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DinerLibrary
{
    /// <summary>
    /// Class of client order.
    /// </summary>
    /// <typeparam name="T">Type of client number field.</typeparam>
    [Serializable]
    public struct Order<T>
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
        public List<Dish> Dishes { get; }

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
        /// Costructor of Order with given date.
        /// </summary>
        /// <param name="clientNumber">Client number.</param>
        /// <param name="dishes">Collection of dishes ordered by client.</param>
        /// <param name="date">Date when order was taken.</param>
        public Order(T clientNumber, List<Dish> dishes, DateTime date) : this(clientNumber, dishes)
        {
            Date = date;
        }

        /// <summary>
        /// Converting Order to String.
        /// </summary>
        /// <returns>The order converted to String.</returns>
        public override string ToString()
        {
            StringBuilder info = new StringBuilder($"Client number: {ClientNumber}; Dishes: ");
            foreach(Dish dish in Dishes)
            {
                info.Append(dish);
            }
            return info.ToString();
        }


    }
}
