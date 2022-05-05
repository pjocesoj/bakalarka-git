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
        /// uloží aktuální stav hry, který zůstane uložený v paměti, dokud se nezavolá metoda Uloz()
        /// </summary>
        /// <param name="jmeno">pod jakým názvem bude postup uložen</param>
        /// <param name="mapa">herní mapa</param>
        /// <param name="hraci">data všech hráčových postav</param>
        /// <param name="polohaHraci">poloha všech hráčových postav</param>
        /// <param name="NPC">data všech NPC</param>
        /// <param name="polohaNPC">poloha všech NPC</param>
        public Ulozeni(string jmeno,Mapa mapa, Hrac[] hraci, Point4D[] polohaHraci, Postava[] NPC, Point4D[] polohaNPC)
        {
            this.Nazev = jmeno;
            this.Mapa = mapa;
            this.Hraci = hraci;
            this.PolohaHracu = polohaHraci;
            this.Postavy = NPC;
            this.PolohaPostav = polohaNPC;
        }

        /// <summary>
        /// vytvoří prázndou instanci, která se naplní při zavolání Nacti()
        /// </summary>
        public Ulozeni() 
        { }

        /// <summary>
        /// uložení postupu na disk či server
        /// </summary>
        public abstract void Uloz();


        /// <summary>
        /// načtení uloženého postupu
        /// </summary>
        public abstract void Nacti(string saveStream);
    }
}
