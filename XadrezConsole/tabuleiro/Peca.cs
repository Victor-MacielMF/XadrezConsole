using System.Text;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.tabuleiro
{
    abstract class Peca
    {
        public Posicao PosicaoAtual { get; set; }
        public Cor Cor { get; set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Cor cor, Tabuleiro tabuleiro)
        {
            Cor = cor;
            Tabuleiro = tabuleiro;
        }

        public void IncrementarQtdeMovimentos()
        {
            QteMovimentos++;
        }

        public bool PodeMover(Posicao posicao)
        {
            Peca Peca = Tabuleiro.PosicaoTabuleiro(posicao);
            return Peca == null || Peca.Cor != Cor;
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
