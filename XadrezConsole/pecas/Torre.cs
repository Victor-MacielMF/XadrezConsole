using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.pecas
{
    internal class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MovimentosPossiveis = new bool[Tabuleiro.DimensaoDoTabuleiro[0], Tabuleiro.DimensaoDoTabuleiro[1]];

            Posicao posicao = new Posicao(0, 0);

            //Refatorar esta parte
            posicao.DefinirValores(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.ExistePeca(posicao))
                {
                    break;
                }

                posicao.Linha -= 1;
            }

            posicao.DefinirValores(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.ExistePeca(posicao))
                {
                    break;
                }

                posicao.Linha += 1;
            }

            posicao.DefinirValores(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.ExistePeca(posicao))
                {
                    break;
                }

                posicao.Coluna += 1;
            }

            posicao.DefinirValores(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                MovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.ExistePeca(posicao))
                {
                    break;
                }

                posicao.Coluna -= 1;
            }

            return MovimentosPossiveis;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}