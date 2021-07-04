using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestovaniCastiKnihovny
{
    public class HracGFX : KnihovnaRPG.Hrac
    {
        Bitmap obr;
        public HracGFX(string jmeno, Bitmap obr) : base(jmeno)
        {
            this.obr = obr;
        }
        public HracGFX(string jmeno, int lv, int HP, KnihovnaRPG.StatList statList, Bitmap obr) : base(jmeno, lv, HP, statList)
        {
            this.obr = obr;
        }
        public Bitmap vzhled
        {
            get
            {
                return obr;
            }
        }
    }
}
