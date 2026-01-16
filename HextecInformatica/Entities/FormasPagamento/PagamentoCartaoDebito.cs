using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.FormasPagamento
{
    public class PagamentoCartaoDebito : IFormasPagamento
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }

        public decimal Valor { get; set; }
    }
}
