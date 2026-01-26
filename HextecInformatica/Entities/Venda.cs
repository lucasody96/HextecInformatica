using HextecInformatica.Entities.Core;

namespace HextecInformatica.Entities
{
    public class Venda
    {
        public List<Produto> ListaItensVenda = [];

        private static int _contadorVendas = 0;
        
        public int NumeroNotaFiscal { get; set; }

        public DateTime DataVenda { get; set; } = DateTime.Now;

        public string ClienteNota{ get; set; } = string.Empty;

        public decimal ValorTotalVenda { get; set; }

        public decimal ValorFrete { get; set; }

        public decimal ValorDesconto { get; set; }

        public Venda()
        {
            //geração do ID automático
            _contadorVendas++;
            NumeroNotaFiscal = _contadorVendas;
        }

        public void AdicionaItemVenda(Produto produto)
        {
            ListaItensVenda.Add(produto);
        }

        public void SalvarVenda(string nomeCliente, decimal valorNotaFiscal, decimal valorFrete, decimal valorDesconto)
        {             
            ClienteNota = nomeCliente;
            ValorTotalVenda = valorNotaFiscal;
            ValorFrete = valorFrete;
            ValorDesconto = valorDesconto;
        }
    }

}
