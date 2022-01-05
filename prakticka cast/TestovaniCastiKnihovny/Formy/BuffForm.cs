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
    public partial class BuffForm : Form
    {
        public BuffForm()
        {
            InitializeComponent();
        }
        PostavaKomp postava;
        GameManager gm;
        private void BuffForm_Load(object sender, EventArgs e)
        {
            gm = new GameManager();
            string[] skupiny = { "combat" };

            Bitmap obr = (Bitmap)Image.FromFile("obrazky//stickman.png");
            postava = new HracKomp("player", 1, 100, gm.GetStatListy(skupiny), obr);
            nastavUI(postava, panel1, progressBar1, label1);
        }

        void nastavUI(PostavaKomp postava, Panel panel, ProgressBar bar, Label lab)
        {
            panel.Controls.Add(postava.GFX.grafika);
            bar.Maximum = postava.Postava.MaxHP;
            bar.Value = postava.Postava.HP;
            lab.Text = postava.ToString();

            postava.Postava.Zranen += Buff_zraneni;
        }

        private void Buff_zraneni(object sender, int e)
        {
            progressBar1.Value = e;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            postava.Postava.EfektyBuffu();
            update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KnihovnaRPG.Stat efekt = new KnihovnaRPG.Stat(gm.ZkratkyStatu[0], 5, 0.5f);
            postava.Postava.PridejBuff(new KnihovnaRPG.Buff(efekt, 3, KnihovnaRPG.BuffType.Buff, KnihovnaRPG.BuffZpusobZmeny.Konstanta));
            update();
        }
        void update()
        {
            label1.Text = postava.ToString();
            label2.Text = postava.Postava.SeznamBuffu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KnihovnaRPG.Stat efekt = new KnihovnaRPG.Stat(gm.ZkratkyStatu[0], -4, -0.5f);
            postava.Postava.PridejBuff(new KnihovnaRPG.Buff(efekt, 3, KnihovnaRPG.BuffType.Buff, KnihovnaRPG.BuffZpusobZmeny.Procento));
            update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            postava.Postava.PridejBuff(new KnihovnaRPG.Buff("DMG",20, 5,postava.Postava,null ));
            update();
        }
    }
}
