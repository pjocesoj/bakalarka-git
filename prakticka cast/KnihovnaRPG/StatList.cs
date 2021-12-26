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
                if (uzUbsahujeZkratku(list[i].zkratka,i)) { throw new StatListTentoPrvekUzExistujeException(); }

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
                if (this.list[i].zkratka == zkratka)
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
                if (list[i].zkratka == zkratka)
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
    }
}
