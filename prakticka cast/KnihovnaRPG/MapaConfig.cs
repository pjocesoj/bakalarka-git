using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaRPG
{
    /// <summary>
    /// nastavení pro generování mapy
    /// </summary>
    public class MapaConfig : INastaveni
    {
        /// <summary>
        /// rozměry mapy
        /// </summary>
        public Velikost Mapa { get; private set; }

        /// <summary>
        /// rozměry chunků
        /// </summary>
        public Velikost Chunk { get; private set; }

        /// <summary>
        /// na jaké souřadnici se hráč spawne
        /// </summary>
        public Point4D Spawn
        {
            get
            {
                if (randomSpawn)
                {
                    Random rng = new Random();
                    int MX = rng.Next(Mapa.X);
                    int MY = rng.Next(Mapa.Y);
                    int CX= rng.Next(Chunk.X);
                    int CY= rng.Next(Chunk.Y);
                    return new Point4D(MX, MY, CX, CY);
                }
                else
                {
                    return spawn;
                }
            }
        }
        bool randomSpawn = true;
        Point4D spawn;

        /// <summary>
        /// v jaké lokaci se hráč spawne(les, město,...)
        /// </summary>
        public Lokace SpawnLokace { get; private set; }

        /// <summary>
        /// jak velké okolí chunku bude načítáno do paměti
        /// </summary>
        public int RenderVzdalenost { get; private set; }

        /// <summary>
        /// vytvoří instanci pro nastavení chování generování mapy
        /// </summary>
        /// <param name="MX">X rozměr mapy</param>
        /// <param name="MY">Y rozměr mapy</param>
        /// <param name="CX">X rozměr chunků</param>
        /// <param name="CY">Y rozměr chunků</param>
        /// <param name="renderVzdalenost">jak velké okolí chunku bude načítáno do paměti</param>
        /// <param name="lokace">v jaké lokaci se hráč spawne(les, město,...)</param>
        public MapaConfig(int MX, int MY, int CX, int CY, int renderVzdalenost, Lokace lokace)
        {
            Mapa = new Velikost(MX, MY);
            Chunk = new Velikost(CX, CY);
            randomSpawn = true;
            SpawnLokace = lokace;
            this.RenderVzdalenost = renderVzdalenost;
        }

        /// <summary>
        /// vytvoří instanci pro nastavení chování generování mapy
        /// </summary>
        /// <param name="MX">X rozměr mapy</param>
        /// <param name="MY">Y rozměr mapy</param>
        /// <param name="CX">X rozměr chunků</param>
        /// <param name="CY">Y rozměr chunků</param>
        /// <param name="renderVzdalenost">jak velké okolí chunku bude načítáno do paměti</param>
        /// <param name="lokace">v jaké lokaci se hráč spawne(les, město,...)</param>
        /// <param name="smx">spawn mapa X</param>
        /// <param name="smy">spawn mapa Y</param>
        /// <param name="scx">spawn chunk X</param>
        /// <param name="scy">spawn chunk Y</param>
        public MapaConfig(int MX, int MY, int CX, int CY, int renderVzdalenost, Lokace lokace, int smx,int smy,int scx,int scy):this(MX,MY,CX,CY, renderVzdalenost,lokace)
        {
            randomSpawn = false;
            spawn = new Point4D(smx, smy, scx, scy);
        }

        /// <summary>
        /// vypíše nastavené rozměry a startovací polohu
        /// </summary>
        /// <returns></returns>
        public string Vypis()
        {
            string start=randomSpawn ? "random":spawn.ToString();
            return $"mapa:\n{Mapa}\nChunk:\n{Chunk}\nstart:\n{start}";
        }

        /// <summary>
        /// rozměry mapy nebo chunku
        /// </summary>
        public struct Velikost
        {
            /// <summary>
            /// X rozměr
            /// </summary>
            public int X { get; private set; }
            /// <summary>
            /// Y rozměr
            /// </summary>
            public int Y { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            public Velikost(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
            /// <summary>
            /// vypíše rozměry
            /// </summary>
            public override string ToString()
            {
                return $"X= {X}\nY= {Y}";
            }
        }
    }
}
