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
        /// Getting recipe.
        /// </summary>
        /// <param name="name">Recipe name.</param>
        /// <returns>Recipe.</returns>
        public static Recipe GetRecipe(string name)
        {
            return new Recipe(name);
        }
    }
}
