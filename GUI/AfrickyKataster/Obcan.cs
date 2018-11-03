using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Obcan : IComparable<Obcan>
    {
        public string RodCislo { get; set; }
        public Nehnutelnost Domov { get; set; }
        public string Meno { get; set; }
        public string Priezvisko { get; set; }
        public DateTime DatumNarodenia { get; set; }
        public List<ListVlastnictva> VlastnictvoObcana { get; set; }

        public Obcan()
        {
            VlastnictvoObcana = new List<ListVlastnictva>();
        }
        public int CompareTo(Obcan other)
        {
            return RodCislo.CompareTo(other.RodCislo);
        }
        public override string ToString()
        {
            return "Rodne Cislo: " + RodCislo + " Meno: " + Meno + " Priezvisko: " + Priezvisko + " Datum narodenia: " + DatumNarodenia.ToShortDateString();
        }
        public  string ToString(int i)
        {
            return "Rodne Cislo: " + RodCislo + " Meno: " + Meno + " Priezvisko: " + Priezvisko;
        }
    }
}
