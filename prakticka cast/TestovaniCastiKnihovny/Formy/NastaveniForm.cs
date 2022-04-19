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
    public partial class NastaveniForm : Form
    {
        public NastaveniForm()
        {
            InitializeComponent();
        }

        private void NastaveniForm_KeyDown(object sender, KeyEventArgs e)
        {
            NastaveniOvladani ovladani=(NastaveniOvladani)GameManager.Singleton.Nastaveni["ovladani"];
            if (e.KeyCode == ovladani.Nahoru) { pictureBox1.Top -= 5; }
            if (e.KeyCode == ovladani.Dolu) { pictureBox1.Top += 5; }
            if (e.KeyCode == ovladani.Doleva) { pictureBox1.Left -= 5; }
            if (e.KeyCode == ovladani.Doprava) { pictureBox1.Left += 5; }
        }

        private void NastaveniForm_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KnihovnaRPG.INastaveni n in GameManager.Singleton.Nastaveni.Values)
            {
                sb.AppendLine(n.Vypis());
                sb.AppendLine("---------");
            }
            label1.Text = sb.ToString();
            label1.Font= new Font("consolas", 10);

            rozmerPB();
        }
        void rozmerPB()
        {
            NastaveniGrafika gr = (NastaveniGrafika)GameManager.Singleton.Nastaveni["grafika"];
            pictureBox1.Width = gr.Sirka;
            pictureBox1.Height = gr.Vyska;
        }
    }
}
