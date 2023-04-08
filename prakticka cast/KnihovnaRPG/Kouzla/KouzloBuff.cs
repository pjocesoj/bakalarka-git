using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// vytvoří nové kouzlo s přetrvávajícím efektem
    /// </summary>
    public class KouzloBuff:Kouzlo
    {
        /// <summary>
        /// efekt působící delší dobu
        /// </summary>
        public Buff Efekt { get; private set; }

        /// <summary>
        /// vytvoří nové léčivé kouzlo
        /// </summary>
        /// <param name="jmeno">název schopnosti</param>
        /// <param name="cooldown">jak rychle po sobě jde použít</param>
        /// <param name="mana">množství many potřebné k použití schopnosti</param>
        /// <param name="efekt">jaký buff kouzlo přidá</param>
        public KouzloBuff(string jmeno, int cooldown, int mana, Buff efekt) : base(jmeno, cooldown, mana)
        {
            this.Efekt = efekt;
        }

        /// <summary>
        /// vypíše info o kouzlu
        /// </summary>
        public override string ToString()
        {
            return $"{base.ToString()}\n{Efekt}";
        }

        /// <summary>
        /// použije kouzlo na cíl
        /// </summary>
        /// <param name="cil">na jakou postavu je kouzlo použito</param>
        /// <param name="sesilatel">kdo kouzlo seslal</param>
        /// <returns>zda se podařilo seslat</returns>
        public override bool Pouzij(Postava cil, Postava sesilatel)
        {
            bool ret = base.Pouzij(cil,sesilatel);
            if (ret)
            {
                cil.PridejBuff(Efekt);
            }
            return ret;
        }
    }
}
