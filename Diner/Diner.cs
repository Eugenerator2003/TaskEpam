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
        IJsonIO jsonIO;

        /// <summary>
        /// Manager of the diner.
        /// </summary>
        public Manager Manager { get; }

        /// <summary>
        /// Kitchen of the diner.
        /// </summary>
        public DinerKitchen Kitchen { get; }

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
            return Kitchen.GetIngredientsByStorageCondition(storageCondition);
        }

        /// <summary>
        /// Adding ingredient to diner kitchen.
        /// </summary>
        /// <param name="ingredients">Ingredient.</param>
        public void AddIngredient(params Ingredient[] ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                Kitchen.AddIngredient(ingredient);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Cook()
        {
            Kitchen.Cook();
        }

        /// <summary>
        /// Getting collection of orders which taken in given period.
        /// </summary>
        /// <param name="startTime">Period start date.</param>
        /// <param name="endTime">Period end date.</param>
        /// <returns>Collection of orders which taken in given period.</returns>
        public List<Order<int>> GetOrdersForPeriod(DateTime startTime, DateTime endTime)
        {
            return Manager.GetOrdersForPeriod(startTime, endTime);
        }

        /// <summary>
        /// Getting a list of ingredients from dinet kitchen.
        /// </summary>
        /// <returns>List of ingredients.</returns>
        public List<Ingredient> GetIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients.AddRange(Kitchen.Ingredients.Values);
            return ingredients;
        }

        /// <summary>
        /// Getting dictionary of free appliences of the diner kitchen.
        /// </summary>
        /// <returns>Dictionary of free appliences.</returns>
        public Dictionary<DinerKitchen.KitchenAppliances, int> GetFreeAppliance()
        {
            return Kitchen.GettFreeAppliences();
        }

        /// <summary>
        /// Constuctor of Diner class.
        /// </summary>
        /// <param name="kitchen">The diner kitchen.</param>
        /// <param name="path">The path to the file in which the information will be saved and loaded.</param>
        public Diner(DinerKitchen kitchen, string path)
        {
            this.Kitchen = kitchen;
            Manager = new Manager(this.Kitchen);
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
