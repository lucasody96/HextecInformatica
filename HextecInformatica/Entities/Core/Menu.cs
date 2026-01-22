namespace HextecInformatica.Entities.Core
{
    public abstract class Menu : Entity
    {
        public static int _contadorMenus = 0;
        public string? Descricao { get; set; }

        public abstract void AcionaMenu();
    }
}
