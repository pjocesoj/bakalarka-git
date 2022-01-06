using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// logika pro postavy (rodičovská třída)
    /// </summary>
    public class Postava
    {
        #region eventy
        /// <summary>
        /// informace o hodnotě zranění pro výpis (zraneny, hodnota zranění)
        /// </summary>
        public event EventHandler<int> Zranen;

        /// <summary>
        /// informace o hodnotě uzdravení pro výpis
        /// </summary>
        public event EventHandler<int> Uzdraven;

        /// <summary>
        /// informace kdo koho zabil(utocnik, zabity)
        /// </summary>
        public event EventHandler<Postava> Zabil;

        /// <summary>
        /// pro zpracovani smrti implementací třídy()
        /// </summary>
        public event EventHandler<EventArgs> Smrt;
        #endregion

        #region properties
        /// <summary>
        /// jméno postavy
        /// </summary>
        public string Jmeno { get; private set; }

        /// <summary>
        /// úroveň postavy
        /// </summary>
        public int LV { get; private set; }

        /// <summary>
        /// aktuální stav životů postavy
        /// </summary>
        public int HP { get; private set; }

        /// <summary>
        /// maximální počet životů postavy
        /// </summary>
        public int MaxHP { get; private set; }

        /// <summary>
        /// seznam všech statů postavy
        /// </summary>
        public StatList Staty { get; private set; }
        #endregion

        /// <summary>
        /// zda je možno postavu zranit
        /// </summary>
        protected bool nezranitelny = false;


        #region konstruktory
        /// <summary>
        /// vytvoří nezranitelné NPC
        /// </summary>
        /// <param name="jmeno">jméno postavy</param>
        public Postava(string jmeno)
        {
            this.Jmeno = jmeno;
            nezranitelny = true;
        }

        ///<summary>vytvoří zranitelnou postavu</summary>
        /// <param name="jmeno">jméno postavy</param>
        /// <param name="lv">level postavy</param>
        /// <param name="HP">aktualní HP=maxHP</param>
        /// <exception cref="PostavaHPException">HP menší než 0</exception>
        public Postava(string jmeno, int lv, int HP)
        {
            this.Jmeno = jmeno;
            this.LV = lv;
            if (HP < 0)
            {
                throw new PostavaHPException("HP nesmí být menší než 0");
            }
            this.HP = HP;
            this.MaxHP = HP;
        }

        ///<summary>vytvoří zranitelnou postavu, která má staty</summary>
        /// <param name="jmeno">jméno postavy</param>
        /// <param name="lv">level postavy</param>
        /// <param name="HP">aktualní HP=maxHP</param>
        /// <param name="statList">staty postavy</param>
        /// <exception cref="PostavaHPException">HP menší než 0</exception>
        public Postava(string jmeno, int lv, int HP, StatList statList) : this(jmeno, lv, HP)
        {
            //this.StatList = statList.Clone();
            this.Staty = statList;
        }
        #endregion

        /// <summary>
        /// výpis informací o postavě
        /// </summary>
        public override string ToString()
        {
            return $"{Jmeno}\nLV{LV}\nHP:{HP}/{MaxHP}\n{Staty}";
        }



        /// <summary>
        /// odečte od zranění obranu a zavolá eventhandlery "Zranen","utocnik.Zabil" a "smrt"
        /// </summary>
        /// <param name="utocnik">postava, která způsobila zranění</param>
        /// <param name="DMG">hodnota poškození</param>
        /// <param name="obrana">typ obrany, kterým je možné poškození snížit</param>
        public virtual void Zraneni(Postava utocnik, double DMG, string obrana)
        {
            if (!nezranitelny && HP > 0)
            {
                double uber = DMG;
                uber-=obrana!=null? Staty[obrana].Hodnota:0;
                HP -= (int)uber;

                Zranen?.Invoke(this, HP);//? zkrácený zápis testu zda není null
                if (HP <= 0)
                {
                    utocnik.Zabil?.Invoke(utocnik, this);

                    Smrt?.Invoke(this, null);
                }
            }
        }

        #region buff
        /// <summary>
        /// aktuální buffy a debuffy působící na postavu
        /// </summary>
        protected List<Buff> buffy = new List<Buff>();

        /// <summary>
        /// aktualizuje zbyvajicí dobu trvání buffů a projeví jejich efekt
        /// </summary>
        public virtual void EfektyBuffu()
        {
            //foreach (Buff efekt in buffy)
            for(int i=buffy.Count-1;i>=0;i--)
            {
                Buff efekt = buffy[i];

                efekt.ZkratDobu();
                if (!efekt.Plati)
                {
                    //buffy.Remove(efekt);
                    odeberBuff(efekt);
                }
                else if (efekt.Druh == BuffType.Zraneni)
                {
                    Zraneni(efekt.Sesilatel, efekt.Efekt.Hodnota,efekt.Obrana);
                }
            }
        }

        /// <summary>
        /// přidá nový Buff a pokud je typ buff/debuff provede efekt
        /// </summary>
        public void PridejBuff(Buff buff)
        {
            buffy.Add(buff);
            if(buff.Druh==BuffType.Buff || buff.Druh==BuffType.Debuff)
            {
                buff.Pouzij(Staty[buff.Efekt.Zkratka], 1);
            }
        }
        private void odeberBuff(Buff buff)
        {
            buffy.Remove(buff);
            if (buff.Druh == BuffType.Buff || buff.Druh == BuffType.Debuff)
            {
                buff.Pouzij(Staty[buff.Efekt.Zkratka], -1);
            }
        }

        /// <summary>
        /// vypíše všechny působící buffy
        /// </summary>
        public string SeznamBuffu()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Buff b in buffy)
            {
                sb.Append(b);
                sb.Append("\n------------\n");
            }
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// přidá HP a zajistí, že nepřesáhnou maximum
        /// </summary>
        /// <param name="HP">kolik HP je přidáno</param>
        public void PridejHP(int HP)
        {
            this.HP += HP;
            if (this.HP > MaxHP) { this.HP = MaxHP; }
            Uzdraven?.Invoke(this, this.HP);
        }
    }
}
