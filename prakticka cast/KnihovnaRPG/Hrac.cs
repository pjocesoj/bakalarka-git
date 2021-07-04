using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    public class Hrac:Postava
    {
        //int exp = 0;
        public Hrac(string jmeno) : base(jmeno)
        {

        }
        public Hrac(string jmeno, int lv, int HP, StatList statList) : base(jmeno, lv, HP,statList)
        {

        }

        public int Exp { get; set; }

        public override string ToString()
        {
            return $"{Jmeno}\nLV{lv}\nExp{Exp}/{100*lv}\nHP:{zivoty}/{maxHP}\n{Staty}";
        }
    }
}
