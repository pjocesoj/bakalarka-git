using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// spotřební předměty (lektvary)
    /// </summary>
    public class Konzumovatelne : Predmet
    {
        /// <summary>
        /// vytvoří nový spotřební předmět
        /// </summary>
        /// <param name="jmeno">jméno</param>
        /// <param name="cena">základní cena vybavení (obchodník bez vlivu charisma)</param>
        /// <param name="hmotnost">hmotnost vybavení (pro inventář s kapacitou podle hmotnosti)</param>
        /// <param name="boosty">co zlepšuje (DMG) nebo doplňuje (HP)</param>
        /// <param name="stackovatelne">zda je možné umístit více kusů do 1 slotu v inventáři</param>
        public Konzumovatelne(string jmeno, int cena, double hmotnost, StatList boosty, bool stackovatelne = true) : base(jmeno, cena, hmotnost, stackovatelne)
        {
            this.Boosty = boosty;
        }

        /// <summary>
        /// staty které zlepšuje (DMG) nebo doplňuje (HP)
        /// </summary>
        public StatList Boosty
        {
            get; private set;
        }

        /// <summary>
        /// výpis informací o vybavení
        /// </summary>
        public override string ToString()
        {
            return $"{base.ToString()}\n{Boosty.ToString()}";
        }

        /// <summary>
        /// porovnává shodnost objektů
        /// </summary>
        /// <param name="p">s čím chcete porovnat (Konzumovatelne nebo potomek)</param>
        public override bool Stejne(Sebratelne p)
        {
            Konzumovatelne k;
            try
            {
                k = (Konzumovatelne)p;
            }
            catch { return false; }

            if (!base.Stejne(p)) { return false; }
            if (this.Boosty != k.Boosty) { return false; }

            return true;
        }
    }
}
