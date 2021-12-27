using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class PostavaKomp
    {
        //podle rady knihy "Game Engine Architecture" změněno na celek tvořený komponentami
        protected GFX grafika;
        protected Postava postava;
       
        public PostavaKomp(){}//protože úplný HracKomp nemá co předat
        public PostavaKomp(GFX GFX, Postava logika)
        {
            this.grafika = GFX;
            this.postava = logika;

            postava.Smrt += smrt;
        }

        public PostavaKomp(Bitmap obr, string jmeno)
        {
            this.grafika = new GFX(100,100,obr);
            this.postava = new Postava(jmeno);
        }
        public PostavaKomp(string jmeno, int lv, int HP, StatList statList, Bitmap obr, int sirka = 100, int vyska = 100)
        {
            grafika = new GFX(sirka, vyska, obr);
            postava = new Postava(jmeno, lv, HP, statList);

            postava.Smrt += smrt;
        }

        public GFX GFX
        {
            get { return grafika; }
        }
        public Postava Postava
        {
            get { return postava; }
        }

        public override string ToString()
        {
            return postava.ToString();
        }
        
        protected void smrt(Object sender,EventArgs e)
        {
            grafika.grafika.Image = new Bitmap(grafika.Width, grafika.Height);
        }
    }
}
