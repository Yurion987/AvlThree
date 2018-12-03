using AvlThree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvlThree
{

    class ZnakStrom<T> where T : IRecord<T>
    {
        private ZnakNode Root;
        private int IndexVSubore;
        private string NazovSuboru;
        private List<int> VolneAdresy;
        public ZnakStrom()
        {
            Root = new ZnakNodeExterny();
            IndexVSubore = 0;
            NazovSuboru = "Data.bin";
            VolneAdresy = new List<int>();


        }
        public bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        // var pocetHashu = BitConverter.GetBytes(tmpParet.Blok[i]);
        //   var pos = nadKtorym.Hlbka % 8;
        //   var konkretnyByte = nadKtorym.Hlbka / 8;
        //           if (IsBitSet(pocetHashu[konkretnyByte], pos))
        private void Rozsir(ref ZnakNode nadKtorym, T novyPrvok)
        {


            var parentNodu = nadKtorym.Parent;

            var tmpExternyNodeCopy = nadKtorym as ZnakNodeExterny;
            var seekParent = tmpExternyNodeCopy.Seek;

            var poleByyyte = new List<byte>(Konstanty.BlokovaciFaktor);
            //nacitja list byte na kontrolu vytovrenie noveho bloku
            var blokExt = new Blok<T>(1);
            blokExt.NacitajBlok(seekParent, NazovSuboru);
            //pridanie dalsieho bloku to pomocneho bloku podla ktoreho sa najde hladane 2 bloky kam sa tento blok ulozi
            blokExt.Data.Add(novyPrvok);


            var novyInternyNode = new ZnakNodeInterny();
            var novyPravy = new ZnakNodeExterny() { Hlbka = nadKtorym.Hlbka + 1, Parent = novyInternyNode };
            var novyLavy = new ZnakNodeExterny() { Hlbka = nadKtorym.Hlbka + 1, Parent = novyInternyNode };
            // vytvorenie noveho nodu s dvama externymy synmi
            novyInternyNode.Hlbka = nadKtorym.Hlbka;
            novyInternyNode.Left = novyLavy;
            novyInternyNode.Right = novyPravy;
            // priradenie noveho potomka 
            if (parentNodu != null)
            {
                novyInternyNode.Parent = parentNodu;
                if ((parentNodu as ZnakNodeInterny).Left == nadKtorym)
                {
                    (parentNodu as ZnakNodeInterny).Left = novyInternyNode;
                }
                else
                {
                    (parentNodu as ZnakNodeInterny).Right = novyInternyNode;
                }
            }
            //nahradenie stareho externeho nodu novym internym
            nadKtorym = novyInternyNode;

            var tmpKtory = novyInternyNode;
            //priradenie starych blokov do novych
            var posByte = tmpExternyNodeCopy.Hlbka / 8;
            var pos = tmpExternyNodeCopy.Hlbka % 8;

            while (true)
            {
                var poleByt = new List<byte>();

                for (int i = 0; i < blokExt.Data.Count; i++)
                {
                    //zahesovany konkretny jeden block
                    //  toto tu bolo pred tym Old VErsion    var konkretnyBlockHash= BitConverter.GetBytes(tmpExternyNodeCopy.Blok[i]);
                    var konkretnyBlockHash = blokExt.Data[i].Hash();
                    var konkretnyByte = konkretnyBlockHash[posByte];
                    if (IsBitSet(konkretnyByte, pos))
                    {
                        poleByt.Add(1);
                    }
                    else
                    {
                        poleByt.Add(0);
                    }

                }
                var pocetJed = poleByt.Count(x => x == 1);
                if (pocetJed != 0 && pocetJed != poleByt.Count)
                {
                    var lavyBlok = new Blok<T>();
                    var pravyBlok = new Blok<T>();

                    for (int l = 0; l < blokExt.Data.Count; l++)
                    {
                        if (poleByt[l] == 0)
                        {
                            lavyBlok.Vloz(blokExt.Data[l]);
                            (tmpKtory.Left as ZnakNodeExterny).PocetZaznamov++;

                        }
                        else
                        {
                            pravyBlok.Vloz(blokExt.Data[l]);
                            (tmpKtory.Right as ZnakNodeExterny).PocetZaznamov++;
                        }

                    }
                    //lavy bude mat vzdy adresu po parentovy z prveho externeho nodu
                    var adresaLaveho = seekParent;
                    (tmpKtory.Left as ZnakNodeExterny).Seek = adresaLaveho;
                    // pravy si ju musi prepocitat lebo nejde cez metodu Zapis
                    var adresaPraveho = IndexVSubore;
                    (tmpKtory.Right as ZnakNodeExterny).Seek = adresaPraveho;

                    IndexVSubore = IndexVSubore + (novyPrvok.GetSize() * Konstanty.BlokovaciFaktor) + 8;
                    lavyBlok.ZapisBlok(adresaLaveho, NazovSuboru);
                    pravyBlok.ZapisBlok(adresaPraveho, NazovSuboru);
                    return;
                }
                else
                {
                    if (poleByt[0] == 0)
                    {
                        //vytvorit strom dolava

                        tmpKtory.Left = new ZnakNodeInterny() { Hlbka = tmpKtory.Hlbka + 1, Parent = tmpKtory };

                        var novyVrcholStromu = tmpKtory.Left as ZnakNodeInterny;

                        var lavy = new ZnakNodeExterny() { Hlbka = novyVrcholStromu.Hlbka + 1, Parent = novyVrcholStromu };
                        var pravy = new ZnakNodeExterny() { Hlbka = novyVrcholStromu.Hlbka + 1, Parent = novyVrcholStromu };
                        novyVrcholStromu.Left = lavy;
                        novyVrcholStromu.Right = pravy;

                        tmpKtory = novyVrcholStromu;

                    }
                    else
                    {
                        //vytvorit strom doprava
                        tmpKtory.Right = new ZnakNodeInterny() { Hlbka = tmpKtory.Hlbka + 1, Parent = tmpKtory };

                        var novyVrcholStromu = tmpKtory.Right as ZnakNodeInterny;

                        var lavy = new ZnakNodeExterny() { Hlbka = novyVrcholStromu.Hlbka + 1, Parent = novyVrcholStromu };
                        var pravy = new ZnakNodeExterny() { Hlbka = novyVrcholStromu.Hlbka + 1, Parent = novyVrcholStromu };
                        novyVrcholStromu.Left = lavy;
                        novyVrcholStromu.Right = pravy;

                        tmpKtory = novyVrcholStromu;
                    }

                    //zvysenie porovnavacieho bytu
                    if (pos == 7)
                    {
                        pos = 0;
                        // ASi ????????????????????????????
                        posByte++;
                    }
                    else
                    {
                        pos++;
                    }


                }

            }
        }
        public bool Add(T novyPrvok)
        {
            var nove = novyPrvok.Hash();

            if (Find(novyPrvok) != null)
            {
                return false;
            }


            if (Root is ZnakNodeExterny)
            {
                if ((Root as ZnakNodeExterny).PocetZaznamov < Konstanty.BlokovaciFaktor)
                {
                    var konkretny = (Root as ZnakNodeExterny);
                    Zapis(novyPrvok, ref konkretny);
                }
                else
                {
                    Rozsir(ref Root, novyPrvok);
                }
            }
            else
            {
                //traverzujem a hladam cestu
                var doNodeBloku = Find(nove);
                if (doNodeBloku.PocetZaznamov < Konstanty.BlokovaciFaktor)
                {
                    Zapis(novyPrvok, ref doNodeBloku);
                }
                else
                {
                    var tentoNode = doNodeBloku as ZnakNode;
                    Rozsir(ref tentoNode, novyPrvok);

                }
            }
            return true;

        }
        private void Zapis(T prvok, ref ZnakNodeExterny nodeBloku)
        {
            if (nodeBloku.Seek == -1)
            {
                nodeBloku.Seek = IndexVSubore;
                //+8 pretoze mma na zaciatku DVA inty ktore oznacuju pocet validnych blokov a index preplnujuceho bloku
                IndexVSubore = IndexVSubore + (prvok.GetSize() * Konstanty.BlokovaciFaktor) + 8;

                var blok = new Blok<T>();
                blok.Vloz(prvok);
                nodeBloku.PocetZaznamov++;
                blok.ZapisBlok(nodeBloku.Seek, NazovSuboru);
            }
            else
            {
                //pozriet ci je v bloku dostatok volneho miesta
                var blok = new Blok<T>(1);
                blok.NacitajBlok(nodeBloku.Seek, NazovSuboru);
                blok.Vloz(prvok);
                nodeBloku.PocetZaznamov++;
                blok.ZapisBlok(nodeBloku.Seek, NazovSuboru);
            }


        }
        private T Find(T prvok)
        {

            var hash = prvok.Hash();
            if (Root is ZnakNodeExterny)
            {
                if ((Root as ZnakNodeExterny).Seek != -1)
                {
                    var konkretny = (Root as ZnakNodeExterny);
                    var tmpBlok = new Blok<T>(1);
                    tmpBlok.NacitajBlok(konkretny.Seek, NazovSuboru);
                    foreach (var item in tmpBlok.Data)
                    {
                        if (item.CompareTo(prvok))
                        {
                            return item;
                        }
                    }
                }
            }
            else
            {
                var tmpNode = Root as ZnakNodeInterny;
                for (int i = 0; i < hash.Length; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (IsBitSet(hash[i], j))
                        {
                            if (tmpNode.Right is ZnakNodeExterny)
                            {
                                var konkretny = (tmpNode.Right as ZnakNodeExterny);
                                var tmpBlok = new Blok<T>(1);
                                tmpBlok.NacitajBlok(konkretny.Seek, NazovSuboru);
                                foreach (var item in tmpBlok.Data)
                                {
                                    if (item.CompareTo(prvok))
                                    {
                                        return item;
                                    }
                                }

                            }
                            else
                            {
                                tmpNode = tmpNode.Right as ZnakNodeInterny;
                            }
                        }
                        else
                        {
                            if (tmpNode.Left is ZnakNodeExterny)
                            {
                                var konkretny = (tmpNode.Left as ZnakNodeExterny);
                                var tmpBlok = new Blok<T>(1);
                                tmpBlok.NacitajBlok(konkretny.Seek, NazovSuboru);
                                foreach (var item in tmpBlok.Data)
                                {
                                    if (item.CompareTo(prvok))
                                    {
                                        return item;
                                    }
                                }

                            }
                            else
                            {
                                tmpNode = tmpNode.Left as ZnakNodeInterny;
                            }
                        }
                    }
                }
                return default(T);
            }
            return default(T);
        }
        private ZnakNodeExterny Find(byte[] hash)
        {

            if (Root is ZnakNodeExterny)
            {
                return Root as ZnakNodeExterny;
            }
            var tmpNode = Root as ZnakNodeInterny;
            for (int i = 0; i < hash.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (IsBitSet(hash[i], j))
                    {

                        if (tmpNode.Right is ZnakNodeExterny)
                        {


                            var konkretny = (tmpNode.Right as ZnakNodeExterny);
                            return konkretny;


                        }
                        else
                        {

                            tmpNode = tmpNode.Right as ZnakNodeInterny;
                        }
                    }
                    else
                    {
                        if (tmpNode.Left is ZnakNodeExterny)
                        {

                            var konkretny = (tmpNode.Left as ZnakNodeExterny);
                            return konkretny;

                        }
                        else
                        {
                            tmpNode = tmpNode.Left as ZnakNodeInterny;
                        }
                    }
                }
            }
            return null;
        }


        public bool Remove(T removeOne)
        {
            var hash = removeOne.Hash();
            var konkretnyNode = Find(hash);

            var konkretnyBlok = new Blok<T>(1);
            konkretnyBlok.NacitajBlok(konkretnyNode.Seek, NazovSuboru);
            foreach (var item in konkretnyBlok.Data)
            {
                if (item.CompareTo(removeOne))
                {
                    konkretnyBlok.Zmaz(removeOne);
                    konkretnyNode.PocetZaznamov--;
                    konkretnyBlok.ZapisBlok(konkretnyNode.Seek, NazovSuboru);
                    Zluc(ref konkretnyNode);
                    konkretnyBlok.ZapisBlok(konkretnyNode.Seek, NazovSuboru);
                    
                    return true;
                }
            }
            return false;
        }
        private void Zluc(ref ZnakNodeExterny mazanyNodeBlok)
        {
          
            Blok<T> zlucenyBlok = new Blok<T>();
            if (mazanyNodeBlok.Parent != null)
            {
                var tmpParent = mazanyNodeBlok.Parent as ZnakNodeInterny;
                var tmpJa = mazanyNodeBlok;
                while (true)
                {
                    Blok<T> lavyBlok = new Blok<T>(1);
                    Blok<T> pravyBlok = new Blok<T>(1);
                    var seekVpravo = -2;
                    var seekVlavo = -2;
                    bool mamExternehoBrata = false;
                    bool somPravySyn = false;
                    if (tmpParent.Right == tmpJa)
                    {
                        //som pravy syn

                        seekVpravo = (tmpParent.Right as ZnakNodeExterny).Seek;
                        somPravySyn = true;
                        //je moj brat tiez externy
                        if (tmpParent.Left is ZnakNodeExterny)
                        {
                          
                            seekVlavo = (tmpParent.Left as ZnakNodeExterny).Seek;
                            mamExternehoBrata = true;
                        }

                    }
                    else
                    {
                        //som lavy syn
                        
                        seekVlavo = (tmpParent.Left as ZnakNodeExterny).Seek;
                        //je moj brat tiez externy
                        if (tmpParent.Right is ZnakNodeExterny)
                        {
                           
                            seekVpravo = (tmpParent.Right as ZnakNodeExterny).Seek;
                            mamExternehoBrata = true;
                        }
                    }
                    //som pravy syn ktory zatial nic nevie
                    if (somPravySyn)
                    {
                        //mam brata ktory je externy node ALE nemusi mat priradenu adresu
                        if (mamExternehoBrata)
                        {
                            var mojBrat = tmpParent.Left as ZnakNodeExterny;
                            //mozu sa zlucit
                            if ((tmpJa.PocetZaznamov + mojBrat.PocetZaznamov) <= Konstanty.BlokovaciFaktor)
                            {
                                if (seekVlavo >= 0)
                                {
                                    lavyBlok.NacitajBlok(mojBrat.Seek, NazovSuboru);
                                    for (int i = 0; i < lavyBlok.PocetValidnych; i++)
                                    {
                                        zlucenyBlok.Vloz(lavyBlok.Data[i]);
                                    }
                                    VolneAdresy.Add(seekVlavo);
                                }

                                pravyBlok.NacitajBlok(tmpJa.Seek, NazovSuboru);

                                for (int i = 0; i < pravyBlok.PocetValidnych; i++)
                                {
                                    zlucenyBlok.Vloz(pravyBlok.Data[i]);
                                }

                                tmpJa = new ZnakNodeExterny() { Hlbka = tmpParent.Hlbka, Parent = tmpParent.Parent, PocetZaznamov = zlucenyBlok.PocetValidnych, Seek = seekVpravo };
                                if (tmpParent.Parent.Parent == null)
                                {
                                    Root = tmpJa as ZnakNodeExterny;
                                    return;
                                }
                                else
                                {
                                    tmpParent = tmpParent.Parent as ZnakNodeInterny;
                                    tmpParent.Right = tmpJa;
                                }
                            }
                            else
                            {
                                // nemozu sa zlucit lebo maju viac zaznamov ako blokovaci faktor
                                return;
                            }
                        }
                        else {
                            //moj brat neni externy mozem rovno skoncit
                            return;
                        }
                    }
                    else
                    {
                        //som Lavy syn a neviem nic ine
                        if (mamExternehoBrata)
                        {
                            var mojBrat = tmpParent.Right as ZnakNodeExterny;
                            //mozu sa zlucit
                            if ((tmpJa.PocetZaznamov + mojBrat.PocetZaznamov) <= Konstanty.BlokovaciFaktor)
                            {
                                if (seekVpravo >= 0)
                                {
                                    pravyBlok.NacitajBlok(mojBrat.Seek, NazovSuboru);
                                    for (int i = 0; i < pravyBlok.PocetValidnych; i++)
                                    {
                                        zlucenyBlok.Vloz(pravyBlok.Data[i]);
                                    }

                                    VolneAdresy.Add(seekVpravo);
                                }

                                lavyBlok.NacitajBlok(tmpJa.Seek, NazovSuboru);

                                for (int i = 0; i < lavyBlok.PocetValidnych; i++)
                                {
                                    zlucenyBlok.Vloz(lavyBlok.Data[i]);
                                }


                                tmpJa = new ZnakNodeExterny() { Hlbka = tmpParent.Hlbka, Parent = tmpParent.Parent, PocetZaznamov = zlucenyBlok.PocetValidnych, Seek = seekVlavo };
                                if (tmpParent.Parent.Parent == null)
                                {
                                    Root = tmpJa as ZnakNodeExterny;
                                    return;
                                }
                                else
                                {
                                    tmpParent = tmpParent.Parent as ZnakNodeInterny;
                                    tmpParent.Left = tmpJa;
                                }
                            }
                            else
                            {
                                // nemozu sa zlucit lebo maju viac zaznamov ako blokovaci faktor
                                return;
                            }
                        }
                        else
                        {
                            //moj brat neni externy mozem rovno skoncit
                            return;
                        }
                    }


                }
            }
        }
    }




    class ZnakNodeInterny : ZnakNode
    {
        public ZnakNode Left { get; set; }
        public ZnakNode Right { get; set; }

    }
    class ZnakNodeExterny : ZnakNode
    {
        public int PocetZaznamov { get; set; }
        public int Seek { get; set; } //  blok alebo adresa co to kurva je ?????

        public ZnakNodeExterny()
        {
            PocetZaznamov = 0;
            Seek = -1;
        }
    }
    class ZnakNode
    {
        public ZnakNode Parent { get; set; }
        public int Hlbka { get; set; }
    }
}

