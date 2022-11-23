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

            Posicao Posicao = new Posicao(0, 0);

            //Aqui estou armazenando todas as 4 direções possiveis que a torre pode fazer.
            int[,] TodosMovimentosPeca = new int[4, 2]
                {
                    { PosicaoAtual.Linha - 1, PosicaoAtual.Coluna }, { PosicaoAtual.Linha + 1, PosicaoAtual.Coluna },
                    { PosicaoAtual.Linha, PosicaoAtual.Coluna + 1 }, { PosicaoAtual.Linha, PosicaoAtual.Coluna - 1 }
                };

            for (int i = 0; i < TodosMovimentosPeca.GetLength(0); i++)
            {
                Posicao.DefinirValores(TodosMovimentosPeca[i, 0], TodosMovimentosPeca[i, 1]);
                while (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
                {
                    MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true;

                    //Este switch é para fazer a torre percorrer todos os caminhos, caso o if acima não o faça parar.
                    switch (i)
                    {
                        case 0:
                            Posicao.Linha -= 1;
                            break;
                        case 1:
                            Posicao.Linha += 1;
                            break;
                        case 2:
                            Posicao.Coluna += 1;
                            break;
                        case 3:
                            Posicao.Coluna -= 1;
                            break;
                    }
                }
            }

            return MovimentosPossiveis;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}