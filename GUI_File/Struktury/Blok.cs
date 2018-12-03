using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struktury
{
    class Blok<T> where T : IRecord<T> 
    {
        public int PocetValidnych { get; set; }
        public List<T> Data { get; set; }
        public int IndexPreplnovaciehoBloku { get; set; }

        public Blok()
        {
            var prvok = (T)Activator.CreateInstance(typeof(T));
            Data = new List<T>();
            for (int i = 0; i < Konstanty.BlokovaciFaktor; i++)
            {
                Data.Add(prvok.CreateDefault());
            }
            PocetValidnych = 0;
            IndexPreplnovaciehoBloku = -1;
        }
        public Blok(int existujuci)
        {
            //pripravy sa miesto pre dva bloky v pameti
            Data = new List<T>();

        }

        public void Vloz(T data)
        {
            Data[PocetValidnych] = data;
            PocetValidnych++;
        }
        public void Zmaz(T data)
        {
            var removedIndex = Data.FindIndex(a => a.CompareTo(data));
            if (removedIndex +1 == PocetValidnych) {
                PocetValidnych--;

            }
            else {
                PocetValidnych--;
                var tmp = Data[PocetValidnych];
                Data[removedIndex] = tmp;
            }
 
        }

        public void ZapisBlok(int seek,string fileName)
        {
            var prvok = (T)Activator.CreateInstance(typeof(T));
            if (PocetValidnych != Konstanty.BlokovaciFaktor)
            {
                for (int i = PocetValidnych; i < Konstanty.BlokovaciFaktor; i++)
                {
                    var cloneData = (T)Activator.CreateInstance(typeof(T));
                    Data[i] = cloneData.CreateDefault();
                }
            }
            var dataNaZapis = new byte[(Data.Count * prvok.GetSize()) +( 2 * sizeof(int))];
            int offset = 0;
            
           

            var bytPoleValid = BitConverter.GetBytes(PocetValidnych);
            Buffer.BlockCopy(bytPoleValid,0,dataNaZapis,offset,bytPoleValid.Length);
            offset = bytPoleValid.Length;

            var bytPoleIndexPrepln = BitConverter.GetBytes(IndexPreplnovaciehoBloku);
            Buffer.BlockCopy(bytPoleIndexPrepln, 0, dataNaZapis, offset, bytPoleIndexPrepln.Length);
            offset += bytPoleIndexPrepln.Length;

            foreach (var item in Data)
            {
               
                Buffer.BlockCopy(item.ToByte(),0, dataNaZapis, offset, item.GetSize());
               
                offset += item.GetSize();
            }
            var writer = new BinaryWriter(new FileStream(fileName, FileMode.OpenOrCreate));
            writer.Seek(seek,SeekOrigin.Begin);
            writer.Write(dataNaZapis);
            writer.Close();

        }
        public void NacitajBlok(int seek,string filename)
        {
            using (BinaryReader b = new BinaryReader(
                 File.Open(filename, FileMode.Open)))
            {
                var prvok = (T)Activator.CreateInstance(typeof(T));
                int pos = seek;
              
                //+8 pretoze mma na zaciatku DVA inty ktore oznacuju pocet validnych blokov a index preplnujuceho bloku
                int length = pos+8 + (prvok.GetSize() * Konstanty.BlokovaciFaktor);
                b.BaseStream.Seek(seek,SeekOrigin.Begin);
              
                    var indexPrvychDvoch = 0;
                  for(int i =0; i < Konstanty.BlokovaciFaktor +2;i++)
                {
                        if (indexPrvychDvoch ==0)
                        {
                            indexPrvychDvoch++;
                           
                       
                            var dataSubor = b.ReadBytes(4);
                        //    pos += 4;
                            // ak niekde padat tak mozno tu
                            PocetValidnych = BitConverter.ToInt32(dataSubor, 0);



                        } else if (indexPrvychDvoch == 1)
                        {
                            indexPrvychDvoch++;

                        var dataSubor = b.ReadBytes(4);
                        //     pos += 4;
                        // ak niekde padat tak mozno tu
                        IndexPreplnovaciehoBloku = BitConverter.ToInt32(dataSubor, 0);
                        }
                        else
                        {
                            
                           // var bufferDat = new byte[prvok.GetSize()];
                            var dataSubor = b.ReadBytes(prvok.GetSize());
                         //   pos += prvok.GetSize();
                            // ak niekde padat tak mozno tu
                            var pom = ((T)Activator.CreateInstance(typeof(T))).FromByte(dataSubor);
                            Data.Add(pom);
                        }
                    }
               
            }
        }
    }
}
