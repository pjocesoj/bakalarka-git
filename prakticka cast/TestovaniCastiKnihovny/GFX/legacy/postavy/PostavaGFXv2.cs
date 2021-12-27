using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;
using System.Windows.Forms;
using System.Drawing;

namespace TestovaniCastiKnihovny
{
    public class PostavaGFXv2
    {
        Postava logika;
        PictureBox pictureBox = new PictureBox();
        public PostavaGFXv2(int sirka, int vyska)
        {
            pictureBox.Width = sirka;
            pictureBox.Height = vyska;
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox.BackColor = Color.White;
        }
        public PostavaGFXv2(string jmeno, int lv, int HP, KnihovnaRPG.StatList statList, Bitmap obr, int sirka=100, int vyska=100) : this(sirka,vyska)
        {
            logika = new Postava(jmeno, lv, HP, statList);
            pictureBox.Image = obr;
        }



        public PictureBox GFX
        {
            get
            {
                return pictureBox;
            }
        }
        public Postava postava
        {
            get { return logika; }
        }
        #region get set logika
        public string Jmeno
        {
            get { return logika.Jmeno; }
        }
        public int LV
        {
            get { return logika.LV; }
        }
        public int HP
        {
            get { return logika.HP; }
            set { logika.HP = value; }
        }
        public int MaxHP
        {
            get { return logika.MaxHP; }
        }
        public StatList Staty
        {
            get { return logika.Staty; }
        }
        
        #endregion

        public void zraneni(Postava utocnik, double DMG, string obrana)
        {
            logika.Zraneni(utocnik, DMG, obrana);
        }
    }
}

