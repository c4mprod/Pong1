using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SerializerException
{
    [Serializable]
    public class RuntimeSerializationException : Exception
    {
        public RuntimeSerializationException() { }
        public RuntimeSerializationException(string message) : base(message) { }
        public RuntimeSerializationException(string message, Exception inner) : base(message, inner) { }
        protected RuntimeSerializationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
