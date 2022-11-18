using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.pecas
{
    internal class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] PosicoesPossiveis = new bool[Tabuleiro.DimensaoDoTabuleiro[0], Tabuleiro.DimensaoDoTabuleiro[1]];
            return PosicoesPossiveis;
        }
    }
}