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
        public int HP { get; set; }

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
        public void Zraneni(Postava utocnik, double DMG, string obrana)
        {
            if (!nezranitelny && HP > 0)
            {
                double uber = DMG - Staty[obrana].Hodnota;
                HP -= (int)uber;

                Zranen?.Invoke(this, HP);//? zkrácený zápis testu zda není null
                if (HP <= 0)
                {
                    utocnik.Zabil?.Invoke(utocnik, this);

                    Smrt?.Invoke(this, null);
                }
            }
        }

    }
}
