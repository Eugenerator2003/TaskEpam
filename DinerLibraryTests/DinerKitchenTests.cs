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
    public class DinerKitchenTests
    {
        [TestMethod()]
        public void CookingTest()
        {
            DinerKitchen kitchen = DinerFabric.GetStandartDinerKitchen();
            Dish dishSteak = new Dish("Steak", Dish.DishType.Dish, 3);
            Dish dishFriedPotatoes = new Dish("Fried potatoes", Dish.DishType.Dish, 2);
            Order<int> order1 = new Order<int>(1, new List<Dish> { dishSteak });
            Order<int> order2 = new Order<int>(2, new List<Dish> { dishFriedPotatoes });
            List<Order<int>> orderExpected = new List<Order<int>>
            {
                order1,
                order2,
            };
            kitchen.AddOrder(order1);
            kitchen.AddOrder(order2);
            while (kitchen.OrdersWaiting.Count > 0)
            {
                kitchen.Cook();
            }
            List<Order<int>> ordersDone = kitchen.GetDoneOrders();
            CollectionAssert.AreEqual(orderExpected, ordersDone);
        }

        [TestMethod()]
        public void FindMostFrequentlyUsedIngredientsTest()
        {
            DinerKitchen kitchen = DinerFabric.GetStandartDinerKitchen();
            Dish dishSteak = new Dish("Steak", Dish.DishType.Dish, 3);
            Dish dishFriedPotatoes = new Dish("Fried potatoes", Dish.DishType.Dish, 2);
            Order<int> order1 = new Order<int>(1, new List<Dish> { dishSteak });
            Order<int> order2 = new Order<int>(2, new List<Dish> { dishFriedPotatoes });
            kitchen.AddOrder(order1);
            kitchen.AddOrder(order2);
            while (kitchen.OrdersWaiting.Count > 0)
            {
                kitchen.Cook();
            }
            List<Order<int>> ordersDone = kitchen.GetDoneOrders();
            Assert.AreEqual("Salt", kitchen.FindMostFrequentlyUsedIngredients()[0]);
        }

        [TestMethod()]
        public void FindLessFrequentlyUsedIngredientsTest()
        {
            DinerKitchen kitchen = DinerFabric.GetStandartDinerKitchen();
            Dish dishSteak = new Dish("Steak", Dish.DishType.Dish, 3);
            Dish dishFriedPotatoes = new Dish("Fried potatoes", Dish.DishType.Dish, 2);
            Order<int> order1 = new Order<int>(1, new List<Dish> { dishSteak });
            Order<int> order2 = new Order<int>(2, new List<Dish> { dishFriedPotatoes });
            kitchen.AddOrder(order1);
            kitchen.AddOrder(order2);
            while (kitchen.OrdersWaiting.Count > 0)
            {
                kitchen.Cook();
            }
            List<Order<int>> ordersDone = kitchen.GetDoneOrders();
            List<string> lessFrequentlyUsed = kitchen.FindLessFrequentlyUsedIngredients();
            Assert.IsTrue(lessFrequentlyUsed.Contains("Potatoes") && lessFrequentlyUsed.Contains("Spices") && lessFrequentlyUsed.Count == 2);
        }

        [TestMethod()]
        public void GetIngredientsByStorageConditionTest()
        {
            DinerKitchen kitchen = DinerFabric.GetStandartDinerKitchen();
            Ingredient beef = new Ingredient("Beef", Ingredient.StorageCondition.Refrigirator, 10, -15, -5, 25);
            List<Ingredient> ingredientsPantry = DinerFabric.GetStandartIngridients();
            ingredientsPantry.Remove(beef);
            CollectionAssert.AreEqual(ingredientsPantry, kitchen.GetIngredientsByStorageCondition(Ingredient.StorageCondition.Pantry));
        }

        [TestMethod()]
        public void MakingRecipeTest()
        {
            Recipe recipeExpected = DinerFabric.GetSteakRecipe();
            DinerKitchen kitchen = new DinerKitchen();
            Recipe recipe = new Recipe("Steak", Dish.DishType.Dish);
            Ingredient beef = new Ingredient("Beef", Ingredient.StorageCondition.Refrigirator, 10, -15, -5);
            Ingredient pepper = new Ingredient("Pepper", Ingredient.StorageCondition.Pantry, 1.4, 0, 25);
            Ingredient salt = new Ingredient("Salt", Ingredient.StorageCondition.Pantry, 0.7, 0, 25);
            Recipe.CookAction step1 = new Recipe.CookAction(Recipe.CookActionType.Cut, DinerKitchen.KitchenAppliances.Knife, 3, beef);
            Recipe.CookAction step2 = new Recipe.CookAction(Recipe.CookActionType.Add, DinerKitchen.KitchenAppliances.PepperShaker, 1, salt, pepper);
            Recipe.CookAction step3 = new Recipe.CookAction(Recipe.CookActionType.Fry, DinerKitchen.KitchenAppliances.Pan, 4);
            kitchen.StartMakingRecipe("Steak", Dish.DishType.Dish);
            kitchen.AddActionToRecipe(step1);
            kitchen.AddActionToRecipe(step2);
            kitchen.AddActionToRecipe(step3);
            kitchen.CompleteMakingRecipe();
            Assert.AreEqual(recipeExpected, kitchen.RecipesBook[0]);
        }
    }
}