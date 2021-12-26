using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// chyba generování zkratky
    /// </summary>
    public class StatVstupMaViceNez2SlovaException : Exception
    {
        /// <summary>
        /// chybný formát zkratky
        /// </summary>
        public StatVstupMaViceNez2SlovaException():base("pro generování zkratky musí být jméno ve tvaru 'název' nebo 'přívlastek název' ") { }

        /// <summary>
        /// chybný formát zkratky
        /// </summary>
        /// <param name="msg">zobrazená zpráva</param>
        public StatVstupMaViceNez2SlovaException(string msg) : base(msg) { }
    }

    /// <summary>
    /// chyba generování zkratky
    /// </summary>
    public class StatZkratkaJeMocDlouhaException : Exception
    {
        /// <summary>
        /// chybný formát zkratky
        /// </summary>
        public StatZkratkaJeMocDlouhaException() : base("zkratka musí mít max 4 znaky") { }

        /// <summary>
        /// chybný formát zkratky
        /// </summary>
        /// <param name="msg">zobrazená zpráva</param>
        public StatZkratkaJeMocDlouhaException(string msg) : base(msg) { }
    }
}
