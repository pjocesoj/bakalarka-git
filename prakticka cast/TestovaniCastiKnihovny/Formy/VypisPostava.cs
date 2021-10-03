﻿using System;
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
    public partial class VypisPostava : Form
    {
        public VypisPostava()
        {
            InitializeComponent();
        }

        private void VypisPostava_Load(object sender, EventArgs e)
        {
            Bitmap obr = (Bitmap)Image.FromFile("obrazky//stickman.png");
            PostavaGFX zakladni = new PostavaGFX("base",obr);
            Zobraz(zakladni, 0);

            List<KnihovnaRPG.Stat> temp = new List<KnihovnaRPG.Stat>();
            temp.Add(new KnihovnaRPG.Stat("kinetic defence", 2));
            //temp.Add(new KnihovnaRPG.Stat("kinetic defence", 4));
            temp.Add(new KnihovnaRPG.Stat("kinetic damage","kDMG", 4));
            temp.Add(new KnihovnaRPG.Stat("damage", "DMG", 6));
            KnihovnaRPG.StatList list = new KnihovnaRPG.StatList(temp);

            PostavaGFX zakladni2 = new PostavaGFX("base+staty",1,10,list, obr);
            Zobraz(zakladni2, 100);
        }

        void Zobraz(PostavaGFX postava,int left)
        {
            #region vytvoreni controls
            Panel p = new Panel();
            p.Width = 100;
            p.Height = 300;
            p.BorderStyle = BorderStyle.FixedSingle;

            PictureBox pb = new PictureBox();
            pb.Width = 100;
            pb.Height = 100;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            Label lab = new Label();
            lab.Height = 200;
            lab.Top = pb.Bottom + 5;

            p.Controls.Add(pb);
            p.Controls.Add(lab);

            this.Controls.Add(p);
            p.Left = left;
            #endregion

            pb.Image = postava.vzhled;
            lab.Text = postava.ToString();
        }
    }
}