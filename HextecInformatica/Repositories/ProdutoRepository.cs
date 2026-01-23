using HextecInformatica.Entities;
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

        public Produto? BuscaLoginNome(string descricao)
        {
            return ListaProdutos.FirstOrDefault(produto => produto.Descricao == descricao);
        }

        public Produto? BuscaID(int id)
        {
            return ListaProdutos.FirstOrDefault(produto => produto.Id == id);
        }
        public void Delete(Produto produto)
        {
            ListaProdutos.Remove(produto);
        }

        public void Update(Produto produto, int CampoAlterado)
        {
            var produtoSelecionado = BuscaID(produto.Id);
            if (produtoSelecionado != null)
            {
                switch (CampoAlterado)
                {
                    case 1:
                        Console.WriteLine("Digite a nova descrição do produto: ");
                        string? novaDescricao = Console.ReadLine();
                        produtoSelecionado.Descricao = novaDescricao ?? string.Empty;
                        break;
                    case 2:
                        decimal novoValor = Utils.EvitaQuebraCodDecimal("Digite o novo valor do produto: ");
                        produtoSelecionado.Valor = novoValor;
                        break;
                    case 3:
                        int novoEstoque = Utils.EvitaQuebraCodInt("Digite a nova quantidade em estoque do produto: ");
                        produtoSelecionado.Estoque = novoEstoque;
                        break;
                }
            }
        }
    }
}
