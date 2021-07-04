using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    public class Inventar
    {
        List<Predmet> obsah; 
        public Inventar()
        {
            obsah= new List<Predmet>();
        }

        public virtual bool pridej(Predmet item)
        {
            obsah.Add(item);
            return true;
        }
    }
}
