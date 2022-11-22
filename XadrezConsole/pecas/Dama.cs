using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XadrezConsole.tabuleiro.enums;
using XadrezConsole.tabuleiro;

namespace XadrezConsole.pecas
{
    internal class Dama : Peca
    {
        public Dama(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) {}

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MovimentosPossiveis = new bool[Tabuleiro.DimensaoDoTabuleiro[0], Tabuleiro.DimensaoDoTabuleiro[1]];

            Posicao Posicao = new Posicao(0, 0);

            //Aqui é os movimentos que ela pode percorrer que nem o Rei
            int[,] TodosMovimentosPecaUm = new int[8, 2]
            {
                { PosicaoAtual.Linha - 1, PosicaoAtual.Coluna }, { PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1 },
                { PosicaoAtual.Linha, PosicaoAtual.Coluna + 1 }, { PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1 },
                { PosicaoAtual.Linha + 1, PosicaoAtual.Coluna }, { PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1 },
                { PosicaoAtual.Linha, PosicaoAtual.Coluna - 1 }, { PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1 }
            };

            //Movimentos Torre + Bisbo
            int[,] TodosMovimentosPecaDois = new int[8, 2]
            {
                { PosicaoAtual.Linha - 2, PosicaoAtual.Coluna }, { PosicaoAtual.Linha + 2, PosicaoAtual.Coluna },// Cima - Baixo
                { PosicaoAtual.Linha, PosicaoAtual.Coluna + 2 }, { PosicaoAtual.Linha, PosicaoAtual.Coluna - 2 },// Direita - Esquerda
                { PosicaoAtual.Linha - 2, PosicaoAtual.Coluna + 2 }, { PosicaoAtual.Linha + 2, PosicaoAtual.Coluna + 2 },//NE - SE
                { PosicaoAtual.Linha + 2, PosicaoAtual.Coluna - 2 }, { PosicaoAtual.Linha - 2, PosicaoAtual.Coluna - 2 } //SO - NO
            };

            //Aqui estou verificando se a posição é valida e se o rei pode se mover para a posição.
            for (int i = 0; i < TodosMovimentosPecaUm.GetLength(0); i++)
            {
                Posicao.DefinirValores(TodosMovimentosPecaUm[i, 0], TodosMovimentosPecaUm[i, 1]);
                if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
                {
                    MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true;
                }
            }

            //Aqui estou verificando se pode se mover que nem o bispo.
            for (int i = 0; i < TodosMovimentosPecaDois.GetLength(0); i++)
            {
                Posicao.DefinirValores(TodosMovimentosPecaDois[i, 0], TodosMovimentosPecaDois[i, 1]);
                while (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
                {
                    MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true;

                    if (Tabuleiro.ExistePeca(Posicao))
                    {
                        break;
                    }

                    //Este switch é para fazer a dama percorrer todos os caminhos, caso o if acima não o faça parar.
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
                        case 4:
                            Posicao.Linha -= 1;
                            Posicao.Coluna += 1;
                            break;
                        case 5:
                            Posicao.Linha += 1;
                            Posicao.Coluna += 1;
                            break;
                        case 6:
                            Posicao.Linha += 1;
                            Posicao.Coluna -= 1;
                            break;
                        case 7:
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
            return "D";
        }
    }
}
