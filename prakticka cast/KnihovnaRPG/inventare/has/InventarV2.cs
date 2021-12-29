using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// inventář s neomezenou kapacitou (rodič pro omezenou kapacitu)
    /// <br/> Predmet implementován jako HAS (nutno implementovat interface Sebratelne)
    /// </summary>
    public class InventarV2
    {
        /// <summary>
        /// seznam předmětů v inventáři
        /// </summary>
        protected List<Sebratelne> obsah;

        /// <summary>
        /// vytvoří nový inventář s neomezenou kapacitou
        /// </summary>
        public InventarV2()
        {
            obsah = new List<Sebratelne>();
        }

        /// <summary>
        /// přidá předmět do inventáře
        /// </summary>
        /// <param name="item">přidávaný předmět</param>
        /// <returns>zda je možné předmět vložit</returns>
        public virtual bool Pridej(Sebratelne item)
        {
            obsah.Add(item);
            return true;
        }
        /// <summary>
        /// odebere předmět z inventáře
        /// </summary>
        /// <param name="item">odebíraný předmět</param>
        public virtual void Odeber(Sebratelne item)
        {
            obsah.Remove(item);
        }

        /// <summary>
        /// vrtátí předmět na indexu i
        /// </summary>
        /// <param name="i">index</param>
        public Sebratelne GetAt(int i)
        {
            return obsah[i];
        }

        /// <summary>
        /// vrtátí předmět na indexu i
        /// </summary>
        /// <param name="i">index</param>
        public Sebratelne this[int i]
        {
            get { return obsah[i]; }
        }

        /// <summary>
        /// vypíše předměty v inventáři
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Sebratelne p in obsah)
            {
                sb.Append($"----------\n{p}\n");
            }
            return sb.ToString();
        }
    }
}
