namespace HextecInformatica.Classes
{
    public class FormaPagamento(int id, string descricao, decimal valor)
    {
        public int Id { get; set; } = id;
        public string Descricao { get; set; } = descricao;

        public decimal Valor { get; set; } = valor;
    }
}
