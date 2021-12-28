﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class PredmetKomp:Sebratelne
    {
        protected GFX grafika;
        protected Predmet predmet;

        public PredmetKomp() { }//protože úplný konstruktor potomka nemá co předat

        public PredmetKomp(GFX GFX, Predmet logika)
        {
            this.grafika = GFX;
            this.predmet = logika;
        }

        public PredmetKomp(string jmeno, int cena, double hmotnost, Bitmap obr, int sirka = 100, int vyska = 100, bool stackovatelne = true)
        {
            grafika = new GFX(sirka, vyska, obr);
            predmet = new Predmet(jmeno, cena, hmotnost,stackovatelne);
        }

        public GFX GFX
        {
            get { return grafika; }
        }
        public Predmet Predmet
        {
            get { return predmet; }
        }

        public override string ToString()
        {
            return predmet.ToString();
        }
    }
}
