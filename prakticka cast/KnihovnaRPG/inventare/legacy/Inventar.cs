using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG.Legacy
{
    /// <summary>
    /// inventář s neomezenou kapacitou (rodič pro omezenou kapacitu)
    /// <br/> Predmet implementován jako IS
    /// </summary>
    public class Inventar
    {
        /// <summary>
        /// seznam předmětů v inventáři
        /// </summary>
        protected List<Predmet> obsah; 

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
        public virtual bool Pridej(Predmet item)
        {
            obsah.Add(item);
            return true;
        }
        /// <summary>
        /// odebere předmět z inventáře
        /// </summary>
        /// <param name="item">odebíraný předmět</param>
        public virtual void Odeber(Predmet item)
        {
            obsah.Remove(item);
        }

        /// <summary>
        /// vrtátí předmět na indexu i
        /// </summary>
        /// <param name="i">index</param>
        public Predmet GetAt(int i)
        {
            return obsah[i];
        }

        /// <summary>
        /// vypíše předměty v inventáři
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Predmet p in obsah)
            {
                sb.Append($"----------\n{p}\n");
            }
            return sb.ToString();
        }
    }
}
