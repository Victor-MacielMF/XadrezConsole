using System;
using XadrezConsole.pecas;
using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.enums;
using XadrezConsole.tabuleiro.exceptions;

namespace XadrezConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            try
            {

            PartidaDeXadrez Partida = new PartidaDeXadrez();

            Tela.ImprimirTabuleiro(Partida.Tabuleiro);

            }catch(TabuleiroException e)

            {
                Console.WriteLine("Erro: {0}", e.Message);
            }
            /*


            Tabuleiro Tabuleiro = new Tabuleiro();

            Posicao pos = Tabuleiro.TextoParaPosicao('b', 8);

            Console.Write("Linha:{0}\nColuna: {1}",pos.Linha, pos.Coluna);
            
            char var = 'a';
            Console.Write();
            */
        }
    }
}