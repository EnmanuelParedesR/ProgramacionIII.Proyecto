using System;
using System.Runtime.Serialization;

namespace MiPrimerProyectoMVC.Controllers
{
    [Serializable]
    internal class VoidValueException : Exception
    {
        public VoidValueException()
        {
        }

        public VoidValueException(string message) : base(message)
        {
        }

        public VoidValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VoidValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}