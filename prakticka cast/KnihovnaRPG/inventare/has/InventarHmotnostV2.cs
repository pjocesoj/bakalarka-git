﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// inventář s kapacitou určovanou hmotností
    /// </summary>
    public class InventarHmotnostV2:InventarV2
    {
        /// <summary>
        /// maximální celková hmotnost předmětů v inventáři
        /// </summary>
        public double Kapacita { get; private set; }

        /// <summary>
        /// jaká hmotnost se již v inventáři nachází
        /// </summary>
        public double Neseno { get; private set; }

        /// <summary>
        /// vytvoří inventář s kapacitou určenou hmotností
        /// </summary>
        /// <param name="kapacita">maximální celková hmotnost předmětů v inventáři</param>
        public InventarHmotnostV2(double kapacita)
        {
            Kapacita = kapacita;
        }

        /// <summary>
        /// přidá předmět do inventáře
        /// </summary>
        /// <param name="item">přidávaný předmět</param>
        /// <returns>zda je možné předmět vložit</returns>
        public override bool Pridej(Sebratelne item)
        {
            if (Neseno + item.Hmotnost <= Kapacita)
            {
                obsah.Add(item);
                Neseno += item.Hmotnost;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}