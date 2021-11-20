using System;
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
        public void SaveRecipes(List<Recipe> recipes)
        {
            foreach(Recipe recipe in recipes)
            {
                Console.WriteLine(JsonSerializer.Serialize(recipe));
            }
        }

        public List<Recipe> LoadRecipe()
        {
            return null;
        }
    }
}
