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
    public partial class Combat : Form
    {
        public Combat()
        {
            InitializeComponent();
        }
        HracKomp hrac;
        PostavaKomp souper;
        private void Combat_Load(object sender, EventArgs e)
        {
            GameManager gm = new GameManager();
            string[] skupiny = { "combat" };

            Bitmap obr = (Bitmap)Image.FromFile("obrazky//stickman.png");
            hrac = new HracKomp("player", 1, 100, gm.GetStatListy(skupiny), obr);
            nastavUI(hrac, panel1, progressBar1,label1);

            souper = new PostavaKomp("NPC", 1, 100, gm.GetStatListy(skupiny), obr);
            nastavUI(souper, panel2, progressBar2, label2);

            souper.Postava.Staty["DMG"].BoostKonst = -2;

            hrac.Postava.Zranen += Hrac_Zranen;
            souper.Postava.Zranen += Souper_Zranen;
        }
        void nastavUI(PostavaKomp postava, Panel panel, ProgressBar bar,Label lab)
        {
            panel.Controls.Add(postava.GFX.grafika);
            bar.Maximum = postava.Postava.MaxHP;
            bar.Value = postava.Postava.HP;
            lab.Text = postava.ToString();
        }

        private void Souper_Zranen(object sender, int e)
        {
            progressBar2.Value = e;
        }

        private void Hrac_Zranen(object sender, int e)
        {
            progressBar1.Value = e;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            souper.Postava.Zraneni(hrac.Postava, hrac.Postava.Staty["DMG"].Hodnota, "DEF");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hrac.Postava.Zraneni(souper.Postava, souper.Postava.Staty["DMG"].Hodnota, "DEF");
        }
    }
}
