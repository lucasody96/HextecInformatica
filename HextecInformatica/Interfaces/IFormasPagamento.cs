using HextecInformatica.Entities;

namespace HextecInformatica.Interfaces
{
    public interface IFormasPagamento
    {
        int Id { get;  }
        string? Descricao { get; }

        decimal Valor { get; set; }
        decimal ProcessarPagamento(decimal valor, decimal totalCompra, Cliente cliente);

        string ToString(string descricao, decimal valor);
    }
}
