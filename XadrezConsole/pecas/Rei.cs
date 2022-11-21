using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.pecas
{
    internal class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] MovimentosPossiveis(Posicao posicao)
        {
            bool[,] MovimentosPossiveis = new bool[Tabuleiro.DimensaoDoTabuleiro[0], Tabuleiro.DimensaoDoTabuleiro[1]];

            posicao = new Posicao(0, 0);

            Posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
            {
                MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true; 
            }
            Posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
            {
                MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true; 
            }
            Posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
            {
                MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true; 
            }
            Posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
            {
                MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true; 
            }

            Posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
            {
                MovimentosPossiveis[Posicao.Linha + 1, Posicao.Coluna -1] = true; 
            }
            Posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
            {
                MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true; 
            }
            Posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
            {
                MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true; 
            }
            Posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
            {
                MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true; 
            }

            return MovimentosPossiveis;
        }

        public override string ToString()
        {
            return "R";
        }


    }
}
