using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GUI
{
    class SpravujKataster
    {
        public AVLTree<KatastralneUzemieCislo> KatasterCislo { get; set; }
        public AVLTree<KatastralneUzemieNazov> KatasterNazov { get; set; }
        public AVLTree<Obcan> Obyvatelstvo { get; set; }
        public SpravujKataster()
        {
            KatasterCislo = new AVLTree<KatastralneUzemieCislo>();
            KatasterNazov = new AVLTree<KatastralneUzemieNazov>();
            Obyvatelstvo = new AVLTree<Obcan>();


            // TESTOVANIE 
          //  PridajOsobu("9876543210987654", "Jozo", "qwer", DateTime.Now);
         //   PridajOsobu("0123456789123456", "Domco", "poiu", DateTime.Now);
            Generator();

            /* PridajNehnutelnost(1, 1, "222", "xxx", 1);
             PridajNehnutelnost(1, 2, "111", "ppp", 1);

             PridajNehnutelnost(2, 1, "321", "aaa", 2);
             PridajNehnutelnost(2, 1, "999", "ooo", 3);

             PridajVlastnika("0123456789123456", 1, 1, 0.5);
             PridajVlastnika("9876543210987654", 1, 1, 0.5);

             PridajVlastnika("0123456789123456", 2, 1, 0.5);

             PridajVlastnika("0123456789123456", 1, 2, 0.5);*/

            // trvale bydlisko obcana TEST CASE
           // Uloha10("0123456789123456", 1, "kataster1");

        }

        public string PridajOsobu(string rodneCislo, string meno, string priezvisko, DateTime datumNar)
        {

            var NovaOsoba = new Obcan() { Meno = meno, RodCislo = rodneCislo, DatumNarodenia = datumNar, Priezvisko = priezvisko };
            if (Obyvatelstvo.Contains(NovaOsoba))
            {
                return "Osoba s rodnym so zadanym rodnym cislom uz exisujte";
            }
            Obyvatelstvo.Add(NovaOsoba);
            return "OK";
        }
        public string PridajList(string nazovKatastru, int IDListu)
        {
            var novyUzem = new KatastralneUzemieNazov() { NazovUzemia = nazovKatastru };
            var tmpListVlast = new ListVlastnictva() { IDListu = IDListu };
            if (KatasterNazov.Contains(novyUzem))
            {
                var konkretnyKat = KatasterNazov.Find(novyUzem);
                if (!konkretnyKat.ListyVlastnictva.Contains(tmpListVlast))
                {
                    tmpListVlast.Uzemie = konkretnyKat;
                    konkretnyKat.ListyVlastnictva.Add(tmpListVlast);
                    //lepsie reisenie ako mat ulozeny jeden list v 2 katastrov
                    KatasterCislo.Find(new KatastralneUzemieCislo() { CisloUzemia = konkretnyKat.CisloUzemia }).ListyVlastnictva.Add(tmpListVlast);
                    return "OK";
                }
                else
                {
                    return "Kataster obsahuje list so zadanym cislom listu";
                }

            }
            else
            {
                return "Zadany nazov katastra sa nenachadza v databaze";
            }
        }
        public string PridajKatastralUzemie(int ID, string nazov)
        {
            var NoveKatastralneUzemCislo = new KatastralneUzemieCislo() { CisloUzemia = ID, NazovUzemia = nazov };
            var NoveKatastralneUzemNazov = new KatastralneUzemieNazov() { CisloUzemia = ID, NazovUzemia = nazov };
            if (!KatasterCislo.Contains(NoveKatastralneUzemCislo))
            {
                if (!KatasterNazov.Contains(NoveKatastralneUzemNazov))
                {
                    KatasterCislo.Add(NoveKatastralneUzemCislo);
                    KatasterNazov.Add(NoveKatastralneUzemNazov);
                    return "OK";
                }
                else
                {
                    return "Uzemie s rovnakym nazvom uz existuje";
                }

            }
            else
            {
                return "Uzemie s rovnakym cislom uz existuje";
            }
        }
        public string PridajNehnutelnost(int IDListu, int IDKatUzemia, string adresa, string popis, int IDNehnutelnosti)
        {

            var tmpKat = new KatastralneUzemieCislo() { CisloUzemia = IDKatUzemia };
            var hladanyList = new ListVlastnictva() { IDListu = IDListu };


            if (KatasterCislo.Contains(tmpKat))
            {
                var plnyKataster = KatasterCislo.Find(tmpKat);
                if (plnyKataster.ListyVlastnictva.Contains(hladanyList))
                {
                    var konkretnyList = plnyKataster.ListyVlastnictva.Find(hladanyList);
                    var novaNehnutelnost = new Nehnutelnost() { Adresa = adresa, Popis = popis, SupisneCislo = IDNehnutelnosti, NehnutelnostVListeVlastnicta = konkretnyList };
                    if (!plnyKataster.NehnutelnostiNaUzemi.Contains(novaNehnutelnost))
                    {
                        konkretnyList.Nehnutelnosti.Add(novaNehnutelnost);
                        //nepridavat do 2 katuzemi ale len do jedneho
                        plnyKataster.NehnutelnostiNaUzemi.Add(novaNehnutelnost);
                        KatasterNazov.Find(new KatastralneUzemieNazov() { NazovUzemia = plnyKataster.NazovUzemia }).NehnutelnostiNaUzemi.Add(novaNehnutelnost);
                        return "OK";
                    }
                    else
                    {
                        // nehnutelnost so supisnym cislom uz existuje
                        return "Nehnutelnost so zadanym supisnym cislom uz existuje";
                    }
                }
                // zle zadane ID listu
                else
                {
                    return "list vlastnictva so zadanym id sa nencahadza v databaze";
                }
            }
            // kataster neexistuje
            else
            {
                return "kataster so zadanym id sa nencahadza v databaze";

            }
        }
        public string PridajVlastnika(string rodCislo, int IdListu, int IdKatastra, double podiel)
        {
            var helpObcan = new Obcan() { RodCislo = rodCislo };
            if (Obyvatelstvo.Contains(helpObcan))
            {
                var tmpObcan = Obyvatelstvo.Find(helpObcan);
                var insertVlastnik = new Vlastnik() { Majitel = tmpObcan, MajetkovyPodiel = podiel };
                var tmpList = new ListVlastnictva() { IDListu = IdListu };
                var tmpKatUzemCislo = new KatastralneUzemieCislo() { CisloUzemia = IdKatastra };
                if (KatasterCislo.Contains(tmpKatUzemCislo))
                {
                    var konkretneUzemie = KatasterCislo.Find(tmpKatUzemCislo);
                    if (konkretneUzemie.ListyVlastnictva.Contains(tmpList))
                    {
                        var konkretnyListVkatastry = konkretneUzemie.ListyVlastnictva.Find(tmpList);

                        if (!konkretnyListVkatastry.Vlastnici.Contains(insertVlastnik))
                        {
                            konkretnyListVkatastry.Vlastnici.Add(insertVlastnik);
                            //pridave kvoli listu na obcovanovy
                            tmpObcan.VlastnictvoObcana.Add(konkretnyListVkatastry);
                            return "OK";
                        }
                        // vlastnik je uz na liste vlastnictva
                        else
                        {
                            return "Vlastnik je uz aktualne na liste vlastnictva";
                        }
                    }
                    // list sa nenachadza v katastry
                    else
                    {
                        return "List so zadanym cislom sa nenachadza v databaze";
                    }
                }
                // zle zadane cislo uzemia
                else
                {
                    return "katastralne uzemie so zadanym cislom sa nenachadza v databaze";
                }
            }
            // nenasiel sa obcan
            else
            {
                return "Obcan so zadanym rodnym cislom neexistuje";
            }

        }
        public List<string> Uloha7(string nazovHladKatUzem)
        {
            var retList = new List<string>();
            var tmpKat = new KatastralneUzemieNazov() { NazovUzemia = nazovHladKatUzem };
            if (KatasterNazov.Contains(tmpKat))
            {
                var konkretnyKat = KatasterNazov.Find(tmpKat);
                if (konkretnyKat.NehnutelnostiNaUzemi.Count != 0)
                {
                    var listNehutelnosti = konkretnyKat.NehnutelnostiNaUzemi.InOrder();
                    foreach (var item in listNehutelnosti)
                    {
                        retList.Add(item.ToString());
                    }
                }
                else
                {
                    retList.Add("Na katastry ziadna nehnutelnost");
                }

                return retList;
            }
            else
            {
                return new List<string>() { "Nenajdeny Kataster so zadanym nazvom: " + nazovHladKatUzem };
            }

        }
        public List<string> Uloha12(string rodCislo, int IdListu, int IdKatastra)
        {
            var retList = new List<string>();
            try
            {
                var findKat = KatasterCislo.Find(new KatastralneUzemieCislo() { CisloUzemia = IdKatastra });
                //-------------------------------------------------------------------------------------------------------FOREACH STROM
                var vlastniciNaListe = findKat.ListyVlastnictva.Find(new ListVlastnictva() { IDListu = IdListu }).Vlastnici.InOrder();
                foreach (var item in vlastniciNaListe)
                {
                    retList.Add(item.ToString());
                }
                return retList;
            }
            catch (Exception)
            {
                return new List<string>() { "Nenajdeny vlastnici na katastry s ID: " + IdKatastra + " v Liste: " + IdListu };
            }
        }
        public string Uloha12Update(string rodCislo, int IdListu, int IdKatastra, double majetkovyPod)
        {
            KatastralneUzemieCislo findKat = null;
            AVLTree<Vlastnik> vlastniciNaListe = null;
            Vlastnik konkretnyUpdatovanyVlastnik = null;

            try
            {
                findKat = KatasterCislo.Find(new KatastralneUzemieCislo() { CisloUzemia = IdKatastra });
            }
            catch (Exception)
            {

                return "Nenajdene katastralne uzemie";
            }

            try
            {
                vlastniciNaListe = findKat.ListyVlastnictva.Find(new ListVlastnictva() { IDListu = IdListu }).Vlastnici;
            }
            catch (Exception)
            {

                return "Nenajdeny list Vlastnictva";
            }

            try
            {
                konkretnyUpdatovanyVlastnik = vlastniciNaListe.Find(new Vlastnik() { Majitel = new Obcan() { RodCislo = rodCislo } });
            }
            catch (Exception)
            {

                return "Nenajdeny Vlastnik so zadanym rodnym cislom";
            }

            konkretnyUpdatovanyVlastnik.MajetkovyPodiel = majetkovyPod;
            return "OK";

        }
        public List<string> Uloha1(int supisCislo, int katUzem)
        {
            var tmpUzem = new KatastralneUzemieCislo() { CisloUzemia = katUzem };
            if (KatasterCislo.Contains(tmpUzem))
            {
                var uzemie = KatasterCislo.Find(tmpUzem);
                var tmpNeh = new Nehnutelnost() { SupisneCislo = supisCislo };
                if (uzemie.NehnutelnostiNaUzemi.Contains(tmpNeh))
                {
                    var konkretnaNeh = uzemie.NehnutelnostiNaUzemi.Find(tmpNeh);
                    var listRet = new List<string>();
                    listRet.Add("Id vlastnickeho listu je: " + konkretnaNeh.NehnutelnostVListeVlastnicta.IDListu + " a nachadza sa v katastry s nazvom: " + konkretnaNeh.NehnutelnostVListeVlastnicta.Uzemie.NazovUzemia);
                    listRet.Add("Adresa nehnutelnosti: " + konkretnaNeh.Adresa);
                    listRet.Add("Popis nehnutelnosti: " + konkretnaNeh.Popis);
                    listRet.Add("Pocet Ubytovanych: " + konkretnaNeh.ObyvateliaNehnutelnosti.Count);
                    if (konkretnaNeh.ObyvateliaNehnutelnosti.Count != 0)
                    {
                        var tmpListOban = konkretnaNeh.ObyvateliaNehnutelnosti.InOrder();
                        foreach (var item in tmpListOban)
                        {
                            listRet.Add(item.ToString());
                        }
                    }
                    //------------------------------------------------------------------ STROM FOREACH
                    listRet.Add("Pocet vlastnikov: " + konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.Count);
                    if (konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.Count != 0)
                    {
                        var tmpListVlast = konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.InOrder();
                        foreach (var item in tmpListVlast)
                        {
                            listRet.Add(item.Majitel.ToString() + " Majetkovy Podiel: " + item.MajetkovyPodiel);
                        }
                    }


                    return listRet;
                }
                else
                {
                    return new List<string>() { "Nenajdena nehnutelnost so zadanym supisnym cislom" };
                }
            }
            else
            {
                return new List<string>() { "Nenajdene katastralne uzemie so zadanym cislom" };
            }
        }
        public List<string> Uloha2(string rodCislo)
        {

            var tmpObcan = new Obcan() { RodCislo = rodCislo };
            if (Obyvatelstvo.Contains(tmpObcan))
            {
                var retList = new List<string>();
                var konkretnyObcan = Obyvatelstvo.Find(tmpObcan);

                if (konkretnyObcan.Domov != null)
                {
                    retList.Add(konkretnyObcan.Domov.ToString(1));
                    retList.Add("V nehnutelnosti byva celkom: " + konkretnyObcan.Domov.ObyvateliaNehnutelnosti.Count + " obcanov");
                }
                else
                {
                    retList.Add("Obcan nema trvale bydlisko");
                }

                return retList;
            }
            else
            {
                return new List<string>() { "Nenajdena osoba so zadanym rodnym cislom" };
            }
        }
        public List<string> Uloha3(int idKatastra, int idList, int supisCislo)
        {
            var tmpKat = new KatastralneUzemieCislo() { CisloUzemia = idKatastra };
            var tmpList = new ListVlastnictva() { IDListu = idList };
            var tmpNeh = new Nehnutelnost() { SupisneCislo = supisCislo };
            if (KatasterCislo.Contains(tmpKat))
            {
                var konkretnyKat = KatasterCislo.Find(tmpKat);
                if (konkretnyKat.ListyVlastnictva.Contains(tmpList))
                {
                    var konkretnyList = konkretnyKat.ListyVlastnictva.Find(tmpList);
                    if (konkretnyList.Nehnutelnosti.Contains(tmpNeh))
                    {
                        var konkretnaNeh = konkretnyList.Nehnutelnosti.Find(tmpNeh);
                        var retList = new List<string>();


                        if (konkretnaNeh.ObyvateliaNehnutelnosti.Count != 0)
                        {
                            retList.Add("V nehnutelnosti zije celkom: " + konkretnaNeh.ObyvateliaNehnutelnosti.Count + " obyvatelov");
                            //----------------------------------------------------------------------------------------- STROM FOR EACH
                            var listObcan = konkretnaNeh.ObyvateliaNehnutelnosti.InOrder();
                            foreach (var item in listObcan)
                            {
                                retList.Add(item.ToString());
                            }
                        }
                        else
                        {
                            retList.Add("Nehnutelnost nieje obyvana");
                        }

                        return retList;
                    }
                    else
                    {
                        return new List<string>() { "Nenajdena nehnutelnost so zadanym supisnym cislom" };
                    }

                }
                else
                {
                    return new List<string>() { "Nenajdeny list vlastnictva so zadanym cislom" };
                }

            }
            else
            {
                return new List<string>() { "Nenajdeny kataster so zadanym cislom" };
            }
        }
        public List<string> Uloha4(int cisKat, int idList)
        {
            var tmpKatUzem = new KatastralneUzemieCislo() { CisloUzemia = cisKat };
            if (KatasterCislo.Contains(tmpKatUzem))
            {
                var konkretneUzem = KatasterCislo.Find(tmpKatUzem);
                var tmpList = new ListVlastnictva() { IDListu = idList };
                if (konkretneUzem.ListyVlastnictva.Contains(tmpList))
                {
                    var konkretnyList = konkretneUzem.ListyVlastnictva.Find(tmpList);
                    var retList = new List<string>();
                    //------------------------------------------------------FOREACH LIST
                    if (konkretnyList.Vlastnici.Count != 0)
                    {
                        var listT = konkretnyList.Vlastnici.InOrder();
                        foreach (var item in listT)
                        {
                            retList.Add("Id Listu: " + konkretnyList.IDListu + " " + item.Majitel.ToString() + " Majetkovy podiel: " + item.MajetkovyPodiel);
                        }
                    }
                    else
                    {
                        retList.Add("List nema ziadnych vlastnikov");
                    }
                    return retList;
                }
                else
                {
                    return new List<string>() { "Nenajdeny list vlastnictva so zadanym cislom" };
                }
            }
            else
            {
                return new List<string>() { "Nenajdeny kataster so zadanym cislom" };
            }
        }
        public List<string> Uloha5(string nazovKat, int supisCislo)
        {
            var tmpUzem = new KatastralneUzemieNazov() { NazovUzemia = nazovKat };
            if (KatasterNazov.Contains(tmpUzem))
            {
                var uzemie = KatasterNazov.Find(tmpUzem);
                var tmpNeh = new Nehnutelnost() { SupisneCislo = supisCislo };
                if (uzemie.NehnutelnostiNaUzemi.Contains(tmpNeh))
                {
                    var konkretnaNeh = uzemie.NehnutelnostiNaUzemi.Find(tmpNeh);
                    var listRet = new List<string>();
                    listRet.Add("Id vlastnickeho listu je: " + konkretnaNeh.NehnutelnostVListeVlastnicta.IDListu + " a nachadza sa v katastry s nazvom: " + konkretnaNeh.NehnutelnostVListeVlastnicta.Uzemie.NazovUzemia);
                    listRet.Add("Adresa nehnutelnosti: " + konkretnaNeh.Adresa);
                    listRet.Add("Popis nehnutelnosti: " + konkretnaNeh.Popis);
                    listRet.Add("Pocet Ubytovanych: " + konkretnaNeh.ObyvateliaNehnutelnosti.Count);
                    if (konkretnaNeh.ObyvateliaNehnutelnosti.Count != 0)
                    {
                        var tmpListOban = konkretnaNeh.ObyvateliaNehnutelnosti.InOrder();
                        foreach (var item in tmpListOban)
                        {
                            listRet.Add(item.ToString());
                        }
                    }
                    //------------------------------------------------------------------ STROM FOREACH
                    listRet.Add("Pocet vlastnikov: " + konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.Count);
                    if (konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.Count != 0)
                    {
                        var tmpListVlast = konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.InOrder();
                        foreach (var item in tmpListVlast)
                        {
                            listRet.Add(item.Majitel.ToString() + " Majetkovy Podiel: " + item.MajetkovyPodiel);
                        }
                    }


                    return listRet;
                }
                else
                {
                    return new List<string>() { "Nenajdena nehnutelnost so zadanym supisnym cislom" };
                }
            }
            else
            {
                return new List<string>() { "Nenajdene katastralne uzemie so zadanym nazvom" };
            }
        }
        public List<string> Uloha6(string nazovKat, int idList)
        {
            var tmpKatUzem = new KatastralneUzemieNazov() { NazovUzemia = nazovKat };
            if (KatasterNazov.Contains(tmpKatUzem))
            {
                var konkretneUzem = KatasterNazov.Find(tmpKatUzem);
                var tmpList = new ListVlastnictva() { IDListu = idList };
                if (konkretneUzem.ListyVlastnictva.Contains(tmpList))
                {
                    var konkretnyList = konkretneUzem.ListyVlastnictva.Find(tmpList);
                    var retList = new List<string>();
                    //------------------------------------------------------FOREACH LIST
                    var listT = konkretnyList.Vlastnici.InOrder();
                    foreach (var item in listT)
                    {
                        retList.Add("Id Listu: " + konkretnyList.IDListu + " " + item.Majitel.ToString() + " Majetkovy podiel: " + item.MajetkovyPodiel);
                    }
                    return retList;
                }
                else
                {
                    return new List<string>() { "Nenajdeny list vlastnictva so zadanym cislom" };
                }
            }
            else
            {
                return new List<string>() { "Nenajdeny kataster so zadanym nazvom" };
            }
        }
        public List<string> Uloha8(string rodCislo, int cisKat)
        {
            if (KatasterCislo.Contains(new KatastralneUzemieCislo() { CisloUzemia = cisKat }))
            {
                var tmpObcan = new Obcan() { RodCislo = rodCislo };
                if (Obyvatelstvo.Contains(tmpObcan))
                {
                    var konkretnyObcan = Obyvatelstvo.Find(tmpObcan);
                    if (konkretnyObcan.VlastnictvoObcana.Count != 0)
                    {
                        var retList = new List<string>();
                        foreach (var item in konkretnyObcan.VlastnictvoObcana)
                        {
                            if (item.Uzemie.CisloUzemia == cisKat)
                            {
                                var podiel = item.Vlastnici.Find(new Vlastnik() { Majitel = konkretnyObcan }).MajetkovyPodiel;
                                //---------------------------------------------------------------FOREACH STROM
                                foreach (var item2 in item.Nehnutelnosti.InOrder())
                                {
                                    retList.Add(item2.ToString() + " Majetkovy podiel: " + podiel);
                                }
                            }
                        }
                        if (retList.Count == 0)
                        {
                            retList.Add("Vlastnik nema v zadanom katastry ziadne nehnutelosti");
                        }
                        return retList;
                    }
                    else
                    {
                        return new List<string>() { "Obcan nieje vlastnikom ziadnej budovy" };
                    }
                }
                else
                {
                    return new List<string>() { "Nenajdeny obcan so zadanym rodnym cislom" };
                }


            }
            else
            {
                return new List<string>() { "Nenajdene katastralne uzemie" };
            }
        }
        public List<string> Uloha9(string rodCislo)
        {

            var tmpObcan = new Obcan() { RodCislo = rodCislo };
            if (Obyvatelstvo.Contains(tmpObcan))
            {
                var konkretnyObcan = Obyvatelstvo.Find(tmpObcan);
                if (konkretnyObcan.VlastnictvoObcana.Count != 0)
                {
                    var retList = new List<string>();
                    foreach (var item in konkretnyObcan.VlastnictvoObcana)
                    {
                        var podiel = item.Vlastnici.Find(new Vlastnik() { Majitel = konkretnyObcan }).MajetkovyPodiel;
                        //---------------------------------------------------------------FOREACH STROM
                        foreach (var item2 in item.Nehnutelnosti.InOrder())
                        {
                            retList.Add(item2.ToString() + " Majetkovy podiel: " + podiel);
                        }
                    }
                    if (retList.Count == 0)
                    {
                        retList.Add("Vlastnik nema v zadanom katastry ziadne nehnutelosti");
                    }
                    return retList;
                }
                else
                {
                    return new List<string>() { "Obcan nieje vlastnikom ziadnej budovy" };
                }
            }
            else
            {
                return new List<string>() { "Nenajdeny obcan so zadanym rodnym cislom" };
            }

        }
        public string Uloha10(string rodCislo, int supisCislo, string katUzemNazov)
        {
            var tmpObcan = new Obcan() { RodCislo = rodCislo };
            var tmpNeh = new Nehnutelnost() { SupisneCislo = supisCislo };
            var tmpKatUzem = new KatastralneUzemieNazov() { NazovUzemia = katUzemNazov };
            if (Obyvatelstvo.Contains(tmpObcan))
            {
                var konkretnyObcan = Obyvatelstvo.Find(tmpObcan);
                if (KatasterNazov.Contains(tmpKatUzem))
                {
                    var konkretneUzemie = KatasterNazov.Find(tmpKatUzem);
                    if (konkretneUzemie.NehnutelnostiNaUzemi.Contains(tmpNeh))
                    {
                        var konkretnaNeh = konkretneUzemie.NehnutelnostiNaUzemi.Find(tmpNeh);
                        if (konkretnyObcan.Domov == null)
                        {
                            konkretnyObcan.Domov = konkretnaNeh;
                            konkretnaNeh.ObyvateliaNehnutelnosti.Add(konkretnyObcan);
                        }
                        else
                        {
                            //zmazanie z nehnutelnosti 
                            konkretnyObcan.Domov.ObyvateliaNehnutelnosti.Delete(konkretnyObcan);
                            konkretnyObcan.Domov = konkretnaNeh;
                            konkretnaNeh.ObyvateliaNehnutelnosti.Add(konkretnyObcan);
                        }
                        return "OK";
                    }
                    else
                    {
                        return "Neexistuje nehnutelnost so supisnym cislom";
                    }
                }
                else
                {
                    return "Neexistuje katastarlne uzemie";
                }

            }
            else
            {
                return "Neexistuje obcan s rodnym cislom";
            }


        }
        public string Uloha11(string rodCisloPodovdneho, int supisCislo, int katCislo, string rodCisloNoveho)
        {
            if (rodCisloPodovdneho != rodCisloNoveho)
            {
                var tmpObcanPodovd = new Obcan() { RodCislo = rodCisloPodovdneho };
                var tmpObcanNovy = new Obcan() { RodCislo = rodCisloNoveho };
                var tmpNehnutelnost = new Nehnutelnost() { SupisneCislo = supisCislo };
                var tmpKatuzem = new KatastralneUzemieCislo() { CisloUzemia = katCislo };
                if (KatasterCislo.Contains(tmpKatuzem))
                {
                    var konkretneUzemie = KatasterCislo.Find(tmpKatuzem);
                    if (konkretneUzemie.NehnutelnostiNaUzemi.Contains(tmpNehnutelnost))
                    {
                        var konkretnaNeh = konkretneUzemie.NehnutelnostiNaUzemi.Find(tmpNehnutelnost);
                        if (Obyvatelstvo.Contains(tmpObcanNovy) && Obyvatelstvo.Contains(tmpObcanPodovd))
                        {
                            var konkretnyNovyObcan = Obyvatelstvo.Find(tmpObcanNovy);
                            var konkretnyPovodObcan = Obyvatelstvo.Find(tmpObcanPodovd);
                            var tmpVlastnikPovod = new Vlastnik() { Majitel = konkretnyPovodObcan };
                            var tmpVlastnikNovy = new Vlastnik() { Majitel = konkretnyNovyObcan };
                            if (konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.Contains(tmpVlastnikPovod))
                            {
                                var konkretnyVlastnikPovod = konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.Find(tmpVlastnikPovod);
                                if (konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.Contains(tmpVlastnikNovy))
                                {
                                    var konkretnyVlastnikNovy = konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.Find(tmpVlastnikNovy);
                                    konkretnyVlastnikNovy.MajetkovyPodiel += konkretnyVlastnikPovod.MajetkovyPodiel;
                                    konkretnyPovodObcan.VlastnictvoObcana.Remove(konkretnaNeh.NehnutelnostVListeVlastnicta);
                                    konkretnaNeh.NehnutelnostVListeVlastnicta.Vlastnici.Delete(konkretnyVlastnikPovod);
                                    return "OK";
                                }
                                else
                                {

                                    konkretnyPovodObcan.VlastnictvoObcana.Remove(konkretnaNeh.NehnutelnostVListeVlastnicta);
                                    konkretnyVlastnikPovod.Majitel = konkretnyNovyObcan;
                                    konkretnyNovyObcan.VlastnictvoObcana.Add(konkretnaNeh.NehnutelnostVListeVlastnicta);
                                    return "OK";
                                }
                            }
                            else
                            {
                                return "Obcan nieje vlastnikom zadanej budovy";
                            }

                        }
                        else
                        {
                            return "Osoba s rodnym cislom neexistuje";
                        }
                    }
                    else
                    {
                        return "Neexistuje nehnutelnost so zadanym supisnym cislom";
                    }
                }
                else
                {
                    return "Neexistuje katastralne uzemie so zadanym cislom";
                }
            }
            else
            {
                return "OK";
            }
        }
        public List<string> Uloha13(string rodCislo, int idListu, int idKatUzem)
        {
            var tmpObcan = new Obcan() { RodCislo = rodCislo };
            var tmoListVlast = new ListVlastnictva() { IDListu = idListu };
            var tmpKatuzem = new KatastralneUzemieCislo() { CisloUzemia = idKatUzem };
            if (Obyvatelstvo.Contains(tmpObcan))
            {
                var konkretnyObcan = Obyvatelstvo.Find(tmpObcan);
                if (KatasterCislo.Contains(tmpKatuzem))
                {
                    var konkretnyKat = KatasterCislo.Find(tmpKatuzem);
                    if (konkretnyKat.ListyVlastnictva.Contains(tmoListVlast))
                    {
                        var konkretnyList = konkretnyKat.ListyVlastnictva.Find(tmoListVlast);
                        var tmpVlastnik = new Vlastnik() { Majitel = tmpObcan };
                        if (konkretnyList.Vlastnici.Contains(tmpVlastnik))
                        {
                            var konkretnyVlasnik = konkretnyList.Vlastnici.Find(tmpVlastnik);
                            konkretnyObcan.VlastnictvoObcana.Remove(konkretnyList);
                            konkretnyList.Vlastnici.Delete(konkretnyVlasnik);
                            var retList = new List<string>();
                            //-------------------------------------------------------------------------------------------------------FOREACH STROM
                            if (konkretnyList.Vlastnici.Count != 0)
                            {
                                var vlastniciNaListe = konkretnyList.Vlastnici.InOrder();
                                foreach (var item in vlastniciNaListe)
                                {
                                    retList.Add(item.ToString());
                                }
                            }
                            if (retList.Count == 0)
                            {
                                retList.Add("List nema ziadnych vlastnikov");
                            }
                            return retList;
                        }
                        else
                        {
                            return new List<string>() { "Vlasnik neni v zadanom liste vlastnictva" };
                        }

                    }
                    else
                    {
                        return new List<string>() { "Nenajdeny list vlastnictva so zadanym cislom" };
                    }

                }
                else
                {
                    return new List<string>() { "Nenajdeny kataster so zadanym cislom" };
                }
            }
            else
            {
                return new List<string>() { "obcan neexistuje" };
            }

        }
        public List<string> Uloha15()
        {
            var retList = new List<string>();
            //----------------------------------STROM FOR EACH
            if (KatasterNazov.Count != 0)
            {
                var listKat = KatasterNazov.InOrder();
                foreach (var item in listKat)
                {
                    retList.Add(item.ToString());
                }
            }
            else
            {
                retList.Add("Neexistuje ziadny kataster");
            }
            return retList;
        }
        public List<string> Uloha19CisloKat(int cisloKat)
        {

            var tmpKat = new KatastralneUzemieCislo() { CisloUzemia = cisloKat };

            if (KatasterCislo.Contains(tmpKat))
            {
                var konkretnyKat = KatasterCislo.Find(tmpKat);
                if (konkretnyKat.ListyVlastnictva.Count != 0)
                {
                    var retList = new List<string>();
                    //----------------------------------------------------FOREACH 
                    var listListov = konkretnyKat.ListyVlastnictva.InOrder();
                    foreach (var item in listListov)
                    {
                        retList.Add(item.ToString());
                    }
                    return retList;
                }
                else
                {
                    return new List<string>() { "Kataster neobsahuje ziadne listy" };
                }
            }
            else
            {
                return new List<string>() { "Nenajdeny kataster so zadanym cislom" };
            }

        }
        public List<string> Uloha19NazovKat(string nazovKat)
        {
            var tmpKat = new KatastralneUzemieNazov() { NazovUzemia = nazovKat };

            if (KatasterNazov.Contains(tmpKat))
            {
                var konkretnyKat = KatasterNazov.Find(tmpKat);
                if (konkretnyKat.ListyVlastnictva.Count != 0)
                {
                    var retList = new List<string>();
                    //----------------------------------------------------FOREACH 
                    var listListov = konkretnyKat.ListyVlastnictva.InOrder();
                    foreach (var item in listListov)
                    {
                        retList.Add(item.ToString());
                    }
                    return retList;
                }
                else
                {
                    return new List<string>() { "Kataster neobsahuje ziadne listy" };
                }
            }
            else
            {
                return new List<string>() { "Nenajdeny kataster so zadanym nazvom" };
            }

        }
        public List<string> Uloha19Listy(int cisloKat, int idListu)
        {
            var novyUzem = new KatastralneUzemieCislo() { CisloUzemia = cisloKat };
            var tmpList = new ListVlastnictva() { IDListu = idListu };
            if (KatasterCislo.Contains(novyUzem))
            {
                var konkretnyKata = KatasterCislo.Find(novyUzem);
                if (konkretnyKata.ListyVlastnictva.Contains(tmpList))
                {
                    var konkretnyList = konkretnyKata.ListyVlastnictva.Find(tmpList);

                    var retList = new List<string>();
                    retList.Add(konkretnyList.ToString(1));
                    return retList;

                }
                else
                {
                    return new List<string>() { "Neexistuje list vlastnictva" };
                }

            }
            else
            {
                return new List<string>() { "Kataster sa nenachadza v databaze" };
            }
        }
        public string Uloha19(int cisloListuDel, int cisloKat, int CisloListNew)
        {
            if (cisloListuDel != CisloListNew)
            {
                var tmpDeletList = new ListVlastnictva() { IDListu = cisloListuDel };
                var tmpNewList = new ListVlastnictva() { IDListu = CisloListNew };
                var tmpKat = new KatastralneUzemieCislo() { CisloUzemia = cisloKat };
                if (KatasterCislo.Contains(tmpKat))
                {
                    var konkretnyKat = KatasterCislo.Find(tmpKat);
                    if (konkretnyKat.ListyVlastnictva.Contains(tmpNewList))
                    {
                        var konkretnyNewList = konkretnyKat.ListyVlastnictva.Find(tmpNewList);
                        if (konkretnyKat.ListyVlastnictva.Contains(tmpDeletList))
                        {
                            var konkretnyDelList = konkretnyKat.ListyVlastnictva.Find(tmpDeletList);
                            if (konkretnyNewList.Nehnutelnosti.Count == 0)
                            {
                                if (konkretnyNewList.Vlastnici.Count == 0)
                                {
                                    var tmpKatNazov = new KatastralneUzemieNazov() { NazovUzemia = konkretnyKat.NazovUzemia };
                                    var konkretnyKatNazov = KatasterNazov.Find(tmpKatNazov);

                                    konkretnyKat.ListyVlastnictva.Delete(konkretnyDelList);
                                    konkretnyKat.ListyVlastnictva.Delete(konkretnyNewList);
                                    konkretnyKatNazov.ListyVlastnictva.Delete(konkretnyDelList);
                                    konkretnyKatNazov.ListyVlastnictva.Delete(konkretnyNewList);


                                    konkretnyDelList.IDListu = konkretnyNewList.IDListu;
                                    konkretnyKat.ListyVlastnictva.Add(konkretnyDelList);
                                    konkretnyKatNazov.ListyVlastnictva.Add(konkretnyDelList);
                                    return "OK";
                                }
                                else
                                {
                                    return "Novy list ma vlastnikov NENI PRAZDNY";

                                }

                            }
                            else
                            {
                                return "Novy list ma nehnutelnosti NENI PRAZDNY";
                            }



                        }
                        else
                        {
                            return "Mazany list neexistuje";
                        }

                    }
                    else
                    {
                        return "Cislo noveho listu neexistuje";
                    }
                }
                else
                {
                    return "Nenajdeny Kataster so zadanym cislom";
                }
            }
            else
            {
                return "Cisla listov sa zhoduju";
            }

        }
        public string Uloha20(int supisCislo, int idList, int idKataster)
        {
            var tmpNeh = new Nehnutelnost() { SupisneCislo = supisCislo };
            var tmpList = new ListVlastnictva() { IDListu = idList };
            var tmpKataster = new KatastralneUzemieCislo() { CisloUzemia = idKataster };


            if (KatasterCislo.Contains(tmpKataster))
            {
                var konkretnyKat = KatasterCislo.Find(tmpKataster);
                if (konkretnyKat.ListyVlastnictva.Contains(tmpList))
                {
                    var konkretnyList = konkretnyKat.ListyVlastnictva.Find(tmpList);
                    if (konkretnyList.Nehnutelnosti.Contains(tmpNeh))
                    {
                        var konkretnaNeh = konkretnyList.Nehnutelnosti.Find(tmpNeh);
                        if (konkretnaNeh.ObyvateliaNehnutelnosti.Count != 0)
                        {
                            //------------------------------------------------------ FOREACH STROM
                            var listObyv = konkretnaNeh.ObyvateliaNehnutelnosti.InOrder();
                            foreach (var item in listObyv)
                            {
                                item.Domov = null;
                            }

                        }
                        konkretnaNeh.ObyvateliaNehnutelnosti.Root = null;
                        konkretnyList.Nehnutelnosti.Delete(konkretnaNeh);
                        konkretnaNeh.NehnutelnostVListeVlastnicta = null;
                        var konkretnyKatNazov = KatasterNazov.Find(new KatastralneUzemieNazov() { NazovUzemia = konkretnyKat.NazovUzemia });
                        konkretnyKatNazov.NehnutelnostiNaUzemi.Delete(konkretnaNeh);
                        konkretnyKat.NehnutelnostiNaUzemi.Delete(konkretnaNeh);
                        return "OK";
                    }
                    else
                    {
                        return "Nehnutelnost neexistuje v zadanom liste vlastnictva";
                    }
                }
                else
                {
                    return "Cislo listu neexistuje v zadanom katastry";
                }
            }
            else
            {
                return "Nenajdeny Kataster so zadanym cislom";
            }

        }
        public List<string> Uloha20Nehnutelnosti(int idKataster, int idList)
        {

            var tmpList = new ListVlastnictva() { IDListu = idList };
            var tmpKataster = new KatastralneUzemieCislo() { CisloUzemia = idKataster };


            if (KatasterCislo.Contains(tmpKataster))
            {
                var konkretnyKat = KatasterCislo.Find(tmpKataster);
                if (konkretnyKat.ListyVlastnictva.Contains(tmpList))
                {
                    var konkretnyList = konkretnyKat.ListyVlastnictva.Find(tmpList);
                    var retList = new List<string>();
                    if (konkretnyList.Nehnutelnosti.Count != 0)
                    {
                        //-----------------------------------------------------------------------FOREACH
                        var listL = konkretnyList.Nehnutelnosti.InOrder();
                        foreach (var item in listL)
                        {
                            retList.Add(item.ToString(1));
                        }
                    }
                    if (retList.Count == 0)
                    {
                        retList.Add("List nema ziadne nehnutelnosti");
                    }
                    return retList;

                }
                else
                {
                    return new List<string>() { "Cislo listu neexistuje v zadanom katastry" };

                }
            }
            else
            {
                return new List<string>() { "Kataster neobsahuje ziadne listy" };
            }
        }
        public List<string> Uloha22(int cisDelKataster, int cisloNewKataster)
        {
            var tmpKatasterNew = new KatastralneUzemieCislo() { CisloUzemia = cisloNewKataster };
            var tmpKatasterDel = new KatastralneUzemieCislo() { CisloUzemia = cisDelKataster };
            if (cisDelKataster != cisloNewKataster)
            {
                if (KatasterCislo.Contains(tmpKatasterDel))
                {
                    var konkretnyDel = KatasterCislo.Find(tmpKatasterDel);
                    if (KatasterCislo.Contains(tmpKatasterNew))
                    {
                        var konkretnyNew = KatasterCislo.Find(tmpKatasterNew);
                        var konkretnyKatasterNazovNew = KatasterNazov.Find(new KatastralneUzemieNazov() { NazovUzemia = konkretnyNew.NazovUzemia });
                        //-----------------------------------------------------------------STROM FOREACH
                        var listNehnutelnostiVKatastry = konkretnyDel.NehnutelnostiNaUzemi.InOrder();
                        foreach (var item in listNehnutelnostiVKatastry)
                        {
                            try
                            {
                                konkretnyNew.NehnutelnostiNaUzemi.Add(item);
                                konkretnyKatasterNazovNew.NehnutelnostiNaUzemi.Add(item);
                            }
                            catch (Exception)
                            {

                                var noveId = konkretnyNew.NehnutelnostiNaUzemi.Max().SupisneCislo + 1;
                                item.SupisneCislo = noveId;
                                konkretnyNew.NehnutelnostiNaUzemi.Add(item);
                                konkretnyKatasterNazovNew.NehnutelnostiNaUzemi.Add(item);
                            }
                        }
                        //-------------------------------------------------------------------STROM FOREACH
                        var listListovVlastnictva = konkretnyDel.ListyVlastnictva.InOrder();

                        foreach (var item in listListovVlastnictva)
                        {
                            try
                            {
                                item.Uzemie = konkretnyKatasterNazovNew;
                                konkretnyNew.ListyVlastnictva.Add(item);
                                konkretnyKatasterNazovNew.ListyVlastnictva.Add(item);
                            }
                            catch (Exception)
                            {

                                var noveId = konkretnyNew.ListyVlastnictva.Max().IDListu + 1;
                                item.IDListu = noveId;
                                konkretnyNew.ListyVlastnictva.Add(item);
                                konkretnyKatasterNazovNew.ListyVlastnictva.Add(item);
                            }
                        }
                        konkretnyDel.NehnutelnostiNaUzemi.Root = null;
                        konkretnyDel.ListyVlastnictva.Root = null;
                        KatasterCislo.Delete(konkretnyDel);

                        var konkretnyKatNazovDel = KatasterNazov.Find(new KatastralneUzemieNazov() { NazovUzemia = konkretnyDel.NazovUzemia });
                        konkretnyKatNazovDel.NehnutelnostiNaUzemi.Root = null;
                        konkretnyKatNazovDel.ListyVlastnictva.Root = null;
                        KatasterNazov.Delete(konkretnyKatNazovDel);

                        return Uloha22Info(konkretnyNew.CisloUzemia);


                    }
                    else
                    {
                        return new List<string>() { "Neexistuje Kataster do ktoreho sa presunu zaznami" };
                    }

                }
                else
                {
                    return new List<string>() { "Neexistuje Kataster ktory bude zmazany" };
                }
            }
            else
            {
                return new List<string>() { "Zadane rovnake cisla katastrov" };
            }



        }
        public List<string> Uloha22Info(int cisloKat)
        {
            var tmpKat = new KatastralneUzemieCislo() { CisloUzemia = cisloKat };
            if (KatasterCislo.Contains(tmpKat))
            {
                var konkretnyKat = KatasterCislo.Find(tmpKat);
                //----------------------------------------------FOREACH
                var retList = new List<string>();
                //---------------------------------------------foreach na zobrazenie
                if (konkretnyKat.ListyVlastnictva.Count != 0)
                {
                    var listCelyNovyKat = konkretnyKat.ListyVlastnictva.InOrder();
                    foreach (var item in listCelyNovyKat)
                    {
                        retList.Add(item.ToString(1));
                        //----------------------------------------------------foreach zobrazenie

                        if (item.Nehnutelnosti.Count != 0)
                        {

                            var listNehnutelnostiList = item.Nehnutelnosti.InOrder();
                            foreach (var item2 in listNehnutelnostiList)
                            {
                                retList.Add("  " + item2.ToString());
                            }
                        }

                    }
                    if (retList.Count == 0) retList.Add("V katastralnom uzemi niesu ziadne nehnutelnosti");

                }
                else
                {
                    retList.Add("Kataster nema ziadne listy vlastnictva");
                }

                return retList;

            }
            else
            {
                return new List<string>() { "Neexistuje kataster" };
            }
        }
        public List<int> VsetkoInfo()
        {
            var retList = new List<int>();


            retList.Add(KatasterCislo.Count);
            retList.Add(Obyvatelstvo.Count);

            var listKat = KatasterCislo.InOrder();
            int vsetkyListy = 0;
            int vsetkyNehnutelnosti = 0;
            foreach (var item in listKat)
            {
                vsetkyListy += item.ListyVlastnictva.Count;
                vsetkyNehnutelnosti += item.NehnutelnostiNaUzemi.Count;
            }
            retList.Add(vsetkyListy);
            retList.Add(vsetkyNehnutelnosti);
            return retList;

        }
        public List<string> UlohaVsetkoNehnutelnosti(int cisloKat)
        {
            var retList = new List<string>();
            var tmpKat = new KatastralneUzemieCislo() { CisloUzemia = cisloKat };
            if (KatasterCislo.Contains(tmpKat))
            {
                var konkretnyKat = KatasterCislo.Find(tmpKat);
                if (konkretnyKat.NehnutelnostiNaUzemi.Count != 0)
                {
                    var listNehutelnosti = konkretnyKat.NehnutelnostiNaUzemi.InOrder();
                    foreach (var item in listNehutelnosti)
                    {
                        retList.Add(item.ToString());
                    }
                }
                else
                {
                    retList.Add("Na katastry ziadna nehnutelnost");
                }

                return retList;
            }
            else
            {
                return new List<string>() { "Nenajdeny Kataster s cislom: " + cisloKat };
            }

        }
        public List<string> VsetkoObyvatelstvo()
        {
            if (Obyvatelstvo.Count != 0)
            {
                //----------------------------------------FOREACH STROM
                var listObyv = Obyvatelstvo.InOrder();
                var retList = new List<string>();
                foreach (var item in listObyv)
                {
                    retList.Add(item.ToString(1));
                }
                return retList;
            }
            return new List<string>() { "Obyvatelia neexistuju " };

        }
        public List<string> VsetkoVlastniciListu(int cisKat, int idList)
        {
            var tmpKatUzem = new KatastralneUzemieCislo() { CisloUzemia = cisKat };
            if (KatasterCislo.Contains(tmpKatUzem))
            {
                var konkretneUzem = KatasterCislo.Find(tmpKatUzem);
                var tmpList = new ListVlastnictva() { IDListu = idList };
                if (konkretneUzem.ListyVlastnictva.Contains(tmpList))
                {
                    var konkretnyList = konkretneUzem.ListyVlastnictva.Find(tmpList);
                    var retList = new List<string>();
                    //------------------------------------------------------FOREACH LIST
                    if (konkretnyList.Vlastnici.Count != 0)
                    {
                        var listT = konkretnyList.Vlastnici.InOrder();
                        foreach (var item in listT)
                        {
                            retList.Add("RC: " + item.Majitel.RodCislo + " MP: " + item.MajetkovyPodiel);
                        }
                    }
                    else
                    {
                        retList.Add("List nema ziadnych vlastnikov");
                    }
                    return retList;
                }
                else
                {
                    return new List<string>() { "Nenajdeny list vlastnictva so zadanym cislom" };
                }
            }
            else
            {
                return new List<string>() { "Nenajdeny kataster so zadanym cislom" };
            }
        }
        public List<string> VsetkoCoVlastniObcanNeh(string rodCislo)
        {
            var tmpObcan = new Obcan() { RodCislo = rodCislo };
            if (Obyvatelstvo.Contains(tmpObcan))
            {
                var konkretnyObcan = Obyvatelstvo.Find(tmpObcan);
                if (konkretnyObcan.VlastnictvoObcana.Count != 0)
                {
                    var retList = new List<string>();
                    foreach (var item in konkretnyObcan.VlastnictvoObcana)
                    {
                        if (item.Nehnutelnosti.Count != 0)
                        {
                            var podiel = item.Vlastnici.Find(new Vlastnik() { Majitel = konkretnyObcan }).MajetkovyPodiel;
                            foreach (var item2 in item.Nehnutelnosti.InOrder())
                            {
                                retList.Add(item2.ToString() + " MP: " + podiel);
                            }
                            //---------------------------------------------------------------FOREACH STROM
                        }
                    }
                    if (retList.Count == 0)
                    {
                        retList.Add("Vlastnik nema v zadanom katastry ziadne nehnutelosti");
                    }
                    return retList;
                }
                else
                {
                    return new List<string>() { "Obcan nieje vlastnikom ziadnej budovy" };
                }
            }
            else
            {
                return new List<string>() { "Nenajdeny obcan so zadanym rodnym cislom" };
            }



        }
        public List<string> VsetkyListyObcana(string rodCislo)
        {
            var tmpObcan = new Obcan() { RodCislo = rodCislo };
            if (Obyvatelstvo.Contains(tmpObcan))
            {
                var konkretnyObcan = Obyvatelstvo.Find(tmpObcan);
                if (konkretnyObcan.VlastnictvoObcana.Count != 0)
                {
                    var retList = new List<string>();
                    if (konkretnyObcan.VlastnictvoObcana.Count != 0)
                    {
                        foreach (var item in konkretnyObcan.VlastnictvoObcana)
                        {
                            retList.Add(item.ToString());
                        }
                    }
                    else
                    {
                        retList.Add("Vlastnik nema v zadanom katastry ziadne nehnutelosti");
                    }

                    return retList;
                }
                else
                {
                    return new List<string>() { "Obcan nieje vlastnikom ziadnej budovy" };
                }
            }
            else
            {
                return new List<string>() { "Nenajdeny obcan so zadanym rodnym cislom" };
            }
        }
        public void Generator()
        {
            Stopwatch time = Stopwatch.StartNew();
            string meno = "meno";
            string priezvisko = "priezvisko";
            string rodneCislo = "0000000000000000";  
            DateTime start = new DateTime(1950, 1, 1);
            Random rnd = new Random(DateTime.Now.Millisecond);
            int range = ((TimeSpan)(new DateTime(2000, 1, 1) - start)).Days;
            //obcania generator
            for (int i = 0; i < GeneratorConst.PocetObyvatelov; i++)
            {
                var koniecRodCisla = i.ToString();
                rodneCislo = rodneCislo.Substring(0, rodneCislo.Length - koniecRodCisla.Length) + koniecRodCisla;
                var celeMeno = meno + koniecRodCisla;
                var celePriezvisko = priezvisko + koniecRodCisla;
                DateTime datumNarodenia = start.AddDays(rnd.Next(range));

                var novaOsoba = new Obcan() { DatumNarodenia = datumNarodenia, Meno = celeMeno, Priezvisko = celePriezvisko, RodCislo = rodneCislo };
                Obyvatelstvo.Add(novaOsoba);
            }

            var nazovKatastra = "kataster";
            // katastre generator
            for (int i = 1; i < GeneratorConst.PocetKatastrov + 1; i++)
            {
                KatasterNazov.Add(new KatastralneUzemieNazov() { CisloUzemia = i, NazovUzemia = nazovKatastra + i.ToString() });
                KatasterCislo.Add(new KatastralneUzemieCislo() { CisloUzemia = i, NazovUzemia = nazovKatastra + i.ToString() });
            }
            
            //generovanie listov
            for (int i = 1; i < GeneratorConst.PocetKatastrov + 1; i++)
            {
                var konkretnyKatNazov= KatasterNazov.Find(new KatastralneUzemieNazov() { NazovUzemia = "kataster"+i });
                var konkretnyKatCislo = KatasterCislo.Find(new KatastralneUzemieCislo() { CisloUzemia = i });
                //vsetky nehnutelnosti deleno katastre + odchylka od celkoveho poctu nehnutelnosti
             
                for (int j = 1; j < GeneratorConst.PocetListovVKatastry + 1; j++)
                {
                    var listVlast = new ListVlastnictva() { IDListu = j, Uzemie = konkretnyKatNazov };
                    konkretnyKatCislo.ListyVlastnictva.Add(listVlast);
                    konkretnyKatNazov.ListyVlastnictva.Add(listVlast);
                    
                }
            }

            var adresa = "adresa";
            var popis = "popis";
      
            //nehnutelnosti generator
            var listKatastrov = KatasterCislo.InOrder();
            for (int j = 0; j < GeneratorConst.PocetKatastrov; j++)
            {
                var pocetNeh = GeneratorConst.PocetNehnutelnosti / GeneratorConst.PocetKatastrov + rnd.Next(0, 50);
                var cisloRandomKat = listKatastrov[j];
                var nazovRandomKat = KatasterNazov.Find(new KatastralneUzemieNazov() { NazovUzemia = listKatastrov[j].NazovUzemia });
                var listyVlastnictvaKatastra = nazovRandomKat.ListyVlastnictva.InOrder();
                for (int i = 0; i < pocetNeh; i++)
                {
                    var randomList = listyVlastnictvaKatastra[rnd.Next(0, listyVlastnictvaKatastra.Count)];
                   
                        var nehnutelnostMax = cisloRandomKat.NehnutelnostiNaUzemi.Max();
                        if (nehnutelnostMax == null)
                        {
                            var supisCislo = 1;
                            var nehnutelnost = new Nehnutelnost() { Adresa = adresa + supisCislo.ToString(), Popis = popis + supisCislo.ToString(), SupisneCislo = supisCislo, NehnutelnostVListeVlastnicta = randomList };
                            cisloRandomKat.NehnutelnostiNaUzemi.Add(nehnutelnost);
                            nazovRandomKat.NehnutelnostiNaUzemi.Add(nehnutelnost);
                            randomList.Nehnutelnosti.Add(nehnutelnost);
                        }
                        else
                        {
                            var supisCislo = cisloRandomKat.NehnutelnostiNaUzemi.Max().SupisneCislo + 1;
                            var nehnutelnost = new Nehnutelnost() { Adresa = adresa + supisCislo.ToString(), Popis = popis + supisCislo.ToString(), SupisneCislo = supisCislo, NehnutelnostVListeVlastnicta = randomList };
                            cisloRandomKat.NehnutelnostiNaUzemi.Add(nehnutelnost);
                            nazovRandomKat.NehnutelnostiNaUzemi.Add(nehnutelnost);
                            randomList.Nehnutelnosti.Add(nehnutelnost);
                        }
                   
                }
                
            }
            var zakladRodCisla = "0000000000000000";
            //ubytovane obyvatelstvo
            for (int i = 0; i < GeneratorConst.PocetUbytovanychObyvatelov; i++)
            {
                var randomKoniecRodCislo = rnd.Next(0, GeneratorConst.PocetObyvatelov).ToString();
                var randomRodCislo = zakladRodCisla.Substring(0, zakladRodCisla.Length - randomKoniecRodCislo.Length) + randomKoniecRodCislo;
                var tmpObcan = new Obcan() { RodCislo = randomRodCislo };
                if (Obyvatelstvo.Contains(tmpObcan))
                {
                    var konkretnyObcan = Obyvatelstvo.Find(tmpObcan);
                    if (konkretnyObcan.Domov == null)
                    {
                        var randomKatasterNazov = "kataster" + rnd.Next(1, GeneratorConst.PocetKatastrov + 1).ToString();
                        var tmpKat = new KatastralneUzemieNazov() { NazovUzemia = randomKatasterNazov };
                        var konkretnyKat = KatasterNazov.Find(tmpKat);
                        var pocetNehnutelnostiKatastra = konkretnyKat.NehnutelnostiNaUzemi.Count;
                        if (pocetNehnutelnostiKatastra != 0)
                        {
                            var listNehnutelnostiKatastra = konkretnyKat.NehnutelnostiNaUzemi.InOrder();
                            var randomNeh = rnd.Next(0, pocetNehnutelnostiKatastra);
                            var supisCisloRandomNeh = listNehnutelnostiKatastra[randomNeh].SupisneCislo;
                            Uloha10(konkretnyObcan.RodCislo, supisCisloRandomNeh, konkretnyKat.NazovUzemia);
                        }
                        else
                        {
                            i--;
                        }
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }

            //vlastnici generator
            foreach (var item in listKatastrov)
            {
                var listListovVlast = item.ListyVlastnictva.InOrder();
                foreach (var item2 in listListovVlast)
                {
                 
                    var listRandomVlasnikov = new List<Vlastnik>();
                    var randomPocetVlastnikNaLite = rnd.Next(0, GeneratorConst.MaxPocetVlastnikov);
                    
                    for (int i = 0; i < randomPocetVlastnikNaLite; i++)
                    {
                        var opakuj = false;
                        var randomKoniecRodCislo = rnd.Next(0, GeneratorConst.PocetObyvatelov).ToString();
                        var randomRodCislo = zakladRodCisla.Substring(0, zakladRodCisla.Length - randomKoniecRodCislo.Length) + randomKoniecRodCislo;
                        var randomVlastnik = Obyvatelstvo.Find(new Obcan() { RodCislo = randomRodCislo });
                        var randomPodiel = Math.Round((rnd.NextDouble() * 100)) / 100;
                        var vlastnik = new Vlastnik() { MajetkovyPodiel = randomPodiel, Majitel = randomVlastnik };

                        for (int j = 0; j < listRandomVlasnikov.Count; j++)
                        {
                            if (listRandomVlasnikov[j].Majitel.RodCislo == vlastnik.Majitel.RodCislo) {
                                opakuj = true;
                                break;
                            }
                        }
                        if (opakuj) {
                            i--;
                        }
                        else
                        {
                            listRandomVlasnikov.Add(vlastnik);
                        }
                    
                    }
                    foreach (var item3  in listRandomVlasnikov)
                    {
                        item2.Vlastnici.Add(item3);
                        item3.Majitel.VlastnictvoObcana.Add(item2);
                    }
                }
            }

            time.Stop();
            var cas = time.Elapsed;
        }
    }
}



