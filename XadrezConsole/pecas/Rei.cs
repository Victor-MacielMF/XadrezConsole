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

            Posicao Posicao = new Posicao(0, 0);

            //Aqui estou armazenando todas as posições possiveis que o Rei pode fazer.
            int[,] TodosMovimentosPeca = new int[8, 2]
            {
                { PosicaoAtual.Linha - 1, PosicaoAtual.Coluna }, { PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1 },
                { PosicaoAtual.Linha, PosicaoAtual.Coluna + 1 }, { PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1 }, 
                { PosicaoAtual.Linha + 1, PosicaoAtual.Coluna }, { PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1 }, 
                { PosicaoAtual.Linha, PosicaoAtual.Coluna - 1 }, { PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1 }
            };

            //Aqui estou verificando se a posição é valida e se o rei pode se mover para a posição.
            for (int i = 0; i < TodosMovimentosPeca.GetLength(0); i++)
            {
                Posicao.DefinirValores(TodosMovimentosPeca[i,0], TodosMovimentosPeca[i, 1]);
                if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
                {
                    MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true;
                }
            }

            return MovimentosPossiveis;
        }

        public override string ToString()
        {
            return "R";
        }


    }
}
