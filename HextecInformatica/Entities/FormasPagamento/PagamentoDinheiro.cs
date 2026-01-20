using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.FormasPagamento
{
    public class PagamentoDinheiro : IFormasPagamento
    {
        public int Id { get; } = 1;
        public string? Descricao { get; } = "Dinheiro";

        public decimal Valor { get; set; }

        public decimal Troco { get; set; }

        public decimal ProcessarPagamento(decimal valor, decimal totalRestante, Cliente cliente)
        {
            if (valor > totalRestante)
            {
                Troco = valor - totalRestante;
                Console.WriteLine($"--> Troco a devolver: R$ {Troco:F2}");

                Console.Write("\nDeseja usar o troco na próxima compra como desconto (S/N)? ");
                string? usaTrocoProxCompra = Console.ReadLine();

                if (usaTrocoProxCompra == "S" || usaTrocoProxCompra == "s")
                {
                    cliente.AdicionarDescontoProximaCompra(Troco);

                    Console.WriteLine($"Valor disponível para ser usado como desconto na próxima compra: R$ {cliente.DescProximaCompra:F2}");
                    
                }

                return totalRestante;
            }
            else
            { 
                Console.WriteLine($"Pagamento em dinheiro recebido: R$ {valor:F2}");
                return valor;
            }
        }

        public string ToString(string descricao, decimal valor)
        {
            return $"{descricao}: {valor:C}";
        }
    }
}
