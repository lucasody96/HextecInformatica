using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.FormasPagamento
{
    public class PagamentoCartaoDebito : IFormasPagamento
    {
        public int Id { get; } = 2;
        public string? Descricao { get; } = "Cartão de Débito";

        public decimal Valor { get; set; } = 0;

        public decimal ProcessarPagamento(decimal valor, decimal totalCompra, Cliente cliente)
        {
            if (valor <= totalCompra)
            {
                Console.WriteLine($"Pagamento via Cartão de Débito processado no valor de: R$ {valor:F2}.");
                Console.ReadKey();
                Valor = valor;
                return Valor;
            }
            else
            {
                Console.WriteLine($"\nValor pago acima do subtotal não permitido para condição de pagamento Cartão de Crédito." +
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
