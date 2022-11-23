using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.pecas
{
    internal class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) { }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MovimentosPossiveis = new bool[Tabuleiro.DimensaoDoTabuleiro[0], Tabuleiro.DimensaoDoTabuleiro[1]];
            Posicao PosicaoMovimento = new Posicao(0, 0);
            Posicao PosicaoCaptura = new Posicao(0, 0);

            int[,] TodosMovimentosPadrao = new int[4,2]
            {
                {PosicaoAtual.Linha - 1, PosicaoAtual.Coluna }, // inicio de jogo branca 
                {PosicaoAtual.Linha - 2, PosicaoAtual.Coluna }, //Inicio de  jogo branca dupla

                {PosicaoAtual.Linha + 1, PosicaoAtual.Coluna },// Inicio de jogo preta 
                {PosicaoAtual.Linha + 2, PosicaoAtual.Coluna } //Inicio de  jogo Preta dupla
            };

            int[,] TodosMovimentosCaptura = new int[4,2]
            {
                {PosicaoAtual.Linha - 1, PosicaoAtual.Coluna -1}, //Tem peca inimiga à esquerda da branca
                {PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1}, // Tem peca inimiga à direita da branca

                {PosicaoAtual.Linha + 1, PosicaoAtual.Coluna -1}, //Tem peca inimiga à esquerda da Preta
                {PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1} // Tem peca inimiga à direita da Preta
            };

            //Aqui eu armazeno onde inicia e onde acaba  o array para delimitar as duas matrizes= TodosMovimentosPadrao & TodosMovimentosCaptura no for abaixo.
            int[] BrancaOuPreta = { Cor == Cor.Branca ? 0 : 2, Cor == Cor.Branca ? 1 : 3 };
            //Uso para saber se tem peça bloqueando o caminho, para não permitir que o peão pule uma peça no movimento especial de inicio.
            bool MovimentoRestringido = false;
            for (int i = BrancaOuPreta[0]; i <= BrancaOuPreta[1]; i++)
            {
                PosicaoMovimento.DefinirValores(TodosMovimentosPadrao[i, 0], TodosMovimentosPadrao[i, 1]);
                PosicaoCaptura.DefinirValores(TodosMovimentosCaptura[i, 0], TodosMovimentosCaptura[i, 1]);

                Peca pecaCaptura = null;

                if (Tabuleiro.PosicaoValida(PosicaoCaptura))
                {
                    pecaCaptura = Tabuleiro.PosicaoTabuleiro(PosicaoCaptura);
                }

                if(Tabuleiro.PosicaoValida(PosicaoMovimento) && !Tabuleiro.ExistePeca(PosicaoMovimento) && MovimentoRestringido == false)
                {
                    MovimentosPossiveis[PosicaoMovimento.Linha, PosicaoMovimento.Coluna] = true;
                }
                else
                {
                    MovimentoRestringido = true;
                }

                if(Tabuleiro.PosicaoValida(PosicaoCaptura) && pecaCaptura != null && pecaCaptura.Cor != Cor)
                {
                    MovimentosPossiveis[PosicaoCaptura.Linha, PosicaoCaptura.Coluna] = true;
                }
            }

            return MovimentosPossiveis;

        }

        public override string ToString()
        {
            return "P";
        }
    }
}
