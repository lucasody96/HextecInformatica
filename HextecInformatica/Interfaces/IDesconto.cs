namespace HextecInformatica.Interfaces
{
    public interface IDesconto
    {
        decimal CalcularDesconto(decimal subtotal);
        string ObterMensagem(decimal valorDesconto);
    }
}
