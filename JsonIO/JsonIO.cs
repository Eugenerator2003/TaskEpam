using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinerLibrary;

namespace DinerLibrary.IO
{
    public class JsonIO : IJsonIO
    {
        /// <summary>
        /// Saving recipes from recipes book to "RecipesBook.json".
        /// </summary>
        /// <param name="recipes">Collection of recipes.</param>
        public void SaveRecipes(List<Recipe> recipes)
        {
            using (StreamWriter streamWriter = new StreamWriter("RecipesBook.json", false, Encoding.UTF8))
            {
                foreach (Recipe recipe in recipes)
                {
                    string recipeInfo = JsonSerializer.Serialize(recipe);
                    streamWriter.WriteLine(recipeInfo);
                }
            }
        }

        /// <summary>
        /// Loading recipes to recipes book from "RecipesBook.json". 
        /// </summary>
        /// <returns>Collection of recipes.</returns>
        public List<Recipe> LoadRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            using (StreamReader streamReader = new StreamReader("RecipesBook.json", Encoding.UTF8))
            {
                string recipeInfo;
                while ((recipeInfo = streamReader.ReadLine()) != null)
                {
                    recipes.Add(JsonSerializer.Deserialize<Recipe>(recipeInfo));
                }
            }
            return recipes;
        }

        /// <summary>
        /// Saving diner status.
        /// </summary>
        /// <param name="recipes">Recipes book.</param>
        /// <param name="orders">Clients orders.</param>
        /// <param name="ingredients">Ingredients.</param>
        /// <param name="appliances">Kitchen appliances.</param>
        public void Save(List<Recipe> recipes, List<Order<int>> orders, List<Ingredient> ingredients, Dictionary<DinerKitchen.KitchenAppliances, int> appliances)
        {
            SaveRecipes(recipes);
            _SaveOrders(orders);
            _SaveIngredients(ingredients);
            _SaveAppliances(appliances);

        }

        private void _SaveOrders(List<Order<int>> orders)
        {
            using (StreamWriter streamWriter = new StreamWriter("Orders.json", false, Encoding.UTF8))
            {
                foreach (Order<int> order in orders)
                {
                    string orderInfo = JsonSerializer.Serialize(order);
                    streamWriter.WriteLine(orderInfo);
                }
            }
        }

        private void _SaveAppliances(Dictionary<DinerKitchen.KitchenAppliances, int> appliances)
        {
            using (StreamWriter streamWriter = new StreamWriter("Appliances.json", false, Encoding.UTF8))
            {
                foreach (KeyValuePair<DinerKitchen.KitchenAppliances, int> pair in appliances)
                {
                    string appliancesInfo = JsonSerializer.Serialize(pair);
                    streamWriter.WriteLine(appliancesInfo);
                }
            }
        }

        private void _SaveIngredients(List<Ingredient> ingredients)
        {
            using (StreamWriter streamWriter = new StreamWriter("Ingredients.json", false, Encoding.UTF8))
            {
                foreach (Ingredient ingredient in ingredients)
                {
                    string ingredientInfo = JsonSerializer.Serialize(ingredient);
                    streamWriter.WriteLine(ingredientInfo);
                }
            }
        }

        public void Load(out List<Recipe> recipes, out List<Order<int>> orders, out List<Ingredient> ingredients, out Dictionary<DinerKitchen.KitchenAppliances, int> appliances)
        {
            throw new NotImplementedException();
        }
    }
}
