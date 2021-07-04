using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    public class Predmet
    {
        string jmeno;
        int cena;
        double hmotnost;
        protected bool stackovatelne;//zda jde dávat k sobě nebo v inventu každý zabírá vlastní slot
        public Predmet(string jmeno,int cena,double hmotnost, bool stackovatelne=true)
        {
            this.jmeno = jmeno;
            this.cena = cena;
            this.hmotnost = hmotnost;
            this.stackovatelne = stackovatelne;
        }
        #region properties
        public string Jmeno
        {
            get { return jmeno; }
        }
        public int Cena
        {
            get { return cena; }
        }
        public double Hmotnost
        {
            get { return hmotnost; }
        }
        /// <summary>
        /// zda jde dávat k sobě nebo v inventu každý zabírá vlastní slot
        /// </summary>
        public bool Stackovatelne
        {
            get { return stackovatelne; }
        }
        #endregion

        public override string ToString()
        {
            return $"{jmeno}\ncena:{cena}\nhmotnost:{hmotnost}";
        }
    }
}
