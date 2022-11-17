using System.Text;
using XadrezConsole.tabuleiro;
using System;
using XadrezConsole.pecas;
using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;
using XadrezConsole.tabuleiro.exceptions;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            bool PegouColunas = false;
            string Colunas = "  ";
            for (int i = 0; i < tabuleiro.DimensaoDoTabuleiro[0]; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.DimensaoDoTabuleiro[1]; j++)
                {
                    if (tabuleiro.PosicaoTabuleiro(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(tabuleiro.PosicaoTabuleiro(i, j));
                        Console.Write(" ");
                    }

                    if (!PegouColunas)
                    {
                        Colunas += Convert.ToChar(j + 65) + " ";
                    }
                }
                PegouColunas = true;
                Console.WriteLine();
            }
            Console.WriteLine(Colunas);
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string Entrada = Console.ReadLine();
            char Coluna = Entrada[0];
            int Linha = int.Parse(Entrada[1] + "");

            return new PosicaoXadrez(Coluna, Linha); 
        }

        public static void ImprimirPeca(Peca peca)
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
        }

    }

}
