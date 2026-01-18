namespace HextecInformatica.Interfaces
{
    public interface IFormaEntrega
    {
        int Id { get; }
        string Descricao { get; }

        Decimal CalculaFrete(decimal subtotal);

        string ObterMensagem(decimal valorFrete);

    }
}
