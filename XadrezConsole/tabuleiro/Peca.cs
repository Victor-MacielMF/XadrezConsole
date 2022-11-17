using System.Text;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.tabuleiro
{
    internal class Peca
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
    }
}
