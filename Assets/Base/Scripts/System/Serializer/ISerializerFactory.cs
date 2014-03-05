using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SerializerContract
{
    public enum SerializerType
    {
        XmlSerializer,
        BinarySerializer,
    }

    public interface ISerializerFactory
    {
        ISerializer Create(SerializerType type);        
    }
}
