using System;
using System.Runtime.Serialization;

namespace MiPrimerProyectoMVC.Model
{
    [Serializable]
    internal class VoidExpirationDateException : Exception
    {
        public VoidExpirationDateException()
        {
        }

        public VoidExpirationDateException(string message) : base(message)
        {
        }

        public VoidExpirationDateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VoidExpirationDateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}