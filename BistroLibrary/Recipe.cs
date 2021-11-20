using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DinerLibrary
{
    /// <summary>
    /// Class of recipe.
    /// </summary>
    [Serializable]
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
            Squeeze,
            /// <summary>
            /// Adding something.
            /// </summary>
            Add
            
        }

        private static Dictionary<CookActionType, double> _actionCost = new Dictionary<CookActionType, double>
        {
            [CookActionType.Fry] = 3.3,
            [CookActionType.Bake] = 4.7,
            [CookActionType.Boil] = 5.2,
            [CookActionType.Cut] = 1.9,
            [CookActionType.Grind] = 2.5,
            [CookActionType.Mix] = 2.1,
            [CookActionType.Squeeze] = 2.4,
            [CookActionType.Add] = 0.4,
        };

        /// <summary>
        /// The tuple of most expensive action type and its cost.
        /// </summary>
        public static (CookActionType, double) MostExpensiveActionType
        {
            get
            {
                CookActionType typeExpensive = CookActionType.Fry;
                double max = 0;
                foreach(CookActionType type in _actionCost.Keys)
                {
                    if (max < _actionCost[type])
                    {
                        max = _actionCost[type];
                        typeExpensive = type;
                    }
                }
                return (typeExpensive, max);
            }
        }

        /// <summary>
        /// The tuple of most cheap action type and its cost.
        /// </summary>
        public static (CookActionType, double) MostCheapActionType
        {
            get
            {
                (CookActionType, double) tuple = MostExpensiveActionType;
                CookActionType typeCheap = tuple.Item1;
                double min = tuple.Item2;
                foreach (CookActionType type in _actionCost.Keys)
                {
                    if (min > _actionCost[type])
                    {
                        min = _actionCost[type];
                        typeCheap = type;
                    }
                }
                return (typeCheap, min);
            }
        }

        /// <summary>
        /// Struct of cook action. Contains a type of action, a necessary kitchen applience, a requiared time,
        /// a list of necessary ingredients and a cost of the action.  
        /// </summary>
        [Serializable]
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
            [JsonIgnore]
            public double TimeSpend { get; internal set; }

            /// <summary>
            /// Constructor of cook action.
            /// </summary>
            /// <param name="actionType">Type of the action.</param>
            /// <param name="appliance">Type of applience necessary for this action.</param>
            /// <param name="timeRequired">Time necessary for action.</param>
            /// <param name="ingredients">Collection of ingredients necessary for this action.</param>
            public CookAction(CookActionType actionType, DinerKitchen.KitchenAppliances appliance, double timeRequired, params Ingredient[] ingredients)
            {
                Cost = _actionCost[actionType];
                Ingredients = new List<string>();
                foreach (Ingredient ingredient in ingredients)
                {
                    Ingredients.Add(ingredient.Name);
                    Cost += ingredient.Cost;
                }
                ActionType = actionType;
                Appliance = appliance;
                TimeRequired = timeRequired;
                TimeSpend = 0;
            }

            /// <summary>
            /// Constructor for json.
            /// </summary>
            /// <param name="ingredients">Collection of ingredients names.</param>
            /// <param name="actionType">Type of action.</param>
            /// <param name="appliance">Required kitchen appliance.</param>
            /// <param name="cost">Cost of action.</param>
            /// <param name="timeRequired">Time required to compelete action.</param>
            [JsonConstructor]
            public CookAction(List<string> ingredients, CookActionType actionType, DinerKitchen.KitchenAppliances appliance, double cost, double timeRequired)
            {
                Cost = cost;
                Ingredients = new List<string>();
                Ingredients.AddRange(ingredients);
                ActionType = actionType;
                Appliance = appliance;
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
                    TimeSpend += 1;
                }
                else
                    throw new RecipeException("Current action is already completed");
            }

            /// <summary>
            /// Static operator of comparing of two cook actions.
            /// </summary>
            /// <param name="cookAction1">First cook action.</param>
            /// <param name="cookAction2">First cook action.</param>
            /// <returns>True if first cook action is equal to second cook action.</returns>
            public static bool operator ==(CookAction cookAction1, CookAction cookAction2)
            {
                bool result = cookAction1.ActionType == cookAction2.ActionType &&
                              cookAction1.Appliance == cookAction2.Appliance &&
                              cookAction1.Cost == cookAction2.Cost &&
                              cookAction1.TimeRequired == cookAction2.TimeRequired &&
                              cookAction1.Ingredients.SequenceEqual(cookAction2.Ingredients);
                return result;
            }

            /// <summary>
            /// Static operator of comparing of two cook action.s
            /// </summary>
            /// <param name="cookAction1">First cook action.</param>
            /// <param name="cookAction2">First cook action.</param>
            /// <returns>rue if first cook action isn't equal to second cook action.</returns>
            public static bool operator !=(CookAction cookAction1, CookAction cookAction2)
            {
                return !(cookAction1 == cookAction2);
            }

            /// <summary>
            /// Converting CookActiong to String.
            /// </summary>
            /// <returns>Cook action converted to String.</returns>
            public override string ToString()
            {
                StringBuilder info = new StringBuilder($"Type: {ActionType}, Appliance: {Appliance}, Cost: {Cost}, Required time: {TimeRequired}");
                if (Ingredients.Count > 0)
                {
                    info.Append(", Ingredients: ");
                    foreach (string ingredientName in Ingredients)
                    {
                        info.Append($"{ingredientName}, ");
                    }
                    info.Remove(info.Length - 2, 2);
                }
                info.Append(";");
                return info.ToString();
            }

        }

        /// <summary>
        /// Name of the recipe.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Type of dish which cooking described in recipe.
        /// </summary>
        public Dish.DishType DishType { get; private set; }

        /// <summary>
        /// Property for getting status of the recipe.
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// List of ingredients names necessary for this recipe.
        /// </summary>
        [JsonIgnore]
        public List<string> Ingredients { get; private set; }

        /// <summary>
        /// Summary cost of all the recipe.
        /// </summary>
        [JsonIgnore]
        public double Cost { get; private set; }

        /// <summary>
        /// Time required for cooking dish by recipe.
        /// </summary>
        [JsonIgnore]
        public double TimeRequired { get; private set; }

        /// <summary>
        /// Index of current item of the cook action collection.
        /// </summary>
        [JsonIgnore]
        public int CurrentActionIndex { get; private set; }

        /// <summary>
        /// Current item of the cook action collection.
        /// </summary>
        [JsonIgnore]
        public CookAction CurrentAction { get => CookActions[CurrentActionIndex]; protected set { CookActions[CurrentActionIndex] = value; } }
        
        /// <summary>
        /// Time required on current action.
        /// </summary>
        [JsonIgnore]
        public double CurrentTimeRequired { get => CurrentAction.TimeRequired; }

        /// <summary>
        /// Time spend on current action.
        /// </summary>
        [JsonIgnore]
        public double CurrentTimeSpend { get => CurrentAction.TimeSpend; }

        /// <summary>
        /// Collection of the cook actions of the recipe.
        /// </summary>
        public List<CookAction> CookActions { get; }

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
        /// Cooking for a minute by recipe.
        /// </summary>
        public void CookByRecipe()
        {
            if (CurrentAction.TimeSpend < CurrentAction.TimeRequired)
            {
                CookAction action = CurrentAction;
                action.TimeSpend += 1;
                CurrentAction = action;
            }
            else
                throw new RecipeException($"Current action already ended. {this} - {CurrentActionIndex} - {CurrentAction}");
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
            Recipe clone = new Recipe(Name, DishType);
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
        /// <param name="dishType">Type of dish which will be described in recipe.</param>
        public Recipe(string name, Dish.DishType dishType)
        {
            Name = name;
            DishType = dishType;
            CookActions = new List<CookAction>();
            Ingredients = new List<string>();
            CurrentActionIndex = 0;
        }

        /// <summary>
        /// Constructor of Recipe for json.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dishType">Type of dish which will be described in recipe.</param>
        /// <param name="isCompleted">Is the recipe completed.</param>
        /// <param name="cookActions">Coolection of cook actions.</param>
        [JsonConstructor]
        public Recipe(string name, Dish.DishType dishType, bool isCompleted, List<CookAction> cookActions)
                : this(name, dishType)
        {
            foreach(CookAction action in cookActions)
            {
                AddCookAction(action);
            }
            IsCompleted = isCompleted;
        }

        /// <summary>
        /// Comparing of the recipe with other object.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>True if object is equal to the recipe.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Recipe recipe = obj as Recipe;
            if (recipe == null)
                return false;
            bool result = DishType == recipe.DishType &&
                          Name == recipe.Name &&
                          IsCompleted == recipe.IsCompleted;
            if (result == true && CookActions.Count == recipe.CookActions.Count)
            {
                for(int i = 0; i < CookActions.Count; i++)
                {
                    if (CookActions[i] != recipe.CookActions[i])
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Getting hash code of the recipe.
        /// </summary>
        /// <returns>Hash code of the recipe.</returns>
        public override int GetHashCode()
        {
            int hashCode = -839592172;
            hashCode = hashCode * -1521134295 + CurrentActionIndex.GetHashCode();
            hashCode = hashCode * -1521134295 + DishType.GetHashCode();
            hashCode = hashCode * -1521134295 + IsCompleted.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(Ingredients);
            hashCode = hashCode * -1521134295 + Cost.GetHashCode();
            hashCode = hashCode * -1521134295 + TimeRequired.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
        
        /// <summary>
        /// Convertation the Recipe to String.
        /// </summary>
        /// <returns>The Recipe converted to String.</returns>
        public override string ToString()
        {
            StringBuilder info = new StringBuilder($"Recipe of {Name}. Type: {DishType}. Cost: {Cost}.");
            if (CookActions.Count > 0)
            {
                info.Append(" Actions: ");
                foreach(CookAction action in CookActions)
                {
                    info.Append($"{action}");
                }
            }
            return info.ToString();
        }
    }
}
