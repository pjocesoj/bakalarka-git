using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnihovnaRPG;
using System.Drawing;

namespace TestovaniCastiKnihovny
{
    public class PostavaGFXv3:Postava
    {
        GFX grafika;
        public PostavaGFXv3(int sirka, int vyska):base("")
        {
            grafika = new GFX(sirka, vyska);
        }
        public PostavaGFXv3(string jmeno, int lv, int HP, StatList statList, Bitmap obr, int sirka = 100, int vyska = 100) : base(jmeno, lv, HP, statList)
        {
            grafika = new GFX(sirka, vyska);
            grafika.grafika.Image = obr;
        }
        public GFX GFX
        {
            get { return grafika; }
        }
        /*protected override void smrt()
        {
            grafika.grafika.Image=new Bitmap(grafika.Width, grafika.Height);
        }*/
    }
}
