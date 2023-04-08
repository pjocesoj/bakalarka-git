using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestovaniCastiKnihovny
{
    class UIVypis:UI
    {
        Label lab;
        public UIVypis(int sirka, int vyska,int okraj=5):base(sirka,vyska)
        {
            lab = new Label();
            lab.Width = Width - 2 * okraj;
            lab.Left = okraj;
            lab.Height = vyska;

            panel.Controls.Add(lab);
        }
        public UIVypis(int left, int top,int sirka, int vyska, int okraj = 5) : this(sirka, vyska,okraj)
        {
            panel.Top = top;
            panel.Left = left;
        }

        public string Text
        {
            get { return lab.Text; }
            set { lab.Text = value; }
        }
    }
}
