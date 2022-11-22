using System;
using System.Text;
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


            PartidaDeXadrez Partida = new PartidaDeXadrez();
            while (!Partida.Terminada)
            {
                try
                {
                    Tela.ImprimirPartida(Partida);

                    Console.Write("\nOrigem: ");
                    Posicao Origem = Tela.LerPosicaoXadrez().TextoParaPosicao(Partida.Tabuleiro);

                    Partida.ValidaPosicaoDeOrigem(Origem);
                    bool[,] MovimentosPossiveis = Partida.Tabuleiro.PosicaoTabuleiro(Origem).MovimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimirPartida(Partida, MovimentosPossiveis);

                    Console.Write("\nDestino: ");
                    Posicao Destino = Tela.LerPosicaoXadrez().TextoParaPosicao(Partida.Tabuleiro);
                    Partida.ValidarPosicaoDeDestino(Origem, Destino);
                    Partida.RealizaJogada(Origem, Destino);
                }
                catch (TabuleiroException e)
                {
                    Console.WriteLine("\n{0}\n\nAperte qualquer tecla para continuar...", e.Message);
                    Console.ReadKey();
                }

                Console.Clear();
                Tela.ImprimirTabuleiro(Partida.Tabuleiro);
            }


        }
    }
}