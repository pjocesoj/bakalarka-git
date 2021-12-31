using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// inventář s kapacitou určovanou počtem předmětů (stackovatelne stejného typu počítany jako 1)
    /// </summary>
    public class InventarPocet : InventarV2
    {
        /// <summary>
        /// maximální počet předmětů v inventáři (stackovatelne počítány jako 1)
        /// </summary>
        public int Kapacita { get; private set; }

        /// <summary>
        /// kolik předmětů se nachází v jadnotlivých stacích
        /// </summary>
        protected List<int> pocetVeStacku;

        /// <summary>
        /// kolik předmětů se již v inventáři nachází (stackovatelne počítány jako 1)
        /// </summary>
        public int Neseno { get; private set; }

        /// <summary>
        /// vytvoří inventář s kapacitou určenou hmotností
        /// </summary>
        /// <param name="kapacita">maximální počet předmětů v inventáři 
        ///                        <br/>(více stejných stackovatelných předmětů se počítá jako 1)</param>
        public InventarPocet(int kapacita)
        {
            Kapacita = kapacita;
            pocetVeStacku = new List<int>();
        }
        
        /// <summary>
        /// přidá předmět do inventáře
        /// </summary>
        /// <param name="item">přidávaný předmět</param>
        /// <returns>zda je možné předmět vložit</returns>
        public override bool Pridej(Sebratelne item)
        {
            if (item.Stackovatelne)
            {
                int i = indexOf(item);

                if (i != -1)
                {
                    pocetVeStacku[i]++;
                    return true;
                }
            }

            if (Neseno < Kapacita)
            {
                obsah.Add(item);
                pocetVeStacku.Add(1);
                Neseno++;
                return true;
            }


            return false;
        }

        /// <summary>
        /// odebere předmět z inventáře
        /// </summary>
        /// <param name="item">odebíraný předmět</param>
        public override void Odeber(Sebratelne item)
        {
            int i = indexOf(item);

            pocetVeStacku[i]--;
            if (pocetVeStacku[i] == 0)
            {
                Neseno--;
                obsah.RemoveAt(i);
                pocetVeStacku.RemoveAt(i);
            }
        }

        /// <summary>
        /// aktualní stav zaplnění inventáře
        /// </summary>
        /// <returns>aktualni pocet/kapacita</returns>
        public override string Stav()
        {
            return $"{obsah.Count}/{Kapacita}";
        }

        /// <summary>
        /// vypíše předměty v inventáři
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Stav());
            sb.Append("\n");
            for (int i = 0; i < obsah.Count; i++)
            {
                sb.Append($"---- {pocetVeStacku[i]}x ----\n{obsah[i]}\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// počet předmětů ve stacku na indexu
        /// </summary>
        /// <param name="i">index</param>
        public int Pocet(int i)
        {
           return pocetVeStacku[i];
        }

        /// <summary>
        /// počet předmětů ve stacku
        /// </summary>
        /// <param name="item">předmět kterého chci vědět počet</param>
        public int Pocet(Sebratelne item)
        {
            int i = indexOf(item);
            return pocetVeStacku[i];
        }
    }
}
