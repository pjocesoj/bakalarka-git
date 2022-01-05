using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class GameManager:GameManagerDLL
    {
        protected override void VytvorSezanamZkratekStatu()
        {
            ZkratkyStatu =new string[]{"DMG","DEF" };
        }
        protected override void VytvorStatListy()
        {
            List<Stat> combat = new List<Stat>();
            combat.Add(new Stat("utok", "DMG", 10));
            combat.Add(new Stat("obrana", "DEF", 5));

            staty.Add("combat", combat);
        }
    }
}
