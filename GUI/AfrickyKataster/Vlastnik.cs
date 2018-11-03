using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Vlastnik : IComparable<Vlastnik>
    {
        public Obcan Majitel { get; set; }
        public double MajetkovyPodiel { get; set; }

        public int CompareTo(Vlastnik other)
        {
            return Majitel.RodCislo.CompareTo(other.Majitel.RodCislo);
        }
        public override string ToString()
        {
            return "Rodne Cislo: " + Majitel.RodCislo + "     Majetkovy podiel: " + MajetkovyPodiel;
        }
        
    }
}
