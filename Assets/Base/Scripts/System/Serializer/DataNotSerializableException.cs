using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SerializerException
{
    [Serializable]
    public class DataNotSerializableException : Exception
    {
        public DataNotSerializableException() { }
        public DataNotSerializableException(string message) : base(message) { }
        public DataNotSerializableException(string message, Exception inner) : base(message, inner) { }
        protected DataNotSerializableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
