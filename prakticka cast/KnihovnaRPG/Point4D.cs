using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// 4 rozměrný bod umožňující určení polohy na mapě
    /// </summary>
    public class Point4D
    {
        /// <summary>
        /// X souřadnice chunku v mapě
        /// </summary>
        public int MX { get; private set; }
        
        /// <summary>
        /// Y souřadnice chunku v mapě
        /// </summary>
        public int MY { get; private set; }
        
        /// <summary>
        /// X souřadnice lokace v chunku
        /// </summary>
        public int CX { get; private set; }
        
        /// <summary>
        /// Y souřadnice lokace v chunku
        /// </summary>
        public int CY { get; private set; }

        /// <summary>
        /// vytvoří nový 4rozměrný bod
        /// </summary>
        /// <param name="MX">X souřadnice chunku v mapě</param>
        /// <param name="MY">Y souřadnice chunku v mapě</param>
        /// <param name="CX">X souřadnice lokace v chunku</param>
        /// <param name="CY">Y souřadnice lokace v chunku</param>
        public Point4D(int MX, int MY, int CX, int CY)
        {
            this.MX = MX;
            this.MY = MY;
            this.CX = CX;
            this.CY = CY;
        }

        /// <summary>
        /// vypíše souřadnici jako mapa= X;Y chunk=X;Y
        /// </summary>
        public override string ToString()
        {
            return $"mapa= {MX} ; {MY}\nchunk= {CX} ; {CY}";
        }
    }
}
