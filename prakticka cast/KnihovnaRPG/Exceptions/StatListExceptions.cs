using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// stat nenalezen
    /// </summary>
    public class StatNenalezenException : Exception
    {
        /// <summary>
        /// stat nenalezen
        /// </summary>
        public StatNenalezenException() { }
       
        /// <summary>
        /// stat nenalezen
        /// </summary>
        /// <param name="msg">zobrazená zpráva</param>
        public StatNenalezenException(string msg) : base(msg) { }
    }

    /// <summary>
    /// seznam nemůže obsahovat 2 staty se stejným jménem
    /// </summary>
    public class StatListTentoPrvekUzExistujeException : Exception
    {
        /// <summary>
        /// seznam nemůže obsahovat 2 staty se stejným jménem
        /// </summary>
        public StatListTentoPrvekUzExistujeException():base("zkratka statu se nesmí opakovat") { }

        /// <summary>
        /// seznam nemůže obsahovat 2 staty se stejným jménem
        /// </summary>
        /// <param name="msg">zobrazená zpráva</param>
        public StatListTentoPrvekUzExistujeException(string msg) : base(msg) { }
    }
}
