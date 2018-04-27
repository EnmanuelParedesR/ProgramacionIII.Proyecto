using System;
using System.Runtime.Serialization;

namespace MiPrimerProyectoMVC.Model
{
    [Serializable]
    internal class CurrentlyInUseException : Exception
    {
        public CurrentlyInUseException()
        {
        }

        public CurrentlyInUseException(string message) : base(message)
        {
        }

        public CurrentlyInUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CurrentlyInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}