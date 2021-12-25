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
    public partial class InventarForm : Form
    {
        public InventarForm()
        {
            InitializeComponent();
        }

        private void Inventar_Load(object sender, EventArgs e)
        {
            Bitmap obr = (Bitmap)Image.FromFile("obrazky//gem.png");
            PredmetGFX item1 = new PredmetGFX("gem", 10, 1,obr);
            Zobraz(item1,0);

            Bitmap obr2 = (Bitmap)Image.FromFile("obrazky//mec.png");
            Bitmap obr3 = (Bitmap)Image.FromFile("obrazky//elik.png");
            VybaveniGFX eqip1 = new VybaveniGFX("mec", 100, 2, null, obr2);
            //Zobraz(eqip1, 100);
            
        }

        void Zobraz(PredmetGFX item, int left)
        {
            #region vytvoreni controls
            Panel p = new Panel();
            p.Width = 100;
            p.Height = 300;
            p.BorderStyle = BorderStyle.FixedSingle;

            PictureBox pb = item.GFX.grafika;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            Label lab = new Label();
            lab.Height = 200;
            lab.Top = pb.Bottom + 5;

            p.Controls.Add(pb);
            p.Controls.Add(lab);

            this.Controls.Add(p);
            p.Left = left;
            #endregion

            lab.Text = item.ToString();
        }
    }
}
