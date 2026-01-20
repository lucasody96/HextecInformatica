using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.FormasPagamento
{
    public class PagamentoCartaoDebito : IFormasPagamento
    {
        public int Id { get; } = 2;
        public string? Descricao { get; } = "Cartão de Débito";

        public decimal Valor { get; set; }

        public decimal ProcessarPagamento(decimal valor, decimal totalCompra, Cliente cliente)
        {
            Console.WriteLine($"Pagamento via Cartão de Débito processado no valor de: R$ {valor:F2}");
            return valor;
        }

        public string ToString(string descricao, decimal valor)
        {
            return $"{descricao}: {valor:C}";
        }
    }
}
