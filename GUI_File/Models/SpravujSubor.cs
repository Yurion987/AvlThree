using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Struktury;

namespace Models
{
    class SpravujSubor
    {
        public ZnakStrom<Record> BudovyPodlaID { get; set; }
        public string NazovSuboru { get; set; }
        public SpravujSubor(string nazovSuboru)
        {

            NazovSuboru = nazovSuboru + ".bin";
            BudovyPodlaID = new ZnakStrom<Record>(NazovSuboru);
            var fs = new FileStream(NazovSuboru, FileMode.Create);
            fs.Close();
            var tmpRec = new List<Record>();
            var rnd = new Random(1);
            var popo = 0;
            //try
            //{
            //    for (int i = 1; i < 100; i++)
            //    {

            //        popo = i;
            //        var randomCislo = rnd.Next(1, 1000);

            //        var nazov = "test";
            //        // nazov = nazov.Substring(0, nazov.Length - randomCislo.ToString().Length) + randomCislo.ToString();
            //        if (BudovyPodlaID.Add(new Record() { ID = randomCislo, Nazov = nazov }))
            //        {
            //            tmpRec.Add(new Record() { ID = randomCislo, Nazov = nazov });
            //        }

            //        var suborList = Kontrola().Where(x => x.ID != 0).OrderBy(x => x.ID).ToList();
            //        tmpRec = tmpRec.OrderBy(x => x.ID).ToList();
            //        var a = suborList.Count;
            //        var b = tmpRec.Count;

            //        for (int k = 0; k < tmpRec.Count; k++)
            //        {
            //            if (tmpRec[k].ID != suborList[k].ID)
            //            {
            //                Console.WriteLine();
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    popo = popo;
            //}

            //try
            //{
            //    for (int i = 1; i < 100; i++)
            //    {
            //        if (i==89) {
            //            Console.WriteLine( );
            //        }
            //        popo = i;
            //        var randomCislo = tmpRec[rnd.Next(0, tmpRec.Count)].ID;

            //        var nazov = "test";
            //        // nazov = nazov.Substring(0, nazov.Length - randomCislo.ToString().Length) + randomCislo.ToString();
            //        if (BudovyPodlaID.Remove(new Record() { ID = randomCislo, Nazov = nazov }))
            //        {
            //            tmpRec.RemoveAt(tmpRec.FindIndex(x => x.ID == randomCislo));
            //          //  tmpRec.Remove(new Record() { ID = randomCislo, Nazov = nazov });
            //        }

            //        var suborList = Kontrola().Where(x => x.ID != 0).OrderBy(x => x.ID).ToList();
            //        tmpRec = tmpRec.OrderBy(x => x.ID).ToList();
            //        var a = suborList.Count;
            //        var b = tmpRec.Count;

            //        for (int k = 0; k < tmpRec.Count; k++)
            //        {
            //            if (tmpRec[k].ID != suborList[k].ID)
            //            {
            //                Console.WriteLine();
            //            }
            //        }
            //        if (tmpRec.Count == 0)
            //        {
            //            break;
            //        }
            //    }

            //}
            //catch (Exception e)
            //{
            //    popo = popo;
            //}




            try
            {
                for (int i = 1; i < 1000; i++)
                {
                    popo = i;
                    var operacia = rnd.Next(0, 2);

                    //--------------------------------------------------------------------------------------- TUNAK NASTANE CHYBA
                    if ( i == 202)
                    {
                        Console.WriteLine();
                    }
                    if (operacia == 0)
                    {

                        var randomcislo = rnd.Next(1, 1000);

                        var nazov = "test";
                        // nazov = nazov.substring(0, nazov.length - randomcislo.tostring().length) + randomcislo.tostring();
                        if (BudovyPodlaID.Add(new Record() { ID = randomcislo, Nazov = nazov }))
                        {
                            tmpRec.Add(new Record() { ID = randomcislo, Nazov = nazov });
                        }

                        var suborlist = Kontrola().Where(x => x.ID != 0).OrderBy(x => x.ID).ToList();
                        tmpRec = tmpRec.OrderBy(x => x.ID).ToList();
                        var a = suborlist.Count;
                        var b = tmpRec.Count;

                        for (int k = 0; k < tmpRec.Count; k++)
                        {
                            if (tmpRec[k].ID != suborlist[k].ID)
                            {
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        if (tmpRec.Count != 0)
                        {
                            var randomcislo = tmpRec[rnd.Next(0, tmpRec.Count)].ID;
                            var nazov = "test";
                            // nazov = nazov.substring(0, nazov.length - randomcislo.tostring().length) + randomcislo.tostring();
                            if (BudovyPodlaID.Remove(new Record() { ID = randomcislo, Nazov = nazov }))
                            {
                                tmpRec.RemoveAt(tmpRec.FindIndex(x => x.ID == randomcislo));
                                //  tmprec.remove(new record() { id = randomcislo, nazov = nazov });
                            }

                            var suborlist = Kontrola().Where(x => x.ID != 0).OrderBy(x => x.ID).ToList();
                            tmpRec = tmpRec.OrderBy(x => x.ID).ToList();
                            var a = suborlist.Count;
                            var b = tmpRec.Count;

                            for (int k = 0; k < tmpRec.Count; k++)
                            {
                                if (tmpRec[k].ID != suborlist[k].ID)
                                {
                                    Console.WriteLine();
                                }
                            }
                        }

                    }


                }

            }
            catch (Exception e)
            {
                popo = popo;
            }



            Console.WriteLine();


            /* BudovyPodlaID.Add(new Record() { ID = 4, Nazov = "Tet4" });
             BudovyPodlaID.Add(new Record() { ID = 2, Nazov = "Tet2" });
             BudovyPodlaID.Add(new Record() { ID = 6, Nazov = "Tet6" });
             //    BudovyPodlaID.Add(new Record() { ID = 7, Nazov = "Tet7" });
             BudovyPodlaID.Add(new Record() { ID = 8, Nazov = "Tes8" });
             //     BudovyPodlaID.Add(new Record() { ID = 9, Nazov = "Tes9" });
             BudovyPodlaID.Add(new Record() { ID = 10, Nazov = "Te10" });
             //    BudovyPodlaID.Add(new Record() { ID = 11, Nazov = "Te11" });
             BudovyPodlaID.Add(new Record() { ID = 12, Nazov = "Te12" });
             //   BudovyPodlaID.Add(new Record() { ID = 13, Nazov = "Te13" });
             BudovyPodlaID.Add(new Record() { ID = 14, Nazov = "Te14" });
             //  BudovyPodlaID.Add(new Record() { ID = 15, Nazov = "Te15" });
             BudovyPodlaID.Add(new Record() { ID = 16, Nazov = "Te16" });


             BudovyPodlaID.Remove(new Record() { ID = 2, Nazov = "Tet2" });
             BudovyPodlaID.Remove(new Record() { ID = 10, Nazov = "Te10" });

             BudovyPodlaID.Add(new Record() { ID = 15, Nazov = "Te15" });
             BudovyPodlaID.Add(new Record() { ID = 7, Nazov = "Tet7" });
             BudovyPodlaID.Add(new Record() { ID = 3, Nazov = "Tet3" });

             BudovyPodlaID.Remove(new Record() { ID = 4, Nazov = "Tes8" });

             BudovyPodlaID.Remove(new Record() { ID = 6, Nazov = "Tes2" });
             BudovyPodlaID.Remove(new Record() { ID = 8, Nazov = "Tes6" });
             BudovyPodlaID.Remove(new Record() { ID = 12, Nazov = "Te12" });

             BudovyPodlaID.Remove(new Record() { ID = 14, Nazov = "Tes9" });
             BudovyPodlaID.Remove(new Record() { ID = 16, Nazov = "Tes5" });
             BudovyPodlaID.Remove(new Record() { ID = 3, Nazov = "Tes3" });
             BudovyPodlaID.Remove(new Record() { ID = 7, Nazov = "Tes7" });
             BudovyPodlaID.Remove(new Record() { ID = 15, Nazov = "Tes1" });

             BudovyPodlaID.Remove(new Record() { ID = 1, Nazov = "Tes1" });
             BudovyPodlaID.Add(new Record() { ID = 1, Nazov = "Tes1" });
             BudovyPodlaID.Add(new Record() { ID = 2, Nazov = "Tes2" });
             BudovyPodlaID.Add(new Record() { ID = 3, Nazov = "Tes3" });*/
        }

        public List<string> SeqSubor()
        {
            var retList = new List<string>();
            using (BinaryReader b = new BinaryReader(
                 File.Open(NazovSuboru, FileMode.Open)))
            {
                var prvok = new Record();
                b.BaseStream.Seek(0, SeekOrigin.Begin);
                var seek = 0;
                while (b.BaseStream.Position != b.BaseStream.Length)
                {
                    var indexPrvychDvoch = 0;
                    var pocetValidnych = -1;
                    var indexPreplnovaciehoBloku = -1;
                    var blokString = "Adresa bloku = " + seek;
                    for (int i = 0; i < Konstanty.BlokovaciFaktor + 2; i++)
                    {
                        if (indexPrvychDvoch == 0)
                        {

                            indexPrvychDvoch++;
                            var dataSubor = b.ReadBytes(4);
                            pocetValidnych = BitConverter.ToInt32(dataSubor, 0);
                            blokString += " Pocet Validnych = " + pocetValidnych;
                        }
                        else if (indexPrvychDvoch == 1)
                        {
                            indexPrvychDvoch++;
                            var dataSubor = b.ReadBytes(4);
                            indexPreplnovaciehoBloku = BitConverter.ToInt32(dataSubor, 0);
                            blokString += " Index Preplnovacieho Suboru = " + indexPreplnovaciehoBloku;
                            retList.Add(blokString);
                        }
                        else
                        {

                            var dataSubor = b.ReadBytes(prvok.GetSize());
                            var pom = prvok.FromByte(dataSubor);
                            retList.Add(pom.ToString());
                        }
                    }
                    seek = seek + 8 + (prvok.GetSize() * Konstanty.BlokovaciFaktor);
                }
            }
            return retList;
        }
        public List<Record> Kontrola()
        {
            var retList = new List<Record>();
            using (BinaryReader b = new BinaryReader(
                 File.Open(NazovSuboru, FileMode.Open)))
            {
                var prvok = new Record();
                b.BaseStream.Seek(0, SeekOrigin.Begin);
                var seek = 0;
                while (b.BaseStream.Position != b.BaseStream.Length)
                {
                    var indexPrvychDvoch = 0;
                    var pocetValidnych = -1;
                    var indexPreplnovaciehoBloku = -1;
                    var blokString = "Adresa bloku = " + seek;
                    for (int i = 0; i < Konstanty.BlokovaciFaktor + 2; i++)
                    {
                        if (indexPrvychDvoch == 0)
                        {

                            indexPrvychDvoch++;
                            var dataSubor = b.ReadBytes(4);
                            pocetValidnych = BitConverter.ToInt32(dataSubor, 0);
                            blokString += " Pocet Validnych = " + pocetValidnych;
                        }
                        else if (indexPrvychDvoch == 1)
                        {
                            indexPrvychDvoch++;
                            var dataSubor = b.ReadBytes(4);
                            indexPreplnovaciehoBloku = BitConverter.ToInt32(dataSubor, 0);
                            blokString += " Index Preplnovacieho Suboru = " + indexPreplnovaciehoBloku;

                        }
                        else
                        {

                            var dataSubor = b.ReadBytes(prvok.GetSize());
                            var pom = prvok.FromByte(dataSubor);
                            retList.Add(new Record() { ID = pom.ID, Nazov = pom.Nazov });
                        }
                    }
                    seek = seek + 8 + (prvok.GetSize() * Konstanty.BlokovaciFaktor);
                }
            }
            return retList;
        }
    }
}
