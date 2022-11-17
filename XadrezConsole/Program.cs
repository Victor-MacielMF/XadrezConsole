using System;
using XadrezConsole.tabuleiro;

namespace XadrezConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] Coordenada = new int[] { 0, 1 }; 
            Posicao Posicao = new Posicao(Coordenada);

            Console.WriteLine(Posicao);

            Tabuleiro tabuleiro = new Tabuleiro();
            Console.WriteLine(tabuleiro.ToString());
        }
    }
}