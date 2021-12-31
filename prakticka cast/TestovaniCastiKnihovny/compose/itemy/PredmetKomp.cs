using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class PredmetKomp : Sebratelne
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
            predmet = new Predmet(jmeno, cena, hmotnost, stackovatelne);
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

        public bool Stejne(Sebratelne s)
        {
            return predmet.Stejne((s as PredmetKomp).predmet);
        }

        #region properties-interface
        public double Hmotnost
        {
            get
            {
                return predmet.Hmotnost;
            }
        }
        public string Jmeno
        {
            get
            {
                return predmet.Jmeno;
            }
        }
        public int Cena
        {
            get
            {
                return predmet.Cena;
            }
        }
        public bool Stackovatelne
        {
            get
            {
                return predmet.Stackovatelne;
            }
        }
        #endregion
    }
}
