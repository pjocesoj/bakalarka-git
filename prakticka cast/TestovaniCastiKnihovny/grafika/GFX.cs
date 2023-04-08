using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TestovaniCastiKnihovny
{
    public class GFX
    {
        PictureBox pictureBox = new PictureBox();
        public GFX(int sirka, int vyska)
        {
            pictureBox.Width = sirka;
            pictureBox.Height = vyska;
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox.BackColor = Color.White;
        }

        public GFX(int sirka, int vyska, Bitmap obr) : this(sirka, vyska)
        {
            pictureBox.Image = obr;
        }

        public PictureBox grafika
        {
            get
            {
                return pictureBox;
            }
        }
        public int Width { get { return pictureBox.Width; } }
        public int Height { get { return pictureBox.Height; } }
    }
}
