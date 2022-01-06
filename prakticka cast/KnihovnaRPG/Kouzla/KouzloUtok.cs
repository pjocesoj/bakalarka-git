using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// kouzlo, které způsobí zranění
    /// </summary>
    public class KouzloUtok:Kouzlo
    {
        /// <summary>
        /// typ poškození, který způsobí
        /// </summary>
        public string Typ { get; private set; }

        /// <summary>
        /// čím jde poškození snížit
        /// </summary>
        public string Obrana { get; private set; }

        /// <summary>
        /// hodnota poškození, které způsobí
        /// </summary>
        public double DMG { get; private set; }

        /// <summary>
        /// vytvoří nové útočné kouzlo
        /// </summary>
        /// <param name="jmeno">název schopnosti</param>
        /// <param name="cooldown">jak rychle po sobě jde použít</param>
        /// <param name="mana">množství many potřebné k použití schopnosti</param>
        /// <param name="typ">typ útoku, který způsobí</param>
        /// <param name="dmg">hodnota poškození, které způsobí</param>
        /// <param name="obrana">čím jde poškození snížit</param>
        public KouzloUtok(string jmeno,int cooldown,int mana,string typ,double dmg,string obrana):base(jmeno,cooldown,mana)
        {
            this.Typ = typ;
            this.DMG = dmg;
            this.Obrana = obrana;
        }

        /// <summary>
        /// vypíše info o kouzlu
        /// </summary>
        public override string ToString()
        {
            return $"{base.ToString()}\n{DMG}{Typ}";
        }

        /// <summary>
        /// použije kouzlo na cíl
        /// </summary>
        /// <param name="cil">na jakou postavu je kouzlo použito</param>
        /// <param name="sesilatel">kdo kouzlo seslal</param>
        /// <returns>zda se podařilo seslat</returns>
        public override bool Pouzij(Postava cil,Postava sesilatel)
        {
            bool ret = base.Pouzij(cil,sesilatel);
            if(ret )
            {
                cil.Zraneni(sesilatel, DMG, Obrana);
            }
            return ret;
        }
    }
}
