using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerLibrary
{
    /// <summary>
    /// Fabric of DinerLibrary.
    /// </summary>
    public static class DinerFabric
    {
        /// <summary>
        /// Getting the diner with standart kitchen.
        /// </summary>
        /// <returns>The diner with standart kitchen.</returns>
        public static Diner GetStandartDiner()
        {
            DinerKitchen kitchen = GetStandartDinerKitchen();
            return new Diner(kitchen);
        }

        /// <summary>
        /// Getting the standart diner kitchen.
        /// </summary>
        /// <returns>The diner kitchen.</returns>
        public static DinerKitchen GetStandartDinerKitchen()
        {
            Recipe steakRecipe = DinerFabric.GetSteakRecipe();
            Recipe friedPotatoesRecipe = DinerFabric.GetFriedPotatoesRecipe();
            DinerKitchen kitchen = new DinerKitchen();
            kitchen.AddRecipeToBook(steakRecipe);
            kitchen.AddRecipeToBook(friedPotatoesRecipe);
            foreach (Ingredient ingredient in DinerFabric.GetStandartIngridients())
            {
                kitchen.AddIngredient(ingredient);
            }
            var dictionary = DinerFabric.GetStandartKitchenAppliances();
            foreach (DinerKitchen.KitchenAppliances appliance in dictionary.Keys)
            {
                kitchen.SetKitchenApplianceNumber(appliance, dictionary[appliance]);
            }
            return kitchen;
        }

        /// <summary>
        /// Getting recipe of steak.
        /// </summary>
        /// <returns>The recipe of steak.</returns>
        public static Recipe GetSteakRecipe()
        {
            Recipe recipe = new Recipe("Steak", Dish.DishType.Dish);
            Ingredient beef = new Ingredient("Beef", Ingredient.StorageCondition.Refrigirator, 10, -15, -5);
            Ingredient pepper = new Ingredient("Pepper", Ingredient.StorageCondition.Pantry, 1.4, 0, 25);
            Ingredient salt = new Ingredient("Salt", Ingredient.StorageCondition.Pantry, 0.7, 0, 25);
            Recipe.CookAction step1 = new Recipe.CookAction(Recipe.CookActionType.Cut, DinerKitchen.KitchenAppliances.Knife, 3, beef);
            Recipe.CookAction step2 = new Recipe.CookAction(Recipe.CookActionType.Add, DinerKitchen.KitchenAppliances.PepperShaker, 1, salt, pepper);
            Recipe.CookAction step3 = new Recipe.CookAction(Recipe.CookActionType.Fry, DinerKitchen.KitchenAppliances.Pan, 4);
            recipe.AddCookAction(step1);
            recipe.AddCookAction(step2);
            recipe.AddCookAction(step3);
            recipe.Complete();
            return recipe;
        }

        /// <summary>
        /// Getting recipe of fried potatoes.
        /// </summary>
        /// <returns>The recipe of fried potatoes.</returns>
        public static Recipe GetFriedPotatoesRecipe()
        {
            Recipe recipe = new Recipe("Fried potatoes", Dish.DishType.Dish);
            Ingredient potatoes = new Ingredient("Potatoes", Ingredient.StorageCondition.Refrigirator, 10, -15, -5);
            Ingredient spices = new Ingredient("Spices", Ingredient.StorageCondition.Pantry, 2, 0, 25);
            Ingredient salt = new Ingredient("Salt", Ingredient.StorageCondition.Pantry, 0.7, 0, 25);
            Recipe.CookAction step1 = new Recipe.CookAction(Recipe.CookActionType.Cut, DinerKitchen.KitchenAppliances.Knife, 3, potatoes);
            Recipe.CookAction step2 = new Recipe.CookAction(Recipe.CookActionType.Add, DinerKitchen.KitchenAppliances.PepperShaker, 1, salt, spices);
            Recipe.CookAction step3 = new Recipe.CookAction(Recipe.CookActionType.Fry, DinerKitchen.KitchenAppliances.Pan, 9);
            recipe.AddCookAction(step1);
            recipe.AddCookAction(step2);
            recipe.AddCookAction(step3);
            recipe.Complete();
            return recipe;
        }

        /// <summary>
        /// Get standart complect of kitchen appliances.
        /// </summary>
        /// <returns>Dictionary with kitchen appliances as keys and their count.</returns>
        public static Dictionary<DinerKitchen.KitchenAppliances, int> GetStandartKitchenAppliances()
        {
            Dictionary<DinerKitchen.KitchenAppliances, int> apliances = new Dictionary<DinerKitchen.KitchenAppliances, int>
            {
                [DinerKitchen.KitchenAppliances.Knife] = 5,
                [DinerKitchen.KitchenAppliances.ChopHammer] = 3,
                [DinerKitchen.KitchenAppliances.Pan] = 10,
                [DinerKitchen.KitchenAppliances.PepperShaker] = 2,
                [DinerKitchen.KitchenAppliances.Pot] = 8,
                [DinerKitchen.KitchenAppliances.Spatula] = 12,
                [DinerKitchen.KitchenAppliances.Spoon] = 20,
            };
            return apliances;
        }

        /// <summary>
        /// Return collection with beef, potatoes, salt, pepper, cabbage, carrot, sugar and others.
        /// </summary>
        /// <returns></returns>
        public static List<Ingredient> GetStandartIngridients()
        {
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient("Beef", Ingredient.StorageCondition.Refrigirator, 10, -15, -5, 25),
                new Ingredient("Potatoes", Ingredient.StorageCondition.Pantry, 3, -5, 25, 150),
                new Ingredient("Salt", Ingredient.StorageCondition.Pantry, 0.5, 0, 25, 100),
                new Ingredient("Pepper", Ingredient.StorageCondition.Pantry, 1.5, 0, 25, 35),
                new Ingredient("Spices", Ingredient.StorageCondition.Pantry, 3, 0, 25, 20),
                new Ingredient("Carrot", Ingredient.StorageCondition.Pantry, 2.5, 0, 25, 40),
            };
            return ingredients;
        }
    }
}
