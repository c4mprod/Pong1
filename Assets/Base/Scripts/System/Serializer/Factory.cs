using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SerializerException;
using SerializerContract;

namespace SerializerFactory
{
    public class Factory : ISerializerFactory
    {
        public Factory()
        {
        }

        public ISerializer Create(SerializerType type)
        {
            switch (type)
            {
                case SerializerType.XmlSerializer:
                    return new XmlSerialize.Serializer();
                case SerializerType.BinarySerializer:
                    return new BinarySerializer.Serializer();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
