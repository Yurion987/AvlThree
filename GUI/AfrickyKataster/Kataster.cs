using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Kataster : IComparable<Kataster>
    {
        public int CisloUzemia { get; set; }
        public string NazovUzemia { get; set; }

        public AVLTree<ListVlastnictva> ListyVlastnictva { get; set; }
        public AVLTree<Nehnutelnost> NehnutelnostiNaUzemi { get; set; }

        public Kataster()
        {
            ListyVlastnictva = new AVLTree<ListVlastnictva>();
            NehnutelnostiNaUzemi = new AVLTree<Nehnutelnost>();

        }

        public int CompareTo(Kataster other)
        {
            return CisloUzemia.CompareTo(other.CisloUzemia);
        }
        public override string ToString()
        {
            return "NU: " + NazovUzemia + " CU: " + CisloUzemia;
        }
    }
}
