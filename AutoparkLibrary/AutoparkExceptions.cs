using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Autopark.Exceptions
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

    class InvalidCargoException : Exception
    {
        public const string Message = "The semitrailer cannot be loaded with this type of cargo";

        public InvalidCargoException()
        {
        }

        public InvalidCargoException(string message) : base(message)
        {
        }

        public InvalidCargoException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }

    class CargoWeightOverflowException : Exception
    {
        public const string Message = "The semitrailer cannot be loaded with weight of this cargo";

        public CargoWeightOverflowException()
        {
        }

        public CargoWeightOverflowException(string message) : base(message)
        {
        }

        public CargoWeightOverflowException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
