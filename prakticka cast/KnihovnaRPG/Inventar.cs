using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// logika pro inventář
    /// </summary>
    public class Inventar
    {
        List<Predmet> obsah; 

        /// <summary>
        /// vytvoří nový inventář s neomezenou kapacitou
        /// </summary>
        public Inventar()
        {
            obsah= new List<Predmet>();
        }

        /// <summary>
        /// přidá předmět do inventáře
        /// </summary>
        /// <param name="item">přidávaný předmět</param>
        /// <returns>zda je možné předmět vložit</returns>
        public virtual bool pridej(Predmet item)
        {
            obsah.Add(item);
            return true;
        }
    }
}
