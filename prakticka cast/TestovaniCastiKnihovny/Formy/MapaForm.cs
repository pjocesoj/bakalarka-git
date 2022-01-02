using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestovaniCastiKnihovny
{
    public partial class MapaForm : Form
    {
        public MapaForm()
        {
            InitializeComponent();
        }

        List<LokaceGFX> lokace = new List<LokaceGFX>();
        #region lokace
        void lokaceInit()
        {
            vytvorLokaci("les", '↑');
            vytvorLokaci("vesnice", 'A');
            vytvorLokaci("cesta", '|');

            sousedi();
        }
        void vytvorLokaci(string jmeno, char symbol)
        {
            Bitmap obr = (Bitmap)Image.FromFile($"obrazky//{jmeno}.png");

            lokace.Add(new LokaceGFX(jmeno, obr, symbol));
        }
        void sousedi()
        {
            lokace[0].PridejSouseda(lokace[2]);

            lokace[1].PridejSouseda(lokace[2]);

            lokace[2].PridejSouseda(lokace[1]);
            lokace[2].PridejSouseda(lokace[0]);
        }
        #endregion
        private void MapaForm_Load(object sender, EventArgs e)
        {
            lokaceInit();

            ChunkKomp c1 = new ChunkKomp(5, 5);
            c1.Vygeneruj(lokace[1],3,3);

            ChunkKomp c2 = new ChunkKomp(5, 5);
            c2.Vygeneruj(c1, null, c1, null);

            ChunkKomp[,] temp = new ChunkKomp[3, 3];
            temp[1, 1] = c1;
            temp[2, 0] = c1;
            temp[2, 1] = c2;

            MapaKomp map = new MapaKomp(3, 3, temp);
            this.Controls.Add(map.GFX.pozadi);
            map.vykresli();

        }

    }
}
