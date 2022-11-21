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

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] movimentosPossiveis = MovimentosPossiveis();
            for (int i = 0; i < Tabuleiro.DimensaoDoTabuleiro[0]; i++)
            {
                for (int j = 0; j < Tabuleiro.DimensaoDoTabuleiro[1]; j++)
                {
                    if (movimentosPossiveis[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool PodeMoverPara(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }
    }
}
