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
            KnihovnaRPG.MapaConfig conf = (KnihovnaRPG.MapaConfig)GameManager.Singleton.Nastaveni["mapa"];
            int chunk = 150;
            int x = conf.Chunk.X;
            int lok = chunk / x;

            GameManager gm = GameManager.Singleton;
            if (gm.Mapa == null)
            {
                gm.SpustHru(1);
            }
            mapa = new MapaKomp(gm.Mapa, 0, 0, chunk, chunk);
            this.Controls.Add(mapa.GFX.pozadi);
            mapa.vykresli();
            this.WindowState = FormWindowState.Maximized;

            KnihovnaRPG.Point4D spawn = gm.PolohaHrac;
            int X = spawn.MX * chunk + spawn.CX * lok;
            int Y = spawn.MY * chunk + spawn.CY * lok;

            this.KeyPreview = true;
            this.KeyUp += GMMapaForm_KeyUp;
            hrac = new PictureBox();
            hrac.Width = lok;
            hrac.Height = lok;
            hrac.Left = X;
            hrac.Top = Y;
            hrac.BackColor = Color.Red;
            hrac.SizeMode = PictureBoxSizeMode.StretchImage;
            mapa.GFX.pozadi.Controls.Add(hrac);
            hrac.BringToFront();

            policko = lok;
        }
        MapaKomp mapa;
        PictureBox hrac;
        int policko;

        private void GMMapaForm_KeyUp(object sender, KeyEventArgs e)
        {
            NastaveniOvladani ovladani = (NastaveniOvladani)GameManager.Singleton.Nastaveni["ovladani"];
            if (e.KeyCode == ovladani.Nahoru) { krok(0, -1); }
            if (e.KeyCode == ovladani.Dolu) { krok(0, 1); }
            if (e.KeyCode == ovladani.Doleva) { krok(-1, 0); }
            if (e.KeyCode == ovladani.Doprava) { krok(1, 0); }
        }

        void krok(int x, int y)
        {
            try
            {
                KnihovnaRPG.Point4D poloha = GameManager.Singleton.PolohaHrac;
                KnihovnaRPG.MapaConfig conf = (KnihovnaRPG.MapaConfig)GameManager.Singleton.Nastaveni["mapa"];
                poloha.Pohyb(x, y, conf);
                //GameManager.Singleton.PohybHrace.Invoke(hrac,poloha);
                GameManager.Singleton.KrokHnadler(0, poloha);
                mapa.vykresli();

                hrac.Left += x * policko;
                hrac.Top += y * policko;
            }
            catch { }
        }
    }
}
