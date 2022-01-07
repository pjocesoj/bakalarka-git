﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class UkolKomp
    {
        public Ukol Ukol { get; private set; }

        public UIVypis UI { get; private set; }

        public UkolKomp(string jmeno, string popis,int kolik, string cil,KnihovnaRPG.UkolOdmena odmena,int sirka=100,int vyska=400)
        {
            UkolPolozka p = new UkolPolozka(kolik, cil, UkolTyp.Zabit);

            Ukol = new Ukol(jmeno, popis,p,odmena);
            UI = new UIVypis(sirka, vyska);

            UI.Text = Ukol.ToString();
        }
        public UkolKomp(string jmeno, string popis, int[] kolik, string[] cil, KnihovnaRPG.UkolOdmena odmena, int sirka = 100, int vyska = 400)
        {
            List<UkolPolozka> polozky = new List<UkolPolozka>();
            for(int i=0;i<kolik.Length;i++)
            {
                polozky.Add(new UkolPolozka(kolik[i], cil[i], UkolTyp.Zabit));
            }

            Ukol = new Ukol(jmeno, popis, polozky,odmena);
            UI = new UIVypis(sirka, vyska);

            UI.Text = Ukol.ToString();
        }

        public void PridejZabiti(string cil, int pocet)
        {
            Ukol.Polozky.Add(new UkolPolozka(pocet, cil, UkolTyp.Zabit));
        }
        public void PridejSebrani(string cil, int pocet)
        {
            Ukol.Polozky.Add(new UkolPolozka(pocet, cil, UkolTyp.Sebrat));
        }
    }
}