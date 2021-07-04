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
        HracGFX hrac;
        //PostavaGFX souper;
        //PostavaGFXv2 souper;
        PostavaGFXv3 souper;
        private void Combat_Load(object sender, EventArgs e)
        {
            KnihovnaRPG.GameManagerDLL gm = new KnihovnaRPG.GameManagerDLL();
            string[] skupiny = { "combat" };

            Bitmap obr = (Bitmap)Image.FromFile("obrazky//stickman.png");
            hrac = new HracGFX("player", 1, 100, gm.GetStatListy(skupiny),obr);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = hrac.vzhled;
            progressBar1.Maximum = hrac.MaxHP;
            progressBar1.Value = hrac.HP;
            label1.Text = hrac.ToString();

            souper = new PostavaGFXv3("NPC", 1, 100, gm.GetStatListy(skupiny), obr);
            //souper = new PostavaGFXv2("NPC", 1, 100, gm.GetStatListy(skupiny), obr);
            //souper = new PostavaGFX("NPC", 1, 100, gm.GetStatListy(skupiny), obr);
            //panel2.Controls.Add(souper.GFX);     
            panel2.Controls.Add(souper.GFX.grafika);   
            progressBar2.Maximum = souper.MaxHP;
            progressBar2.Value = souper.HP;
            label2.Text = souper.ToString();

            souper.Staty["DMG"].BoostKonst = -2;

            hrac.Zranen += Hrac_Zranen;
            souper.Zranen += Souper_Zranen;
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
            souper.zraneni(hrac,hrac.Staty["DMG"].Hodnota,"DEF");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //hrac.zraneni(souper.postava,souper.Staty["DMG"].Hodnota, "DEF");
            hrac.zraneni(souper, souper.Staty["DMG"].Hodnota, "DEF");
        }
    }
}
