using System;
using System.Runtime.Serialization;

namespace MiPrimerProyectoMVC.Model
{
    [Serializable]
    internal class VoidServiceNameException : Exception
    {
        public VoidServiceNameException()
        {
        }

        public VoidServiceNameException(string message) : base(message)
        {
        }

        public VoidServiceNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VoidServiceNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}