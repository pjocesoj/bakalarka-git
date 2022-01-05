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
        protected List<IPredmet> obsah;

        /// <summary>
        /// vytvoří nový inventář s neomezenou kapacitou
        /// </summary>
        public InventarV2()
        {
            obsah = new List<IPredmet>();
        }

        /// <summary>
        /// přidá předmět do inventáře
        /// </summary>
        /// <param name="item">přidávaný předmět</param>
        /// <returns>zda je možné předmět vložit</returns>
        public virtual bool Pridej(IPredmet item)
        {
            obsah.Add(item);
            return true;
        }
        /// <summary>
        /// odebere předmět z inventáře
        /// </summary>
        /// <param name="item">odebíraný předmět</param>
        public virtual void Odeber(IPredmet item)
        {
            obsah.Remove(item);
        }

        /// <summary>
        /// vrtátí předmět na indexu i
        /// </summary>
        /// <param name="i">index</param>
        public IPredmet GetAt(int i)
        {
            return obsah[i];
        }

        /// <summary>
        /// vrtátí předmět na indexu i
        /// </summary>
        /// <param name="i">index</param>
        public IPredmet this[int i]
        {
            get { return obsah[i]; }
        }

        /// <summary>
        /// vypíše předměty v inventáři
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Stav());
            sb.Append("\n");
            foreach (IPredmet p in obsah)
            {
                sb.Append($"----------\n{p}\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// aktualní stav zaplnění inventáře
        /// </summary>
        /// <returns>počet předmětů a jejich celková hmotnost</returns>
        public virtual string Stav()
        {
            double hmot = 0;
            double pocet = 0;
            foreach (IPredmet p in obsah)
            {
                hmot += p.Hmotnost;
                pocet++;
            }

            return $"{pocet}ks, vaha:{hmot}";
        }

        #region hledani

       /// <summary>
       /// vrátí index předmětu v inventáři
       /// <br/>porovnává data ne reference
       /// </summary>
       /// <param name="item">hledaný předmět</param>
        protected int indexOf(IPredmet item)
        {
            for (int i = 0; i < obsah.Count; i++)
            {
                if (obsah[i].Stejne(item))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// vrátí index stacku předmětů (-1 pokud v inventáři nejsou)
        /// </summary>
        /// <param name="item">hledaný předmět</param>
        public virtual int IndexOfStack(IPredmet item)
        {
            return indexOf(item);
        }

        /// <summary>
        /// řekne zda se předmět již nachází v inventáři
        /// </summary>
        /// <param name="item">hledaný předmět</param>
        public bool UzJeVInventu(IPredmet item)
        {
            int ret = indexOf(item);
            if (ret != -1) { return true; }
            else { return false; }
        }
        #endregion

    }
}
