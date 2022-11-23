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
            InserirNovaPeca("A1", new Torre(Cor.Branca, Tabuleiro));
            InserirNovaPeca("B1", new Cavalo(Cor.Branca, Tabuleiro));
            InserirNovaPeca("C1", new Bispo(Cor.Branca, Tabuleiro));
            InserirNovaPeca("D1", new Dama(Cor.Branca, Tabuleiro));
            InserirNovaPeca("E1", new Rei(Cor.Branca, Tabuleiro));
            InserirNovaPeca("F1", new Bispo(Cor.Branca, Tabuleiro));
            InserirNovaPeca("G1", new Cavalo(Cor.Branca, Tabuleiro));
            InserirNovaPeca("H1", new Torre(Cor.Branca, Tabuleiro));
            InserirNovaPeca("A2", new Peao(Cor.Branca, Tabuleiro));
            InserirNovaPeca("B2", new Peao(Cor.Branca, Tabuleiro));
            InserirNovaPeca("C2", new Peao(Cor.Branca, Tabuleiro));
            InserirNovaPeca("D2", new Peao(Cor.Branca, Tabuleiro));
            InserirNovaPeca("E2", new Peao(Cor.Branca, Tabuleiro));
            InserirNovaPeca("F2", new Peao(Cor.Branca, Tabuleiro));
            InserirNovaPeca("G2", new Peao(Cor.Branca, Tabuleiro));
            InserirNovaPeca("H2", new Peao(Cor.Branca, Tabuleiro));



            InserirNovaPeca("A8", new Torre(Cor.Preta, Tabuleiro));
            InserirNovaPeca("B8", new Cavalo(Cor.Preta, Tabuleiro));
            InserirNovaPeca("C8", new Bispo(Cor.Preta, Tabuleiro));
            InserirNovaPeca("D8", new Dama(Cor.Preta, Tabuleiro));
            InserirNovaPeca("E8", new Rei(Cor.Preta, Tabuleiro));
            InserirNovaPeca("F8", new Bispo(Cor.Preta, Tabuleiro));
            InserirNovaPeca("G8", new Cavalo(Cor.Preta, Tabuleiro));
            InserirNovaPeca("H8", new Torre(Cor.Preta, Tabuleiro));
            InserirNovaPeca("A7", new Peao(Cor.Preta, Tabuleiro));
            InserirNovaPeca("B7", new Peao(Cor.Preta, Tabuleiro));
            InserirNovaPeca("C7", new Peao(Cor.Preta, Tabuleiro));
            InserirNovaPeca("D7", new Peao(Cor.Preta, Tabuleiro));
            InserirNovaPeca("E7", new Peao(Cor.Preta, Tabuleiro));
            InserirNovaPeca("F7", new Peao(Cor.Preta, Tabuleiro));
            InserirNovaPeca("G7", new Peao(Cor.Preta, Tabuleiro));
            InserirNovaPeca("H7", new Peao(Cor.Preta, Tabuleiro));

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
