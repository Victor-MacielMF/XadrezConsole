using System;
using XadrezConsole.tabuleiro;

namespace XadrezConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro Tabuleiro = new Tabuleiro();
            Tela.ImprimirTabuleiro(Tabuleiro);
        }
    }
}