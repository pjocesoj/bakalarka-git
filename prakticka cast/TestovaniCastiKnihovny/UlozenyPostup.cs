﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaRPG;

namespace TestovaniCastiKnihovny
{
    class UlozenyPostup : Ulozeni
    {
        public override void Nacti()
        {
            throw new NotImplementedException();
        }

        public override void Uloz()
        {
            MapaConfig.Velikost chunk = (GameManager.Singleton.Nastaveni["mapa"] as MapaConfig).Chunk;
            string mapaHlavicka=$"{Mapa.X};{Mapa.Y};{chunk.X};{chunk.Y}:";
            string mapa = Mapa.SaveStream();
        }
    }
}
