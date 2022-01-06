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
        public bool Splnen { get; private set; }
    }
}
