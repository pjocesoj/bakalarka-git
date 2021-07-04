using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    public class Vybaveni:Predmet
    {
        StatList staty;
        public Vybaveni(string jmeno, int cena, double hmotnost, StatList statList,bool stackovatelne = false):base( jmeno,  cena,  hmotnost,  stackovatelne)
        {
            this.staty = statList;
        }

        public StatList Staty
        {
            get { return staty; }
        }

        public override string ToString()
        {
            return $"{base.ToString()}\n{staty.ToString()}";
        }
    }
}
