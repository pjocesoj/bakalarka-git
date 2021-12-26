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
        /// 
        /// </summary>
        public GameManagerDLL()
        {
            vytvorStatListy();
        }

        void vytvorStatListy()
        {
            //ve finalu bude prázdný a bude házet exception aby pri implementaci byli vytvoreny seznamy

            List<Stat> combat = new List<Stat>();
            combat.Add(new Stat("utok", "DMG", 10));
            combat.Add(new Stat("obrana", "DEF", 5));

            staty.Add("combat",combat);
        }

        /// <summary>
        /// spoji všechny statlisty zvolenych skupin a vrátí je jako 1 seznam
        /// </summary>
        /// <param name="skupiny">názvy skupin, které chcete</param>
        public StatList GetStatListy(string[]skupiny)
        {
            List<Stat> ret = new List<Stat>();
            for(int i=0;i<skupiny.Length;i++)
            {
                ret.AddRange(staty[skupiny[i]]);
            }
            return new StatList(ret); 
        }
        Dictionary<string, List<Stat>> staty = new Dictionary<string, List<Stat>>();
    }
}
