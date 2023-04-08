using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// odměna za úkol ( pro případ, že implementaci nebude vyhovovat Ukolodmena)
    /// </summary>
    public interface IUkolOdmena
    {
        /// <summary>
        /// zkušenosti za splnění úkolu
        /// </summary>
        int Exp { get; }
    }
}
