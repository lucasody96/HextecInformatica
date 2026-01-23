using HextecInformatica.Entities.Core;
using HextecInformatica.Interfaces;
using HextecInformatica.Repositories;

namespace HextecInformatica.Entities.MenusColaborador
{
    public class MenuGerenciaEstoque : Menu, IMenus<ProdutoRepository>
    {
        private readonly ProdutoRepository ProdutoRepository;
        public MenuGerenciaEstoque(ProdutoRepository prodRepo)
        {
            //geração do ID automático
            Id = 1;
            Descricao = "Gerenciar Estoque";
            ProdutoRepository = prodRepo;
        }

        public override void AcionaMenu()
        {
            int opcaoSelecionada;
            do
            {
                Console.Clear();
                ExibirCabecalho(Descricao!);

                Visualizar(ProdutoRepository);

                Console.WriteLine("\nOpções Disponíveis:\n");
                Console.WriteLine("  [1] - Inserir Novo Produto");
                Console.WriteLine("  [2] - Remover Produto");
                Console.WriteLine("  [3] - Atualizar Dados do Produto");
                Console.WriteLine("  [4] - Retornar ao Menu Anterior\n");

                opcaoSelecionada = Utils.EvitaQuebraCodInt("Escolha uma opção: ");

                switch (opcaoSelecionada)
                {
                    case 1:
                        Adicionar(ProdutoRepository);
                        break;

                    case 2:
                        Remover(ProdutoRepository);
                        break;
                    case 3:
                        Atualizar(ProdutoRepository);
                        // Atualizar(produtoRepos);
                        break;
                    case 4:
                        //visualizar Produtos
                        Console.WriteLine("Retornando ao menu anterior... Pressione qualquer tecla para continuar");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente...");
                        Console.ReadKey();
                        break;
                }

            } while (opcaoSelecionada != 4);

        }

        public void Adicionar(ProdutoRepository produtoRepos)
        {
            Console.Clear();
            Utils.FormataCabecalho("Adicionar Produto ao Estoque");

            Visualizar(produtoRepos);

            Console.Write("\nDigite a descrição do produto: ");
            string? descricaoProduto = Console.ReadLine();
            Console.Write("Digite a quantidade do produto: ");
            int quantidadeProduto = Utils.EvitaQuebraCodInt("");
            Console.Write("Digite o preço do produto: ");
            decimal precoProduto = Utils.EvitaQuebraCodDecimal("");

            Produto novoProduto = new (descricaoProduto!, precoProduto, quantidadeProduto);
            produtoRepos.Adiciona(novoProduto);

            Console.WriteLine($"\nProduto '{novoProduto.Descricao}' adicionado com sucesso ao estoque!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");

            Console.ReadKey();
        }

        public void Remover(ProdutoRepository produtoRepos)
        {
            // Implementar lógica para remover produto do estoque
            Console.Clear();
            Utils.FormataCabecalho("Remover Produto do Estoque");
            Visualizar(produtoRepos);

            int idProduto = Utils.EvitaQuebraCodInt("\nDigite o ID do produto que deseja remover: ");
            Produto? produtoParaRemover = produtoRepos.BuscaID(idProduto);

            if (produtoParaRemover != null)
            {
                Console.WriteLine($"\nTem certeza que deseja remover o produto '{produtoParaRemover.Descricao}'? (S/N): ");
                string? confirmacao = Console.ReadLine();

                if (confirmacao!.ToUpper() == "S")
                {
                    produtoRepos.Delete(produtoParaRemover);
                    Console.WriteLine($"\nProduto '{produtoParaRemover.Descricao}' removido com sucesso do estoque!");
                }
                else
                {
                    Console.WriteLine("\nOperação cancelada. Nenhum produto foi removido. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

            }else
                Console.WriteLine("\nProduto não encontrado. Verifique o ID e tente novamente.");
        }

        public void Atualizar(ProdutoRepository produtoRepos)
        {
            // Implementar lógica para atualizar dados do produto
            Console.Clear();
            Utils.FormataCabecalho("Atualizar Dados do Produto");
            Visualizar(produtoRepos);

            int idProduto = Utils.EvitaQuebraCodInt("\nDigite o ID do produto que deseja atualizar: ");
            Produto? produtoParaAtualizar = produtoRepos.BuscaID(idProduto);

            if (produtoParaAtualizar != null)
            {
                Console.WriteLine($"\nAtualizando dados do produto '{produtoParaAtualizar.Descricao}':");

                Console.WriteLine("Campos disponíveis para atualização: ");
                Console.WriteLine("  [1] - Descrição");
                Console.WriteLine("  [2] - Preço");
                Console.WriteLine("  [3] - Quantidade em Estoque");

                int campoEscolhido = Utils.EvitaQuebraCodInt("\nEscolha o campo que deseja atualizar (1-3): ");

                produtoRepos.Update(produtoParaAtualizar, campoEscolhido);

            }
            else
                Console.WriteLine("\nProduto não encontrado. Verifique o ID e tente novamente.");
        }

        public void Visualizar (ProdutoRepository produto)
        {
            Console.WriteLine();
            Utils.FormataCabecalho("PRODUTOS DO ESTOQUE");
            Utils.FormataCabecalhoTabela();
            foreach (var Produto in produto.ListaProdutos)
            {
                Utils.FormataLinhaProdutos(Produto.Id, Produto.Descricao, Produto.Valor, Produto.Estoque);
            }

            Utils.ImprimeLinhaSeparadora('-');
        }
    }
}
