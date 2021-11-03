using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkLibrary.Exceptions
{
    class NoTrailerException : Exception
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

    class TruckMaxWeightOverflowException : Exception
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

    class InvalidProductStorageConditionException : Exception
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

    class SemitrailleMaxDimensionsOverflowException : Exception
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

    class NoProductsLoadedException : Exception
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

    class InvalidProductTypeException : Exception
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
}
