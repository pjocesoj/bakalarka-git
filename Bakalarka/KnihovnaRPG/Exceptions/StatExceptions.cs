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
        public StatVstupMaViceNez2SlovaException():base("pro generování zkratky musí být jméno ve tvaru 'název' nebo 'přívlastek název' ") { }
        public StatVstupMaViceNez2SlovaException(string msg) : base(msg) { }
    }

    public class StatZkratkaJeMocDlouhaException : Exception
    {
        public StatZkratkaJeMocDlouhaException() : base("zkratka musí mít max 4 znaky") { }
        public StatZkratkaJeMocDlouhaException(string msg) : base(msg) { }
    }
}
