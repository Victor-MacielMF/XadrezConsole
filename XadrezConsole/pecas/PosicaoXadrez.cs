using System;
using System.Text;
using XadrezConsole.tabuleiro;
using XadrezConsole.tabuleiro.exceptions;

namespace XadrezConsole.pecas
{
    class PosicaoXadrez
    {
        public string Texto { get; set; }
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(string texto)
        {
            Texto = texto;
        }

        public Posicao TextoParaPosicao(Tabuleiro tabuleiro)
        {
            //Organizar este código.
            //Por padrão, as colunas sempre irão ter apenas 1 caracter
            int QuantidadeLetrasColuna = 0;


            while (Char.IsLetter(Texto[QuantidadeLetrasColuna]))
            {
                ++QuantidadeLetrasColuna;
            }

            int ColunaInformada = Convert.ToInt32(Texto[0]);
            // 65 pq é a letra A
            int ColunaMax = tabuleiro.DimensaoDoTabuleiro[1] + 65;

            if (QuantidadeLetrasColuna > 1 && QuantidadeLetrasColuna == 0 || ColunaInformada > ColunaMax)
            {
                throw new TabuleiroException("Coluna inválida.");
            }

            //Pegou a coluna
           Coluna = Texto[0];

            //Pegou a linha
            bool LinhaValida = Int32.TryParse(Texto.Substring(1), out int LinhaNova);

            if (!LinhaValida || LinhaNova > tabuleiro.DimensaoDoTabuleiro[1])
            {
                throw new TabuleiroException("Linha inválida.");
            }

            Linha = LinhaNova;
            
            return new Posicao(tabuleiro.DimensaoDoTabuleiro[0] - Linha, Coluna - 'A');
        }

        public override string ToString()
        {

            return "" + Coluna + Linha;
        }
    }
}