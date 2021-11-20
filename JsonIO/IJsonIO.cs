using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinerLibrary;

namespace DinerLibrary.IO
{
    public interface IJsonIO
    {
        void Save(List<Recipe> recipes, List<Order<int>> orders, List<Ingredient> ingredients,
                                                                Dictionary<DinerKitchen.KitchenAppliances, int> appliances);

        void Load(out List<Recipe> recipes, out List<Order<int>> orders, out List<Ingredient> ingredients,
                                                                out Dictionary<DinerKitchen.KitchenAppliances, int> appliances);

        void SaveRecipes(List<Recipe> recipes);

        List<Recipe> LoadRecipes();
    }
}
