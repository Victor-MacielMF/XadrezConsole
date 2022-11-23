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
        public Peca PecaVulneravelPassant { get; private set; }

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

            // #Promoção -- na versão 2.0 colocar essa responsabilidade para uma função.
            Peca PecaPossivelPromocao = Tabuleiro.PosicaoTabuleiro(destino.Linha, destino.Coluna);
            if (PecaPossivelPromocao is Peao && (destino.Linha == 0 || destino.Linha + 1 == Tabuleiro.DimensaoDoTabuleiro[0]))
            {
                PecaPossivelPromocao = Tabuleiro.RetirarPeca(destino);
                PecasEmJogo.Remove(PecaPossivelPromocao);
                Peca Dama = new Dama(PecaPossivelPromocao.Cor, Tabuleiro);
                Tabuleiro.ColocarPeca(Dama, destino);
                PecasEmJogo.Add(Dama);
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

            // #jogadaespecial en passant
            if (PecaPossivelPromocao is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                PecaVulneravelPassant = PecaPossivelPromocao;
            }
            else
            {
                PecaVulneravelPassant = null;
            }
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca PecaQueMoveu = Tabuleiro.RetirarPeca(destino);
            PecaQueMoveu.DecrementarQtdeMovimentos();
            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                PecasCaputuradas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(PecaQueMoveu, origem);

            // #jogadaespecial roque pequeno
            if (PecaQueMoveu is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                T.IncrementarQtdeMovimentos();
                Tabuleiro.ColocarPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (PecaQueMoveu is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                T.IncrementarQtdeMovimentos();
                Tabuleiro.ColocarPeca(T, origemT);
            }

            // Passant
            if (PecaQueMoveu is Peao && pecaCapturada == PecaVulneravelPassant && destino.Coluna != origem.Coluna)
            {
                Posicao SofreuPassant;
                if (PecaQueMoveu.Cor == Cor.Branca)
                {
                    SofreuPassant = new Posicao(destino.Linha + 1, destino.Coluna);
                }
                else
                {
                    SofreuPassant = new Posicao(destino.Linha - 1, destino.Coluna);
                }
                Tabuleiro.ColocarPeca(PecaVulneravelPassant, SofreuPassant);
            }

        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca Peca = Tabuleiro.RetirarPeca(origem);
            Peca.IncrementarQtdeMovimentos();
            Peca PecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(Peca, destino);
            if (PecaCapturada != null)
            {
                PecasCaputuradas.Add(PecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (Peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(origemT);
                T.IncrementarQtdeMovimentos();
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (Peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(origemT);
                T.IncrementarQtdeMovimentos();
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            // Passant
            Posicao SofreuPassant = null;
            if (Peca is Peao && PecaCapturada == null && destino.Coluna != origem.Coluna)
            {
                if (Peca.Cor == Cor.Branca)
                {
                    SofreuPassant = new Posicao(destino.Linha + 1, destino.Coluna);
                }
                else
                {
                    SofreuPassant = new Posicao(destino.Linha - 1, destino.Coluna);
                }
                PecaCapturada = Tabuleiro.RetirarPeca(SofreuPassant);
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
            InserirNovaPeca("E1", new Rei(Cor.Branca, Tabuleiro, this));
            InserirNovaPeca("F1", new Bispo(Cor.Branca, Tabuleiro));
            InserirNovaPeca("G1", new Cavalo(Cor.Branca, Tabuleiro));
            InserirNovaPeca("H1", new Torre(Cor.Branca, Tabuleiro));
            InserirNovaPeca("A2", new Peao(Cor.Branca, Tabuleiro, this));
            InserirNovaPeca("B2", new Peao(Cor.Branca, Tabuleiro, this));
            InserirNovaPeca("C2", new Peao(Cor.Branca, Tabuleiro, this));
            InserirNovaPeca("D2", new Peao(Cor.Branca, Tabuleiro, this));
            InserirNovaPeca("E2", new Peao(Cor.Branca, Tabuleiro, this));
            InserirNovaPeca("F2", new Peao(Cor.Branca, Tabuleiro, this));
            InserirNovaPeca("G2", new Peao(Cor.Branca, Tabuleiro, this));
            InserirNovaPeca("H2", new Peao(Cor.Branca, Tabuleiro, this));



            InserirNovaPeca("A8", new Torre(Cor.Preta, Tabuleiro));
            InserirNovaPeca("B8", new Cavalo(Cor.Preta, Tabuleiro));
            InserirNovaPeca("C8", new Bispo(Cor.Preta, Tabuleiro));
            InserirNovaPeca("D8", new Dama(Cor.Preta, Tabuleiro));
            InserirNovaPeca("E8", new Rei(Cor.Preta, Tabuleiro, this));
            InserirNovaPeca("F8", new Bispo(Cor.Preta, Tabuleiro));
            InserirNovaPeca("G8", new Cavalo(Cor.Preta, Tabuleiro));
            InserirNovaPeca("H8", new Torre(Cor.Preta, Tabuleiro));
            InserirNovaPeca("A7", new Peao(Cor.Preta, Tabuleiro, this));
            InserirNovaPeca("B7", new Peao(Cor.Preta, Tabuleiro, this));
            InserirNovaPeca("C7", new Peao(Cor.Preta, Tabuleiro, this));
            InserirNovaPeca("D7", new Peao(Cor.Preta, Tabuleiro, this));
            InserirNovaPeca("E7", new Peao(Cor.Preta, Tabuleiro, this));
            InserirNovaPeca("F7", new Peao(Cor.Preta, Tabuleiro, this));
            InserirNovaPeca("G7", new Peao(Cor.Preta, Tabuleiro, this));
            InserirNovaPeca("H7", new Peao(Cor.Preta, Tabuleiro, this));

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
