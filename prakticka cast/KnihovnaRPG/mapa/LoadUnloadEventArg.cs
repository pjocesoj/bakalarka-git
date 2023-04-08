using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// argument pro event GameManagerDLL.ChunkLoadUnload
    /// </summary>
    public class LoadUnloadEventArg
    {
        /// <summary>
        /// X souřadnice chunku
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Y souřadnice chunku
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// zda má být chunk načte či uvolněn z paměti
        /// </summary>
        public LoadUnloadAkce akce { get; private set; }

        private LoadUnloadEventArg(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// vytvoří EventArg pro načtení chunku do paměti
        /// </summary>
        /// <param name="X">X souřadnice</param>
        /// <param name="Y">Y souřadnice</param>
        public static LoadUnloadEventArg Load(int X, int Y)
        {
            LoadUnloadEventArg ret = new LoadUnloadEventArg(X, Y);
            ret.akce = LoadUnloadAkce.load;
            return ret;
        }
        /// <summary>
        /// vytvoří EventArg pro uvolnění chunku z paměti
        /// </summary>
        /// <param name="X">X souřadnice</param>
        /// <param name="Y">Y souřadnice</param>
        public static LoadUnloadEventArg Unload(int X, int Y)
        {
            LoadUnloadEventArg ret = new LoadUnloadEventArg(X, Y);
            ret.akce = LoadUnloadAkce.unload;
            return ret;
        }
    }
    /// <summary>
    /// jaká akce se má s chunkem provést
    /// </summary>
    public enum LoadUnloadAkce
    {
        /// <summary>
        /// 
        /// </summary>
        load,
        /// <summary>
        /// 
        /// </summary>
        unload
    }
}
