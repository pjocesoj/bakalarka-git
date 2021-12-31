using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// předměty, které si může postava nasadit
    /// </summary>
    public class Vybaveni:Predmet
    {
        /// <summary>
        /// vytvoří nový kus vybavení
        /// </summary>
        /// <param name="jmeno">jméno</param>
        /// <param name="cena">základní cena vybavení (obchodník bez vlivu charisma)</param>
        /// <param name="hmotnost">hmotnost vybavení (pro inventář s kapacitou podle hmotnosti)</param>
        /// <param name="statList">seznam statů (DMG, DEF, ...)</param>
        /// <param name="stackovatelne">zda je možné umístit více kusů do 1 slotu v inventáři</param>
        public Vybaveni(string jmeno, int cena, double hmotnost, StatList statList,bool stackovatelne = false):base( jmeno,  cena,  hmotnost,  stackovatelne)
        {
            this.Staty = statList;
        }

        /// <summary>
        /// staty vybavení (DMG, DEF, ...)
        /// </summary>
        public StatList Staty
        {
            get;private set;
        }

        /// <summary>
        /// výpis informací o vybavení
        /// </summary>
        public override string ToString()
        {
            return $"{base.ToString()}\n{Staty.ToString()}";
        }

        /// <summary>
        /// porovnává shodnost objektů
        /// </summary>
        /// <param name="p">s čím chcete porovnat (Vybaveni nebo jeho potomek)</param>
        public override bool Stejne(Sebratelne p)
        {
            //pokud je jiného typu nemá smysl pokračovat
            Vybaveni v;
            try
            {
                v = (Vybaveni)p;
            }
            catch { return false; }

            if (!base.Stejne(p)) { return false; }
            if (this.Staty != v.Staty) { return false; }

            return true;
        }
    }
}
