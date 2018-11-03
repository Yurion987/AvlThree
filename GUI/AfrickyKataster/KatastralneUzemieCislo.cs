using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class KatastralneUzemieCislo : Kataster,IComparable<KatastralneUzemieCislo>
    {
   
        public KatastralneUzemieCislo() : base()
        {
          
            
        }

        public int CompareTo(KatastralneUzemieCislo other)
        {
            return CisloUzemia.CompareTo(other.CisloUzemia);
        }
    }
}
