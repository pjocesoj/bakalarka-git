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
        UkolKomp ukol;
        private void UkolForm_Load(object sender, EventArgs e)
        {
            //ukol = new UkolKomp("zabit", "vzorový úkol bla\nbla\nbla",10,"cil");
            string[] cile = { "cil", "nepodstatny" };
            int[] pocet = { 10, 20 };
            ukol = new UkolKomp("zabit", "vzorový úkol bla\nbla\nbla", pocet, cile);
            this.Controls.Add(ukol.UI.pozadi);

            hrac = new PostavaKomp("hrac", 2, 100, null, null);
            hrac.Postava.Zabil += Zabil; ;
        }

        private void Zabil(object sender, KnihovnaRPG.Postava e)
        {
            ukol.Ukol.UpdateStav(e.Jmeno);
            ukol.UI.Text = ukol.Ukol.ToString();
        }

        PostavaKomp hrac;

        private void button1_Click(object sender, EventArgs e)
        {
            PostavaKomp cil = new PostavaKomp("cil", 1, 10, null, null);            
            cil.Postava.Zraneni(hrac.Postava, 20, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PostavaKomp cil = new PostavaKomp("nepodstatny", 1, 10, null, null);
            cil.Postava.Zraneni(hrac.Postava, 20, null);
        }
    }
}
