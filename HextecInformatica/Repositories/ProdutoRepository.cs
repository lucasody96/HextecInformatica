using HextecInformatica.Entities.Core;
using HextecInformatica.Interfaces;

namespace HextecInformatica.Repositories
{
    public class ProdutoRepository : IRepositories<Produto>
    {
        public List<Produto> ListaProdutos { get; set; } = [];
        
        public void Adiciona(Produto produto)
        {
            ListaProdutos.Add(produto);
        }

        public Produto? BuscaNome(string descricao)
        {
            return ListaProdutos.FirstOrDefault(produto => produto.Descricao == descricao);
        }

        public Produto? BuscaID(int id)
        {
            return ListaProdutos.FirstOrDefault(produto => produto.Id == id);
        }
    }
}
