using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// seznam statů
    /// </summary>
    public class StatList
    {
        Stat[] list;
        #region konstruktory
        /// <summary>
        /// vytvoří nový list (použítí v managerovy zbytek se klonuje)
        /// </summary>
        /// <param name="list">list statů</param>
        /// <exception cref="StatListTentoPrvekUzExistujeException"></exception>
        public StatList(List<Stat> list)
        {
            this.list = new Stat[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                if (uzUbsahujeZkratku(list[i].Zkratka,i)) { throw new StatListTentoPrvekUzExistujeException(); }

                this.list[i] = list[i].clone();
            }
        }
        /// <summary>
        /// pro klonování kdy už není třeba ověřovat opakující se staty (všechny duplicity by už měli být zachyceny)
        /// </summary>
        private StatList(Stat[] list)
        {
            this.list = new Stat[list.Length];
            for(int i=0;i<list.Length;i++)
            {
                this.list[i]=list[i].clone();
            }
        }
        
        #endregion
        private bool uzUbsahujeZkratku(string zkratka,int n)
        {
            for(int i=0;i<n;i++)
            {
                if (this.list[i].Zkratka == zkratka)
                {
                    return true;
                }
            }
            return false;
        }

        private int najdiStat(string zkratka)
        {
            for(int i=0;i<list.Length;i++)
            {
                if (list[i].Zkratka == zkratka)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// podle zkratky najde stat
        /// </summary>
        /// <param name="zkratka">hledaná zkratka</param>
        /// <returns></returns>
        public Stat this[string zkratka]
        {
            get
            {
                int i = najdiStat(zkratka);
                if (i < 0) { throw new StatNenalezenException(); }
                return list[i];
            }
            set
            {

            }
        }

        /// <summary>
        /// vytvoří klon listu aby se navzájem neovlivňovali
        /// </summary>
        public StatList Clone()
        {
            /*List<Stat> ret = new List<Stat>();
            foreach (Stat s in list)
            {
                ret.Add(s.clone());
            }
            return new StatList(ret);*/
            return new StatList(list);
        }

        /// <summary>
        /// vypíše všechny staty v seznamu
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Stat s in list)
            {
                sb.Append($"{s}\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// zda se StatListy shodují
        /// </summary>
        /// <param name="L">levá strana</param>
        /// <param name="P">pravá strana</param>
        public static bool operator ==(StatList L, StatList P)
        {
            if (L.list.Length!=P.list.Length){ return false; }

            for (int i = 0; i < L.list.Length; i++)
            {
                if (L.list[i] != P.list[i]) { return false; }
            }

            return true;
        }

        /// <summary>
        /// zda se StatListy liší
        /// </summary>
        /// <param name="L">levá strana</param>
        /// <param name="P">pravá strana</param>
        public static bool operator !=(StatList L, StatList P)
        {
            return !(L == P);
        }

        /// <summary>
        /// sloučí StatListy do 1
        /// </summary>
        /// <param name="statListy">StatListy k sloučení</param>
        public static StatList SlucStatListy(List<StatList> statListy)
        {
            int delka = 0;
            foreach (StatList s in statListy)
            {
                delka += s.list.Length;
            }
            Stat[] ret = new Stat[delka];

            int i = 0;
            foreach (StatList SL in statListy)
            {
                for (int j = 0; j < SL.list.Length; j++)
                {
                    ret[i] = SL.list[j];
                    i++;
                }
            }
            return new StatList(ret);
        }

        #region jen aby VS dalo pokoj
        /// <summary>
        /// vyhodnotí zda jsou objekty identické
        /// </summary>
        /// <param name="obj">porovnávaný objekt</param>
        public override bool Equals(object obj)
        {
            if (!(obj is StatList)) { return false; }
            return this == (StatList)obj;
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
