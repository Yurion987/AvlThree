using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class KatastralneUzemieNazov : Kataster, IComparable<KatastralneUzemieNazov>
    {
      

        public KatastralneUzemieNazov() : base()
        {
            
        }

        public int CompareTo(KatastralneUzemieNazov other)
        {
            return NazovUzemia.CompareTo(other.NazovUzemia);
        }
    }
}
