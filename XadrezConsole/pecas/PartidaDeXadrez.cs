using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.pecas
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        private int Turno;
        private Cor JogadorAtual;

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro();
            Turno = 1;
            JogadorAtual = Cor.Branca;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao Origem, Posicao Destino)
        {
            Peca Peca = Tabuleiro.RetirarPeca(Origem);
            Peca.IncrementarQtdeMovimentos();
            Peca PecaCapturada = Tabuleiro.RetirarPeca(Destino);
            Tabuleiro.ColocarPeca(Peca, Destino);
        }

        private void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Rei(Cor.Preta, Tabuleiro), Tabuleiro.TextoParaPosicao('d',8));
            Tabuleiro.ColocarPeca(new Rei(Cor.Preta, Tabuleiro), Tabuleiro.TextoParaPosicao('e', 8));
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), Tabuleiro.TextoParaPosicao('c', 8));
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), Tabuleiro.TextoParaPosicao('f', 8));

            Tabuleiro.ColocarPeca(new Rei(Cor.Branca, Tabuleiro), Tabuleiro.TextoParaPosicao('d', 1));
            Tabuleiro.ColocarPeca(new Rei(Cor.Branca, Tabuleiro), Tabuleiro.TextoParaPosicao('e', 1));
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), Tabuleiro.TextoParaPosicao('c', 1));
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), Tabuleiro.TextoParaPosicao('f', 1));

        }
    }
}
