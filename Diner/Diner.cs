using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinerLibrary.IO;

namespace DinerLibrary
{
    /// <summary>
    /// Class of diner. Contains a manager and a kitchen.
    /// </summary>
    public class Diner
    {
        private IJsonIO jsonIO;
        private Manager _manager;
        private DinerKitchen _kitchen;

        /// <summary>
        /// Getting the tuple of most expensive action type and its cost.
        /// </summary>
        /// <returns>Couple of most expensive action type and its cost.</returns>
        public (Recipe.CookActionType, double) GetMostExpensiveCookActionType()
        {
            return Recipe.MostExpensiveActionType;
        }

        /// <summary>
        /// Getting the tuple of most cheap action type and its cost.
        /// </summary>
        /// <returns>Couple of most cheap action type and its cost.</returns>
        public (Recipe.CookActionType, double) GetMostCheapCookActionType()
        {
            return Recipe.MostCheapActionType;
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
            return _manager.GetExpensesForPeriod(startDate, endDate, type);
        }

        /// <summary>
        /// Getting cost of the given cook action type.
        /// </summary>
        /// <param name="type">Action type.</param>
        /// <returns>Cost of the given cook action type.</returns>
        public double GetActionTypeCost(Recipe.CookActionType type)
        {
            return Recipe.GetActionTypeCost(type);
        }

        /// <summary>
        /// Getting collection of ingredients with given storage condition type.
        /// </summary>
        /// <param name="storageCondition">Storage condidition type.</param>
        /// <returns>Collection of ingredients with given storage condition type.</returns>
        public List<Ingredient> GetIngredientsByStorageCondition(Ingredient.StorageCondition storageCondition)
        {
            return _kitchen.GetIngredientsByStorageCondition(storageCondition);
        }

        /// <summary>
        /// Adding ingredient to diner kitchen.
        /// </summary>
        /// <param name="ingredients">Ingredient.</param>
        public void AddIngredient(params Ingredient[] ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                _kitchen.AddIngredient(ingredient);
            }
        }

        /// <summary>
        /// Cooking in the kitchen for one period of time.
        /// </summary>
        public void Cook()
        {
            _kitchen.Cook();
        }

        /// <summary>
        /// Getting collection of orders which taken in given period.
        /// </summary>
        /// <param name="startTime">Period start date.</param>
        /// <param name="endTime">Period end date.</param>
        /// <returns>Collection of orders which taken in given period.</returns>
        public List<Order<int>> GetOrdersForPeriod(DateTime startTime, DateTime endTime)
        {
            return _manager.GetOrdersForPeriod(startTime, endTime);
        }

        /// <summary>
        /// Getting a list of ingredients from dinet kitchen.
        /// </summary>
        /// <returns>List of ingredients.</returns>
        public List<Ingredient> GetIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients.AddRange(_kitchen.Ingredients.Values);
            return ingredients;
        }

        /// <summary>
        /// Getting dictionary of free appliences of the diner kitchen.
        /// </summary>
        /// <returns>Dictionary of free appliences.</returns>
        public Dictionary<DinerKitchen.KitchenAppliances, int> GetFreeAppliance()
        {
            return _kitchen.GetFreeAppliences();
        }

        /// <summary>
        /// Constuctor of Diner class.
        /// </summary>
        /// <param name="kitchen">The diner kitchen.</param>
        /// <param name="path">The path to the file in which the information will be saved and loaded.</param>
        public Diner(DinerKitchen kitchen, string path)
        {
            this._kitchen = kitchen;
            _manager = new Manager(this._kitchen);
        }

        /// <summary>
        /// Constuctor of Diner class. Uses standart file path "E:\".
        /// </summary>
        /// <param name="kitchen">The diner kitchen.</param>
        public Diner(DinerKitchen kitchen) : this(kitchen, @"E:\")
        {

        }
    }
}
