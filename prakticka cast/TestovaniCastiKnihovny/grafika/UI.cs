using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TestovaniCastiKnihovny
{
    class UI
    {
        protected Panel panel;

        public UI(int sirka, int vyska)
        {
            panel = new Panel();
            panel.Width = sirka;
            panel.Height = vyska;
            panel.BackColor = Color.Bisque;
            panel.AutoScroll = true;
        }
        public UI(int left,int top, int sirka, int vyska):this(sirka,vyska)
        {
            panel.Left = left;
            panel.Top = top;
        }

        public Panel pozadi
        {
            get
            {
                return panel;
            }
        }
        public int Width { get { return panel.Width; } }
        public int Height { get { return panel.Height; } }
    }
}
