using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.pecas
{
    internal class Rei : Peca
    {
        public PartidaDeXadrez Partida { get; private set; }
        public Rei(Cor cor, Tabuleiro tabuleiro, PartidaDeXadrez partida) : base(cor, tabuleiro)
        {
            Partida = partida;
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
                Posicao.DefinirValores(TodosMovimentosPeca[i, 0], TodosMovimentosPeca[i, 1]);
                if (Tabuleiro.PosicaoValida(Posicao) && PodeMover(Posicao))
                {
                    MovimentosPossiveis[Posicao.Linha, Posicao.Coluna] = true;
                }
            }

            //RoquePequeno
            if (QteMovimentos == 0)
            {
                Peca PossivelTorreDireita = Tabuleiro.PosicaoTabuleiro(new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 3));
                if (RoqueElegivel(PossivelTorreDireita)) // Verifico se o Rei é elegivel. dps verifico se a torre é.
                {
                    Posicao ColunaF = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
                    Posicao ColunaG = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 2);
                    if (Tabuleiro.PosicaoTabuleiro(ColunaF) == null && Tabuleiro.PosicaoTabuleiro(ColunaG) == null)
                    {
                        MovimentosPossiveis[PosicaoAtual.Linha, PosicaoAtual.Coluna + 2] = true;
                    }
                }

                //RoqueGrande
                Peca PossivelTorreEsquerda = Tabuleiro.PosicaoTabuleiro(new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 4));
                if (RoqueElegivel(PossivelTorreEsquerda)) // Verifico se o Rei é elegivel. dps verifico se a torre é.
                {
                    Posicao ColunaD = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
                    Posicao ColunaC = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 2);
                    Posicao ColunaB = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 3);
                    if (Tabuleiro.PosicaoTabuleiro(ColunaD) == null && Tabuleiro.PosicaoTabuleiro(ColunaC) == null && Tabuleiro.PosicaoTabuleiro(ColunaB) == null)
                    {
                        MovimentosPossiveis[PosicaoAtual.Linha, PosicaoAtual.Coluna - 2] = true;
                    }
                }
            }
            return MovimentosPossiveis;
        }

        public bool RoqueElegivel(Peca possivelTorre)
        {
            return !Partida.PartidaEstaEmXeque &&
                   (possivelTorre != null && possivelTorre is Torre && possivelTorre.QteMovimentos == 0 && possivelTorre.Cor == Cor);
        }
        public override string ToString()
        {
            return "R";
        }


    }
}
