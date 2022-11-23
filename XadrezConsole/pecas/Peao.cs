using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.pecas
{
    internal class Peao : Peca
    {
        private PartidaDeXadrez Partida;
        public Peao(Cor cor, Tabuleiro tabuleiro, PartidaDeXadrez partida) : base(cor, tabuleiro)
        {
            Partida = partida;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MovimentosPossiveis = new bool[Tabuleiro.DimensaoDoTabuleiro[0], Tabuleiro.DimensaoDoTabuleiro[1]];
            Posicao PosicaoMovimento = new Posicao(0, 0);
            Posicao PosicaoCaptura = new Posicao(0, 0);
            Posicao PosicaoPassant = new Posicao(0, 0);

            int[,] TodosMovimentosPadrao = new int[4, 2]
            {
                {PosicaoAtual.Linha - 1, PosicaoAtual.Coluna }, // inicio de jogo branca 
                {PosicaoAtual.Linha - 2, PosicaoAtual.Coluna }, //Inicio de  jogo branca dupla

                {PosicaoAtual.Linha + 1, PosicaoAtual.Coluna },// Inicio de jogo preta 
                {PosicaoAtual.Linha + 2, PosicaoAtual.Coluna } //Inicio de  jogo Preta dupla
            };

            int[,] TodosMovimentosCaptura = new int[4, 2]
            {
                {PosicaoAtual.Linha - 1, PosicaoAtual.Coluna -1}, //Tem peca inimiga à esquerda da branca
                {PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1}, // Tem peca inimiga à direita da branca

                {PosicaoAtual.Linha + 1, PosicaoAtual.Coluna -1}, //Tem peca inimiga à esquerda da Preta
                {PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1} // Tem peca inimiga à direita da Preta
            };
            int[,] TodosMovimentosPassant = new int[4, 2]
            {
                { 3, PosicaoAtual.Coluna - 1 }, //Tem peca inimiga à esquerda da branca
                { 3, PosicaoAtual.Coluna + 1 }, // Tem peca inimiga à direita da branca

                { 4, PosicaoAtual.Coluna - 1 }, //Tem peca inimiga à esquerda da Preta
                { 4, PosicaoAtual.Coluna + 1 } // Tem peca inimiga à direita da Preta
            };

            //Aqui eu armazeno onde inicia e onde acaba  o array para delimitar as duas matrizes= TodosMovimentosPadrao & TodosMovimentosCaptura no for abaixo.
            int[] BrancaOuPreta = { Cor == Cor.Branca ? 0 : 2, Cor == Cor.Branca ? 1 : 3 };
            //Uso para saber se tem peça bloqueando o caminho, para não permitir que o peão pule uma peça no movimento especial de inicio.
            bool MovimentoRestringido = false;
            for (int i = BrancaOuPreta[0]; i <= BrancaOuPreta[1]; i++)
            {
                PosicaoMovimento.DefinirValores(TodosMovimentosPadrao[i, 0], TodosMovimentosPadrao[i, 1]);
                PosicaoCaptura.DefinirValores(TodosMovimentosCaptura[i, 0], TodosMovimentosCaptura[i, 1]);
                PosicaoPassant.DefinirValores(TodosMovimentosPassant[i, 0], TodosMovimentosPassant[i, 1]);

                Peca pecaCaptura = null;

                if (Tabuleiro.PosicaoValida(PosicaoCaptura))
                {
                    pecaCaptura = Tabuleiro.PosicaoTabuleiro(PosicaoCaptura);
                }
                //Verifica se a quantidade de movimentos é superior a um, para bloquear junto
                if (Tabuleiro.PosicaoValida(PosicaoMovimento) && !Tabuleiro.ExistePeca(PosicaoMovimento)  && 
                    PrimeiroMovimentoEspecial(i, BrancaOuPreta) && MovimentoRestringido == false)
                {
                    MovimentosPossiveis[PosicaoMovimento.Linha, PosicaoMovimento.Coluna] = true;
                }
                else
                {
                    MovimentoRestringido = true;
                }

                if (Tabuleiro.PosicaoValida(PosicaoCaptura) && pecaCaptura != null && pecaCaptura.Cor != Cor)
                {
                    MovimentosPossiveis[PosicaoCaptura.Linha, PosicaoCaptura.Coluna] = true;
                }

                if (Tabuleiro.PosicaoValida(PosicaoPassant))
                {
                    Peca PecaTabuleiroPassant = Partida.PecaVulneravelPassant;
                    Peca PecaPassant = Tabuleiro.PosicaoTabuleiro(PosicaoPassant);
                    if ((PecaPassant != null && PecaPassant.Cor != Cor) && (PecaTabuleiroPassant == PecaPassant))
                    {
                        MovimentosPossiveis[PosicaoCaptura.Linha, PosicaoCaptura.Coluna] = true;
                    }
                }
            }

            return MovimentosPossiveis;

        }

        private bool PrimeiroMovimentoEspecial(int index, int[] BrancaOuPreta)
        {
            return (index == BrancaOuPreta[0] || (QteMovimentos == 0 && index == BrancaOuPreta[1]));
        }
        public override string ToString()
        {
            return "P";
        }
    }
}

/*                if (PosicaoAtual.Linha == 3)
                {
                    Posicao esquerda = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.PosicaoTabuleiro(esquerda) == Partida.PecaVulneravelPassant)
                    {
                        MovimentosPossiveis[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && ExisteInimigo(direita) && Tabuleiro.PosicaoTabuleiro(direita) == Partida.PecaVulneravelPassant)
                    {
                        MovimentosPossiveis[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
*/