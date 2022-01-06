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

        List<KouzloKomp> kouzla;
        private void Combat_Load(object sender, EventArgs e)
        {
            //GameManager gm = new GameManager();
            GameManager gm = GameManager.Singleton;

            string[] skupiny = { "combat" };

            Bitmap obr = (Bitmap)Image.FromFile("obrazky//stickman.png");
            hrac = new HracKomp("player", 1, 100, gm.GetStatListy(skupiny), obr);
            nastavUI(hrac, panel1, progressBar1,label1);

            souper = new PostavaKomp("NPC", 1, 100, gm.GetStatListy(skupiny), obr);
            nastavUI(souper, panel2, progressBar2, label2);

            souper.Postava.Staty["DMG"].BoostKonst = -2;

            hrac.Postava.Zranen += Hrac_Zranen;
            souper.Postava.Zranen += Souper_Zranen;

            hrac.Postava.Uzdraven += Hrac_Zranen;

            vytvorKouzla();
        }
        void nastavUI(PostavaKomp postava, Panel panel, ProgressBar bar,Label lab)
        {
            panel.Controls.Add(postava.GFX.grafika);
            bar.Maximum = postava.Postava.MaxHP;
            bar.Value = postava.Postava.HP;
            lab.Text = postava.ToString();
        }

        void vytvorKouzla()
        {
            kouzla = new List<KouzloKomp>();

            Bitmap obr = (Bitmap)Image.FromFile("obrazky//fireball.png");
            KouzloKomp fireball = new KouzloKomp("fireball", 2, 0, "DMG", 10, null,obr);
            fireball.Cil = souper;
            fireball.Seslal = hrac;
            flowLayoutPanel1.Controls.Add(fireball.GFX.grafika);
            kouzla.Add(fireball);

            obr = (Bitmap)Image.FromFile("obrazky//heal.png");
            KouzloKomp heal = new KouzloKomp("heal", 2, 0, 10, obr);
            heal.Cil = hrac;
            heal.Seslal = hrac;
            flowLayoutPanel1.Controls.Add(heal.GFX.grafika);
            kouzla.Add(heal);

            obr = (Bitmap)Image.FromFile("obrazky//DMG up.png");
            KnihovnaRPG.Stat s = new KnihovnaRPG.Stat(GameManager.Singleton.ZkratkyStatu[0], 5, 0);
            KnihovnaRPG.Buff b = new KnihovnaRPG.Buff(s, 4, KnihovnaRPG.BuffType.Buff, KnihovnaRPG.BuffZpusobZmeny.Konstanta);
            KouzloKomp dmg = new KouzloKomp("DMG up", 2, 0, b, obr);
            dmg.Cil = hrac;
            dmg.Seslal = hrac;
            flowLayoutPanel1.Controls.Add(dmg.GFX.grafika);
            kouzla.Add(dmg);
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

        private void button3_Click(object sender, EventArgs e)
        {
            foreach(KouzloKomp k in kouzla)
            {
                k.Kouzlo.DalsiKolo();
            }
            hrac.Postava.EfektyBuffu();
            souper.Postava.EfektyBuffu();

            label1.Text = hrac.ToString();
            label2.Text = souper.ToString();
        }
    }
}
