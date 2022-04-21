using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// společný předek pro nastavení, umožnující veškeré nastavení mít v GameManagerovi
    /// </summary>
    public interface INastaveni
    {
        /// <summary>
        /// vypíše nastavení a jejich hodnoty
        /// </summary>
        string Vypis();
    }
}
