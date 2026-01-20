using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.FormasPagamento
{
    public class PagamentoBoleto : IFormasPagamento
    {
        public int Id { get; } = 4;
        public string? Descricao { get; } = "Boleto";

        public decimal Valor { get; set; }

        public decimal ProcessarPagamento(decimal valor, decimal totalCompra, Cliente cliente)
        {
            Console.WriteLine($"Pagamento via Boleto processado no valor de: R$ {valor:F2}");
            return valor;

        }

        public string ToString(string descricao, decimal valor)
        {
            return $"{descricao}: {valor:C}";
        }
    }
}
