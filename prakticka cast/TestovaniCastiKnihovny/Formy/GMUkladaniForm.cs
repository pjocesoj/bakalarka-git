using KnihovnaRPG;
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
    public partial class GMUkladaniForm : Form
    {
        public GMUkladaniForm()
        {
            InitializeComponent();
        }
        GameManager GM = GameManager.Singleton;
        private void button1_Click(object sender, EventArgs e)
        {
            GM.SpustHru(2,false);

            vypis();

            GM.Uloz("save1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GM.Nacti("save1");

            vypis();
        }

        void vypis()
        {
            MapaKomp m = new MapaKomp(GM.Mapa, 250);
            pictureBox1.Image = m.ObrMapy(pictureBox1.Width, pictureBox1.Height);

            label1.Text = postavyToString(GM.NPC, GM.PolohaNPC);
            label2.Text = postavyToString(GM.Hraci, GM.PolohaHracu);
        }

        string postavyToString(IList<Postava> postavy,IList<Point4D> polohy)
        {            
            StringBuilder sb = new StringBuilder();
            for(int i=0;i< postavy.Count;i++) 
            {
                sb.AppendLine($"{{{polohy[i].SaveStream()}}}\n{postavy[i]}----------");
            }
            return sb.ToString();
        }
    }
}
