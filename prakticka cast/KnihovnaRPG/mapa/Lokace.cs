using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// flyweight třída reprezentující lokaci a nesoucí data pro generování
    /// </summary>
   public class Lokace
    {
        /// <summary>
        /// název lokace
        /// </summary>
        public string Nazev { get; private set; }

        /// <summary>
        /// jaké lokace se mohou vygenerovat jako sousední
        /// </summary>
        public List<Lokace> MuzeSousedit { get; private set; }
        
        /// <summary>
        /// jací nepřátelé se mohou v lokaci spawnout
        /// </summary>
        public List<String> MuzeSpawnout { get; private set; }
        
        /// <summary>
        /// vytvoří lokaci, která může sousedit jen sama se sebou a nenachází se zde nepřátelé
        /// </summary>
        /// <param name="nazev">název lokace</param>
        public Lokace(string nazev)
        {
            Nazev = nazev;

            MuzeSousedit = new List<Lokace>();
            MuzeSousedit.Add(this);
        }
    }
}
