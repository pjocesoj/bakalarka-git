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
    public partial class UkolForm : Form
    {
        public UkolForm()
        {
            InitializeComponent();
        }

        PostavaKomp hrac;
        UkolKomp ukol;
        InventarKompV2 inventar;
        private void UkolForm_Load(object sender, EventArgs e)
        {
            //ukol = new UkolKomp("zabit", "vzorový úkol bla\nbla\nbla",10,"cil");
            string[] cile = { "cil", "nepodstatny" };
            int[] pocet = { 10, 20 };
            ukol = new UkolKomp("zabit", "vzorový úkol bla\nbla\nbla", pocet, cile);
            ukol.PridejSebrani("dulezity", 5);

            this.Controls.Add(ukol.UI.pozadi);

            hrac = new PostavaKomp("hrac", 2, 100, null, null);
            hrac.Postava.Zabil += Zabil; ;

            inventar = new InventarKompV2(20.0,button4.Right+50,0);
            this.Controls.Add(inventar.GFX.pozadi);
            inventar.Invent.Pridan += Sebran;
        }

        private void Sebran(object sender, KnihovnaRPG.IPredmet e)
        {
            ukol.Ukol.UpdateStav(e.Jmeno);
            ukol.UI.Text = ukol.Ukol.ToString();
        }

        private void Zabil(object sender, KnihovnaRPG.Postava e)
        {
            ukol.Ukol.UpdateStav(e.Jmeno);
            ukol.UI.Text = ukol.Ukol.ToString();
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            PostavaKomp cil = new PostavaKomp("cil", 1, 10, null, null);            
            cil.Postava.Zraneni(hrac.Postava, 20, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PostavaKomp nepodstat = new PostavaKomp("nepodstatny", 1, 10, null, null);
            nepodstat.Postava.Zraneni(hrac.Postava, 20, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PredmetKomp dulezity = new PredmetKomp("dulezity", 10, 1, null);
            inventar.Pridej(dulezity);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PredmetKomp zbytecnost = new PredmetKomp("zbytecnost", 10, 1, null);
            inventar.Pridej(zbytecnost);
        }
    }
}
