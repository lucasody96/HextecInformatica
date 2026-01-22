using HextecInformatica.Entities.Core;
using HextecInformatica.Interfaces;
using HextecInformatica.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace HextecInformatica.Entities.MenusColaborador
{
    public class MenuGerenciaEstoque : Menu
    {
        public MenuGerenciaEstoque()
        {
            //geração do ID automático
            Id = 1;
            Descricao = "GERENCIAR ESTOQUE";
        }

        public override void AcionaMenu()
        {
            ProdutoRepository produtoRepos = new();
            Console.Clear();
            Utils.FormataCabecalho(Descricao!);

            int opcaoSelecionada;

            Console.WriteLine("\nOpções Disponíveis:\n");
            Console.WriteLine("  [1] - Adicionar Produto ao Estoque");
            Console.WriteLine("  [2] - Remover Produto do Estoque");
            Console.WriteLine("  [3] - Atualizar Dados do Produto");
            Console.WriteLine("  [4] - Retornar ao menu anterior\n");

            opcaoSelecionada = Utils.EvitaQuebraCodInt("Escolha uma opção: ");

            switch (opcaoSelecionada)
            {
                case 1:
                    Adicionar(produtoRepos);
                    break;

                case 2:
                    // Remover(produtoRepos);
                    break;
                case 3:
                    // Atualizar(produtoRepos);
                    break;
                case 4:
                    // Retornar ao menu anterior
                    break;
                default:
                    Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente...");
                    Console.ReadKey();
                    break;
            }
        }

        public static void Adicionar(ProdutoRepository produtoRepos)
        {
            Console.Clear();
            Utils.FormataCabecalho("ADICIONAR PRODUTO AO ESTOQUE");
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

        public static void Remover(ProdutoRepository produtoRepos)
        {
            // Implementar lógica para remover produto do estoque
        }

        public static void Atualizar(ProdutoRepository produtoRepos)
        {
            // Implementar lógica para atualizar dados do produto
        }
    }
}
