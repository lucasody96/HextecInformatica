namespace HextecInformatica.Entities.Core
{
    public abstract class Menu : Entity
    {
        public string? Descricao { get; set; }

        public abstract void AcionaMenu();

        protected void ExibirCabecalho(string descricao)
        {
            Console.Clear();
            Utils.FormataCabecalho(Descricao!);
        }
    }
}
