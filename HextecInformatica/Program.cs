using System;

namespace HextecInformatica
{
    class Program
    {
        static void Main(string[] args)
        {
            //Nome da loja
            const string NOME_LOJA = "Hextec Informática";
            //Código dos produtos previamente cadastrados
            int[] codProduto = { 1, 2, 3, 4, 5, 6, 7 };
            //Nome dos produtos previamente cadastrados
            string[] nomeProduto = { "Mouse sem fio", "Pen Drive", "SSD" , "Memória Ram", "Monitor", "Headset Gamer", "Placa de vídeo" };
            //Valor dos produtos previamente cadastrados
            double[] valorProduto = { 65.90, 44.90, 390.49, 280.89, 749.99, 231.89, 2100.99 };
            //Estoque dos produtos previamente cadastrados
            //Simular o estoque, pelo menos dois dos produtos da loja tem que estar estogado.
            int[] estoqueProduto = { 32, 25, 10, 0, 15, 0, 1 };

            //variáveis usadas no escopo principal e funções
            double totalPagamento = 0, valorFrete = 0, valorDesconto = 0, valRestante = 0 ;
            string impressaoItensNota = "", descFormaPagamento = "";

            int codItemRemovido, pontosFidelidade;

            //Métodos/funções
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

            //Boas vindas à loja
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"   Bem vindo a {NOME_LOJA}");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Selecione a opção desejada:");
            Console.Write("\n[1] - Comprar (cliente)");
            Console.Write("\n[2] - Logar no sistema (Colaborador da loja)");
            Console.Write("\n[3] - Sair");
            Console.Write("\nO que você deseja fazer? ");
            string opcaoLogin = Console.ReadLine();
            switch (opcaoLogin)
            {
                case "1":
                    Console.Write("\nDigite seu nome: ");
                    string nomeUsuario = Console.ReadLine();

                    //Listar os produtos da loja e permitir que ele escolha - ele só pode escolher 5 produtos
                    Console.WriteLine($"\nSeja bem vindo {nomeUsuario}! Lista dos produtos disponíveis no nosso estoque para compra:");
                    Console.WriteLine("\nCódigo - Nome do produto - Valor - Quantidade disponível");
                    for (int i = 0; i < codProduto.Length; i++)
                    {
                        Console.Write($"\n {codProduto[i]} - {nomeProduto[i]} - R$ {valorProduto[i]:F2} | {estoqueProduto[i]}");
                    }

                    //opção para selecionar a quantidade de itens.
                    Console.Write("\n\nQual a quantidade de itens que deseja comprar (de 1 a 7 itens)? ");
                    int qtdItensSelecionados = Convert.ToInt32(Console.ReadLine());

                    //Seleção de produtos e soma do valor total de pagamento
                    if (qtdItensSelecionados < 1)
                        Console.WriteLine("Quantidade de itens informada menor que 1, programa sendo encerrado...");
                    else if (qtdItensSelecionados > 7)
                        Console.WriteLine("Quantidade de itens informada menor que 1, programa sendo encerrado...");
                    else
                    {

                        for (int i = 0; i < qtdItensSelecionados; i++)
                        {
                            Console.Write($"\nDigite o código do produto {i + 1} a ser selecionado: ");
                            int codProdutoSelecionado = Convert.ToInt32(Console.ReadLine());
                            AdicionaItemNotaFiscal(codProdutoSelecionado);
                        }

                        //opção para retirar ate 3 itens se ele quiser.
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
                            Console.Write("Qual o valor de desconto do seu cupom? R$ ");
                            valorDesconto = Convert.ToDouble(Console.ReadLine());
                            CupomDesconto(valorDesconto);
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
                                                      "informe o valor condinzente ao restante "       +
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
                        Console.WriteLine($"Nome do cliente: {nomeUsuario}");
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
                    break;
                case "2":
                    //Opção para visualizar o sistema como vendedor/colaborador da loja
                    Console.WriteLine("Em construção, saindo do programa...");
                    break;
                case "3":
                    Console.WriteLine("Saindo do programa....");
                    break;
                default:
                    Console.WriteLine("Opção inválida! Programa sendo encerrado...");
                    break;
            } 
            Console.ReadKey();
        }
    }
}
