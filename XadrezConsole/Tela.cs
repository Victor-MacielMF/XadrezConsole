using System.Text;
using XadrezConsole.tabuleiro;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            StringBuilder saida = new StringBuilder();
            for (int i = 0; i < tabuleiro.DimensaoDoTabuleiro[0]; i++)
            {
                for (int j = 0; j < tabuleiro.DimensaoDoTabuleiro[1]; j++)
                {
                    Peca? Peca = tabuleiro.PosicaoTabuleiro(i, j);
                    if (Peca == null)
                    {
                        saida.Append("-");
                    }
                    else
                    {
                        saida.Append(Peca);
                    }
                    saida.Append(" ");
                }
                saida.AppendLine();
            }

            Console.WriteLine(saida.ToString());
        }
    }
}
