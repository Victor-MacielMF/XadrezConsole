using System.Text;

namespace XadrezConsole.tabuleiro
{
    internal class Tabuleiro
    {
        // {0} Linha x {1} Coluna
        private int ValorPadrao = 8;
        public int[] DimensaoDoTabuleiro { get; set; } = new int[2];
        public Peca[,] Pecas { get; private set; }


        public Tabuleiro()
        {
            DimensaoDoTabuleiro[0] = ValorPadrao;
            DimensaoDoTabuleiro[1] = ValorPadrao;
            Pecas = new Peca[ValorPadrao, ValorPadrao];
        }

        public Peca PosicaoTabuleiro(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Este é um tabuleiro ").Append(DimensaoDoTabuleiro[0]).Append(" x ").Append(DimensaoDoTabuleiro[1]).Append(".").Append(Pecas);
            return sb.ToString();
        }
    }
}
