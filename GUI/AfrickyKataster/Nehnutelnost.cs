using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Nehnutelnost : IComparable<Nehnutelnost>
    {
      
        public int SupisneCislo { get; set; }
        public string Adresa { get; set; }
        public string Popis { get; set; }

        public AVLTree<Obcan> ObyvateliaNehnutelnosti { get; set; } 
        public ListVlastnictva NehnutelnostVListeVlastnicta { get; set; }

        public Nehnutelnost()
        {
            ObyvateliaNehnutelnosti = new AVLTree<Obcan>(); 
        }

        public int CompareTo(Nehnutelnost other)
        {
            return SupisneCislo.CompareTo(other.SupisneCislo);
        }
        public override string ToString() {
            return "Cislo: " + SupisneCislo + " Popis: "+Popis;
        }
        public string ToString(int i)
        {
            if (i == -1) {
                return "Supisne Cislo: " + SupisneCislo + " Adresa: " + Adresa + " Popis: " + Popis + " Cislo Listu v ktorom je nehnutelnost: "+NehnutelnostVListeVlastnicta.IDListu;
            }
            return "Supisne Cislo: " + SupisneCislo +" Adresa: "+Adresa+ " Popis: " + Popis;
        }
       
    }
}
