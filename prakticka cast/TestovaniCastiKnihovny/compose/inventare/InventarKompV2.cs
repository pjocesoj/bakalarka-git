﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class InventarKompV2
    {
        protected UI grafika;
        protected Inventar invent;

        public InventarKompV2() { }//protože úplný konstruktor potomka nemá co předat

        public InventarKompV2(UI GFX, Inventar logika)
        {
            this.grafika = GFX;
            this.invent = logika;
        }

        public InventarKompV2(int left = 0, int top = 0, int sirka = 100, int vyska = 400)
        {
            grafika = new UIVypis(left, top, sirka, vyska);
            invent = new Inventar();
        }

        public InventarKompV2(double kapacita,int left = 0, int top = 0, int sirka = 100, int vyska = 400)
        {
            grafika = new UIVypis(left, top, sirka, vyska);
            invent = new InventarHmotnost(kapacita);
        }

        public UI GFX
        {
            get { return grafika; }
        }
        public Inventar Invent
        {
            get { return invent; }
        }

        public virtual bool Pridej(IPredmet item)
        {
            bool ret = invent.Pridej(item);
            UIVypis temp = (UIVypis)grafika;
            temp.Text = invent.ToString();
            return ret;
        }

        public virtual void Odeber(PredmetKomp item)
        {
            invent.Odeber(item);
            (grafika as UIVypis).Text = invent.ToString();
        }

        public override string ToString()
        {
            return invent.ToString();
        }
    }
}
