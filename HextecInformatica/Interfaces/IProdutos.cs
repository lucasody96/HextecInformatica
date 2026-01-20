namespace HextecInformatica.Interfaces
{
    public interface IProdutos
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Estoque { get; set; }
        public int QuantidadeComprada { get; set; }
        public string ToString();
    }
}
