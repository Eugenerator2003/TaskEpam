using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkLibrary.Exceptions
{
    /// <summary>
    /// Represents error with semi-trailer absence.
    /// </summary>
    public class NoSemitrailerException : Exception
    {
        public NoSemitrailerException()
        {
        }

        public NoSemitrailerException(string message) : base(message)
        {
        }

        public NoSemitrailerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Represents error with truck carrying capacity exceed.
    /// </summary>
    public class TruckCarryingCapacityOverflowException : Exception
    {
        public TruckCarryingCapacityOverflowException()
        {
        }

        public TruckCarryingCapacityOverflowException(string message) : base(message)
        {
        }

        public TruckCarryingCapacityOverflowException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Represents error with invalid storage condition for product.
    /// </summary>
    public class InvalidProductStorageConditionException : Exception
    {

        public InvalidProductStorageConditionException()
        {
        }

        public InvalidProductStorageConditionException(string message) : base(message)
        {
        }


    }

    /// <summary>
    /// Represents error with semi-trailer max dimensions overflow.
    /// </summary>
    public class SemitrailleMaxDimensionsOverflowException : Exception
    {

        public SemitrailleMaxDimensionsOverflowException()
        {
        }

        public SemitrailleMaxDimensionsOverflowException(string message) : base(message)
        {
        }

        public SemitrailleMaxDimensionsOverflowException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }

    /// <summary>
    /// Represents error with loaded products absence.
    /// </summary>
    public class NoProductsLoadedException : Exception
    {
        public NoProductsLoadedException()
        {
        }

        public NoProductsLoadedException(string message) : base(message)
        {
        }

        public NoProductsLoadedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoProductsLoadedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Represents error with invalid product type.
    /// </summary>
    public class InvalidProductTypeException : Exception
    {
        public InvalidProductTypeException()
        {
        }

        public InvalidProductTypeException(string message) : base(message)
        {
        }

        public InvalidProductTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidProductTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Represents error with invalid garage Id.
    /// </summary>
    public class InvalidGarageIDException : Exception
    {
        public InvalidGarageIDException()
        {
        }

        public InvalidGarageIDException(string message) : base(message)
        {
        }

        public InvalidGarageIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidGarageIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Represents error with product absence.
    /// </summary>
    public class InvalidProductException : Exception
    {
        public InvalidProductException()
        {
        }

        public InvalidProductException(string message) : base(message)
        {
        }

        public InvalidProductException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
