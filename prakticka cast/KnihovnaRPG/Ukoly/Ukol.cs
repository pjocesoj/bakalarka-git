using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// úkol pro hráče
    /// </summary>
    public class Ukol
    {
        /// <summary>
        /// hráč právě splnil úkol
        /// </summary>
        public event EventHandler<IUkolOdmena> Dokoncen;

        /// <summary>
        /// název úkolu
        /// </summary>
        public string Jmeno { get; private set; }

        /// <summary>
        /// text úkolu
        /// </summary>
        public string Popis { get; private set; }

        /// <summary>
        /// zda hráč úkol již splnil
        /// </summary>
        public bool Splnen { get; protected set; }

        /// <summary>
        /// seznam co je třeba splnit k dokončení úkolu a aktuální stav
        /// </summary>
        public List<UkolPolozka> Polozky { get; protected set; }

        /// <summary>
        /// co hráč za splnění dostane
        /// </summary>
        public IUkolOdmena Odmena { get; protected set; }


        /// <summary>
        /// vytvoří nový úkol
        /// </summary>
        /// <param name="jmeno">název úkolu</param>
        /// <param name="popis">text úkolu</param>
        /// <param name="splnen">zda byl splněn (použito jen pokud hra obsahuje seznam splněných úkolů)</param>
        /// <param name="polozka">co je třeba splnit k dokončení úkolu</param>
        public Ukol(string jmeno, string popis, UkolPolozka polozka, bool splnen = false)
        {
            this.Jmeno = jmeno;
            this.Popis = popis;

            this.Splnen = splnen;

            this.Polozky = new List<UkolPolozka>();
            Polozky.Add(polozka);
        }

        /// <summary>
        /// vytvoří nový úkol
        /// </summary>
        /// <param name="jmeno">název úkolu</param>
        /// <param name="popis">text úkolu</param>
        /// <param name="splnen">zda byl splněn (použito jen pokud hra obsahuje seznam splněných úkolů)</param>
        /// <param name="polozky">seznam co je třeba splnit k dokončení úkolu</param>
        public Ukol(string jmeno, string popis, List<UkolPolozka> polozky, bool splnen = false)
        {
            this.Jmeno = jmeno;
            this.Popis = popis;

            this.Splnen = splnen;

            this.Polozky = polozky;
        }

        /// <summary>
        /// vytvoří nový úkol
        /// </summary>
        /// <param name="jmeno">název úkolu</param>
        /// <param name="popis">text úkolu</param>
        /// <param name="splnen">zda byl splněn (použito jen pokud hra obsahuje seznam splněných úkolů)</param>
        /// <param name="polozky">seznam co je třeba splnit k dokončení úkolu</param>
        /// <param name="odmena">co hráč dostane za splnění úkolu</param>
        public Ukol(string jmeno, string popis, List<UkolPolozka> polozky, IUkolOdmena odmena, bool splnen = false) : this(jmeno, popis, polozky, splnen)
        {
            this.Odmena = odmena;
        }

        /// <summary>
        /// vytvoří nový úkol
        /// </summary>
        /// <param name="jmeno">název úkolu</param>
        /// <param name="popis">text úkolu</param>
        /// <param name="splnen">zda byl splněn (použito jen pokud hra obsahuje seznam splněných úkolů)</param>
        /// <param name="polozka">co je třeba splnit k dokončení úkolu</param>
        /// <param name="odmena">co hráč dostane za splnění úkolu</param>
        public Ukol(string jmeno, string popis, UkolPolozka polozka, IUkolOdmena odmena, bool splnen = false) : this(jmeno, popis, polozka, splnen)
        {
            this.Odmena = odmena;
        }

        /// <summary>
        /// vypíše zadání a stav úkolu
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (UkolPolozka p in Polozky)
            {
                sb.Append(p);
                sb.Append("\n");
            }
            return $"{Jmeno}\n-------------\n{Popis}\n-----\n{sb}";
        }

        /// <summary>
        /// aktualizace stavu úkolu
        /// </summary>
        /// <param name="novy">objekt u kterého se zjišťuje, zda se jedná o položku</param>
        /// <returns>zda je položka splněna</returns>
        public bool UpdateStav(Object novy)
        {
            if (!Splnen)
            {
                bool ret = true;
                foreach (UkolPolozka p in Polozky)
                {
                    bool b = p.UpdateStav(novy);
                    ret = ret && b;
                }
                Splnen = ret;
                if (Splnen)
                {
                    Dokoncen?.Invoke(this, Odmena);
                }
                return ret;
            }
            return true;
        }
    }
}
