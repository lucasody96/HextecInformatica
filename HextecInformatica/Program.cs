using HextecInformatica.Entities;
using HextecInformatica.Entities.Core;
using HextecInformatica.Repositories;
using HextecInformatica.Services;

namespace HextecInformatica
{
    class Program
    {

        private static void Main()
        {
            //Repositórios
            var clienteRepo = new ClienteRepository();
            var colaboradorRepo = new ColaboradorRepository();
            var produtoRepo = new ProdutoRepository();

            //Dados iniciais
            Dados.CarregaProdutos(produtoRepo);
            Dados.CarregaColaboradores(colaboradorRepo);
            Dados.CarregaClientes(clienteRepo);

            //================================================================
            //PROGRAMA PRINCIPAL
            //================================================================
            Loja? Hextec = new("HEXTEC INFORMÁTICA");
            
            bool execucaoPrograma = true;

            do
            {
                Console.Clear();
                Utils.FormataCabecalho($"BEM-VINDO À {Hextec.Nome}");
                Console.WriteLine("\nSelecione a opção desejada:\n");
                Console.WriteLine("  [1] - Comprar (Cliente)");
                Console.WriteLine("  [2] - Área do Colaborador");
                Console.WriteLine("  [3] - Sair\n");

                Console.Write("O que você deseja fazer? ");

                string? opcaoLogin = Console.ReadLine();
                switch (opcaoLogin)
                {
                    case "1":
                        IniciarVenda(produtoRepo, clienteRepo);
                        break;
                    case "2":
                        AcessarColaborador(clienteRepo, produtoRepo, colaboradorRepo);
                        break;
                    case "3":
                        Console.WriteLine("Saindo do programa....");
                        execucaoPrograma = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida!\n");
                        break;
                }

            } while (execucaoPrograma);

            Console.ReadKey();

            //================================================================
            // MÉTODOS/FUNÇÕES Auxiliares
            //================================================================
            static void IniciarVenda(ProdutoRepository produtoRepo, ClienteRepository clienteRepo) 
            {

                Console.Write("\nDigite seu nome: ");
                string? nomeCliente = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nomeCliente))
                {
                    Console.WriteLine("Nome não pode ser vazio. Pressione qualquer tecla para voltar ao menu.");
                    Console.ReadKey();
                    return;
                }

                Cliente? ClienteLoja = ClienteService.VerificaClienteExistente(clienteRepo, nomeCliente);

                CarrinhoService carrinhoService = new();
                // método "Catálogo de Itens"
                carrinhoService.ImprimeListaProdutosDisponiveis(produtoRepo);

                //opção para selecionar a quantidade de itens. Criar método
                //Passar a lista da loja para a lista do carrinho
                carrinhoService.AdicionaItensCarrinho(produtoRepo);

                //visualização dos itens do carrinho + valor a ser pago, subtotal
                carrinhoService.VisualizaçãoItensCarrinho();

                //Permite ao cliente remover o item do carrinho, caso haja produtos
                carrinhoService.RemoveItensCarrinho(produtoRepo);

                //Seleciona a forma de entrega
                carrinhoService.FormaEntrega();

                if (carrinhoService.ListaItensCarrinho.Count > 0)
                {
                    //opção para colocar um cupom de desconto no final da venda
                    carrinhoService.CalculoDescontoCupom();

                    //Opção de usar o desconto de cashback da compra anterior
                    carrinhoService.CalculoDescontoCashback(ClienteLoja!);

                    Console.Clear();
                    carrinhoService.VisualizaçãoItensCarrinho();
                    Console.WriteLine("\nPressione uma tecla para prosseguir!");
                    Console.ReadKey();

                    //Seleção de produtos e soma do valor total de pagamento
                    carrinhoService.FormaPagamentoSelecionada(ClienteLoja!);

                    var vendaService = new VendaService();
                    Console.Clear();
                    //Simular uma nota fiscal simples - em texto no terminal.
                    //campos disponíveis, nome da loja, nome usuario/cliente, lista de produtos, valor frete e desconto e total de pagamento
                    vendaService.ImprimeNotaFiscal(ClienteLoja!, carrinhoService);

                    // Limpa a lista do carrinho, os pagamentos e totais
                    carrinhoService.LimpaCarrinho();
                }
                else
                    Console.WriteLine("\nCarrinho está vazio, não é possível prosseguir. Pressione enter para reiniciar a sua compra.");

                    Console.ReadKey();
            }

            static void AcessarColaborador(ClienteRepository clienteRepos, ProdutoRepository produtoRepos, ColaboradorRepository colaboradorRepos)
            {
                ColaboradorService? colaboradorService = new();

                Colaborador? colaboradorLogado = ColaboradorService.VerificaColaboradorExistente(colaboradorRepos);

                if (colaboradorLogado == null)
                {
                    Console.WriteLine("Falha no login. Pressione qualquer tecla para voltar ao menu principal.");
                    Console.ReadKey();
                    return;
                }

                //carrega os menus do colaborador
                colaboradorService.CarregarMenus(produtoRepos, clienteRepos, colaboradorRepos);

                int opcaoSelecionada;
                do
                {
                    //exibe os menus do colaborador
                    colaboradorService.ExibeMenusColaborador();

                    opcaoSelecionada = Utils.EvitaQuebraCodInt("Escolha uma opção: ");

                    if (opcaoSelecionada != 5)
                    {
                        colaboradorService.AcionaMenuColaborador(opcaoSelecionada);
                    }
                    else
                        Console.WriteLine("Saindo da área do colaborador... Pressione qualquer tecla para voltar ao menu principal.");

                } while (opcaoSelecionada != 5);
                

                Console.ReadKey();
            }
        }
    }
}
