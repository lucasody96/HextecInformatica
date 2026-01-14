using HextecInformatica.Classes;
using HextecInformatica.Classes.Repositorio;

namespace HextecInformatica
{
    class Program
    {

        private static void Main()
        {

            //================================================================
            //PROGRAMA PRINCIPAL
            //================================================================
            Loja? Hextec = new("HEXTEC INFORMÁTICA");
            Dados.CarregaProdutos(Hextec);
            Dados.CarregaColaboradores(Hextec);

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
                        IniciarVenda();
                        break;
                    case "2":
                        AcessarColaborador();
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
           
            void IniciarVenda() 
            {
                Cliente? ClienteLoja;
              
                Console.Write("\nDigite seu nome: ");
                string? nomeCliente = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nomeCliente))
                {
                    Console.WriteLine("Nome não pode ser vazio. Pressione qualquer tecla para voltar ao menu.");
                    Console.ReadKey();
                    return;
                }
                //Buscar o cliente usando o método loja
                Cliente? ClienteExiste = Hextec.ClienteJaComprou(nomeCliente!);
                // Aproveitamos os dados que vieram da busca
                ClienteLoja = ClienteExiste;

                if (ClienteExiste != null)
                {
                    Console.WriteLine(ClienteLoja!.MensagemBoasVindas());
                    Console.ReadKey();
                } 
                else
                {
                    ClienteLoja = new Cliente(nomeCliente);
                    ClienteLoja.DadosCliente();
                    Hextec.CadastrarCliente(ClienteLoja);
                    Console.WriteLine(ClienteLoja.MensagemBoasVindas());
                    Console.ReadKey();
                }

                Console.Clear();
                Utils.FormataCabecalho("CATÁLOGO DE PRODUTOS");
                Utils.FormataCabecalhoTabela();

                foreach (var produto in Hextec.ListaProdutos)
                {
                    // Chama o método formatador
                    Utils.FormataLinhaProdutos(produto.Id, produto.Descricao, produto.Valor, produto.Estoque);
                }

                Utils.ImprimeLinhaSeparadora('-');
                // método "Catálogo de Itens"

                //opção para selecionar a quantidade de itens. Criar método
                //Passar a lista da loja para a lista do carrinho
                Carrinho? CarrinhoCompraAtual = new(Hextec.ListaProdutos);

                bool codZero = false;
                while (!codZero)
                {
                    int codProdutoSelecionado = Classes.Utils.EvitaQuebraCodInt("\nSelecione o item a ser adicionado ao carrinho ou 0 para sair: ");

                    if (codProdutoSelecionado > 0)
                    {
                        CarrinhoCompraAtual.AdicionaItensCarrinho(codProdutoSelecionado);
                    }
                    else if (codProdutoSelecionado < 0)
                        Console.WriteLine("Valor informado inválido, por favor, tente novamente!");
                    else
                        codZero = true;
                }
                //visualização dos itens do carrinho + valor a ser pago, subtotal
                CarrinhoCompraAtual.VisualizaçãoItensCarrinho();

                //Permite ao cliente remover o item do carrinho, caso haja produtos
                Console.Write("Deseja remover algum item (S/N)? ");
                string? respRemoveItem = Console.ReadLine();

                if (respRemoveItem == "S" || respRemoveItem == "s")
                {
                    codZero = false;
                    while (!codZero)
                    {
                        int codProdutoRemovido = Classes.Utils.EvitaQuebraCodInt("\nDigite o código do produto a ser removido (0 para sair): ");

                        if (codProdutoRemovido > 0)
                        {
                            CarrinhoCompraAtual.RemoveItensCarrinho(codProdutoRemovido);
                        }
                        else if (codProdutoRemovido < 0)
                            Console.WriteLine("Valor informado inválido, por favor, tente novamente!");
                        else
                            codZero = true;
                    }
                }

                if (CarrinhoCompraAtual.ListaItensCarrinho.Count > 0)
                {
                    //opção para ele selecionar a forma de entrega
                    Console.Clear();
                    
                    Console.WriteLine("\nFormas de entrega disponíveis com seus respectivos valores: ");
                    Console.WriteLine("1 - Retirada na loja - Grátis");
                    Console.WriteLine("2 - Entrega padrão - R$ 20,00, acima de R$ 300,00 é gratis ");
                    Console.WriteLine("3 - Entrega expressa - R$ 40,00, acima de R$ 500,00 é grátis: ");
                    bool formaEntregaInvalida = false;
                    while (!formaEntregaInvalida)
                    {
                        int respFormaEntrega = Classes.Utils.EvitaQuebraCodInt("\nQual a forma de entrega desejada (informe de 1 a 3)? ");

                        if (respFormaEntrega > 0 && respFormaEntrega <= 3)
                        {
                            CarrinhoCompraAtual.FormaEntrega(respFormaEntrega);
                            Console.WriteLine("\nPressione alguma tecla para prosseguir!");
                            Console.ReadKey();
                            formaEntregaInvalida = true;
                        }
                        else
                            Console.WriteLine("Forma de entrega inválida.");
                    }

                    //opção para colocar um cupom de desconto no final da venda
                    Console.Clear();
                    if (CarrinhoCompraAtual.Subtotal >= 250 && CarrinhoCompraAtual.Subtotal < 500)
                        Console.WriteLine("Você ganhou um cupom de desconto de 5% nesta compra! Digite CUPOM5%DESCONTO para utilizá-lo");
                    else if (CarrinhoCompraAtual.Subtotal >= 500 && CarrinhoCompraAtual.Subtotal < 1000)
                        Console.WriteLine("Você ganhou um cupom de desconto de 10% nesta compra! Digite CUPOM10%DESCONTO para utilizá-lo");
                    else if (CarrinhoCompraAtual.Subtotal >= 1000)
                        Console.WriteLine("Você ganhou um cupom de desconto de 15% nesta compra! Digite CUPOM15%DESCONTO para utilizá-lo");

                    if (CarrinhoCompraAtual.Subtotal > 250)
                    {
                        Console.Write("\nDeseja usar o cupom (S/N)? ");
                        string? respPossuiCupomDesconto = Console.ReadLine();

                        if (respPossuiCupomDesconto == "S" || respPossuiCupomDesconto == "s")
                        {
                            CarrinhoCompraAtual.CalculoDescontoCupom();
                            Console.WriteLine("\nPressione alguma tecla para prosseguir!");
                            Console.ReadKey();
                        }
                    }

                    //Opção de usar o desconto de cashback da compra anterior
                    if (ClienteLoja != null && ClienteLoja.DescProximaCompra > 0)
                    {
                        Console.WriteLine($"\nvocê possui R$ {ClienteLoja.DescProximaCompra:F2} de desconto acumulado de compras anteriores.");
                        Console.Write("Deseja usar o desconto (S/N)? ");
                        string? respUsaDescontoAnterior = Console.ReadLine();

                        if (respUsaDescontoAnterior == "S" || respUsaDescontoAnterior == "s")
                            CarrinhoCompraAtual.CalculoDescontoCashback(ClienteLoja);

                        Console.WriteLine("\nPressione alguma tecla para prosseguir!");
                        Console.ReadKey();
                    }

                    Console.Clear();
                    CarrinhoCompraAtual.VisualizaçãoItensCarrinho();
                    Console.WriteLine("\nPressione uma tecla para prosseguir!");
                    Console.ReadKey();
                    //Seleção de produtos e soma do valor total de pagamento
                    do
                    {
                        Console.Clear();
                        //carrega as formas de pagamento disponíveis
                        Dados.FormasPagamentosDisponíveis(CarrinhoCompraAtual);
                        Console.WriteLine($"\nTotal a ser pago: R$ {CarrinhoCompraAtual.TotalCompra:F2}");
                        Console.WriteLine("Selecione a forma de pagamento conforme listado abaixo:");
                        Console.WriteLine("1 - Dinheiro");
                        Console.WriteLine("2 - Cartão de Crédito");
                        Console.WriteLine("3 - Cartão de Débito");
                        Console.WriteLine("4 - Boleto");

                        int formaPagamento = Utils.EvitaQuebraCodInt($"Digite o código da condição de pagamento a ser utilizada: ");
                        decimal valorFormaPagamento = Utils.EvitaQuebraCodDecimal($"Valor: R$ ");

                        CarrinhoCompraAtual.FormaPagamentoSelecionada(formaPagamento, valorFormaPagamento, ClienteLoja!);
                        Console.WriteLine("\nPressione uma tecla para prosseguir!");
                        Console.ReadKey();

                    } while (CarrinhoCompraAtual.TotalCompra > 0);

                    
                    Console.Clear();
                    Venda? VendaAtual = new();
                    //Simular uma nota fiscal simples - em texto no terminal.
                    //campos disponíveis, nome da loja, nome usuario/cliente, lista de produtos, valor frete e desconto e total de pagamento
                    VendaAtual.ImprimeNotaFiscal(ClienteLoja!, CarrinhoCompraAtual);

                    // Limpa a lista do carrinho, os pagamentos e totais
                    CarrinhoCompraAtual.LimpaCarrinho();
                }
                else
                    Console.WriteLine("\nCarrinho está vazio, não é possível prosseguir. Pressione enter para reiniciar a sua compra.");

                    Console.ReadKey();
            }

            void AcessarColaborador() 
            {
                bool EhColaborador = false;
                do
                {
                    Console.Clear();
                    Utils.FormataCabecalho("ACESSO PARA COLABORADORES");
                    Console.Write("\nLogin: ");

                    string? usuarioColaboradorInput = Console.ReadLine();
                    string usuarioColaborador = string.IsNullOrWhiteSpace(usuarioColaboradorInput) 
                        ? "" 
                        : usuarioColaboradorInput.Trim().ToUpper();

                    Console.Write("Senha: ");
                    string? senhaColaborador = Utils.LerSenhaComAsterisco();

                    Colaborador? ColaboradorLogado = Hextec.VerificaColaboradorLoja(usuarioColaborador, senhaColaborador);

                    if (ColaboradorLogado != null)
                    {
                        EhColaborador = true;
                        Hextec.RetornaColaboradorLogado(usuarioColaborador);
                        Console.ReadKey();
                    }else
                    {
                        Console.WriteLine("\nLogin ou senha incorretos!");
                        Console.Write("Deseja tentar novamente (S/N)? ");
                        string? respLogin = Console.ReadLine();

                        if (respLogin == "N" || respLogin == "n")
                        {
                            Console.WriteLine("\nVoltando ao menu anterior. Pressione alguma tecla para prosseguir...");
                            Console.ReadKey();
                            return;
                        }
                    }

                } while (!EhColaborador);

                bool execucaoComoColaborador = true;

                do
                {
                    Console.Clear();
                    Utils.FormataCabecalho("MENUS DOS COLABORADORES");
                    Console.WriteLine("\nOpções Disponíveis:\n");
                    Console.WriteLine("  [1] - Gerenciar Estoque");
                    Console.WriteLine("  [2] - Visualizar Vendas");
                    Console.WriteLine("  [3] - Relatórios");
                    Console.WriteLine("  [4] - Logout\n");

                    int? menuSelecionado = Utils.EvitaQuebraCodInt("Selecione o menu desejado: ");
                    switch (menuSelecionado)
                    {
                        case 1:
                            Console.Clear();
                            Utils.FormataCabecalho("GERENCIAMENTO DE ITENS DO ESTOQUE");
                            Console.WriteLine("\nOpções Disponíveis:\n");
                            Console.WriteLine("  [1] - Visualização");
                            Console.WriteLine("  [2] - Entrada");
                            Console.WriteLine("  [3] - Ajuste");
                            Console.WriteLine("  [4] - Logout\n");
                            //Criar método para gerenciar estoque
                            int? menuSelecionadoEstoque = Utils.EvitaQuebraCodInt("Selecione o menu desejado: ");
                            Hextec.GerenciamentoEstoque(menuSelecionadoEstoque);



                            Console.WriteLine("\nPressione algum botão para prosseguir");
                            Console.ReadLine();
                            break;
                        case 2:
                            //Criar método para Visualizar Vendas
                            Console.WriteLine("Em construção, pressione algum botão para prosseguir");
                            Console.ReadLine();
                            break;
                        case 3:
                            //Criar método para Relatórios
                            Console.WriteLine("Em construção, pressione algum botão para prosseguir");
                            Console.ReadLine();
                            break;
                        case 4:
                            //sai para o menu anterior
                            Console.WriteLine("Saindo do programa....Pressione alguma botão para prosseguir");
                            execucaoComoColaborador = false;
                            break;
                        default:
                            Console.WriteLine("Opção inválida!\n");
                            break;
                    }

                } while (execucaoComoColaborador);
               



                Console.ReadKey();
            }
        }
    }
}
