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
            : base(jmeno, mapa, hraci, polohaHraci, NPC, polohaNPC)
        { }

        public UlozenyPostup() { }

        public override void Uloz()
        {
            MapaConfig.Velikost chunk = (GameManager.Singleton.Nastaveni["mapa"] as MapaConfig).Chunk;
            string mapaHlavicka = $"{Mapa.X};{Mapa.Y};{chunk.X};{chunk.Y};";
            string mapa = $"{mapaHlavicka}{Mapa.SaveStream()}";

            StringBuilder hraci = new StringBuilder();
            for (int i = 0; i < Hraci.Length; i++)
            {
                hraci.Append($"{PolohaHracu[i].SaveStream()};{Hraci[i].SaveStream()}@");
            }

            StringBuilder postavy = new StringBuilder();
            if (Postavy.Length != PolohaPostav.Length) { throw new Exception("rozdilny pocet NPC a poloh"); }
            for (int i = 0; i < Postavy.Length; i++)
            {
                postavy.Append($"{PolohaPostav[i].SaveStream()};{Postavy[i].SaveStream()}@");
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
            useky[0] = null;
            nactiPostavy(useky[2],'h');
            nactiPostavy(useky[3], 'p');
        }
        void nactiMapu(string stream)
        {
            string[] split = stream.Split(';');
            int MX = Convert.ToInt32(split[0]);
            int MY = Convert.ToInt32(split[1]);
            int CX = Convert.ToInt32(split[2]);
            int CY = Convert.ToInt32(split[3]);
            string data = split[4];
            split = null;

            Dictionary<char, int> lok = new Dictionary<char, int>();
            lok.Add('↑',0);
            lok.Add('A', 1);
            lok.Add('|', 2);
            lok.Add('H', 3);

            Chunk[,] mapa = new Chunk[MX, MY];

            int x = 0;
            int y = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 'ø')
                {
                    mapa[x, y] = null;
                }
                else
                {
                    mapa[x, y] = nactiChunk(data, ref i, CX, CY,lok);
                }

                x++;
                if (x == MX)
                {
                    x = 0;
                    y++;
                }
            }

            Mapa = new Mapa(MX, MY, mapa);
        }
        Chunk nactiChunk(string stream, ref int i, int CX, int CY,Dictionary<char,int> lok)
        {
            Lokace[,] lokace = new Lokace[CX, CY];
            GameManager GM = GameManager.Singleton;
            
            for (int y = 0; y < CY; y++)
            {
                for (int x = 0; x < CX; x++)
                {
                    char key = stream[i];
                    lokace[x, y] = GM.Lokace[lok[key]];
                    i++;
                }
            }
            i--;
            return new Chunk(CX, CY, lokace);
        }

        void nactiPostavy(string stream,char typ)
        {
            string[] postavy = stream.Split('@');

            if (typ == 'p')
            {
                Postavy = new Postava[postavy.Length - 1];
                PolohaPostav = new Point4D[postavy.Length - 1];
            }
            else
            {
                Hraci = new Hrac[postavy.Length - 1];
                PolohaHracu = new Point4D[postavy.Length - 1];
            }

            for(int i=0;i<postavy.Length-1;i++)
            {
                nactiPostavu(postavy[i],typ,i);
                postavy[i] = null;
            }
        }
        void nactiPostavu(string stream, char typ,int i)
        {
            string[] split = stream.Split(';');
            int MX = Convert.ToInt32(split[0]);
            int MY = Convert.ToInt32(split[1]);
            int CX = Convert.ToInt32(split[2]);
            int CY = Convert.ToInt32(split[3]);
            Point4D poloha = new Point4D(MX, MY, CX, CY);

            //return $"{Jmeno};{LV};{HP}/{MaxHP};{staty};{nezr}";
            string jmeno = split[4];
            int lv = Convert.ToInt32(split[5]);

            string[] hp = split[6].Split('/');
            int HP = Convert.ToInt32(hp[0]);
            int maxHP = Convert.ToInt32(hp[1]);
            hp = null;

            string statStream = split[7];
            StatList list = nactiStatList(split,7);

            string nezr = split[typ=='h'? split.Length - 3 : split.Length - 1];
            Boolean nezran = nezr == "T";

            if (typ == 'p')
            {
                
                Postava p = new Postava(jmeno, lv, HP, maxHP, list, nezran);
                Postavy[i]= p;
                PolohaPostav[i] = poloha;
            }
            else
            {
                int exp = Convert.ToInt32(split[split.Length - 2]);
                int gold = Convert.ToInt32(split[split.Length - 1]);

                Hrac h = new Hrac(jmeno, lv, HP, maxHP, list,nezran,exp, gold);
                Hraci[i] = h;
                PolohaHracu[i] = poloha;
            }
        }
        StatList nactiStatList(string[] stream,int i)
        {
            if (stream[i] == "-")
            {
                return null;
            }
            List<Stat> ret = new List<Stat>();
            while (stream[i] != "")
            {
                string jm = stream[i];
                double zak = Convert.ToDouble(stream[i + 1]);
                double bk=Convert.ToDouble(stream[i + 2]);
                float bp= Convert.ToSingle(stream[i + 3]);
                Stat s = new Stat(jm, zak,bk,bp);

                ret.Add(s);
                i += 4;
            }

            return new StatList(ret);
        }
    }
}
