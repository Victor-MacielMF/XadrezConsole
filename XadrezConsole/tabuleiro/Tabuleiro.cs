using System.Text;
using XadrezConsole.tabuleiro.exceptions;

namespace XadrezConsole.tabuleiro
{
    internal class Tabuleiro
    {
        // {0} Linha x {1} Coluna
        private int ValorPadrao = 8;
        public int[] DimensaoDoTabuleiro { get; set; } = new int[2];
        public Peca[,] Pecas { get; private set; }


        public Tabuleiro()
        {
            DimensaoDoTabuleiro[0] = ValorPadrao;
            DimensaoDoTabuleiro[1] = ValorPadrao;
            Pecas = new Peca[ValorPadrao, ValorPadrao];
        }

        public Posicao TextoParaPosicao(char coluna, int linha)
        {
            return new Posicao(DimensaoDoTabuleiro[0] - linha, coluna - 'a');
        }
        public Peca PosicaoTabuleiro(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca PosicaoTabuleiro(Posicao posicao)
        {
            return Pecas[posicao.Linha, posicao.Coluna];
        }
        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
            {
                throw new TabuleiroException("Já existe uma peça nesta posição.");
            }

            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.PosicaoAtual = posicao;
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            return PosicaoTabuleiro(posicao) != null;
        }
        public void ValidarPosicao(Posicao posicao)
        {
            if (!PosicaoValida(posicao))
            {
                throw new TabuleiroException("Posição inválida.");
            }
        }

        public bool PosicaoValida(Posicao posicao)
        {
            return ((posicao.Linha >= 0 && posicao.Linha <= DimensaoDoTabuleiro[0]) &&
                    (posicao.Coluna >= 0 && posicao.Coluna <= DimensaoDoTabuleiro[1]));
        }
    }
}
