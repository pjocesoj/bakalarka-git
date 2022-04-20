using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// správce starající se vytváření postav a obsahující všechny globální hodnoty
    /// </summary>
    public abstract class GameManagerDLL
    {
        /// <summary>
        /// veškerá nastavení hry, jako je ovládání, zvuk, grafika, ...
        /// </summary>
        public Dictionary<string, INastaveni> Nastaveni { get; protected set; }

        /// <summary>
        /// seznam zkratek statů vyskytujících se ve hře
        /// </summary>
        public string[] ZkratkyStatu;

        /// <summary>
        /// kategorie statListů (např souboj, obchod,...)
        /// </summary>
        protected Dictionary<string, List<Stat>> staty = new Dictionary<string, List<Stat>>();

        /// <summary>
        /// seznam všech lokací vyskytujících se ve hře
        /// </summary>
        protected List<Lokace> lokace = new List<Lokace>();

        /// <summary>
        /// inicializace hry (nastavení, staty, lokace)
        /// </summary>
        protected GameManagerDLL()
        {
            VytvorNastaveni();

            VytvorSezanamZkratekStatu();
            VytvorStatListy();

            VytvorLokace();
        }

        /// <summary>
        /// vytvoří novou hru
        /// </summary>
        public virtual void SpustHru()
        {
            
        }

        #region nastaveni
        /// <summary>
        /// vytvoří slovník všech nastavení [nazev,INastaveni]
        /// </summary>
        protected abstract void VytvorNastaveni();
        #endregion
        #region staty
        #region init
        /// <summary>
        /// vytvoří seznam všech zkratek statů vyskytujících se ve hře
        /// </summary>
        /// <exception cref="NotImplementedException">virtualní metoda, která musí mít implementaci</exception>
        protected abstract void VytvorSezanamZkratekStatu();

        /// <summary>
        /// vytvoří StatListy a seskupí do skupin (boj, obchod, ...)
        /// </summary>
        protected abstract void VytvorStatListy();
        /*{
            throw new NotImplementedException("nejde vytvořit obecná a je proto potřeba přetížit a nasvit vlastní hodnoty");
        }*/
        #endregion
        #region GetStatListy pro LV1
        /// <summary>
        /// spoji všechny statlisty zvolenych skupin a vrátí je jako 1 seznam 
        /// <br/>(pro LV1)
        /// </summary>
        /// <param name="skupiny">názvy skupin, které chcete</param>
        public StatList GetStatListy(string[] skupiny)
        {
            List<Stat> ret = new List<Stat>();
            for (int i = 0; i < skupiny.Length; i++)
            {
                ret.AddRange(staty[skupiny[i]]);
            }
            return new StatList(ret);
        }

        /// <summary>
        /// vrátí zvolenou skupinu statů 
        /// <br/>(pro LV1)
        /// </summary>
        /// <param name="skupina">skupina, kterou chcete</param>
        public StatList GetstatList(string skupina)
        {
            return new StatList(staty[skupina]);
        }
        #endregion
        #region GetStatList pro konkretni LV
        /// <summary>
        /// vrátí zvolenou skupinu statů pro příslušný LV
        /// </summary>
        /// <param name="skupina">skupina, kterou chcete</param>
        /// <param name="lv">pro jaký lv má přepočítat hodnotu</param>
        public StatList GetStatList(string skupina, int lv)
        {
            List<Stat> ret = new List<Stat>();
            List<Stat> temp = staty[skupina];

            for (int i = 0; i < temp.Count; i++)
            {
                ret.Add(temp[i].clone());
                ret[i].Zaklad = vypocetStatZaklad(ret[i], lv);
            }
            return new StatList(ret);
        }

        /// <summary>
        /// spoji všechny zvolené skupiny statů do 1 StatListu pro příslušný LV
        /// </summary>
        /// <param name="skupiny">názvy skupin, které chcete</param>
        /// <param name="lv">pro jaký lv má přepočítat hodnotu</param>
        public StatList GetStatList(string[] skupiny, int lv)
        {
            List<StatList> temp = new List<StatList>();
            foreach (string skup in skupiny)
            {
                temp.Add(GetStatList(skup, lv));
            }

            return StatList.SlucStatListy(temp);
        }
        #endregion
        /// <summary>
        /// podle LV vypočítá základní hodnotu stat
        /// <br/>využívána metodou GetStatList()
        /// <br/>výchozí: zaklad(lv)=zaklad(1)*lv
        /// </summary>
        protected virtual double vypocetStatZaklad(Stat stat, int lv)
        {
            return stat.Zaklad * lv;
        }

        #endregion
        #region postavy
        /// <summary>
        /// vytvoří novou postavu na základě požadavků
        /// </summary>
        /// <param name="jmeno">jméno nové postavy</param>
        /// <param name="lv">level postavy</param>
        /// <param name="HP">počet životů postavy</param>
        /// <param name="skupinaStatu">jaké skupiny statů postava bude mít (combat, obchod, ...)</param>
        /// <returns>instance KnihovnaRPG.Postava</returns>      
        protected virtual Postava NovaPostava(string jmeno,int lv,int HP, string[] skupinaStatu)
        {
            StatList staty = GetStatList(skupinaStatu, lv);
            Postava ret = new Postava(jmeno, lv, HP, staty);
            return ret;
        }

        /// <summary>
        /// vytvoří novou nezranitelnou postavu na základě požadavků
        /// </summary>
        /// <param name="jmeno">jméno nové postavy</param>
        /// <param name="lv">level postavy</param>
        /// <param name="skupinaStatu">jaké skupiny statů postava bude mít (combat, obchod, ...)</param>
        /// <returns>instance KnihovnaRPG.Postava</returns> 
        protected virtual Postava NovaNezranitelnaPostava(string jmeno, int lv, string[] skupinaStatu)
        {
            StatList staty = GetStatList(skupinaStatu, lv);
            Postava ret = new Postava(jmeno, lv, staty);
            return ret;
        }

        /// <summary>
        /// vytvoří nového hráče na základě požadavků
        /// </summary>
        /// <param name="jmeno">jméno hráče</param>
        /// <param name="lv">level hráče</param>
        /// <param name="HP">počet životů hráče</param>
        /// <param name="skupinaStatu">jaké skupiny statů hráč bude mít (combat, obchod, ...)</param>
        /// <returns>instance KnihovnaRPG.Hrac</returns> 
        protected virtual Hrac NovyHrac(string jmeno, int lv,int HP, string[] skupinaStatu)
        {
            StatList staty = GetStatList(skupinaStatu, lv);
            Hrac ret = new Hrac(jmeno, lv, HP, staty);
            return ret;
        }
        #endregion

        /// <summary>
        /// vytvoří seznam všech lokací ve hře a jejich vazeb, které spolu mohou sousedit
        /// </summary>
        protected abstract void VytvorLokace();

    }
}
