using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.pecas
{
    internal class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MovimentosPossiveis = new bool[Tabuleiro.DimensaoDoTabuleiro[0], Tabuleiro.DimensaoDoTabuleiro[1]];

            Posicao posicao = new Posicao(0, 0);
            
            
            //Refatorar esta parte
            posicao.DefinirValores(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true; 
            }
            posicao.DefinirValores(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true; 
            }
            posicao.DefinirValores(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true; 
            }
            posicao.DefinirValores(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true; 
            }
            posicao.DefinirValores(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true; 
            }
            posicao.DefinirValores(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true; 
            }
            posicao.DefinirValores(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true; 
            }
            posicao.DefinirValores(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true; 
            }
            
            return MovimentosPossiveis;
        }

        public override string ToString()
        {
            return "R";
        }


    }
}
