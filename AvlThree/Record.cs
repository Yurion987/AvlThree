using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvlThree
{
    class Record : IRecord<Record>
    {
        public int ID { get; set; }
        public string Nazov { get; set; }

        public Record()
        {
                
        }

        public Record CreateDefault()
        {
            return new Record() { ID = 0, Nazov = "co00" };

        }

        public Record FromByte(byte[] byteArr)
        {
            
            var id = BitConverter.ToInt32(byteArr, 0);
            var nazov = Encoding.ASCII.GetString(byteArr,4,4);
            return new Record() { ID = id, Nazov = nazov };
            
        }

        public int GetSize()
        {
            //pre teraz
            return 8;
        }

        public byte[] Hash()
        {
            return BitConverter.GetBytes(ID);
        }

        public byte[] ToByte()
        {
            var offset = 0;
            var dataNaZapis = new byte[GetSize()];
            var bytPoleValid = BitConverter.GetBytes(ID);
            Buffer.BlockCopy(bytPoleValid, 0, dataNaZapis, offset, bytPoleValid.Length);
            offset = bytPoleValid.Length;
            var bytPoleIndexPrepln = Encoding.ASCII.GetBytes(Nazov);
            Buffer.BlockCopy(bytPoleIndexPrepln, 0, dataNaZapis, offset, bytPoleIndexPrepln.Length);
            offset = bytPoleIndexPrepln.Length;
            return dataNaZapis;
        }

        public bool CompareTo(Record druhy)
        {
            return ID == druhy.ID;
        }
    }
}
