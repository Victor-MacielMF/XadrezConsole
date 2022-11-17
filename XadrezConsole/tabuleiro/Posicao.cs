using System.Text;

namespace XadrezConsole.tabuleiro
{
    internal class Posicao
    {
        //{0}Linha x {1}Coluna
        public int[] PosicaoDaPeca { get; set; } = new int[2];

        public Posicao(int[] posicaoAtualDaPeça)
        {
            PosicaoDaPeca[0] = posicaoAtualDaPeça[0];
            PosicaoDaPeca[1] = posicaoAtualDaPeça[1];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Posicao da peça: ").Append(PosicaoDaPeca[0]).Append(" x ").Append(PosicaoDaPeca[1]).Append(".");
            return sb.ToString();
        }
    }
}
