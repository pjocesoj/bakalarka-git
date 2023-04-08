using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// rodičovská třída pro vše co lze vložit do inventáře
    /// </summary>
    public class Predmet:IPredmet
    {
        #region properties
        /// <summary>
        /// název předmětu
        /// </summary>
        public string Jmeno { get; private set; }

        /// <summary>
        /// základní cena vybavení (obchodník bez vlivu charisma)
        /// </summary>
        public int Cena { get; private set; }

        /// <summary>
        /// hmotnost vybavení (pro inventář s kapacitou podle hmotnosti)
        /// </summary>
        public double Hmotnost { get; private set; }

        /// <summary>
        /// zda jde dávat k sobě nebo v inventu každý zabírá vlastní slot
        /// </summary>
        public bool Stackovatelne
        {
            get;private set;
        }
        #endregion

        /// <summary>
        /// vytvoří nový předmět
        /// </summary>
        /// <param name="jmeno">jméno</param>
        /// <param name="cena">základní cena vybavení (obchodník bez vlivu charisma)</param>
        /// <param name="hmotnost">hmotnost vybavení (pro inventář s kapacitou podle hmotnosti)</param>
        /// <param name="stackovatelne">zda je možné umístit více kusů do 1 slotu v inventáři</param>
        public Predmet(string jmeno, int cena, double hmotnost, bool stackovatelne = true)
        {
            this.Jmeno = jmeno;
            this.Cena = cena;
            this.Hmotnost = hmotnost;
            this.Stackovatelne = stackovatelne;
        }


        /// <summary>
        /// výpis informací o předmětu
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Jmeno}\ncena:{Cena}\nhmotnost:{Hmotnost}";
        }

        /// <summary>
        /// porovnává shodnost objektů
        /// </summary>
        /// <param name="p">s čím chcete porovnat</param>
        public virtual bool Stejne(IPredmet p)
        {
            if (this.Jmeno != p.Jmeno){ return false; }
            if (this.Hmotnost != p.Hmotnost){ return false; }
            if (this.Cena != p.Cena) { return false; }
            if (this.Stackovatelne != p.Stackovatelne) { return false; }

            return true;
        }
        //interface nepovoluje operator a override
    }
}
