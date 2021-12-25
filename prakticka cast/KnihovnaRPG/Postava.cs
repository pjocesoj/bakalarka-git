using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    public class Postava
    {
        public event EventHandler<int> Zranen;
        public event EventHandler<Postava> Zabil;

        string jmeno;
        protected int lv;
        protected int zivoty;
        protected int maxHP;
        bool nezranitelny = false;
        StatList StatList;
        #region konstruktory
        /// <summary>
        /// vytvoří nezranitelné NPC
        /// </summary>
        /// <param name="jmeno">jméno postavy</param>
        public Postava(string jmeno)
        {
            this.jmeno = jmeno;
            nezranitelny = true;
        }

        /// <param name="jmeno">jméno postavy</param>
        /// <param name="lv">level postavy</param>
        /// <param name="HP">aktualní HP=maxHP</param>
        /// <exception cref="PostavaHPException">HP menší než 0</exception>
        public Postava(string jmeno, int lv, int HP)
        {
            this.jmeno = jmeno;
            this.lv = lv;
            if (HP < 0)
            {
                throw new PostavaHPException("HP nesmí být menší než 0");
            }
            this.zivoty = HP;
            this.maxHP = HP;
        }
                
        /// <param name="jmeno">jméno postavy</param>
        /// <param name="lv">level postavy</param>
        /// <param name="HP">aktualní HP=maxHP</param>
        /// <param name="statList">staty postavy</param>
        public Postava(string jmeno, int lv, int HP, StatList statList) : this(jmeno, lv, HP)
        {
            //this.StatList = statList.Clone();
            this.StatList = statList;
        }
        #endregion
        public override string ToString()
        {
            return $"{jmeno}\nLV{lv}\nHP:{zivoty}/{maxHP}\n{StatList}";
        }
        #region get set
        public string Jmeno
        {
            get { return jmeno; }
        }
        public int LV
        {
            get { return lv; }
        }
        public int HP
        {
            get { return zivoty; }
            set { zivoty = value; }
        }
        public int MaxHP
        {
            get { return maxHP; }
        }
        public StatList Staty
        {
            get { return StatList; }
        }
        #endregion


        /// <summary>
        /// odečte od zranění obranu a zavolá eventhandlery "Zranen","utocnik.Zabil" a metodu "smrt"
        /// </summary>
        /// <param name="utocnik">postava, která způsobila zranění</param>
        /// <param name="DMG">hodnota poškození</param>
        /// <param name="obrana">typ obrany, kterým je možné poškození snížit</param>
        public void zraneni(Postava utocnik,double DMG, string obrana)
        {
            if(!nezranitelny && zivoty>0)
            {
                double uber = DMG - Staty[obrana].Hodnota;
                HP -=(int) uber;

                Zranen?.Invoke(this,HP);//? testuje zda není null
                if(HP<=0)
                {
                    utocnik.Zabil?.Invoke(utocnik,this);

                    smrt();
                }
            }
        }
        /// <summary>
        /// tato metoda je zavolána při smrti (tato je abstraktní a je třeba ji přetížit
        /// </summary>
        /// <exception cref="NotImplementedException">nepřetížená abstract</exception>
        protected virtual void smrt()
        {
            throw new NotImplementedException("pouzita metoda z knihovnaRPG.Postava přetěžte v GFX");
        }
    }
}
