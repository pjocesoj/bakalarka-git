using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// dočasné ovlivnění statu
    /// </summary>
    public class Buff
    {

        #region properties
        /// <summary>
        /// stat a hodnota
        /// </summary>
        public Stat Efekt { get; private set; }

        /// <summary>
        /// zbývající doba po jakou ovlivnění trvá (kola, vteřiny,...)
        /// </summary>
        public int Doba { get; private set; }

        /// <summary>
        /// o jaký druh ovlivnění se jedná
        /// </summary>
        public BuffType Druh { get; private set; }

        /// <summary>
        /// jakým způsobem ovlivní stat
        /// </summary>
        public BuffZpusobZmeny ZpusobZmeny { get; private set; }

        /// <summary>
        /// kdo seslal buff
        /// </summary>
        public Postava Sesilatel { get; private set; }

        /// <summary>
        /// čím se dá snížit toto průběžně způsobavané zranění
        /// <br/> efekt jen u BuffType.Zraneni
        /// </summary>
        public String Obrana { get; private set; }
        #endregion

        /// <summary>
        /// vytvoří buff/debuff
        /// </summary>
        /// <param name="efekt">ovlivněný stat</param>
        /// <param name="doba">doba trvání</param>
        /// <param name="druh">o jaký druh ovlivnění se jedná</param>
        /// <param name="zpusobZmeny">jakým způsobem ovlivní stat</param>
        public Buff(Stat efekt, int doba, BuffType druh, BuffZpusobZmeny zpusobZmeny)
        {
            this.Efekt = efekt;
            this.Doba = doba;
            this.Druh = druh;
            this.ZpusobZmeny = zpusobZmeny;
        }

        /// <summary>
        /// vytvoří buff/debuff
        /// </summary>
        /// <param name="efekt">ovlivněný stat</param>
        /// <param name="doba">doba trvání</param>
        /// <param name="druh">o jaký druh ovlivnění se jedná</param>
        /// <param name="sesilatel">kdo buff seslal</param>
        /// <param name="zpusobZmeny">jakým způsobem ovlivní stat</param>
        public Buff(Stat efekt, int doba, Postava sesilatel, BuffType druh = BuffType.Zraneni, BuffZpusobZmeny zpusobZmeny = BuffZpusobZmeny.Konstanta) : this(efekt, doba, druh, zpusobZmeny)
        {
            this.Sesilatel = sesilatel;
        }

        /// <summary>
        /// vytvoří průběžné zranění
        /// </summary>
        /// <param name="typ">typ zranění(jed, oheň,...)</param>
        /// <param name="dmg">hodnota zranění</param>
        /// <param name="doba">doba trvání</param>
        /// <param name="sesilatel">kdo buff seslal</param>
        /// <param name="obrana">čím se dá snížit toto zranění</param>
        public Buff(string typ,int dmg, int doba, Postava sesilatel, string obrana)
        {
            Efekt = new Stat(typ, dmg);
            this.Doba = doba;
            this.Sesilatel = sesilatel;
            this.Sesilatel = sesilatel;

            this.Druh = BuffType.Zraneni;
            this.ZpusobZmeny = BuffZpusobZmeny.Zaklad;
        }

        /// <summary>
        /// sníží zbývající dobu o 1
        /// </summary>
        public void ZkratDobu()
        {
            Doba--;
        }

        /// <summary>
        /// zda efekt ještě trvá nebo už skončil
        /// </summary>
        public bool Plati
        {
            get
            {
                return (Doba > 0);
            }
        }

        /// <summary>
        /// aplikování a rušení efektu buffu
        /// </summary>
        /// <param name="meneny">ovlivněný stat</param>
        /// <param name="sgn">1=aplikace, -1=rušení</param>
        public void Pouzij(Stat meneny, int sgn)
        {
            if (sgn != 1 && sgn != -1) { throw new FormatException("sgn musí být 1 nebo -1"); }

            switch (ZpusobZmeny)
            {
                case BuffZpusobZmeny.Procento:
                    proc(meneny, sgn);
                    break;
                case BuffZpusobZmeny.Konstanta:
                    meneny.BoostKonst += sgn * Efekt.BoostKonst;
                    break;
                case BuffZpusobZmeny.Zaklad:
                    meneny.Zaklad += sgn * Efekt.Zaklad;
                    break;
            }
        }
        private float origProc;
        private void proc(Stat meneny, int sgn)
        {
            if (sgn > 0)
            {
                origProc = meneny.BoostProc;
                if (meneny.BoostProc == 0)
                {
                    meneny.BoostProc = 100;
                }
                meneny.BoostProc *= Efekt.BoostProc;
            }
            else { meneny.BoostProc = origProc; }
        }

        /// <summary>
        /// vypíše info o buffu
        /// </summary>
        public override string ToString()
        {
            return $"{Druh}\n{Efekt.Nazev}: {efektToString()}\nzbyva: {Doba}";
        }
        private string efektToString()
        {
            switch (ZpusobZmeny)
            {
                case BuffZpusobZmeny.Procento:
                    return $"{Efekt.BoostProc*100}%";
                case BuffZpusobZmeny.Konstanta:
                    return Efekt.BoostKonst.ToString();
                case BuffZpusobZmeny.Zaklad:
                    return Efekt.Zaklad.ToString();
                default:return "error";
            }
        }
    }
    /// <summary>
    /// o jaký druh ovlivnění se jedná
    /// </summary>
    public enum BuffType
    {
        /// <summary>
        /// dočasně vylepší stat postavy
        /// </summary>
        Buff,

        /// <summary>
        /// dočasně sníží stat postavy
        /// </summary>
        Debuff,

        /// <summary>
        /// průběžně způsobuje zranění
        /// </summary>
        Zraneni,

        /// <summary>
        /// definován implementací
        /// </summary>
        Jiny
    };

    /// <summary>
    /// jakým způsobem ovlivňuje stat
    /// </summary>
    public enum BuffZpusobZmeny
    {
        /// <summary>
        /// změna hodnoty o procento
        /// </summary>
        Procento,

        /// <summary>
        /// změna hodnoty o číslo
        /// </summary>
        Konstanta,

        /// <summary>
        /// změna základu, ze kterého je počítána výsledná hodnota
        /// </summary>
        Zaklad
    }
}
