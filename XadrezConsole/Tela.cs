using System.Text;
using XadrezConsole.tabuleiro;
using XadrezConsole.pecas;
using XadrezConsole.tabuleiro.enums;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            Console.Clear();
            Tela.ImprimirTabuleiro(partida.Tabuleiro);
            ImprimirPecasCapturadas(partida);
            if (!partida.Terminada)
            {
                Console.WriteLine("\n\nTurno: {0}\nAguardando jogada: {1}", partida.Turno, partida.JogadorAtual);
                if (partida.PartidaEstaEmXeque)
                {
                    Console.WriteLine("\nVOCÊ ESTÁ EM XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("\n\nTurno: {0}\n\nXEQUEMATE!\nVencedor: {1}", partida.Turno, partida.JogadorAtual);
            }
        }

        public static void ImprimirPartida(PartidaDeXadrez partida, bool[,] movimentosPossiveis)
        {
            Console.Clear();
            Tela.ImprimirTabuleiro(partida.Tabuleiro, movimentosPossiveis);
            ImprimirPecasCapturadas(partida);
            Console.WriteLine("\n\nTurno: {0}\nAguardando jogada: {1}", partida.Turno, partida.JogadorAtual);
            if (partida.PartidaEstaEmXeque)
            {
                Console.WriteLine("\nVOCÊ ESTÁ EM XEQUE!");
            }
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            bool PegouColunas = false;
            string Colunas = "  ";
            for (int i = 0; i < tabuleiro.DimensaoDoTabuleiro[0]; i++)
            {
                Console.Write(tabuleiro.DimensaoDoTabuleiro[0] - i + " ");
                for (int j = 0; j < tabuleiro.DimensaoDoTabuleiro[1]; j++)
                {
                    ImprimirPeca(tabuleiro.PosicaoTabuleiro(i, j));
                    if (!PegouColunas)
                    {
                        Colunas += tabuleiro.NumeroParaPalavra(j) + " ";
                    }
                }
                PegouColunas = true;
                Console.WriteLine();
            }
            Console.WriteLine(Colunas);
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] movimentosPoossiveis)
        {
            ConsoleColor FundoOriginal = Console.BackgroundColor;
            ConsoleColor FundoAlterado = ConsoleColor.DarkGray;

            bool PegouColunas = false;
            string Colunas = "  ";
            for (int i = 0; i < tabuleiro.DimensaoDoTabuleiro[0]; i++)
            {
                Console.Write(tabuleiro.DimensaoDoTabuleiro[0] - i + " ");
                for (int j = 0; j < tabuleiro.DimensaoDoTabuleiro[1]; j++)
                {
                    Console.BackgroundColor = movimentosPoossiveis[i, j] ? FundoAlterado : FundoOriginal;

                    ImprimirPeca(tabuleiro.PosicaoTabuleiro(i, j));

                    if (!PegouColunas)
                    {
                        Colunas += tabuleiro.NumeroParaPalavra(j) + " ";
                    }

                    Console.BackgroundColor = FundoOriginal;
                }
                PegouColunas = true;
                Console.WriteLine();
            }
            Console.WriteLine(Colunas);
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string Entrada = Console.ReadLine().ToUpper();

            return new PosicaoXadrez(Entrada);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor OriginalColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = OriginalColor;
                }

                Console.Write(" ");
            }
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.Write("\nPeças capturadas:\nBrancas: ");
            ImprimirConjunto(partida.PecasCapturadasCor(Cor.Branca));
            Console.Write("\nPretas: ");
            ConsoleColor OriginalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadasCor(Cor.Preta));
            Console.ForegroundColor = OriginalColor;
        }

        public static void ImprimirConjunto(HashSet<Peca> pecas)
        {
            StringBuilder sb = new StringBuilder().Append("[  ");
            foreach (Peca peca in pecas)
            {
                sb.Append(peca).Append(" ");
            }

            sb.Append(" ]");

            Console.Write(sb.ToString());
        }

    }

}
