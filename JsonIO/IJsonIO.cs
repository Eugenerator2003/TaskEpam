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
        //void Save();

        //void Load();

        void SaveRecipes(List<Recipe> recipes);

        List<Recipe> LoadRecipe();
    }
}
