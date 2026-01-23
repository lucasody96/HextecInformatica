using HextecInformatica.Entities.Core;
using HextecInformatica.Repositories;

namespace HextecInformatica.Entities.MenusColaborador
{
    public class MenuVisualizaVendas: Menu
    {
        private readonly ProdutoRepository ProdutoRepository;
        public MenuVisualizaVendas(ProdutoRepository prodRepo)
        {
            Id = 2;
            Descricao = "Visualizar Vendas";
            ProdutoRepository = prodRepo;
        }
        public override void AcionaMenu()
        {
        }
    }
}
