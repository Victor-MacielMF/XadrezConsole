using System.Diagnostics;
using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.pecas
{
    internal class Bispo : Peca
    {
        public Bispo(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) {}

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MovimentosPossiveis = new bool[Tabuleiro.DimensaoDoTabuleiro[0], Tabuleiro.DimensaoDoTabuleiro[1]];

            Posicao Posicao = new Posicao(0, 0);

            int[,] TodosMovimentosPecaDois = new int[4, 2]
            {
                { PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1 }, { PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1 },//NE - SE
                { PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1 }, { PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1 } //SO - NO
            };

            for (int i = 0; i < TodosMovimentosPecaDois.GetLength(0); i++)
            {
                Posicao.DefinirValores(TodosMovimentosPecaDois[i, 0], TodosMovimentosPecaDois[i, 1]);
                while (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
                {
                    MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true;

                    //Este switch é para fazer o bispo percorrer todos os caminhos, caso o if acima não o faça parar.
                    switch (i)
                    {
                        case 0:
                            Posicao.Linha -= 1;
                            Posicao.Coluna += 1;
                            break;
                        case 1:
                            Posicao.Linha += 1;
                            Posicao.Coluna += 1;
                            break;
                        case 2:
                            Posicao.Linha += 1;
                            Posicao.Coluna -= 1;
                            break;
                        case 3:
                            Posicao.Linha -= 1;
                            Posicao.Coluna -= 1;
                            break;
                    }
                }
            }

            return MovimentosPossiveis;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
