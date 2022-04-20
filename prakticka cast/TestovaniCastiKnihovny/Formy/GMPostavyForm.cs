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
    public partial class GMPostavyForm : Form
    {
        public GMPostavyForm()
        {
            InitializeComponent();
        }

        PostavaKomp NPC;
        HracKomp hrac;
        private void GMPostavyForm_Load(object sender, EventArgs e)
        {
            GameManager gm = GameManager.Singleton;
            string[] skup = { "combat", "magie" };

            NPC = gm.NovaPostavaGFX("NPC", 5, 200, skup);
            zobraz(NPC,0);

            hrac = gm.NovyHracGFX("player1", 5, 200, skup);
            zobraz(hrac, 100);
        }

        void zobraz(PostavaKomp postava,int left )
        {
            Panel pan = new Panel();
            pan.Width = 100;
            pan.Height = this.Height;
            pan.Left = left;
            this.Controls.Add(pan);

            PictureBox pb = postava.GFX.grafika;
            pan.Controls.Add(pb);

            Label lab = new Label();
            lab.Top = pb.Bottom + 5;
            lab.Height = pan.Height - lab.Top;
            lab.Text = postava.ToString();
            pan.Controls.Add(lab);
        }
    }
}
