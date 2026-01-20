using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.FormasPagamento
{
    public class PagamentoCartaoCredito : IFormasPagamento
    {
        public int Id { get; } = 3;
        public string? Descricao { get; } = "Cartão de Crédito";

        public decimal Valor { get; set; }

        public decimal ProcessarPagamento(decimal valor, decimal totalCompra, Cliente cliente)
        {             
            Console.WriteLine($"Pagamento via Cartão de Crédito processado no valor de: R$ {valor:F2}");
            return valor;
        }

        public string ToString(string descricao, decimal valor)
        {
            return $"{descricao}: {valor:C}";
        }
    }
    
}
