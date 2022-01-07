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
            KnihovnaRPG.UkolOdmena odmena = new KnihovnaRPG.UkolOdmena(250, 100, new PredmetKomp("odmena", 1000, 1, null));

            //ukol = new UkolKomp("zabit", "vzorový úkol bla\nbla\nbla",10,"cil");
            string[] cile = { "cil", "nepodstatny" };
            int[] pocet = { 10, 20 };
            ukol = new UkolKomp("zabit", "vzorový úkol bla\nbla\nbla", pocet, cile, odmena);
            ukol.PridejSebrani("dulezity", 5);

            ukol.Ukol.Dokoncen += Ukol_Dokoncen;
            this.Controls.Add(ukol.UI.pozadi);

            hrac = new HracKomp("hrac", 2, 100, null, null);
            hrac.Postava.Zabil += Zabil; ;
            label1.Text = hrac.ToString();

            inventar = new InventarKompV2(20.0, button4.Right + 50, 0);
            this.Controls.Add(inventar.GFX.pozadi);
            inventar.Invent.Pridan += Sebran;
        }

        private void Ukol_Dokoncen(object sender, KnihovnaRPG.IUkolOdmena e)
        {
            (hrac.Postava as KnihovnaRPG.Hrac).PridejExp(e.Exp);
            (hrac.Postava as KnihovnaRPG.Hrac).Penize += (e as KnihovnaRPG.UkolOdmena).Penize;
            inventar.Pridej((e as KnihovnaRPG.UkolOdmena).Predmet);
            
            label1.Text = hrac.ToString();
        }

        void ukolUpdate(string s)
        {
            ukol.Ukol.UpdateStav(s);
            ukol.UI.Text = ukol.Ukol.ToString();     
        }

        private void Sebran(object sender, KnihovnaRPG.IPredmet e)
        {
            ukolUpdate(e.Jmeno);
        }

        private void Zabil(object sender, KnihovnaRPG.Postava e)
        {
            ukolUpdate(e.Jmeno);
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
