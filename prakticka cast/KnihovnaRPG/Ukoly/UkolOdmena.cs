using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// odměna, kterou hráč dostane za splnění za úkolu
    /// </summary>
    public class UkolOdmena : IUkolOdmena
    {
        /// <summary>
        /// zkušenosti za splnění
        /// </summary>
        public int Exp { get; private set; }

        /// <summary>
        /// peníze za splnění
        /// </summary>
        public int Penize { get; private set; }

        /// <summary>
        /// předmět za splnění
        /// </summary>
        public IPredmet Predmet { get; private set; }

        /// <summary>
        /// vytvoří nový seznam odměn za splnění úkolu
        /// </summary>
        /// <param name="exp">zkušenosti za splnění</param>
        /// <param name="penize">peníze za splnění</param>
        /// <param name="item">předmět za splnění</param>
        public UkolOdmena(int exp,int penize,IPredmet item)
        {
            Exp = exp;
            Penize = penize;
            Predmet = item;
        }

        /// <summary>
        /// vypíše odměny
        /// </summary>
        public override string ToString()
        {
            return $"{Exp}Exp, {Penize}G, {Predmet.Jmeno}";
        }
    }
}
