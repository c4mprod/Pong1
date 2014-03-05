using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SerializerContract
{
    public interface ISerializer
    {
        void Serialize<T>(ref T obj, string path);
        void Unserialize<T>(ref T obj, string path);
    }
}
