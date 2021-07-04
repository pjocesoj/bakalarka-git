using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    public class StatNenalezenException : Exception
    {
        public StatNenalezenException() { }
        public StatNenalezenException(string msg) : base(msg) { }
    }

    public class StatListTentoPrvekUzExistujeException : Exception
    {
        public StatListTentoPrvekUzExistujeException():base("zkratka statu se nesmí opakovat") { }
        public StatListTentoPrvekUzExistujeException(string msg) : base(msg) { }
    }
}
