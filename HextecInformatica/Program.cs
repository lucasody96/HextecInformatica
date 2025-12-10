using System;
using System.Collections.Generic;
using System.Linq; // Ajuda para somar valores

namespace HextecInformatica
{
    class Program
    {
        static void Main(string[] args)
        {
            //=================================================================
            // 1. DICTIONARYS
            //=================================================================
            Dictionary<int, string> nomeProduto = new Dictionary<int, string>();
            Dictionary<int, double> valorProduto= new Dictionary<int, double>();
            Dictionary<int, int> estoqueProduto = new Dictionary<int, int>();

            //================================================================
            //2. STACKS E QUEUE
            //================================================================
            //static Stack<int> pilhaCarrinho = new Stack<int>();

            //static Queue<string> filaPagamentos = new Queue<string>();

            //================================================================
            //3. VARIÁVEIS E CONSTANTES
            //================================================================
            const string NOME_LOJA = "Hextec Informática";

            double totalPagamento = 0, valorFrete = 0, valorDesconto = 0, valRestante = 0 ;
            string impressaoItensNota = "", descFormaPagamento = "";
            int codItemRemovido, pontosFidelidade;

            //================================================================
            //4. PROGRAMA PRINCIPAL
            //================================================================
            //Boas vindas à loja

            bool execucaoPrograma = true;
            ProdutosCadastrados();

            do
            {
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine($"   Bem-vindo à {NOME_LOJA}");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("Selecione a opção desejada:");
                Console.Write("\n[1] - Comprar (cliente)");
                Console.Write("\n[2] - Logar no sistema (Colaborador da loja)");
                Console.Write("\n[3] - Sair");
                Console.Write("\n\nO que você deseja fazer? ");

                string opcaoLogin = Console.ReadLine();
                switch (opcaoLogin)
                {
                    case "1":
                        IniciarCompra();
                        break;
                    case "2":
                        //Opção para visualizar o sistema como vendedor/colaborador da loja
                        InciarComoUsuario();
                        execucaoPrograma = false;
                        break;
                    case "3":
                        Console.WriteLine("Saindo do programa....");
                        execucaoPrograma = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Programa sendo encerrado...");
                        break;
                }

            } while (execucaoPrograma);

            Console.ReadKey();


            //================================================================
            //5. MÉTODOS/FUNÇÕES
            //================================================================
            void ProdutosCadastrados()
            {
                //Adicionando produtos (parametros a serem informados - ID, Nome, Valor, Estoque)
                AdicionarProdutos(1, "Mouse sem fio", 65.90, 32);
                AdicionarProdutos(2, "Pen Drive", 44.90, 25);
                AdicionarProdutos(3, "SSD", 390.49, 10);
                AdicionarProdutos(4, "Memória Ram", 280.89, 0);
                AdicionarProdutos(5, "Monitor", 749.99, 15);
                AdicionarProdutos(6, "Headset Gamer", 231.89, 0);
                AdicionarProdutos(7, "Placa de vídeo", 2100.99, 1);
            }

            void AdicionarProdutos(int codProduto, string descricao, double valor, int estoque) 
            {
                nomeProduto.Add(codProduto, descricao);
                valorProduto.Add(codProduto, valor);
                estoqueProduto.Add(codProduto, estoque);
            }

            void IniciarCompra() 
            {
                valRestante = 0; //zerar o valor a ser pago, garantindo estar zerado ao iniciar uma nova compra

                Console.Write("\nDigite seu nome: ");
                string nomeCliente = Console.ReadLine();

                //Listar os produtos da loja e permitir que ele escolha - ele só pode escolher 5 produtos
                //Colocar dentro de um método "Catálogo de Itens"
                ListaItensCadastrados(nomeCliente);

                //opção para selecionar a quantidade de itens. Criar método que use stack
                SelecaoProdutosStack();
                Console.Write("\n\nQual a quantidade de itens que deseja comprar (de 1 a 7 itens)? ");
                int qtdItensSelecionados = Convert.ToInt32(Console.ReadLine());

                //Seleção de produtos e soma do valor total de pagamento
                if (qtdItensSelecionados < 1)
                    Console.WriteLine("Quantidade de itens informada menor que 1, programa sendo encerrado...");
                else
                {

                    for (int i = 0; i < qtdItensSelecionados; i++)
                    {
                        Console.Write($"\nDigite o código do produto {i + 1} a ser selecionado: ");
                        int codProdutoSelecionado = Convert.ToInt32(Console.ReadLine());
                        AdicionaItemNotaFiscal(codProdutoSelecionado);
                    }

                    //opção para retirar itens se ele quiser
                    Console.Write("\nDeseja retirar algum item (S - sim ou N - não)? ");
                    string retornoSeRemoveItem = Console.ReadLine();

                    if (retornoSeRemoveItem == "S" || retornoSeRemoveItem == "s")
                    {
                        Console.Write("Quantos itens deseja remover? ");
                        int qtdItensRemovidos = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < qtdItensRemovidos; i++)
                        {
                            Console.Write("Digite o código do produto a remover: ");
                            codItemRemovido = Convert.ToInt32(Console.ReadLine());
                            RemoveItemNotaFiscal(codItemRemovido);
                        }
                    }

                    //opção para ele selecionar a forma de entrega
                    Console.WriteLine("\nFormas de entrega disponíveis com seus respectivos valores: ");
                    Console.WriteLine("1 - Retirada na loja - Grátis");
                    Console.WriteLine("2 - Entrega padrão - R$ 20,00, acima de R$ 300,00 é gratis ");
                    Console.WriteLine("3 - Entrega expressa - R$ 40,00, acima de R$ 500,00 é grátis: ");
                    Console.Write("\nQual a forma de entrega desejada (informe de 1 a 3)? ");
                    string respFormaEntrega = Console.ReadLine();
                    FormaEntrega(respFormaEntrega);

                    //opção para colocar um cupom de desconto no final da venda
                    Console.Write("\nPossui cupom de desconto (S/N)? ");
                    string respPossuiDesconto = Console.ReadLine();
                    if (respPossuiDesconto == "S" || respPossuiDesconto == "s")
                    {
                        bool saidaLoop = false;

                        while (!saidaLoop)
                        {
                            Console.Write("Qual o valor de desconto do seu cupom? R$ ");

                            if (double.TryParse(Console.ReadLine(), out valorDesconto))
                            {
                                if (valorDesconto > 0)
                                {
                                    CupomDesconto(valorDesconto);
                                    saidaLoop = true;
                                }
                                else
                                    Console.WriteLine("Valor não pode ser R$ 0,00 ou negativo. Tente novamente.");
                            }
                            else
                                Console.WriteLine("Valor inválido (Informe apenas números)");
                        }
                    }

                    //opção para ele pagar com mais de uma forma, colocando o valor em cada uma das formas.
                    //Pode escolher entre pagar em dinheiro e cartão
                    Console.WriteLine($"\nTotal a ser pago: R$ {totalPagamento}");
                    Console.WriteLine("Selecione a forma de pagamento conforme listado abaixo:");
                    Console.WriteLine("1 - Dinheiro");
                    Console.WriteLine("2 - Cartão de Crédito");
                    Console.WriteLine("3 - Cartão de Débito");
                    Console.WriteLine("4 - Boleto");

                    valRestante = totalPagamento;
                    int numCondPagamento = 1;
                    do
                    {
                        Console.WriteLine($"\nSubtotal a ser pago: R$ {valRestante:F2}");
                        Console.Write($"Digite o código da condição de pagamento {numCondPagamento}: ");
                        int formaPagamento = Convert.ToInt32(Console.ReadLine());

                        if (formaPagamento >= 1 && formaPagamento <= 4)
                        {
                            Console.Write($"Digite o valor da condição de pagamento selecionada: ");
                            double valorFormaPagamento = Convert.ToDouble(Console.ReadLine());
                            if (valorFormaPagamento <= valRestante)
                            {
                                AdicionaFormaPagamento(formaPagamento, valorFormaPagamento);
                                numCondPagamento++;
                            }
                            else
                            {
                                Console.WriteLine("Valor informado acima do permitido, por favor," +
                                                  "informe o valor condinzente ao restante " +
                                                  $"a ser pago: {valRestante}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Condição de pagamento informa inválida!");
                        }

                    } while (valRestante > 0);

                    //Simular uma nota fiscal simples - em texto no terminal.
                    //campos disponíveis, nome da loja, nome usuario/cliente, lista de produtos, valor frete e desconto e total de pagamento
                    Console.WriteLine("\n\n========================================");
                    Console.WriteLine("              NOTA FISCAL              ");
                    Console.WriteLine("========================================");
                    Console.WriteLine($"Nome do cliente: {nomeCliente}");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                    Console.Write("Lista de itens:");
                    Console.WriteLine($"{impressaoItensNota}");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine($"Valor do frete: R$ {valorFrete:F2}" +
                                      $"\nValor desconto: R$ {valorDesconto:F2}");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine($"Total Pagamento: {totalPagamento:F2}");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine($"Formas de pagamento: {descFormaPagamento}");
                    Console.WriteLine("========================================");

                    //Se ele gastar mais de 100 reais ele ganha 10 pontos de fidelidade, cada ponto de fidelidade da a ele 0,5% de desconto na próxima compra.
                    if (totalPagamento > 100.00)
                    {
                        Console.Write("\nPontos de fidelidade adquiridos com esta compra: ");
                        pontosFidelidade = 10;
                        Console.WriteLine($"{pontosFidelidade} pontos" +
                                           "\nCada ponto de fidelidade é convertido em 0,5% de desconto na próxima compra!");
                    }

                    Console.WriteLine("\nObrigado pela preferência e volte sempre!");
                }

            }

            void ListaItensCadastrados(string nomeCliente)
            {
                Console.WriteLine($"\nSeja bem vindo {nomeCliente}! Lista de produtos disponíveis no nosso estoque para compra:");
                Console.WriteLine("\nCódigo - Nome do produto - Valor - Em estoque");

                foreach (var produtoCadastrado in nomeProduto)
                {
                    int codProduto = produtoCadastrado.Key;
                    string descricaoProduto = produtoCadastrado.Value;
                    //Usar codProduto para buscar o preço e estoque de outros dicionários
                    double PrecoProduto = valorProduto[codProduto];
                    int estoque = estoqueProduto[codProduto];

                    Console.Write($"\n {codProduto} - {descricaoProduto} - R$ {PrecoProduto:F2} | {estoque}");
                }
            }
            void SelecaoProdutosStack()
            {

            }

            void AdicionaItemNotaFiscal(int produtoSelecionado)
            {

                for (int i = 0; i < codProduto.Length; i++)
                {
                    if (produtoSelecionado == codProduto[i])
                    {
                        if (estoqueProduto[i] > 0)
                        {
                            //
                            impressaoItensNota += $"\n{nomeProduto[i]}.....R$ {valorProduto[i]:F2}";
                            totalPagamento += valorProduto[i];
                            estoqueProduto[i]--;
                            break;
                        }
                        else
                            Console.WriteLine("Item esgotado! Não será adicionado as suas compras");                            
                    }  
                }      
            }

            void RemoveItemNotaFiscal (int produtoSelecionado)
            {
                //fazer semelhante ao romaneio com uma linha de devolução
                for(int i = 0; i < codProduto.Length; i++)
                {
                    if (produtoSelecionado == codProduto[i])
                    {
                        impressaoItensNota += $"\n(Devolvido) {nomeProduto[i]}.....R$ -{valorProduto[i]:F2}";
                        totalPagamento -= valorProduto[i];
                        estoqueProduto[i]++;
                    }
                }              
            }

            void CupomDesconto(double valDescontoCupom)
            {
                totalPagamento -= valDescontoCupom;
                Console.WriteLine($"Valor de R$ {valDescontoCupom:F2} do cupom desconto foi adicionado com sucesso!");
            }

            void FormaEntrega (string FormaEntregaSelecionada)
            {
                switch (FormaEntregaSelecionada)
                {
                    case "1":
                        totalPagamento += valorFrete;
                        break;
                    case "2":
                        if (totalPagamento > 300.00)
                            totalPagamento += valorFrete;
                        else
                        {
                            valorFrete = 20.00;
                            totalPagamento += valorFrete;
                        }
                        break;
                    case "3":
                        if (totalPagamento > 500.00)
                            totalPagamento += valorFrete;
                        else
                        {
                            valorFrete = 40.00;
                            totalPagamento += valorFrete;
                        }
                        break;
                }                    
            }

            void AdicionaFormaPagamento(int formaPagamentoSelecionada, double valorFormaPagamentoSelecionada)
            {
                //switch
                switch (formaPagamentoSelecionada)
                {
                    case 1:
                        descFormaPagamento += $"\nDinheiro: R$ {valorFormaPagamentoSelecionada:F2}";
                        //Nova funcionalidade - lógica de troco
                        Console.Write("Qual o valor entregue em dinheiro? ");
                        double valorEmDinheiro = Convert.ToDouble( Console.ReadLine() );

                        if (valorEmDinheiro > valorFormaPagamentoSelecionada) 
                        {
                            double troco = valorEmDinheiro - valorFormaPagamentoSelecionada;
                            Console.WriteLine($"--> Troco a devolver: R$ {troco:F2}");
                            descFormaPagamento += $"(Troco: R$ {troco:F2})";
                            valRestante -= valorFormaPagamentoSelecionada;
                            // Força o arredondamento para 2 casas decimais
                            valRestante = Math.Round(valRestante, 2);
                        }
                        else if (valorEmDinheiro <= valorFormaPagamentoSelecionada)
                        {
                            valRestante -= valorFormaPagamentoSelecionada;
                            valRestante = Math.Round(valRestante, 2);
                        }
                        break;
                    case 2:

                        descFormaPagamento += $"\nCartão de Crédito: R$ {valorFormaPagamentoSelecionada:F2}";
                        valRestante -= valorFormaPagamentoSelecionada;
                        valRestante = Math.Round(valRestante, 2);
                        break;
                    case 3:

                        descFormaPagamento += $"\nCartão de Débito: R$ {valorFormaPagamentoSelecionada:F2}";
                        valRestante -= valorFormaPagamentoSelecionada;
                        valRestante = Math.Round(valRestante, 2);
                        break;
                    case 4:

                        descFormaPagamento += $"\nBoleto: R$ {valorFormaPagamentoSelecionada:F2}";
                        valRestante -= valorFormaPagamentoSelecionada;
                        valRestante = Math.Round(valRestante, 2);
                        break;
                }
            }



            void InciarComoUsuario()
            {
                Console.WriteLine("Em construção, saindo do programa...");
            }

            //Métodos para mensagens de exceção (até ser passado sobre isso)
            void EvitaQuebraCodInt(string mensagem)
            {

            }

            void EvitaQuebraCodFloat(string mensagem)
            {

            }


        }
    }
}
