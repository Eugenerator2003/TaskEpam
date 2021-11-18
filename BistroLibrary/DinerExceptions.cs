using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DinerLibrary
{
    class CurrentRecipeException : Exception
        {
            public CurrentRecipeException()
            {
            }

            public CurrentRecipeException(string message) : base(message)
            {
            }

            public CurrentRecipeException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected CurrentRecipeException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

    class RecipeAlreadyCompeletedException : Exception
    {
        public RecipeAlreadyCompeletedException()
        {
        }

        public RecipeAlreadyCompeletedException(string message) : base(message)
        {
        }

        public RecipeAlreadyCompeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RecipeAlreadyCompeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    class RecipeException : Exception
    {
        public RecipeException()
        {
        }

        public RecipeException(string message) : base(message)
        {
        }

        public RecipeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RecipeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    class OrderException : Exception
    {
        public OrderException()
        {
        }

        public OrderException(string message) : base(message)
        {
        }

        public OrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    class DishPreparingException : Exception
    {
        public DishPreparingException()
        {
        }

        public DishPreparingException(string message) : base(message)
        {
        }

        public DishPreparingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DishPreparingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    class InvalidIngredientException : Exception
    {
        public InvalidIngredientException()
        {
        }

        public InvalidIngredientException(string message) : base(message)
        {
        }

        public InvalidIngredientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidIngredientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
