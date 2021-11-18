using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerLibrary
{
    /// <summary>
    /// Class of Dish.
    /// </summary>
    public class Dish
    {
        /// <summary>
        /// Name of the dish.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Count of portions of the dish.
        /// </summary>
        public int PortionCount { get; }

        /// <summary>
        /// Count of portions of the dish left to cooking.
        /// </summary>
        public int PortionLeft { get; private set; }

        /// <summary>
        /// Status of dish is done.
        /// </summary>
        public bool IsDone { get; private set; }

        /// <summary>
        /// Status of cooking now.
        /// </summary>
        public bool CookingNow { get; private set; }

        /// <summary>
        /// Deacreasing a count of left portions by one.
        /// </summary>
        public void AddDonePortion()
        {
            if (PortionLeft > 0)
            {
                PortionLeft--;
            }
            else
                throw new DishPreparingException("No portion nedeed for cooking");

        }

        /// <summary>
        /// Setting status of dish to done.
        /// </summary>
        public void SetDone()
        {
            if (!CookingNow)
            {
                IsDone = true;
                CookingNow = false;
            }
            else
                throw new DishPreparingException("Dish wasn't cooked");
        }

        /// <summary>
        /// Setting status of dish of cooking now.
        /// </summary>
        public void SetPrepared()
        {
            if (!CookingNow)
            {
                CookingNow = true;
            }
            else
                throw new DishPreparingException("Dish is prepared already");
        }

        /// <summary>
        /// Constructor of Dish.
        /// </summary>
        /// <param name="name">Dish name.</param>
        /// <param name="portionCount">Count of portion for cooking.</param>
        public Dish(string name, int portionCount)
        {
            Name = name;
            PortionCount = portionCount;
            IsDone = false;
            PortionLeft = portionCount;
        }
    }
}
