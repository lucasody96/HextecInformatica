using HextecInformatica.Entities.Core;

namespace HextecInformatica.Repositories
{
    public class ProdutoRepository
    {
        public List<Produto> ListaProdutos { get; set; } = [];
        
        public void AdicionaProduto(Produto produto)
        {
            ListaProdutos.Add(produto);
        }
    }
}
