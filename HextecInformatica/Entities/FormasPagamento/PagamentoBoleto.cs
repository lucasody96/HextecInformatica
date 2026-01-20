using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.FormasPagamento
{
    public class PagamentoBoleto : IFormasPagamento
    {
        public int Id { get; } = 4;
        public string? Descricao { get; } = "Boleto";

        public decimal Valor { get; set; } = 0;

        public decimal ProcessarPagamento(decimal valor, decimal totalCompra, Cliente cliente)
        {
            if (valor <= totalCompra)
            {
                Console.WriteLine($"Pagamento via Boleto processado no valor de: R$ {valor:F2}.");
                Console.ReadKey();
                Valor = valor;
                return Valor;
            }else
            {
                Console.WriteLine($"\nValor pago acima do subtotal não permitido para condição de pagamento Boleto."  +
                                   "\nPressione alguma tecla para prosseguir");
                Console.ReadKey();
                return Valor;
            }
        }

        public string ToString(string descricao, decimal valor)
        {
            return $"{descricao}: {valor:C}";
        }
    }
}
