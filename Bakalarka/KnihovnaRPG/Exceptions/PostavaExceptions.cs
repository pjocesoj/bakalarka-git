using System;

/*
 * protože Exceptions mají jen konstruktory a žádnou logiku
 * jsou všchny Exceptions patřící k 1 třídě ve stejném souboru 
 * aby v projektu nebylo několik desítek souborů obsahující jen 10 řádků
*/

namespace KnihovnaRPG
{
    /// <summary>
    /// chyba při vytvaření postavy s HP<0 nebo HPmaxHP
    /// </summary>
    public class PostavaHPException : Exception
    {
        public PostavaHPException(){ }
        public PostavaHPException(string msg) : base(msg) { }
    }

}
