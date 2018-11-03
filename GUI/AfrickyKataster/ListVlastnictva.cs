using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class ListVlastnictva : IComparable<ListVlastnictva>
    {
        public AVLTree<Nehnutelnost> Nehnutelnosti { get; set; }
        public AVLTree<Vlastnik> Vlastnici { get; set; }
        public KatastralneUzemieNazov Uzemie { get; set; }
        public int IDListu { get; set; }

        public ListVlastnictva()
        {
            Nehnutelnosti = new AVLTree<Nehnutelnost>();
            Vlastnici = new AVLTree<Vlastnik>();

        }
        public int CompareTo(ListVlastnictva other)
        {
            return IDListu.CompareTo(other.IDListu);
        }
        public override string ToString()
        {
            return "Id listu: " + IDListu + " V katastry: "+Uzemie.CisloUzemia + " S nazvom: "+Uzemie.NazovUzemia;
        }
        public string ToString(int i) {
            return "Id listu: " + IDListu + " Pocet nehnutelnosti v Liste: " + Nehnutelnosti.Count+ " Pocet vlastnikov na liste: "+Vlastnici.Count;
        }

    }
}
