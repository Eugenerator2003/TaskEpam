using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkLibrary.Exceptions
{
    public class NoTrailerException : Exception
    {
        public NoTrailerException()
        {
        }

        public NoTrailerException(string message) : base(message)
        {
        }

        public NoTrailerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class TruckMaxWeightOverflowException : Exception
    {
        public TruckMaxWeightOverflowException()
        {
        }

        public TruckMaxWeightOverflowException(string message) : base(message)
        {
        }

        public TruckMaxWeightOverflowException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TruckMaxWeightOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class InvalidProductStorageConditionException : Exception
    {

        public InvalidProductStorageConditionException()
        {
        }

        public InvalidProductStorageConditionException(string message) : base(message)
        {
        }

        public InvalidProductStorageConditionException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }

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
