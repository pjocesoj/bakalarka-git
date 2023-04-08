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
    public partial class ChunkForm : Form
    {
        public ChunkForm()
        {
            InitializeComponent();
        }

        List<LokaceGFX> lokace = new List<LokaceGFX>();

        private void Mapa_Load(object sender, EventArgs e)
        {
            vytvorLokaci("les",'↑');
            vytvorLokaci("vesnice",'A');
            vytvorLokaci("cesta",'|');

            sousedi();

            ChunkKomp chunk1 = new ChunkKomp(6, 5);
            this.Controls.Add(chunk1.GFX.pozadi);
            chunk1.Vygeneruj(lokace[0], 2, 2);

            this.Width = 600;
            this.Height = 600;

            pictureBox1.Left = 750;
            pictureBox1.Width = 200;
            pictureBox1.Height = 200;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = chunk1.ObrChunku();
        }
        void vytvorLokaci(string jmeno,char symbol)
        {
            Bitmap obr = (Bitmap)Image.FromFile($"obrazky//{jmeno}.png");

            lokace.Add(new LokaceGFX(jmeno, obr,symbol));
        }

        void sousedi()
        {
            lokace[0].PridejSouseda(lokace[2]);

            lokace[1].PridejSouseda(lokace[2]);

            lokace[2].PridejSouseda(lokace[1]);
            lokace[2].PridejSouseda(lokace[0]);
        }
    }
}
