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

            Bitmap obr = (Bitmap)Image.FromFile($"obrazky//hrad.png");
            lokace.Add(new LokaceGFX("hrad", lokace,obr,'H',100,100));
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

            MapaKomp map = new MapaKomp(5, 5);
            map.Vygeneruj(lokace[3],2,2,5,5,2,2,2);
            this.Controls.Add(map.GFX.pozadi);
            map.vykresli();

            minimap(map, 500,500);
        }

        void minimap(MapaKomp map,int w,int h)
        {
            PictureBox pb = new PictureBox();
            pb.Left = this.Controls[0].Right+10;
            pb.Width=w;
            pb.Height = h;

            pb.Image = map.ObrMapy(w,h);
            this.Controls.Add(pb);
        }

    }
}
