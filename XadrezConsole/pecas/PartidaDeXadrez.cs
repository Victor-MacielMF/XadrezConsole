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
        public bool PartidaEstaEmXeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro();
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            PecasEmJogo = new HashSet<Peca>();
            PecasCaputuradas = new HashSet<Peca>();
            PartidaEstaEmXeque = false;
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
            Peca PecaCapturada = ExecutaMovimento(origem, destino);

            if (JogadorEstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, PecaCapturada);
                throw new TabuleiroException("Você não pode executar um movimento que te coloque em xeque.");
            }

            //Verificando se o adversário está em xeque.
            if (JogadorEstaEmXeque(CorAdversaria(JogadorAtual)))
            {
                PartidaEstaEmXeque = true;
            }
            else
            {
                PartidaEstaEmXeque = false;
            }

            if (JogadorEstaEmXequeMate(CorAdversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca pecaQueMoveu = Tabuleiro.RetirarPeca(destino);
            pecaQueMoveu.DecrementarQtdeMovimentos();
            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                PecasCaputuradas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(pecaQueMoveu, origem);

        }

        public Peca ExecutaMovimento(Posicao Origem, Posicao Destino)
        {
            Peca Peca = Tabuleiro.RetirarPeca(Origem);
            Peca.IncrementarQtdeMovimentos();
            Peca PecaCapturada = Tabuleiro.RetirarPeca(Destino);
            Tabuleiro.ColocarPeca(Peca, Destino);
            if (PecaCapturada != null)
            {
                PecasCaputuradas.Add(PecaCapturada);
            }

            return PecaCapturada;
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
            InserirNovaPeca("H7", new Torre(Cor.Branca, Tabuleiro));
            InserirNovaPeca("D1", new Rei(Cor.Branca, Tabuleiro));
            InserirNovaPeca("C1", new Torre(Cor.Branca, Tabuleiro));

            InserirNovaPeca("B8", new Torre(Cor.Preta, Tabuleiro));
            InserirNovaPeca("A8", new Rei(Cor.Preta, Tabuleiro));
            InserirNovaPeca("D4", new Bispo(Cor.Branca, Tabuleiro));

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

        private Cor CorAdversaria(Cor corAtual)
        {
            if (corAtual == Cor.Preta)
            {
                return Cor.Branca;
            }
            else
            {
                return Cor.Preta;
            }
        }

        private Peca ReiCorInformada(Cor cor)
        {
            foreach (Peca peca in PecasEmJogoCor(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }
            }

            return null;
        }

        public bool JogadorEstaEmXeque(Cor cor)
        {
            Peca Rei = ReiCorInformada(cor);
            foreach (Peca peca in PecasEmJogoCor(CorAdversaria(cor)))
            {
                bool[,] PecaAdversarioMovimentosPossiveis = peca.MovimentosPossiveis();
                if (PecaAdversarioMovimentosPossiveis[Rei.PosicaoAtual.Linha, Rei.PosicaoAtual.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool JogadorEstaEmXequeMate(Cor cor)
        {
            if (!JogadorEstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca peca in PecasEmJogoCor(cor))
            {
                bool[,] MovimentosPossiveis = peca.MovimentosPossiveis();
                for (int i = 0; i < Tabuleiro.DimensaoDoTabuleiro[0]; i++)
                {
                    for (int j = 0; j < Tabuleiro.DimensaoDoTabuleiro[1]; j++)
                    {
                        if (MovimentosPossiveis[i, j])
                        {
                            Posicao Origem = peca.PosicaoAtual;
                            Posicao Destino = new Posicao(i, j);
                            Peca PecaCapturada = ExecutaMovimento(Origem, Destino);

                            bool TesteXequeMate = JogadorEstaEmXeque(cor);

                            DesfazMovimento(Origem, Destino, PecaCapturada);

                            if (!TesteXequeMate)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }

}
