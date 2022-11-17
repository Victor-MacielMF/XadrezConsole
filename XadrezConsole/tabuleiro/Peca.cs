﻿using System.Text;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.tabuleiro
{
    internal class Peca
    {
        public Posicao PosicaoDaPeca { get; set; }
        public Cor Cor { get; set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set;}

        public Peca(Posicao posicaoDaPeca, Cor cor, Tabuleiro tabuleiro)
        {
            PosicaoDaPeca = posicaoDaPeca;
            Cor = cor;
            Tabuleiro = tabuleiro;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(PosicaoDaPeca.ToString()).Append("Cor: ").AppendLine(Cor.ToString());

            return sb.ToString();
        }
    }
}
