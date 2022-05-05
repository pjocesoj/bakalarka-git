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
    public partial class GMUkldaniForm : Form
    {
        public GMUkldaniForm()
        {
            InitializeComponent();
        }
        GameManager GM = GameManager.Singleton;
        private void button1_Click(object sender, EventArgs e)
        {
            GM.SpustHru(2);
            MapaKomp m = new MapaKomp(GM.Mapa,250);
            pictureBox1.Image=m.ObrMapy(pictureBox1.Width,pictureBox1.Height);

            GM.Uloz();
        }
    }
}
