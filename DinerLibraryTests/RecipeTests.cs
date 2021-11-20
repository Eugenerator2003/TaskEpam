using Microsoft.VisualStudio.TestTools.UnitTesting;
using DinerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerLibrary.Tests
{
    [TestClass()]
    public class RecipeTests
    {
        [DataTestMethod()]
        [DataRow(Recipe.CookActionType.Mix, DinerKitchen.KitchenAppliances.Spatula, 3)]
        [DataRow(Recipe.CookActionType.Fry, DinerKitchen.KitchenAppliances.Pan, 4)]
        [DataRow(Recipe.CookActionType.Cut, DinerKitchen.KitchenAppliances.Knife, 6)]
        public void CookActionCookTest(Recipe.CookActionType type, DinerKitchen.KitchenAppliances appliance,
                                    double timeRequied)
        {
            Recipe.CookAction cookAction = new Recipe.CookAction(type, appliance, timeRequied);
            for(int i = 0; i < timeRequied; i++)
            {
                cookAction.Cook();
            }
            double cost = Recipe.GetActionTypeCost(type);
            bool result = cost == cookAction.Cost && timeRequied == cookAction.TimeSpend;
            Assert.IsTrue(result);
        }

        [DataTestMethod()]
        [DataRow("Steak", "Meat", "Pepper", "Salt", 10, 1.5, 0.7)]
        [DataRow("Fried potatoes", "Potatoes", "Spices", "Salt", 5, 2, 0.7)]
        public void MakingRecipeTest(string recipeName, string ingrName1, string ingrName2, string ingrName3,
                                    double ingrCost1, double ingrCost2, double ingrCost3)
        {
            Recipe recipe = new Recipe(recipeName, Dish.DishType.Dish);
            Ingredient ingr1 = new Ingredient(ingrName1, Ingredient.StorageCondition.Refrigirator, ingrCost1, -15, -4);
            Ingredient ingr2 = new Ingredient(ingrName2, Ingredient.StorageCondition.Pantry, ingrCost2, 15, 25);
            Ingredient ingr3 = new Ingredient(ingrName3, Ingredient.StorageCondition.Pantry, ingrCost3, 15, 25);
            Recipe.CookAction step1 = new Recipe.CookAction(Recipe.CookActionType.Cut, DinerKitchen.KitchenAppliances.Knife, 3, ingr1);
            Recipe.CookAction step2 = new Recipe.CookAction(Recipe.CookActionType.Add, DinerKitchen.KitchenAppliances.PepperShaker, 1, ingr2, ingr3);
            Recipe.CookAction step3 = new Recipe.CookAction(Recipe.CookActionType.Fry, DinerKitchen.KitchenAppliances.Pan, 9);
            recipe.AddCookAction(step1);
            recipe.AddCookAction(step2);
            recipe.AddCookAction(step3);
            recipe.Complete();
            List<Recipe.CookAction> actions = new List<Recipe.CookAction>
            {
                step1,
                step2,
                step3
            };
            Recipe recipeExpeted = new Recipe(recipeName, Dish.DishType.Dish, true, actions);
            Assert.AreEqual(recipeExpeted, recipe);

        }

        [TestMethod()]
        public void MostExpensiveActionTypeTest()
        {
            (Recipe.CookActionType, double) tuple;
            tuple = Recipe.MostExpensiveActionType;
            bool result = tuple.Item1 == Recipe.CookActionType.Boil && tuple.Item2 == 5.2;
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void MostCheapActionTypeTest()
        {
            (Recipe.CookActionType, double) tuple;
            tuple = Recipe.MostCheapActionType;
            bool result = tuple.Item1 == Recipe.CookActionType.Add && tuple.Item2 == 0.4;
            Assert.IsTrue(result);
        }
    }
}