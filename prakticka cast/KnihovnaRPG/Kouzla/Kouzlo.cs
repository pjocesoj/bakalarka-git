using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// rodičovská třídá pro kouzla
    /// </summary>
    public abstract class Kouzlo
    {
        /// <summary>
        /// název kouzla
        /// </summary>
        public string Nazev { get; private set; }

        /// <summary>
        /// jak rychle po sobě jde použít
        /// </summary>
        public int Nabijeni { get; private set; }

        /// <summary>
        /// kolik akztuálně zbývá než se nabije
        /// </summary>
        public int ZbyvaDoNabiti { get; private set; }

        /// <summary>
        /// množství many potřebné k použití kouzla
        /// </summary>
        public double Mana { get; private set; }

        /// <summary>
        /// nastaví jméno, cooldown a manu z konstruktoru potomka
        /// </summary>
        /// <param name="jmeno">název schopnosti</param>
        /// <param name="cooldown">jak rychle po sobě jde použít</param>
        /// <param name="mana">množství many potřebné k použití schopnosti</param>
        public Kouzlo(string jmeno,int cooldown,int mana)
        {
            this.Nazev = jmeno;
            this.Nabijeni = cooldown;
            this.Mana = mana;

            ZbyvaDoNabiti = 0;
        }

        /// <summary>
        /// použije kouzlo na cíl
        /// </summary>
        /// <param name="cil">na jakou postavu je kouzlo použito</param>
        /// <param name="sesilatel">kdo kouzlo seslal</param>
        /// <returns>zda se podařilo seslat</returns>
        public virtual bool Pouzij(Postava cil,Postava sesilatel)
        {
            if (ZbyvaDoNabiti == 0)
            {
                ZbyvaDoNabiti = Nabijeni;
                return true;
            }
            return false;
        }

        /// <summary>
        /// sníží zbývající dobu do nabití o 1
        /// </summary>
        public void DalsiKolo()
        {
            if (ZbyvaDoNabiti > 0) { ZbyvaDoNabiti--; }
        }

        /// <summary>
        /// vypíše info o kouzlu
        /// </summary>
        public override string ToString()
        {
            return $"{Nazev}\n{Mana}MP\nnabijeni: {Nabijeni}";
        }
    }
}
