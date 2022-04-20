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
    public partial class GMMapaForm : Form
    {
        public GMMapaForm()
        {
            InitializeComponent();
        }

        private void GMMapaForm_Load(object sender, EventArgs e)
        {
            GameManager gm = GameManager.Singleton;
            gm.SpustHru();
            MapaKomp mapa = new MapaKomp(gm.Mapa, 0, 0,150,150);
            this.Controls.Add(mapa.GFX.pozadi);
            mapa.vykresli();
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
