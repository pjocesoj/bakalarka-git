using KnihovnaRPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestovaniCastiKnihovny
{
    class InventarKomp
    {
        protected UI grafika;
        protected Inventar invent;

        public InventarKomp() { }//protože úplný konstruktor potomka nemá co předat

        public InventarKomp(UI GFX, Inventar logika)
        {
            this.grafika = GFX;
            this.invent = logika;
        }

        public InventarKomp(int left = 0,int top=0, int sirka = 100,int vyska=400)
        {
            grafika = new UIVypis(left, top, sirka, vyska);
            invent = new Inventar();
        }

        public UI GFX
        {
            get { return grafika; }
        }
        public Inventar Invent
        {
            get { return invent; }
        }

        public virtual bool Pridej(PredmetKomp item)
        {
            bool ret= invent.Pridej(item.Predmet);
            UIVypis temp = (UIVypis)grafika;
            temp.Text = invent.ToString();
            return ret;
        }

        public virtual void Odeber(PredmetKomp item)
        {
            invent.Odeber(item.Predmet);
            (grafika as UIVypis).Text = invent.ToString();
        }

        public override string ToString()
        {
            return invent.ToString();
        }
    }
}
