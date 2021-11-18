using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerLibrary
{
    /// <summary>
    /// Class of diner kitchen.
    /// </summary>
    public class DinerKitchen
    {
        /// <summary>
        /// Enumeration of kitchen appliances.
        /// </summary>
        public enum KitchenAppliances
        {
            /// <summary>
            /// Cooking spoon.
            /// </summary>
            Spoon,
            /// <summary>
            /// Cooking knife.
            /// </summary>
            Knife,
            /// <summary>
            /// Cooking chop hammer.
            /// </summary>
            ChopHammer,
            /// <summary>
            /// Cooking spatula.
            /// </summary>
            Spatula,
            /// <summary>
            /// Cooking pan.
            /// </summary>
            Pan, // голд
            /// <summary>
            /// Cooking pot.
            /// </summary>
            Pot,
        }

        private Recipe currentRecipe;
     
        /// <summary>
        /// Collection of completed recipes.
        /// </summary>
        public List<Recipe> RecipesBook { get; }

        /// <summary>
        /// Dictionary of ingredients in the kitchen. 
        /// </summary>
        public Dictionary<string, Ingredient> Ingredients;

        private Dictionary<string, int> _ingridientUsedCount;

        /// <summary>
        /// Dictionary of appliances in the kitchen.
        /// </summary>
        public Dictionary<KitchenAppliances, int> Appliances { get; }

        /// <summary>
        /// Collection of orders which will be done.
        /// </summary>
        public List<Order<int>> OrdersWaiting { get; }

        private List<Order<int>> _ordersDone;

        private Dictionary<KitchenAppliances, int> _applienecesFree;

        private List<(Dish, Recipe)> _currentDishes;

        private int _tempMin;

        private int _tempMax;

        /// <summary>
        /// Getting names of most frequently used ingredients.
        /// </summary>
        /// <returns>Collections of names of most frequently used ingredients.</returns>
        public List<string> FindMostFrequentlyUsedIngredients()
        {
            List<string> ingredientsNames = new List<string>();
            int max = 0;
            foreach(int count in _ingridientUsedCount.Values)
            { 
                if (max < count)
                {
                    max = count;
                }
            }
            foreach(string name in _ingridientUsedCount.Keys)
            {
                if (_ingridientUsedCount[name] == max)
                {
                    ingredientsNames.Add(name);
                }
            }
            return ingredientsNames;
        }

        /// <summary>
        /// Getting names of less frequently used ingredients.
        /// </summary>
        /// <returns>Collections of names of less frequently used ingredients.</returns>
        public List<string> FindLessFrequentlyUsedIngredients()
        {
            List<string> ingredientsNames = new List<string>();
            int min = 0;
            foreach (int count in _ingridientUsedCount.Values)
            {
                if (min > count)
                {
                    min = count;
                }
            }
            foreach (string name in _ingridientUsedCount.Keys)
            {
                if (_ingridientUsedCount[name] == min)
                {
                    ingredientsNames.Add(name);
                }
            }
            return ingredientsNames;
        }

        /// <summary>
        /// Getting collection of ingredients stored in the kitchen with given type.
        /// </summary>
        /// <param name="storageCondition"></param>
        /// <returns>Collection of ingredients stored in the kitchen with given type.</returns>
        public List<Ingredient> GetIngredientsByStorageCondition(Ingredient.StorageCondition storageCondition)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach(Ingredient ingredient in Ingredients.Values)
            {
                if (ingredient.StorageType == storageCondition)
                {
                    ingredients.Add(ingredient);
                }
            }
            return ingredients;
        }

        /// <summary>
        /// Getting dictionary of free appliences of the diner kitchen.
        /// </summary>
        /// <returns>Dictionary of free appliences.</returns>
        public Dictionary<KitchenAppliances, int> GettFreeAppliences()
        {
            return _applienecesFree;
        }

        /// <summary>
        /// Getting collection of done orders.
        /// </summary>
        /// <returns></returns>
        public List<Order<int>> GetDoneOrders()
        {
            // todo создание клонов и очистка готовых заказов
            return _ordersDone;
        }

        /// <summary>
        /// Cooking in a minute.
        /// </summary>
        public void Cook()
        {
            if (OrdersWaiting.Count > 0)
            {
                _CookDishes(0);
                int startIndex = _currentDishes.Count;
                _AddCurrentDishes();
                if (startIndex != _currentDishes.Count)
                {
                    _CookDishes(startIndex);
                }
                _RemoveDoneOrders();
            }
            else
                throw new OrderException("There are no waiting orders");
        }

        /// <summary>
        /// Cooking dishes from queue of dished cooking now.
        /// </summary>
        /// <param name="startIndex">Index on which the dishes from queue will be cooked.</param>
        private void _CookDishes(int startIndex)
        {
            List<(Dish, Recipe)> dishesDone = new List<(Dish, Recipe)>();
            for(int i = startIndex; i < _currentDishes.Count; i++)
            {
                Dish dish = _currentDishes[i].Item1;
                Recipe recipe = _currentDishes[i].Item2;
                if (dish.PortionLeft > 0)
                {
                    if (recipe.CurrentActionIndex == recipe.CookActions.Count - 1 && recipe.CurrentAction.TimeSpend == recipe.CurrentAction.TimeRequired)
                    {
                        _applienecesFree[recipe.CurrentAction.Appliance] += 1;
                        dish.AddDonePortion();
                        recipe.GoToFirstAction();
                        if (dish.PortionLeft == 0)
                            continue;
                    }
                    else if (recipe.CurrentAction.TimeRequired == recipe.CurrentAction.TimeSpend)
                    {
                        _applienecesFree[recipe.CurrentAction.Appliance] += 1;
                        recipe.GoToNextAction();
                        _applienecesFree[recipe.CurrentAction.Appliance] -= 1;
                    }
                    if (recipe.CurrentActionIndex == 0)
                    {
                        _applienecesFree[recipe.CurrentAction.Appliance] -= 1;
                    }
                    recipe.CurrentAction.Cook();

                }
                else
                {
                    dish.SetDone();
                    dishesDone.Add((dish, recipe));
                }
            }
        }

        /// <summary>
        /// Adding dishes to queue of dishes cooked now.
        /// </summary>
        private void _AddCurrentDishes()
        {
            Dictionary<KitchenAppliances, int> appliancesClone = new Dictionary<KitchenAppliances, int>();
            foreach(KitchenAppliances key in _applienecesFree.Keys)
            {
                appliancesClone.Add(key, _applienecesFree[key]);
            }
            foreach (Order<int> order in OrdersWaiting)
            {
                foreach(Dish dish in order.Dishes)
                {
                    Recipe recipe = (Recipe)GetRecipeFromBook(dish).Clone();
                    if (appliancesClone[recipe.CurrentAction.Appliance] > 0)
                    {
                        _currentDishes.Add((dish, recipe));
                        appliancesClone[recipe.CurrentAction.Appliance] -= 1;
                    }
                }
            }
        }

        /// <summary>
        /// Removing done dishes from queue.
        /// </summary>
        private void _RemoveDoneOrders()
        {
            List<Order<int>> ordersDone = new List<Order<int>>();
            foreach(Order<int> order in OrdersWaiting)
            {
                bool isDone = true;
                foreach(Dish dish in order.Dishes)
                {
                    if (!dish.IsDone)
                    {
                        isDone = false;
                        break;
                    }
                }
                if (isDone)
                {
                    ordersDone.Add(order);
                }
            }
            foreach(Order<int> order in ordersDone)
            {
                OrdersWaiting.Remove(order);
                _ordersDone.Add(order);
            }
        }

        /// <summary>
        /// Adding ingredient to the kitchen storage.
        /// </summary>
        /// <param name="ingredient">Ingredient.</param>
        public void AddIngredient(Ingredient ingredient)
        {
            bool isSiutableCondition = true;
            if (Ingredients.Count > 0 && ingredient.StorageType == Ingredient.StorageCondition.Refrigirator)
            {
                if (ingredient.TemperatureMin < _tempMin || ingredient.TemperatureMax > _tempMax)
                    isSiutableCondition = false;
            }
            if (isSiutableCondition)
            {
                if (Ingredients.ContainsKey(ingredient.Name))
                {
                    Ingredients[ingredient.Name].AddQuantity(ingredient.Quantity);
                }
                else
                {
                    Ingredients.Add(ingredient.Name, ingredient);
                    _SetTemperature();
                }
            }
        }

        /// <summary>
        /// Setting maximum and minimum temperature of kitchen storage.
        /// </summary>
        private void _SetTemperature()
        { 
            foreach(Ingredient ingridient in Ingredients.Values)
            {
                if (ingridient.TemperatureMin > _tempMin)
                {
                    _tempMin = ingridient.TemperatureMin;
                }
                if (ingridient.TemperatureMax < _tempMax)
                {
                    _tempMax = ingridient.TemperatureMax;
                }
            }
        }

        /// <summary>
        /// Adding client order from manager.
        /// </summary>
        /// <param name="order">Client order.</param>
        public void AddOrder(Order<int> order) 
        {
            List<Recipe> recipes = new List<Recipe>();
            foreach(Dish dish in order.Dishes)
            {
                recipes.Add(GetRecipeFromBook(dish));
            }
            if (!recipes.Contains(null))
            {
                bool isCanToAdd = true;
                Dictionary<string, int> ingridientsCount = new Dictionary<string, int>();
                foreach (Recipe recipe in recipes)
                {
                    foreach(Recipe.CookAction action in recipe.CookActions)
                    {
                        if (!Appliances.ContainsKey(action.Appliance))
                        {
                            isCanToAdd = false;
                            throw new OrderException("There are no kitchen appliances for this recipe");
                        }
                        foreach(string name in action.Ingredients)
                        {
                            if (ingridientsCount.ContainsKey(name))
                            {
                                ingridientsCount[name] += 1;
                            }
                            else
                            {
                                ingridientsCount.Add(name, 1);
                            }
                        }
                    }
                }
                foreach(string name in ingridientsCount.Keys)
                {
                    if (Ingredients.ContainsKey(name))
                    {
                        if (Ingredients[name].Quantity < ingridientsCount[name])
                        {
                            isCanToAdd = false;
                            throw new OrderException("There are no ingridients for this recipe");
                        }
                        else
                        {
                            Ingredients[name].RemoveQuantity(ingridientsCount[name]);
                            _addIngredientUseCount(name, ingridientsCount[name]);
                        }
                    }
                    else
                        throw new OrderException("There are no ingridients for this recipe");
                }
                if (isCanToAdd)
                {
                    OrdersWaiting.Add(order);
                }
            }
            else
                throw new OrderException("There are no recipe for order dishes");

        }

        private void _addIngredientUseCount(string name, int count)
        {
            if (_ingridientUsedCount.ContainsKey(name))
            {
                _ingridientUsedCount[name] += count;
            }
            else
            {
                _ingridientUsedCount.Add(name, count);
            }
        }

        /// <summary>
        /// Getting recipe from the book of recipes.
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        private Recipe GetRecipeFromBook(Dish dish)
        {
            Recipe requiredRecipe = null;
            foreach(Recipe recipe in RecipesBook)
            {
                if (recipe.Name == dish.Name)
                {
                    requiredRecipe = recipe;
                    break;
                }
            }
            return requiredRecipe;
        }

        /// <summary>
        /// Starting making recipe.
        /// </summary>
        /// <param name="recipeName"></param>
        public void StartMakingRecipe(string recipeName /**/)
        {
            if (currentRecipe == null)
            {
                bool isCanToAdd = true;
                foreach(Recipe recipe in RecipesBook)
                {
                    if (recipe.Name == recipeName)
                    {
                        isCanToAdd = false;
                        throw new RecipeException("Recipe with this name already exists");
                    }
                }
                if (isCanToAdd)
                {
                    currentRecipe = DinerFabric.GetRecipe(recipeName);
                }
            }
            else
                throw new CurrentRecipeException("Can't start making recipe. Now is writting another recipe");
        }

        /// <summary>
        /// Adding action to the current recipe.
        /// </summary>
        /// <param name="cookAction"></param>
        public void AddActionToRecipe(Recipe.CookAction cookAction)
        {
            if (currentRecipe != null)
            {
                currentRecipe.AddCookAction(cookAction);
            }
            else
                throw new CurrentRecipeException("Can't add action to recipe. No current recipe");
        }

        /// <summary>
        /// Completing the current recipe.
        /// </summary>
        public void CompleteMakingRecipe()
        {
            if (currentRecipe != null)
            {
                currentRecipe.Complete();
                RecipesBook.Add(currentRecipe);
                currentRecipe = null;
            }
            else
                throw new CurrentRecipeException("No current recipe!");
        }

        /// <summary>
        /// Setting count of kitchen appliance.
        /// </summary>
        /// <param name="appliance">Kitchen appliance.</param>
        /// <param name="count">Count.</param>
        public void SetKitchenApplianceCount(KitchenAppliances appliance, int count)
        {
            if (Appliances.ContainsKey(appliance))
            {
                Appliances[appliance] = count;
            }
            else
            {
                Appliances.Add(appliance, count);
            }
        }

        // todo а надо ли?
        private void _MakeAppliancesDictionary(params int[] appliancesCounts)
        {
            string[] appliancesNames = Enum.GetNames(typeof(KitchenAppliances));
            if (appliancesCounts.Length == appliancesNames.Length)
            {
                for (int i = 0; i < appliancesCounts.Length; i++)
                {
                    KitchenAppliances appliance;
                    Enum.TryParse(appliancesNames[i], out appliance);
                    //KitchenAppliances appliance = Enum.Parse(typeof(KitchenAppliances), appliancesNames[i]);
                    Appliances.Add(appliance, appliancesCounts[i]);
                    _applienecesFree.Add(appliance, appliancesCounts[i]);
                }
            }
        }

        /// <summary>
        /// Constructor of DinerKitchen.
        /// </summary>
        /// <param name="recipes">Collection of recipes.</param>
        /// <param name="appliancesCounts"></param>
        public DinerKitchen(List<Recipe> recipes, params int[] appliancesCounts)
        {
            RecipesBook = recipes != null ? recipes : new List<Recipe>();
            _currentDishes = new List<(Dish, Recipe)>();
            Appliances = new Dictionary<KitchenAppliances, int>();
            _applienecesFree = new Dictionary<KitchenAppliances, int>();
            _MakeAppliancesDictionary(appliancesCounts);
            Ingredients = new Dictionary<string, Ingredient>();
            _ordersDone = new List<Order<int>>();
            _ingridientUsedCount = new Dictionary<string, int>();
        }
    }
}
