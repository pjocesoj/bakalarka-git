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

        List<PredmetKomp> itemy = new List<PredmetKomp>();
        InventarKompV2 invent2;
        private void Inventar_Load(object sender, EventArgs e)
        {
            Bitmap obr = (Bitmap)Image.FromFile("obrazky//gem.png");
            PredmetKomp item1 = new PredmetKomp("gem", 10, 1, obr);
            Zobraz(item1,0);

            Bitmap obr2 = (Bitmap)Image.FromFile("obrazky//mec.png");
            List<KnihovnaRPG.Stat> temp = new List<KnihovnaRPG.Stat>();
            temp.Add(new KnihovnaRPG.Stat("DMG", 5));
            KnihovnaRPG.StatList list = new KnihovnaRPG.StatList(temp);
            VybaveniKomp eqip1 = new VybaveniKomp("mec", 100, 2, list, obr2);
            Zobraz(eqip1, 100);

            Bitmap obr3 = (Bitmap)Image.FromFile("obrazky//elik.png");
            temp = new List<KnihovnaRPG.Stat>();
            temp.Add(new KnihovnaRPG.Stat("HP", 20));
            list = new KnihovnaRPG.StatList(temp);
            KonzumovatelneKomp elik1 = new KonzumovatelneKomp("HP elik", 5, 0.1, list, obr3);
            Zobraz(elik1, 200);

            itemy.Add(item1);
            itemy.Add(eqip1);
            itemy.Add(elik1);

            //invent2 = new InventarKompV2(300, 0);
            //invent2 = new InventarKompV2(5.0,300,0);
            invent2 = new InventarGridKomp(6, 2, 3, 300, 0);
            //invent2 = new InventarGridKomp(6,2,3,300,0,50,50);

            this.Controls.Add(invent2.GFX.pozadi);

            invent2.Invent.Pridan += Invent_Pridan;
            invent2.Invent.Odebran += Invent_Odebran;
            invent2.Invent.Plny += Invent_Plny;
        }
        #region test event
        private void Invent_Plny(object sender, KnihovnaRPG.IPredmet e)
        {
            MessageBox.Show("plny event");
        }

        private void Invent_Odebran(object sender, KnihovnaRPG.IPredmet e)
        {
            MessageBox.Show("odebran event");
        }

        private void Invent_Pridan(object sender, KnihovnaRPG.IPredmet e)
        {
            MessageBox.Show("pridan event");
        }
        #endregion

        void Zobraz(PredmetKomp item, int left)
        {
            #region vytvoreni controls
            Panel p = new Panel();
            p.Width = 100;
            p.Height = 400;
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

            Button but = new Button();
            but.Text = "přidej";
            but.Width = 80;
            but.Height = 20;
            but.Left = 10;
            but.Top = lab.Bottom + 10;
            but.Click += pridej_Click;
            p.Controls.Add(but);
        }

        private void pridej_Click(object sender, EventArgs e)
        {
            int i = (sender as Button).Parent.TabIndex;
            //bool b=invent2.Pridej(itemy[i]);
            //if (!b) { MessageBox.Show("plný"); }

            invent2.Pridej(itemy[i]);
        }

        private void InventarForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //MessageBox.Show("del");
                PredmetKomp temp = (PredmetKomp)invent2.Invent.GetAt(1);
                invent2.Odeber(temp);
            }
        }
    }
}
