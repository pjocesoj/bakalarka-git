using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// flyweight třída reprezentující lokaci a nesoucí data pro generování
    /// </summary>
    public class Lokace
    {
        #region properties
        /// <summary>
        /// název lokace
        /// </summary>
        public string Nazev { get; private set; }

        /// <summary>
        /// jaké lokace se mohou vygenerovat jako sousední
        /// </summary>
        public List<Lokace> MuzeSousedit { get; private set; }

        /// <summary>
        /// jací nepřátelé se mohou v lokaci spawnout
        /// </summary>
        public List<String> MuzeSpawnout { get; private set; }
        #endregion

        #region konstruktory
        /// <summary>
        /// vytvoří lokaci, která může sousedit jen sama se sebou a nenachází se zde nepřátelé
        /// </summary>
        /// <param name="nazev">název lokace</param>
        public Lokace(string nazev)
        {
            Nazev = nazev;

            MuzeSousedit = new List<Lokace>();
            MuzeSousedit.Add(this);
        }

        /// <summary>
        /// vytvoří lokaci, která může sousedit lokacemi v seznamu a nenachází se zde nepřátelé
        /// </summary>
        /// <param name="nazev">název lokace</param>
        /// <param name="sousedi">lokace se kterými může sousedit</param>
        public Lokace(string nazev, List<Lokace> sousedi)
        {
            Nazev = nazev;

            MuzeSousedit = new List<Lokace>();
            PridejSouseda(sousedi);
        }
        #endregion

        #region dodatecne pridani sousedu

        /// <summary>
        /// přidá novou lokaci do seznamu sousesdů
        /// </summary>
        /// <param name="soused">přidávaná lokace</param>
        public void PridejSouseda(Lokace soused)
        {
            MuzeSousedit.Add(soused);
        }

        /// <summary>
        /// přidá nové lokace do seznamu sousesdů
        /// </summary>
        /// <param name="sousedi">přidávaná lokace</param>
        public void PridejSouseda(List<Lokace> sousedi)
        {
            foreach (Lokace l in sousedi)
            {
                MuzeSousedit.Add(l);
            }
        }
        #endregion

        /// <summary>
        /// vypíše název a možné sousedy
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Lokace s in MuzeSousedit)
            {
                sb.Append(s.Nazev);
                sb.Append(";");
            }
            return $"{Nazev}({sb})";
        }

        /// <summary>
        /// reprezentace v Chunk.ToString()
        /// </summary>
        public virtual char Symbol()
        {
            return Nazev[0];
        }

        #region AND=průnik Muzesousedit

        /// <summary>
        /// vytvoří průnik seznamů lokací se kterými je možné sousedit.
        /// <br/>pokud je 1 null vrátí celý druhý seznam
        /// </summary>
        /// <param name="L">Levá strana</param>
        /// <param name="P">Pravá strana</param>
        public static List<Lokace> operator &(Lokace L, Lokace P)
        {
            if (L == null & P == null) { return null; }
            if (L == null) { return P.MuzeSousedit; }
            if (P == null) { return L.MuzeSousedit; }

            return and(L.MuzeSousedit, P.MuzeSousedit);
        }

        /// <summary>
        /// vytvoří průnik seznamů lokací se kterými je možné sousedit.
        /// <br/>pokud je 1 null vrátí celý druhý seznam
        /// </summary>
        /// <param name="L">Levá strana</param>
        /// <param name="P">Pravá strana</param>
        public static List<Lokace> operator &(Lokace L, List<Lokace> P)
        {
            if (L == null & P == null) { return null; }
            if (L == null) { return P; }
            if (P == null) { return L.MuzeSousedit; }

            return and(L.MuzeSousedit, P);
        }

        /// <summary>
        /// vytvoří průnik seznamů lokací se kterými je možné sousedit.
        /// <br/>pokud je 1 null vrátí celý druhý seznam
        /// </summary>
        /// <param name="L">Levá strana</param>
        /// <param name="P">Pravá strana</param>
        public static List<Lokace> operator &(List<Lokace> L, Lokace P)
        {
            if (L == null & P == null) { return null; }
            if (L == null) { return P.MuzeSousedit; }
            if (P == null) { return L; }

            return and(L, P.MuzeSousedit);
        }

        /// <summary>
        /// vytvoří průnik seznamů lokací se kterými je možné sousedit.
        /// <br/>pokud je 1 null vrátí celý druhý seznam
        /// </summary>
        /// <param name="L">Levá strana</param>
        /// <param name="P">Pravá strana</param>
        public static List<Lokace> AND(List<Lokace> L, List<Lokace> P)
        {
            if (L == null & P == null) { return null; }
            if (L == null) { return P; }
            if (P == null) { return L; }
            return and(L, P);
        }        

        //ošetření null se liší a je zbytečné 2x testovat to samé
        private static List<Lokace> and(List<Lokace> L, List<Lokace> P)
        {
            List<Lokace> ret = new List<Lokace>();
            List<Lokace> temp = new List<Lokace>(P);

            foreach (Lokace A in L)
            {
                foreach (Lokace B in temp)
                {
                    if (A == B)
                    {
                        ret.Add(A);
                        temp.Remove(A);
                        break;
                    }
                }
            }

            return ret;
        }
        #endregion
    }
}
