using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;
using XadrezConsole.tabuleiro.exceptions;

namespace XadrezConsole.pecas
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public HashSet<Peca> PecasEmJogo { get; set; }
        public HashSet<Peca> PecasCaputuradas { get; set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro();
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            PecasEmJogo = new HashSet<Peca>();
            PecasCaputuradas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ValidaPosicaoDeOrigem(Posicao posicao)
        {
            Peca peca = Tabuleiro.PosicaoTabuleiro(posicao);
            if (peca == null)
            {
                throw new TabuleiroException("Não existe peça na posição selecionada.");
            }
            if (JogadorAtual != peca.Cor)
            {
                throw new TabuleiroException("A peça selecionada é do jogador adversário.");
            }

            if (!peca.ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existe movimentos possiveis para esta peça.");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.PosicaoTabuleiro(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("A peça selecionada não pode se mover para o local informado.");
            }
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ExecutaMovimento(Posicao Origem, Posicao Destino)
        {
            Peca Peca = Tabuleiro.RetirarPeca(Origem);
            Peca.IncrementarQtdeMovimentos();
            Peca PecaCapturada = Tabuleiro.RetirarPeca(Destino);
            Tabuleiro.ColocarPeca(Peca, Destino);
            if (PecaCapturada != null)
            {
                PecasCaputuradas.Add(PecaCapturada);
            }
        }

        public void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        private void ColocarPecas()
        {
            InserirNovaPeca("C1", new Torre(Cor.Branca, Tabuleiro));
            InserirNovaPeca("D2", new Torre(Cor.Branca, Tabuleiro));
            InserirNovaPeca("C2", new Torre(Cor.Branca, Tabuleiro));
            InserirNovaPeca("E2", new Torre(Cor.Branca, Tabuleiro));
            InserirNovaPeca("E1", new Torre(Cor.Branca, Tabuleiro));
            InserirNovaPeca("D1", new Rei(Cor.Branca, Tabuleiro));

            InserirNovaPeca("C8", new Torre(Cor.Preta, Tabuleiro));
            InserirNovaPeca("D7", new Torre(Cor.Preta, Tabuleiro));
            InserirNovaPeca("C7", new Torre(Cor.Preta, Tabuleiro));
            InserirNovaPeca("E7", new Torre(Cor.Preta, Tabuleiro));
            InserirNovaPeca("E8", new Torre(Cor.Preta, Tabuleiro));
            InserirNovaPeca("D8", new Rei(Cor.Preta, Tabuleiro));

        }

        private void InserirNovaPeca(string posicao, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(posicao).TextoParaPosicao(Tabuleiro));
            PecasEmJogo.Add(peca);
        }

        public HashSet<Peca> PecasCapturadasCor(Cor cor)
        {
            HashSet<Peca> Auxiliar = new HashSet<Peca>();
            foreach (Peca peca in PecasCaputuradas)
            {
                if (peca.Cor == cor)
                {
                    Auxiliar.Add(peca);
                }
            }

            return Auxiliar;
        }

        public HashSet<Peca> PecasEmJogoCor(Cor cor)
        {
            HashSet<Peca> Auxiliar = new HashSet<Peca>();
            foreach (Peca peca in PecasEmJogo)
            {
                if (peca.Cor == cor)
                {
                    Auxiliar.Add(peca);
                }
            }
            Auxiliar.ExceptWith(PecasCapturadasCor(cor));

            return Auxiliar;
        }
    }  

}
