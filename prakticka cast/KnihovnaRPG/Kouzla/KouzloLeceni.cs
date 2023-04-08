using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// kouzlo, které přidá životy
    /// </summary>
    public class KouzloLeceni:Kouzlo
    {
        /// <summary>
        /// kolik HP přidá
        /// </summary>
        public int HP { get; private set; }

        /// <summary>
        /// vytvoří nové léčivé kouzlo
        /// </summary>
        /// <param name="jmeno">název schopnosti</param>
        /// <param name="cooldown">jak rychle po sobě jde použít</param>
        /// <param name="mana">množství many potřebné k použití schopnosti</param>
        /// <param name="HP">kolik HP přidá</param>
        public KouzloLeceni(string jmeno, int cooldown, int mana, int HP) : base(jmeno, cooldown, mana)
        {
            this.HP = HP;
        }

        /// <summary>
        /// vypíše info o kouzlu
        /// </summary>
        public override string ToString()
        {
            return $"{base.ToString()}\n+{HP}HP";
        }

        /// <summary>
        /// použije kouzlo na cíl
        /// </summary>
        /// <param name="cil">na jakou postavu je kouzlo použito</param>
        /// <param name="sesilatel">kdo kouzlo seslal</param>
        /// <returns>zda se podařilo seslat</returns>
        public override bool Pouzij(Postava cil, Postava sesilatel)
        {
            bool ret = base.Pouzij(cil, sesilatel);
            if (ret)
            {
                cil.PridejHP(HP);
            }
            return ret;
        }
    }
}
