using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{

    /// <summary>
    /// jednotlivé položky v úkolu (zabití, předměty,...)
    /// </summary>
    public class UkolPolozka
    {
        /// <summary>
        /// co hráč musí splnit (jméno nepřitele/předmětu, lokace,...)
        /// </summary>
        public object Polozka { get; private set; }
        /// <summary>
        /// o jaký typ položky se jedná (zabít nepřítele / sebrat předmět / dojít na místo)
        /// </summary>
        public UkolTyp Typ { get; private set; }

        /// <summary>
        /// kolik je potřeba k splnění úkolu
        /// </summary>
        public int Pocet { get; private set; }

        /// <summary>
        /// kolik už hráč splnil
        /// </summary>
        public int Hotovo { get; set; }

        /// <summary>
        /// vytvoří novou položku úkolu
        /// </summary>
        /// <param name="pocet">kolik je potřeba k splnění úkolu</param>
        /// <param name="polozka">co hráč musí splnit (jméno nepřitele/předmětu, lokace,...)</param>
        /// <param name="typ">o jaký typ položky se jedná</param>
        public UkolPolozka(int pocet, object polozka, UkolTyp typ)
        {
            this.Pocet = pocet;
            this.Hotovo = 0;

            this.Polozka = polozka;

            this.Typ = typ;
        }

        /// <summary>
        /// vypíše stav položky
        /// </summary>
        public override string ToString()
        {
            return $"{Typ} {Polozka}: {Hotovo}/{Pocet}";
        }

        bool dokoncena = false;


        /// <summary>
        /// aktualizace stavu položky úkolu
        /// </summary>
        /// <param name="novy">objekt u kterého se zjišťuje, zda se jedná o položku</param>
        /// <returns>zda je položka splněna</returns>
        public virtual bool UpdateStav(object novy)
        {
            if (!dokoncena)//aby Hotovo neslo pres Pocet
            {
                if (novy.ToString() == Polozka.ToString())
                {
                    Hotovo++;
                }
                if (Hotovo >= Pocet)
                {
                    dokoncena = true;
                    return true;
                }
                return true;
            }
            return true;
        }
    }



    /// <summary>
    /// o jaký typ úkolu se jedná
    /// </summary>
    public enum UkolTyp
    {
        /// <summary>
        /// zabít nepřítele
        /// </summary>
        Zabit,

        /// <summary>
        /// sebrat předmět
        /// </summary>
        Sebrat,

        /// <summary>
        /// dojít na konkrétní místo
        /// </summary>
        Dojit,

        /// <summary>
        /// promluvit s postavou
        /// </summary>
        Promluv,

        /// <summary>
        /// definován implementací
        /// </summary>
        Jiny
    } 
}
