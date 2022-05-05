using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class GameManager : GameManagerDLL
    {
        private static GameManager singleton;
        public static GameManager Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new GameManager();
                }
                return singleton;
            }
        }
        private GameManager() : base() { }

        protected override void VytvorNastaveni()
        {
            Nastaveni.Add("ovladani", new NastaveniOvladani());
            Nastaveni.Add("grafika", new NastaveniGrafika(70, 70));
        }
        #region staty
        protected override void VytvorSezanamZkratekStatu()
        {
            ZkratkyStatu = new string[] { "DMG", "DEF" };
        }
        protected override void VytvorStatListy()
        {
            List<Stat> combat = new List<Stat>();
            combat.Add(new Stat("utok", "DMG", 10));
            combat.Add(new Stat("obrana", "DEF", 5));

            List<Stat> magie = new List<Stat>();
            magie.Add(new Stat("mana", "MP", 10));
            magie.Add(new Stat("inteligence", "INT", 20));

            staty.Add("combat", combat);
            staty.Add("magie", magie);
        }
        #endregion
        #region postavy
        public PostavaKomp NovaPostavaGFX(string jmeno, int lv, int HP, string[] skupinaStatu)
        {
            Postava logika = NovaPostava(jmeno, lv, HP, skupinaStatu);
            Bitmap obr = (Bitmap)Image.FromFile("obrazky//stickman.png");
            GFX gfx = new GFX(100, 100, obr);
            return new PostavaKomp(gfx, logika);
        }
        public HracKomp NovyHracGFX(string jmeno, int lv, int HP, string[] skupinaStatu)
        {
            Hrac logika = NovyHrac(jmeno, lv, HP, skupinaStatu);
            Bitmap obr = (Bitmap)Image.FromFile("obrazky//stickman.png");
            GFX gfx = new GFX(100, 100, obr);
            return new HracKomp(gfx, logika);
        }
        #endregion

        #region mapa
        #region lokace
        protected override void VytvorLokace()
        {
            vytvorLokaci("les", '↑');
            vytvorLokaci("vesnice", 'A');
            vytvorLokaci("cesta", '|');
            vytvorLokaci("hrad", 'H');

            int[] vaz = { 2 };
            vytvorVazbyLokaci(lokace[0], vaz);
            vytvorVazbyLokaci(lokace[1], vaz);

            int[] vaz2 = { 0, 1 };
            vytvorVazbyLokaci(lokace[2], vaz2);

            int[] vaz3 = { 0, 1, 2 };
            vytvorVazbyLokaci(lokace[3], vaz3);
        }
        void vytvorLokaci(string jmeno, char symbol)
        {
            Bitmap obr = (Bitmap)Image.FromFile($"obrazky//{jmeno}.png");

            lokace.Add(new LokaceGFX(jmeno, obr, symbol));
        }
        void vytvorVazbyLokaci(Lokace lokace, int[] indexy)
        {
            foreach (int i in indexy)
            {
                lokace.PridejSouseda(this.lokace[i]);
            }
        }
        #endregion
        protected override void MapaConf()
        {
            //mapaConf(5, 5, 5, 5, 1);
            mapaConf(5, 5, 5, 5, 1,lokace[3],2,2,2,2);
        }

        #endregion

        public override void Uloz()
        {
            UlozenyPostup save = new UlozenyPostup();
            save.Mapa = Mapa;

            save.Uloz();
        }
    }
}
