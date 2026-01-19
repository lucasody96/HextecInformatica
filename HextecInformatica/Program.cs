using HextecInformatica.Entities;
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

            //Iniciando os services
            var vendaService = new VendaService();
            var colaboradorService = new ColaboradorService();

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
                        //AcessarColaborador(produtoRepo, colaboradorRepo);
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
           
            void IniciarVenda(ProdutoRepository produtoRepo, ClienteRepository clienteRepo) 
            {
                ClienteService clienteService = new();

                Console.Write("\nDigite seu nome: ");
                string? nomeCliente = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nomeCliente))
                {
                    Console.WriteLine("Nome não pode ser vazio. Pressione qualquer tecla para voltar ao menu.");
                    Console.ReadKey();
                    return;
                }

                Cliente? ClienteLoja = clienteService.VerificaClienteExistente(clienteRepo, nomeCliente);

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
                    //do
                    //{
                    //    Console.Clear();
                    //    //carrega as formas de pagamento disponíveis
                    //    Dados.FormasPagamentosDisponíveis(CarrinhoCompraAtual);
                    //    Console.WriteLine($"\nTotal a ser pago: R$ {CarrinhoCompraAtual.TotalCompra:F2}");
                    //    Console.WriteLine("Selecione a forma de pagamento conforme listado abaixo:");
                    //    Console.WriteLine("1 - Dinheiro");
                    //    Console.WriteLine("2 - Cartão de Crédito");
                    //    Console.WriteLine("3 - Cartão de Débito");
                    //    Console.WriteLine("4 - Boleto");

                    //    int formaPagamento = Utils.EvitaQuebraCodInt($"Digite o código da condição de pagamento a ser utilizada: ");
                    //    decimal valorFormaPagamento = Utils.EvitaQuebraCodDecimal($"Valor: R$ ");

                    //    CarrinhoCompraAtual.FormaPagamentoSelecionada(formaPagamento, valorFormaPagamento, ClienteLoja!);
                    //    Console.WriteLine("\nPressione uma tecla para prosseguir!");
                    //    Console.ReadKey();

                    //} while (CarrinhoCompraAtual.TotalCompra > 0);

                    
                    //Console.Clear();
                    //Venda? VendaAtual = new();
                    ////Simular uma nota fiscal simples - em texto no terminal.
                    ////campos disponíveis, nome da loja, nome usuario/cliente, lista de produtos, valor frete e desconto e total de pagamento
                    //VendaAtual.ImprimeNotaFiscal(ClienteLoja!, CarrinhoCompraAtual);

                    //// Limpa a lista do carrinho, os pagamentos e totais
                    //CarrinhoCompraAtual.LimpaCarrinho();
                }
                else
                    Console.WriteLine("\nCarrinho está vazio, não é possível prosseguir. Pressione enter para reiniciar a sua compra.");

                    Console.ReadKey();
            }

            //void AcessarColaborador() 
            //{
            //    bool EhColaborador = false;
            //    do
            //    {
            //        Console.Clear();
            //        Utils.FormataCabecalho("ACESSO PARA COLABORADORES");
            //        Console.Write("\nLogin: ");

            //        string? usuarioColaboradorInput = Console.ReadLine();
            //        string usuarioColaborador = string.IsNullOrWhiteSpace(usuarioColaboradorInput) 
            //            ? "" 
            //            : usuarioColaboradorInput.Trim().ToUpper();

            //        Console.Write("Senha: ");
            //        string? senhaColaborador = Utils.LerSenhaComAsterisco();

            //        Colaborador? ColaboradorLogado = Hextec.VerificaColaboradorLoja(usuarioColaborador, senhaColaborador);

            //        if (ColaboradorLogado != null)
            //        {
            //            EhColaborador = true;
            //            Hextec.RetornaColaboradorLogado(usuarioColaborador);
            //            Console.ReadKey();
            //        }else
            //        {
            //            Console.WriteLine("\nLogin ou senha incorretos!");
            //            Console.Write("Deseja tentar novamente (S/N)? ");
            //            string? respLogin = Console.ReadLine();

            //            if (respLogin == "N" || respLogin == "n")
            //            {
            //                Console.WriteLine("\nVoltando ao menu anterior. Pressione alguma tecla para prosseguir...");
            //                Console.ReadKey();
            //                return;
            //            }
            //        }

            //    } while (!EhColaborador);

            //    bool execucaoComoColaborador = true;

            //    do
            //    {
            //        Console.Clear();
            //        Utils.FormataCabecalho("MENUS DOS COLABORADORES");
            //        Console.WriteLine("\nOpções Disponíveis:\n");
            //        Console.WriteLine("  [1] - Gerenciar Estoque");
            //        Console.WriteLine("  [2] - Visualizar Vendas");
            //        Console.WriteLine("  [3] - Relatórios");
            //        Console.WriteLine("  [4] - Logout\n");

            //        int? menuSelecionado = Utils.EvitaQuebraCodInt("Selecione o menu desejado: ");
            //        switch (menuSelecionado)
            //        {
            //            case 1:
            //                Console.Clear();
            //                Utils.FormataCabecalho("GERENCIAMENTO DE ITENS DO ESTOQUE");
            //                Console.WriteLine("\nOpções Disponíveis:\n");
            //                Console.WriteLine("  [1] - Visualização");
            //                Console.WriteLine("  [2] - Entrada");
            //                Console.WriteLine("  [3] - Ajuste");
            //                Console.WriteLine("  [4] - Logout\n");
            //                //Criar método para gerenciar estoque
            //                int? menuSelecionadoEstoque = Utils.EvitaQuebraCodInt("Selecione o menu desejado: ");
            //                Hextec.GerenciamentoEstoque(menuSelecionadoEstoque);

            //                Console.WriteLine("\nPressione algum botão para prosseguir");
            //                Console.ReadLine();
            //                break;
            //            case 2:
            //                //Criar método para Visualizar Vendas
            //                Console.WriteLine("Em construção, pressione algum botão para prosseguir");
            //                Console.ReadLine();
            //                break;
            //            case 3:
            //                //Criar método para Relatórios
            //                Console.WriteLine("Em construção, pressione algum botão para prosseguir");
            //                Console.ReadLine();
            //                break;
            //            case 4:
            //                //sai para o menu anterior
            //                Console.WriteLine("Saindo do programa....Pressione alguma botão para prosseguir");
            //                execucaoComoColaborador = false;
            //                break;
            //            default:
            //                Console.WriteLine("Opção inválida!\n");
            //                break;
            //        }

            //    } while (execucaoComoColaborador);
               



            //    Console.ReadKey();
            //}
        }
    }
}
