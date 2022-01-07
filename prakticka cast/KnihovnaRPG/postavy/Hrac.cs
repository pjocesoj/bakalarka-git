using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// hráčovi postavy
    /// </summary>
    public class Hrac:Postava
    {
        /// <summary>
        /// nezranitelná postava, která má jen jméno
        /// </summary>
        /// <param name="jmeno"></param>
        public Hrac(string jmeno) : base(jmeno)
        {

        }

        ///<summary>vytvoří nového hráče</summary>
        /// <param name="jmeno">jméno postavy</param>
        /// <param name="lv">level postavy</param>
        /// <param name="HP">aktualní HP=maxHP</param>
        /// <param name="statList">staty postavy</param>
        /// <exception cref="PostavaHPException">HP menší než 0</exception>
        public Hrac(string jmeno, int lv, int HP, StatList statList) : base(jmeno, lv, HP,statList)
        {

        }

        /// <summary>
        /// počet zkušeností
        /// </summary>
        public int Exp { get; protected set; }

        /// <summary>
        /// kolik peněz hráč má
        /// </summary>
        public int Penize { get; set; }


        /// <summary>
        /// vypíše všechny informace o hráčově postavě
        /// </summary>
        public override string ToString()
        {
            return $"{Jmeno}\nLV{LV}\nExp{Exp}/{100*LV}\nHP:{HP}/{MaxHP}\n{Staty}\n{Penize}G";
        }

        /// <summary>
        /// přidá zkušenosti a po překročení hranice (100*LV) zvýší LV
        /// </summary>
        /// <param name="exp">počet získaných zkušeností</param>
        public virtual void PridejExp(int exp)
        {
            int LvUp = 100 * LV;

            Exp += exp;
            if(Exp>LvUp)
            {
                Exp -= LvUp;
                LV++;
            }
        }
    }
}
