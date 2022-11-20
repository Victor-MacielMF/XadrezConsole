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
            //Por padrão, as colunas sempre irão ter apenas 1 caracter, tendo no maximo 26 colunas. Se necessário reformular futuramente.
            int QuantidadeLetrasColuna = 0;
            int ColunaInformada = tabuleiro.PalavraParaBase64(Texto[0]);
            int ColunaMax = tabuleiro.ColunaParaBase64();


            while (Char.IsLetter(Texto[QuantidadeLetrasColuna]))
            {
                ++QuantidadeLetrasColuna;
            }
        
            if (QuantidadeLetrasColuna > 1 && QuantidadeLetrasColuna == 0 || ColunaInformada > ColunaMax)
            {
                throw new TabuleiroException("Coluna inválida.");
            }

            bool LinhaValida = Int32.TryParse(Texto.Substring(1), out int LinhaNova);

            if (!LinhaValida || LinhaNova > tabuleiro.DimensaoDoTabuleiro[1])
            {
                throw new TabuleiroException("Linha inválida.");
            }

            Coluna = Texto[0];
            Linha = LinhaNova;
            
            return new Posicao(tabuleiro.DimensaoDoTabuleiro[0] - Linha, Coluna - 'A');
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Coluna).Append(Linha);
            return sb.ToString();
        }
    }
}