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
    public class GameManagerDLL
    {

        /// <summary>
        /// seznam zkratek statů vyskytujících se ve hře
        /// </summary>
        public string[] ZkratkyStatu;

        /// <summary>
        /// kategorie statListů (např souboj, obchod,...)
        /// </summary>
        protected Dictionary<string, List<Stat>> staty = new Dictionary<string, List<Stat>>();

        /// <summary>
        /// 
        /// </summary>
        protected GameManagerDLL()
        {
            VytvorSezanamZkratekStatu();
            VytvorStatListy();
        }

        #region staty

        /// <summary>
        /// vytvoří seznam všech zkratek statů vyskytujících se ve hře
        /// </summary>
        protected virtual void VytvorSezanamZkratekStatu()
        {
            throw new NotImplementedException("nejde vytvořit obecná a je proto potřeba přetížit a nasvit vlastní hodnoty");
        }

        /// <summary>
        /// vytvoří StatListy a seskupí do skupin (boj, obchod, ...)
        /// </summary>
        protected virtual void VytvorStatListy()
        {
            throw new NotImplementedException("nejde vytvořit obecná a je proto potřeba přetížit a nasvit vlastní hodnoty");
        }

        /// <summary>
        /// spoji všechny statlisty zvolenych skupin a vrátí je jako 1 seznam
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
        #endregion
    }
}
