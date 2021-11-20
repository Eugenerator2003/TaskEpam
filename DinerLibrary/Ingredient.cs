using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DinerLibrary
{
    /// <summary>
    /// Struct of ingridient.
    /// </summary>
    [Serializable]
    public struct Ingredient
    {
        /// <summary>
        /// Enumeration of storage condition of the ingridients.
        /// </summary>
        public enum StorageCondition
        {
            /// <summary>
            /// Refrigirator storage condition.
            /// </summary>
            Refrigirator,
            /// <summary>
            /// Pantry storage condition.
            /// </summary>
            Pantry
        }

        /// <summary>
        /// Ingredient sorage condition.
        /// </summary>
        public StorageCondition StorageType { get; }

        /// <summary>
        /// Name of the ingridient.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Cost of the one unit of the ingredient.
        /// </summary>
        public double Cost { get; }

        /// <summary>
        /// Quantity of the units of the ingridients.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Minimum temperature for storing the ingredient.
        /// </summary>
        public int TemperatureMin { get; }

        /// <summary>
        /// Maximum temperature for storing the ingredient.
        /// </summary>
        public int TemperatureMax { get; }

        /// <summary>
        /// Adding quantity to ingredient quantity.
        /// </summary>
        /// <param name="quantity">Added quantity.</param>
        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        /// <summary>
        /// Removing quantity from ingredient quantity.
        /// </summary>
        /// <param name="quantity">Removed quantity.</param>
        public void RemoveQuantity(int quantity)
        {
            if (Quantity - quantity > 0)
            {
                Quantity -= quantity;
            }
            else
                throw new ArgumentException("Ingredient quantity can't become negative");
        }


        /// <summary>
        /// Constructor of Ingredient.
        /// </summary>
        /// <param name="name">Name of the ingredient.</param>
        /// <param name="storageCondition">Ingredient storage condition.</param>
        /// <param name="cost">Cost of the ingredient.</param>
        /// <param name="tempMin">Minimum temperature for storing the ingredient.</param>
        /// <param name="tempMax">Maximum temperature for storing the ingredient.</param>
        public Ingredient(string name, StorageCondition storageCondition, double cost, int tempMin, int tempMax)
        {
            Name = name;
            StorageType = storageCondition;
            Cost = cost;
            TemperatureMin = tempMin;
            TemperatureMax = tempMax;
            Quantity = 1;
        }

        /// <summary>
        /// Constructor of Ingredient with arbitrary quantity.
        /// </summary>
        /// <param name="name">Name of the ingredient.</param>
        /// <param name="storageCondition">Ingredient storage condition.</param>
        /// <param name="cost">Cost of the ingredient.</param>
        /// <param name="tempMin">Minimum temperature for storing the ingredient.</param>
        /// <param name="tempMax">Maximum temperature for storing the ingredient.</param>
        /// <param name="quantity">Arbitrary quantity of the ingredient.</param>
        public Ingredient(string name, StorageCondition storageCondition, double cost, int tempMin, int tempMax, int quantity) 
                            : this(name, storageCondition, cost, tempMin, tempMax)
        {
            if (quantity > 0)
            {
                Quantity = quantity;
            }
            else
                throw new ArgumentException("Invalid ingridient quantity");
        }

    }
}
