using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struktury
{
    interface IRecord<T>
    {
        byte[] Hash();
        int GetSize();
        T FromByte(byte[] byteArr);
        T CreateDefault();
        byte[] ToByte();
        bool CompareTo(T druhy);
    }
}
