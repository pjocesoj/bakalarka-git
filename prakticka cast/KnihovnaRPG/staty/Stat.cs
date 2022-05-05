using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// staty postavy nebo předmětu
    /// </summary>
    public class Stat
    {
        string jmeno;
        string zkrat;
        double zaklad;
        double boostKonst;
        float boostProc;

        #region konstruktory postava
        /// <param name="jmeno">celé jméno max.2 slova (zkratka: "defence"-> "DEF" | "fire defence"-> "fDEF" </param>
        /// <param name="zaklad">základní hodnota statu</param>
        /// <exception cref="StatVstupMaViceNez2SlovaException"></exception>
        public Stat(string jmeno, double zaklad)
        {
            this.jmeno = jmeno;
            try
            {
                this.zkrat = jmenoToZkratka(jmeno);
            }
            catch (StatVstupMaViceNez2SlovaException e) { throw e; }//metoda za chybu nemůže proto ji házím otud

            this.zaklad = zaklad;
        }

        /// <param name="jmeno">celé jméno</param>
        /// <param name="zkratka">"defence"-> "DEF" | "fire defence"-> "fDEF"></param>
        /// <param name="zaklad">základní hodnota statu</param>
        /// <exception cref="StatZkratkaJeMocDlouhaException"></exception>
        public Stat(string jmeno, string zkratka, double zaklad)
        {
            this.jmeno = jmeno;
            try
            {
                this.zkrat = zkratka;
                //this.zkrat = unifikaceZkratky(zkratka);
            }
            catch (StatZkratkaJeMocDlouhaException e) { throw e; }
            this.zaklad = zaklad;
        }

        //nová postava nemá boost

        #endregion
        #region konstruktory item
        /// <summary>
        /// konstruktor pro stat předmětu
        /// </summary>
        /// <param name="jmeno">celé jméno max.2 slova (zkratka: "defence"-> "DEF" | "fire defence"-> "fDEF" </param>
        /// <param name="boostKonst">hodnota přičtená k základní hodnotě postavy při použití</param>
        /// <param name="boostProc">procentuální navýšení hodnoty postavy při použití</param>
        /// <exception cref="StatVstupMaViceNez2SlovaException"></exception>
        public Stat(string jmeno, double boostKonst, float boostProc)
        {
            this.jmeno = jmeno;
            try
            {
                this.zkrat = jmenoToZkratka(jmeno);
            }
            catch (StatVstupMaViceNez2SlovaException e) { throw e; }//metoda za chybu nemůže proto ji házím otud

            this.boostKonst = boostKonst;
            this.boostProc = boostProc;
        }
        /// <summary>
        /// konstruktor pro stat předmětu
        /// </summary>
        /// <param name="jmeno">celé jméno</param>
        /// <param name="zkratka">"defence"-> "DEF" | "fire defence"-> "fDEF"></param>
        /// <param name="boostKonst">hodnota přičtená k základní hodnotě postavy při použití</param>
        /// <param name="boostProc">procentuální navýšení hodnoty postavy při použití</param>
        /// <exception cref="StatZkratkaJeMocDlouhaException"></exception>
        public Stat(string jmeno, string zkratka, double boostKonst, float boostProc)
        {
            this.jmeno = jmeno;
            try
            {
                this.zkrat = zkratka;
                //this.zkrat = unifikaceZkratky(zkratka);
            }
            catch (StatZkratkaJeMocDlouhaException e) { throw e; }

            this.boostKonst = boostKonst;
            this.boostProc = boostProc;
        }

        //item nemá vlastní stat jen boostuje ty postavy

        #endregion

        #region get+set
        /// <summary>
        /// celý název statu
        /// </summary>
        public string Nazev
        {
            get { return jmeno; }
        }

        /// <summary>
        /// zkratka názvu statu (4 znaky) 
        /// </summary>
        public string Zkratka
        {
            get { return zkrat; }
        }

        /// <summary>
        /// základní hodnota statu
        /// </summary>
        public double Zaklad
        {
            get { return zaklad; }
            set { zaklad = value; }
        }

        /// <summary>
        /// hodnota přičtená k základní hodnotě
        /// </summary>
        public double BoostKonst
        {
            get { return boostKonst; }
            set { boostKonst = value; }
        }

        /// <summary>
        /// procentuální navýšení hodnoty
        /// </summary>
        public float BoostProc
        {
            get { return boostProc; }
            set { boostProc = value; }
        }
        #endregion

        /// <summary>
        /// vysledná hodnota po započtení boostů
        /// </summary>
        public double Hodnota
        {
            get
            {
                double ret = zaklad + boostKonst;
                ret += (ret / 100) * boostProc;
                return ret;
            }
        }

        /// <summary>
        /// vypíše zkratku a hodnotu
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //return $"{zkrat}: {Hodnota}";
            return $"{unifikaceZkratky(zkrat)}: {Hodnota}";
        }

        /// <summary>
        /// vypíše celý název a hodnotu
        /// </summary>
        /// <returns></returns>
        public string Vypis()
        {
            return $"{jmeno}: {Hodnota}";
        }

        /// <summary>
        /// string sloužící k ukládání aktualniho stavu
        /// </summary>
        public virtual string SaveStream()
        {
            return $"{Zkratka};{zaklad};{boostKonst};{boostProc}";
        }

        #region vytvareni zkratky
        /// <summary>
        /// vytvoření zkratky z názvu
        /// </summary>
        /// <param name="jmeno">celý název max 2 slova</param>
        /// <returns>"defence"-> "DEF" | "fire defence"-> "fDEF"</returns>
        /// <exception cref="StatVstupMaViceNez2SlovaException">jméno má vice než 2 slova</exception>
        private string jmenoToZkratka(string jmeno)
        {
            string[] temp = jmeno.Split(' ');
            if (temp.Length > 2)
            {
                throw new StatVstupMaViceNez2SlovaException("pro vytvoření zkratky z názvu musí mít max 2 slova");
            }
            else if (temp.Length == 2)
            {
                string typ = temp[0][0] + "";//char nemá ToLower()
                string ret = $"{typ.ToLower()}{temp[1].Remove(3).ToUpper()}";
                return ret;
            }
            else
            {
                if (temp[0].Length > 3)//aby neházelo chybu když se zadá název <3
                {
                    return $" {temp[0].Remove(3).ToUpper()}";
                }
                else
                {
                    return temp[0];
                    //return unifikaceZkratky(temp[0]);
                }
            }
        }

        /// <summary>
        /// ověří zda má zkratka max 4 znaky a pokud má méně doplní mezerami
        /// </summary>
        /// <param name="orig"></param>
        /// <exception cref="StatZkratkaJeMocDlouhaException"></exception>
        /// <returns>zkratku ze 4 znaku</returns>
        private string unifikaceZkratky(string orig)
        {
            if (orig.Length > 4) { throw new StatZkratkaJeMocDlouhaException(); }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (int i = orig.Length; i < 4; i++)
                {
                    sb.Append(" ");
                }
                sb.Append(orig);
                return sb.ToString();
            }
        }
        #endregion

        #region klonovani
        /// <summary>
        /// pomocny konstruktor pro klonování (za normálních okolností nikdy nejsou potřeba všechny hodnoty)
        /// </summary>
        private Stat(string jmeno, string zkrat, double zaklad, double boostKonst, float boostProc)
        {
            this.jmeno = jmeno;
            this.zkrat = zkrat;
            this.zaklad = zaklad;
            this.boostKonst = boostKonst;
            this.boostProc = boostProc;
        }
        /// <summary>
        /// vytvoří klon tohoto objektu
        /// </summary>
        public Stat clone()
        {
            return new Stat(jmeno, zkrat, zaklad, boostKonst, boostProc);
        }


        #endregion

        /// <summary>
        /// zda se Staty shodují
        /// </summary>
        /// <param name="L">levá strana</param>
        /// <param name="P">pravá strana</param>
        public static bool operator ==(Stat L, Stat P)
        {
            if (L.jmeno != P.jmeno) { return false; }
            if (L.zkrat != P.zkrat) { return false; }
            if (L.Zaklad != P.Zaklad) { return false; }
            if (L.BoostKonst != P.BoostKonst) { return false; }
            if (L.BoostProc != P.BoostProc) { return false; }

            return true;
        }

        /// <summary>
        /// zda se Staty liší
        /// </summary>
        /// <param name="L">levá strana</param>
        /// <param name="P">pravá strana</param>
        public static bool operator !=(Stat L, Stat P)
        {
            return !(L == P);
        }

        #region jen aby VS dalo pokoj
        /// <summary>
        /// vyhodnotí zda jsou objekty identické
        /// </summary>
        /// <param name="obj">porovnávaný objekt</param>
        public override bool Equals(object obj)
        {
            if (!(obj is Stat)) { return false; }
            return this == (Stat)obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>base.GetHashCode()</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
