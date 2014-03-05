using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using SerializerContract;
using SerializerException;

namespace XmlSerialize
{
    public class Serializer : ISerializer
    {
        public Serializer()
        {
        }

        public void Serialize<T>(ref T obj, string path)
        {
            if (!obj.GetType().IsSerializable)
                throw new DataNotSerializableException();

            XmlSerializer xs = new XmlSerializer(typeof(T));
            FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            try
            {
                xs.Serialize(fs, obj);
                fs.Close();
            }
            catch (Exception)
            {
                throw new RuntimeSerializationException();
            }
        }

        public void Unserialize<T>(ref T obj, string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            if (!obj.GetType().IsSerializable)
                throw new DataNotSerializableException();

            XmlSerializer xs = new XmlSerializer(typeof(T));
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
            try
            {
                obj = (T)xs.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                throw new RuntimeSerializationException();
            }
        }
    }
}
