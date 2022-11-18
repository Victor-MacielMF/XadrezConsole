using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole.pecas
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro();
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
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
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez("C1").TextoParaPosicao(Tabuleiro));
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez("C2").TextoParaPosicao(Tabuleiro));
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez("D2").TextoParaPosicao(Tabuleiro));
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez("E2").TextoParaPosicao(Tabuleiro));
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez("E1").TextoParaPosicao(Tabuleiro));
            Tabuleiro.ColocarPeca(new Rei(Cor.Branca, Tabuleiro), new PosicaoXadrez("D1").TextoParaPosicao(Tabuleiro));

            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez("C7").TextoParaPosicao(Tabuleiro));
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez("C8").TextoParaPosicao(Tabuleiro));
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez("D7").TextoParaPosicao(Tabuleiro));
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez("E7").TextoParaPosicao(Tabuleiro));
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez("E8").TextoParaPosicao(Tabuleiro));
            Tabuleiro.ColocarPeca(new Rei(Cor.Preta, Tabuleiro), new PosicaoXadrez("D8").TextoParaPosicao(Tabuleiro));

        }
    }
}
