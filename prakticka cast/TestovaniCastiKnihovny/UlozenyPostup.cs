using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;
using System.IO;

namespace TestovaniCastiKnihovny
{
    class UlozenyPostup : Ulozeni
    {
        public UlozenyPostup(string jmeno, Mapa mapa, Hrac[] hraci, Point4D[] polohaHraci, Postava[] NPC, Point4D[] polohaNPC) 
            : base(jmeno, mapa,hraci,polohaHraci,NPC,polohaNPC)
        { }

        public UlozenyPostup() { }

        public override void Uloz()
        {
            MapaConfig.Velikost chunk = (GameManager.Singleton.Nastaveni["mapa"] as MapaConfig).Chunk;
            string mapaHlavicka=$"{Mapa.X};{Mapa.Y};{chunk.X};{chunk.Y}:";
            string mapa = $"{mapaHlavicka}{Mapa.SaveStream()}";

            StringBuilder hraci = new StringBuilder();
            for(int i=0;i<Hraci.Length;i++)
            {
                hraci.AppendLine($"{PolohaHracu[i].SaveStream()}:{Hraci[i].SaveStream()}");
            }

            StringBuilder postavy = new StringBuilder();
            if (Postavy.Length != PolohaPostav.Length) { throw new Exception("rozdilny pocet NPC a poloh"); }
            for (int i = 0; i < Postavy.Length; i++)
            {
                postavy.AppendLine($"{PolohaPostav[i].SaveStream()}:{Postavy[i].SaveStream()}");
            }

            string saveStream = $"{mapa}\n-\n{hraci}-\n{postavy}";
            using (FileStream fs = new FileStream($"{Nazev}.save", FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(saveStream);
                sw.Close();
            }
        }
        
        public override void Nacti(string saveStream)
        {
            string[] useky = saveStream.Split('\n');
            nactiMapu(useky[0]);
        }
        void nactiMapu(string stream)
        {
            string[] data = stream.Split(';');
            int MX = Convert.ToInt32(data[0]);
            int MY = Convert.ToInt32(data[1]);
            int CX = Convert.ToInt32(data[2]);
            int CY = Convert.ToInt32(data[3]);

            Mapa = new Mapa(MX, MY);
        }
    }
}
