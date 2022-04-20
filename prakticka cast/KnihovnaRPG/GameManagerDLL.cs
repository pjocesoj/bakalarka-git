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
        #region nastaveni
        /// <summary>
        /// veškerá nastavení hry, jako je ovládání, zvuk, grafika, ...
        /// </summary>
        public Dictionary<string, INastaveni> Nastaveni { get { return nastaveni; } }     
        //v implementaci není třeba řešit inicializaci
        private Dictionary<string, INastaveni> nastaveni = new Dictionary<string, INastaveni>();
        #endregion

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
        /// herní mapa (logická část)
        /// </summary>
        public Mapa Mapa { get; protected set; }
        
        /// <summary>
        /// kde se na mapě nachází všichni hráči
        /// <br/> pro více než 1 hráčskou postavu
        /// </summary>
        public Point4D[] PolohaHracu { get; set; }

        /// <summary>
        /// kde se na mapě nachází hráč
        /// <br/> pro 1 hráčskou postavu
        /// </summary>
        public Point4D PolohaHrac 
        {
            get { return PolohaHracu[0]; }
            set { PolohaHracu[0] = value; } 
        }

        /// <summary>
        /// inicializace hry (nastavení, staty, lokace)
        /// </summary>
        protected GameManagerDLL()
        {
            VytvorNastaveni();

            VytvorSezanamZkratekStatu();
            VytvorStatListy();

            VytvorLokace();
            MapaConf();
        }

        /// <summary>
        /// vygeneruje mapu podle MapaConfig
        /// </summary>
        public virtual void SpustHru()
        {
            MapaConfig conf = (MapaConfig)nastaveni["mapa"];
            Mapa=Mapa.Vygeneruj(conf);
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
        protected virtual Postava NovaPostava(string jmeno, int lv, int HP, string[] skupinaStatu)
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
        protected virtual Hrac NovyHrac(string jmeno, int lv, int HP, string[] skupinaStatu)
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

        #region mapaConf
        /// <summary>
        /// metoda volaná konstruktorem, ve které je třeba zavolat mapaConf s požadoanými hodnotami
        /// </summary>
        protected abstract void MapaConf();

        /// <summary>
        /// vytvoří instanci MapaConfig a přidá ji do nastavení
        /// <br/>spawn=random
        /// </summary>
        /// <param name="MX">X rozměr mapy</param>
        /// <param name="MY">Y rozměr mapy</param>
        /// <param name="CX">X rozměr chunků</param>
        /// <param name="CY">Y rozměr chunků</param>
        /// <param name="renderVzdalenost">jak velké okolí chunku bude načítáno do paměti</param>
        /// <param name="lokace">v jaké lokaci se hráč spawne(les, město,...)</param>
        protected void mapaConf(int MX, int MY, int CX, int CY,int renderVzdalenost, Lokace lokace)
        {
            nastaveni.Add("mapa",new MapaConfig(MX, MY, CX, CY, renderVzdalenost,lokace));
        }

        /// <summary>
        /// vytvoří instanci MapaConfig a přidá ji do nastavení
        /// <br/>spawn=[smx,smy,scx,scy]
        /// </summary>
        /// <param name="MX">X rozměr mapy</param>
        /// <param name="MY">Y rozměr mapy</param>
        /// <param name="CX">X rozměr chunků</param>
        /// <param name="CY">Y rozměr chunků</param>
        /// <param name="renderVzdalenost">jak velké okolí chunku bude načítáno do paměti</param>
        /// <param name="lokace">v jaké lokaci se hráč spawne(les, město,...)</param>
        /// <param name="smx">spawn mapa X</param>
        /// <param name="smy">spawn mapa Y</param>
        /// <param name="scx">spawn chunk X</param>
        /// <param name="scy">spawn chunk Y</param>
        protected void mapaConf(int MX, int MY, int CX, int CY,int renderVzdalenost, Lokace lokace, int smx, int smy, int scx, int scy)
        {
            nastaveni.Add("mapa",new MapaConfig(MX, MY, CX, CY, renderVzdalenost,lokace,smx,smy,scx,scy));
        }
        #endregion
    }
}
