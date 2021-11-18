using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerLibrary
{
    /// <summary>
    /// Class of recipe.
    /// </summary>
    public class Recipe : ICloneable
    {
        /// <summary>
        /// Enumeration of recipe action.
        /// </summary>
        public enum CookActionType
        {
            /// <summary>
            /// Frying.
            /// </summary>
            Fry,
            /// <summary>
            /// Boiling.
            /// </summary>
            Boil,
            /// <summary>
            /// Grinding.
            /// </summary>
            Grind,
            /// <summary>
            /// Cutting.
            /// </summary>
            Cut,
            /// <summary>
            /// Mixing.
            /// </summary>
            Mix,
            /// <summary>
            /// Baking.
            /// </summary>
            Bake,
            /// <summary>
            /// Squeezing.
            /// </summary>
            Squeeze
        }

        private static Dictionary<CookActionType, double> _actionCost = new Dictionary<CookActionType, double>
        {
            [CookActionType.Fry] = 3.3,
            [CookActionType.Bake] = 4.7,
            [CookActionType.Boil] = 5.2,
            [CookActionType.Cut] = 1.9,
            [CookActionType.Grind] = 2.5,
            [CookActionType.Mix] = 2.1,
            [CookActionType.Squeeze] = 2.4
        };

        /// <summary>
        /// Struct of cook action. Contains a type of action, a necessary kitchen applience, a requiared time,
        /// a list of necessary ingredients and a cost of the action.  
        /// </summary>
        public struct CookAction
        {
            /// <summary>
            /// List of necessary ingredients.
            /// </summary>
            public List<string> Ingredients { get; }
            
            /// <summary>
            /// Type of action.
            /// </summary>
            public CookActionType ActionType { get; }

            /// <summary>
            /// Kitchen appliance necessary for this action.
            /// </summary>
            public DinerKitchen.KitchenAppliances Appliance { get; }

            /// <summary>
            /// Cost of the action.
            /// </summary>
            public double Cost { get; }

            /// <summary>
            /// Time required for the action.
            /// </summary>
            public double TimeRequired { get; }

            /// <summary>
            /// Time spent on this action.
            /// </summary>
            public double TimeSpend { get; private set; }

            /// <summary>
            /// Constructor of cook action.
            /// </summary>
            /// <param name="actionType">Type of the action.</param>
            /// <param name="applience">Type of applience necessary for this action.</param>
            /// <param name="timeRequired">Time necessary for action.</param>
            /// <param name="ingredients">Collection of ingredients necessary for this action.</param>
            public CookAction(CookActionType actionType, DinerKitchen.KitchenAppliances applience, double timeRequired, params Ingredient[] ingredients)
            {
                Cost = _actionCost[actionType];
                Ingredients = new List<string>();
                foreach(Ingredient ingredient in ingredients)
                {
                    Ingredients.Add(ingredient.Name);
                    Cost += ingredient.Cost;
                }
                ActionType = actionType;
                Appliance = applience;
                TimeRequired = timeRequired;
                TimeSpend = 0;
            }

            /// <summary>
            /// Performing action while a minute.
            /// </summary>
            public void Cook()
            {
                if (TimeSpend < TimeRequired)
                {
                    TimeSpend++;
                }
                else
                    throw new RecipeException("Current action is already completed");
            }

        }

        /// <summary>
        /// Property for getting status of the recipe.
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// List of ingredients names necessary for this recipe.
        /// </summary>
        public List<string> Ingredients { get; private set; }

        /// <summary>
        /// Summary cost of all the recipe.
        /// </summary>
        public double Cost { get; private set; }

        /// <summary>
        /// Time required for cooking dish by recipe.
        /// </summary>
        public double TimeRequired { get; private set; }

        /// <summary>
        /// Index of current item of the cook action collection.
        /// </summary>
        public int CurrentActionIndex { get; private set; }

        /// <summary>
        /// Current item of the cook action collection.
        /// </summary>
        public CookAction CurrentAction { get => CookActions[CurrentActionIndex]; }

        /// <summary>
        /// Name of the recipe.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Collection of the cook actions of the recipe.
        /// </summary>
        public List<CookAction> CookActions { get; private set; }

        /// <summary>
        /// Getting cost of the given cook action type.
        /// </summary>
        /// <param name="type">Action type.</param>
        /// <returns>Cost of the given cook action type.</returns>
        public static double GetActionTypeCost(CookActionType type)
        {
            return _actionCost[type];
        }

        /// <summary>
        /// Going to next cook action.
        /// </summary>
        public void GoToNextAction()
        {
            if (IsCompleted)
            {
                if (CurrentActionIndex + 1 < CookActions.Count)
                {
                    CurrentActionIndex++;
                }
                else
                    throw new RecipeException("Recipe doesn't have next cook actions");
            }
            else
                throw new RecipeException("Can't go to next cook action. Recipe doesn't completed");
        }

        /// <summary>
        /// Going to first cook action.
        /// </summary>
        public void GoToFirstAction()
        {
            if (IsCompleted)
            {
                CurrentActionIndex = 0;
            }
            else
                throw new RecipeException("Can't go to first cook action. Recipe doesn't completed");
        }

        /// <summary>
        /// Adding a cook action for cook actions collection.
        /// </summary>
        /// <param name="action"></param>
        public void AddCookAction(CookAction action)
        {
            if (!IsCompleted)
            {
                CookActions.Add(action);
                Ingredients.AddRange(action.Ingredients);
                Cost += action.Cost;
                TimeRequired += action.TimeRequired;
            }
            else
                throw new RecipeAlreadyCompeletedException("Can't add action to recipe. Recipe is already completed");
        }

        /// <summary>
        /// Completing the recipe. After using this method you can't adding a another cook action.
        /// </summary>
        public void Complete()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
            }
            else
                throw new RecipeAlreadyCompeletedException("Can't complete recipe. Recipe is already completed");
        }

        /// <summary>
        /// Getting clone of the recipe with current action index equals to zero.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Recipe clone = new Recipe(Name);
            foreach (CookAction action in CookActions)
                clone.AddCookAction(action);
            clone.IsCompleted = IsCompleted;
            clone.CurrentActionIndex = 0;
            return clone;
        }

        /// <summary>
        /// Constructor of Recipe.
        /// </summary>
        /// <param name="name"></param>
        public Recipe(string name)
        {
            Name = name;
            CookActions = new List<CookAction>();
            Ingredients = new List<string>();
            CurrentActionIndex = 0;
        }

        
    }
}
