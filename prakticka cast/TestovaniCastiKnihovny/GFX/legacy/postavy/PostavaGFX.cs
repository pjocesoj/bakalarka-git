using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestovaniCastiKnihovny
{    
    class PostavaGFX : KnihovnaRPG.Postava
    {
        Bitmap obr;
        public PostavaGFX(string jmeno, Bitmap obr) : base(jmeno)
        {
            this.obr = obr;
        }
        public PostavaGFX(string jmeno, int lv, int HP, KnihovnaRPG.StatList statList, Bitmap obr) : base(jmeno,lv,HP,statList)
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

        /*protected override void smrt()
        {
            obr = new Bitmap(obr.Width, obr.Height);
           
        }*/
    }
}
