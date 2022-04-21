using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// uložení herního postupu
    /// </summary>
    public abstract class Ulozeni
    {
        /// <summary>
        /// pod jakým názvem bude postup uložen
        /// </summary>
        public string Nazev { get; set; }

        /// <summary>
        /// herní mapa
        /// </summary>
        public Mapa Mapa { get; set; }

        /// <summary>
        /// data všech hráčových postav
        /// </summary>
        public Hrac[] Hraci { get; set; }
        /// <summary>
        /// poloha všech hráčových postav
        /// </summary>
        public Point4D[] PolohaHracu { get; set; }
        /// <summary>
        /// data všech NPC
        /// </summary>
        public Postava[] Postavy { get; set; }
        /// <summary>
        /// poloha všech NPC
        /// </summary>
        public Point4D[] PolohaPostav { get; set; }

        /// <summary>
        /// uložení postupu na disk či server
        /// </summary>
        public abstract void Uloz();

        /// <summary>
        /// načtení uloženého postupu
        /// </summary>
        public abstract void Nacti();
    }
}
