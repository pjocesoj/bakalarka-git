using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TestovaniCastiKnihovny
{
    class UIGrid:UI
    {
        GFX[,] bunky;
        Point mrizka;
        int okraj;
        Point rozmerBunky;
        public UIGrid(int radku, int sloupcu,int left,int top, int vyskaBunky=100, int sirkaBunky=100,int okraj = 5) : base(left,top,sirka(sloupcu,sirkaBunky,okraj), vyska(radku,vyskaBunky,okraj))
        {
            bunky = new GFX[sloupcu, radku];
            mrizka = new Point(sloupcu, radku);
            this.okraj = okraj;
            rozmerBunky = new Point(sirkaBunky, vyskaBunky);

            for (int x = 0; x < sloupcu; x++)
            {
                for (int y = 0; y < radku; y++)
                {
                    newGFX(x, y);
                }
            }
        }
        #region rozmer panel
        static int sirka(int sloupcu,int sirkaBunky,int okraj)
        {
            return sloupcu * sirkaBunky + (sloupcu+1) * okraj;
        }
        static int vyska(int radku, int vyskaBunky, int okraj)
        {
            return radku * vyskaBunky + (radku+1) * okraj;
        }
        #endregion
        Point spocitejPolohu(int x, int y)
        {
            int left = x * rozmerBunky.X + (x+1) * okraj;
            int top = y * rozmerBunky.Y + (y+1) * okraj;

            return new Point(left, top);
        }
        void newGFX(int x, int y)
        {
            GFX n = new GFX(rozmerBunky.X, rozmerBunky.Y);
            bunky[x, y] = n;

            Point poloha = spocitejPolohu(x, y);
            n.grafika.Left = poloha.X;
            n.grafika.Top = poloha.Y;

            panel.Controls.Add(n.grafika);
        }

        public void SetBunku(GFX g, int x, int y)
        {
            bunky[x, y].grafika.Image = g.grafika.Image;
        }
        public void SetBunku(Bitmap obr, int x, int y)
        {
            bunky[x, y].grafika.Image = obr;
        }

        public void prazdny(int x, int y)
        {          
            bunky[x, y].grafika.Image=new Bitmap(rozmerBunky.X, rozmerBunky.Y);
        }

        public Point RozmerBunka
        {
            get
            {
                return rozmerBunky;
            }
        }
    }
}
