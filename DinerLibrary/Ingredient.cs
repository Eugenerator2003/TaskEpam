using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DinerLibrary
{
    /// <summary>
    /// Struct of ingridient.
    /// </summary>
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
        /// Minimum temperature for storing the ingredient.
        /// </summary>
        public int TemperatureMin { get; }

        /// <summary>
        /// Maximum temperature for storing the ingredient.
        /// </summary>
        public int TemperatureMax { get; }

        /// <summary>
        /// Quantity of the units of the ingridients.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Opeator of comparing of two ingredients.
        /// </summary>
        /// <param name="ingredient1">First ingredient.</param>
        /// <param name="ingredient2">Second ingredient.</param>
        /// <returns>True if ingredients are equal to each other.</returns>
        public static bool operator ==(Ingredient ingredient1, Ingredient ingredient2)
        {
            bool isEqual = ingredient1.Name == ingredient2.Name &&
                           ingredient1.Cost == ingredient2.Cost &&
                           ingredient1.StorageType == ingredient2.StorageType &&
                           (StorageCondition.Refrigirator == ingredient1.StorageType ?
                           (ingredient1.TemperatureMin == ingredient2.TemperatureMin &&
                           ingredient1.TemperatureMax == ingredient2.TemperatureMax) : true);
            return isEqual;
        }

        /// <summary>
        /// Opeator of comparing of two ingredients.
        /// </summary>
        /// <param name="ingredient1">First ingredient.</param>
        /// <param name="ingredient2">Second ingredient.</param>
        /// <returns>True if ingredients aren't equal to each other.</returns>
        public static bool operator !=(Ingredient ingredient1, Ingredient ingredient2)
        {
            return !(ingredient1 == ingredient2);
        }

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
        /// <param name="storageType">Ingredient storage condition.</param>
        /// <param name="cost">Cost of the ingredient.</param>
        /// <param name="temperatureMin">Minimum temperature for storing the ingredient.</param>
        /// <param name="temperatureMax">Maximum temperature for storing the ingredient.</param>
        public Ingredient(string name, StorageCondition storageType, double cost, int temperatureMin, int temperatureMax)
        {
            Name = name;
            StorageType = storageType;
            Cost = cost;
            TemperatureMin = temperatureMin;
            TemperatureMax = temperatureMax;
            Quantity = 1;
        }

        /// <summary>
        /// Constructor of Ingredient with arbitrary quantity.
        /// </summary>
        /// <param name="name">Name of the ingredient.</param>
        /// <param name="storageType">Ingredient storage condition.</param>
        /// <param name="cost">Cost of the ingredient.</param>
        /// <param name="temperatureMin">Minimum temperature for storing the ingredient.</param>
        /// <param name="temperatureMax">Maximum temperature for storing the ingredient.</param>
        /// <param name="quantity">Arbitrary quantity of the ingredient.</param>
        [JsonConstructor]
        public Ingredient(string name, double cost, StorageCondition storageType, int temperatureMin, int temperatureMax, int quantity) 
                            : this(name, storageType, cost, temperatureMin, temperatureMax)
        {
            if (quantity > 0)
            {
                Quantity = quantity;
            }
            else
                throw new ArgumentException("Invalid ingridient quantity");
        }

        /// <summary>
        /// Converting Dish to String.
        /// </summary>
        /// <returns>Dish converted to String.</returns>
        public override string ToString()
        {
            return $"{Name}; Cost: {Cost}; Storage: {StorageType}; Quantity: {Quantity}; Temp min: {TemperatureMin}; Temp max: {TemperatureMax}";
        }
    }
}
