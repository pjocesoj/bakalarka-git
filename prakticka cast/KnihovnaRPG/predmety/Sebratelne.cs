using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// umožňuje vložit objekt do Inventáře
    /// </summary>
    public interface Sebratelne
    {
        #region properties
        /// <summary>
        /// název předmětu
        /// </summary>
        string Jmeno { get; }

        /// <summary>
        /// základní cena vybavení (obchodník bez vlivu charisma)
        /// </summary>
        int Cena { get;  }

        /// <summary>
        /// hmotnost vybavení (pro inventář s kapacitou podle hmotnosti)
        /// </summary>
        double Hmotnost { get; }

        /// <summary>
        /// zda jde dávat k sobě nebo v inventu každý zabírá vlastní slot
        /// </summary>
        bool Stackovatelne{get; }
        #endregion
    }
}
