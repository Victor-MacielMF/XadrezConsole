﻿using System;
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

            Tabuleiro Tabuleiro = new Tabuleiro();
            
            Torre Torre1 = new Torre(Cor.Preta, Tabuleiro);
            Torre Torre2 = new Torre(Cor.Preta, Tabuleiro);

            Rei Rei1 = new Rei(Cor.Preta, Tabuleiro);
            Rei Rei2 = new Rei(Cor.Preta, Tabuleiro);

            Tabuleiro.ColocarPeca(Rei1, new Posicao(0,1));
            Tabuleiro.ColocarPeca(Rei2, new Posicao(1, 1));
            Tabuleiro.ColocarPeca(Torre1, new Posicao(2, 1));
            Tabuleiro.ColocarPeca(Torre2, new Posicao(0, -5));
            Tela.ImprimirTabuleiro(Tabuleiro);

            }catch(TabuleiroException e)

            {
                Console.WriteLine("Erro: {0}", e.Message);
            }
        }
    }
}