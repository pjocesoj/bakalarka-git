using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class KouzloKomp
    {
        public GFX GFX { get; private set; }
        public Kouzlo Kouzlo { get; private set; }

        private KouzloKomp(Bitmap obr, int sirka = 100, int vyska = 100)
        {
            this.GFX = new GFX(sirka, vyska, obr);
            this.GFX.grafika.Click += Grafika_Click;
        }
        public KouzloKomp(string jmeno, int cooldown, int mana, string typ, double dmg, string obrana,Bitmap obr, int sirka = 100, int vyska = 100) : this(obr, sirka, vyska)
        {
            this.Kouzlo = new KouzloUtok(jmeno, cooldown, mana, typ, dmg, obrana);
        }
        public KouzloKomp(string jmeno, int cooldown, int mana, int HP, Bitmap obr, int sirka = 100, int vyska = 100) : this(obr, sirka, vyska)
        {
            this.Kouzlo = new KouzloLeceni(jmeno, cooldown, mana, HP);
        }
        public KouzloKomp(string jmeno, int cooldown, int mana, Buff efekt, Bitmap obr, int sirka = 100, int vyska = 100) : this(obr, sirka, vyska)
        {
            this.Kouzlo = new KouzloBuff(jmeno, cooldown, mana, efekt);
        }

        //simulace volby cíle
        public PostavaKomp Seslal { get; set; }
        public PostavaKomp Cil { get; set; }
        private void Grafika_Click(object sender, EventArgs e)
        {
            if(!Kouzlo.Pouzij(Cil.Postava,Seslal.Postava))
            {
                System.Windows.Forms.MessageBox.Show($"ještě {Kouzlo.ZbyvaDoNabiti} kol");
            }
        }
    }
}
