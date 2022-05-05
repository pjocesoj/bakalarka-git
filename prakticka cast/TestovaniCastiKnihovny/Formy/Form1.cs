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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VypisPostava f2 = new VypisPostava();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Combat f3 = new Combat();
            f3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InventarForm f4 = new InventarForm();
            f4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChunkForm f5 = new ChunkForm();
            f5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MapaForm f6 = new MapaForm();
            f6.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BuffForm f7 = new BuffForm();
            f7.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UkolForm f8 = new UkolForm();
            f8.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new NastaveniForm().Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new GMPostavyForm().Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new GMMapaForm().Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new GMUkldaniForm().Show();
        }
    }
}
