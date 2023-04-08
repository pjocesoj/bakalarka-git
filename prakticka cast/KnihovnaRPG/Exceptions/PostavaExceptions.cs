using System;

/*
 * protože Exceptions mají jen konstruktory a žádnou logiku
 * jsou všchny Exceptions patřící k 1 třídě ve stejném souboru 
 * aby v projektu nebylo několik desítek souborů obsahující jen 10 řádků
*/

namespace KnihovnaRPG
{
    /// <summary>
    /// chyba při vytvaření postavy s HP nižším než 0 nebo vyšším než maxHP
    /// </summary>
    public class PostavaHPException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public PostavaHPException(){ }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">zobrazená zpráva</param>
        public PostavaHPException(string msg) : base(msg) { }
    }

}
