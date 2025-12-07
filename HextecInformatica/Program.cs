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
            double totalPagamento = 0, valorFrete = 0, valorDesconto = 0;
            string impressaoItensNota = "", descFormaPagamento = "";

            int codItemRemovido, pontosFidelidade;

            //Métodos/funções
            void AdicionaItemNotaFiscal(int produtoSelecionado)
            {

                for (int i = 0; i < codProduto.Length; i++)
                {
                    if (produtoSelecionado == codProduto[i])
                    {
                        impressaoItensNota += $"\n{nomeProduto[i]}.....R$ {valorProduto[i]:F2}";
                        totalPagamento += valorProduto[i];
                        estoqueProduto[i]--;
                        break;
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
                    default:
                        Console.WriteLine("Valor informado incorretamente, será considerado a forma de entrega de retirada na loja");
                        totalPagamento += valorFrete;
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
                        break;
                    case 2:
                        descFormaPagamento += $"\nCartão de Crédito: R$ {valorFormaPagamentoSelecionada:F2}";
                        break;
                    case 3:
                        descFormaPagamento += $"\nCartão de Débito: R$ {valorFormaPagamentoSelecionada:F2}";
                        break;
                    case 4:
                        descFormaPagamento += $"\nBoleto: R$ {valorFormaPagamentoSelecionada:F2}";
                        break;
                }
            }       

            //Boas vindas à loja
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"   Bem vindo a {NOME_LOJA}");
            Console.WriteLine("-----------------------------------------");
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
                    Console.Write($"\nDigite o código do produto {i+1} a ser selecionado: ");
                    int codProdutoSelecionado = Convert.ToInt32(Console.ReadLine());
                    AdicionaItemNotaFiscal(codProdutoSelecionado);
                }
               
                //opção para retirar ate 3 itens se ele quiser.
                Console.Write("Deseja retirar algum item (S - sim ou N - não)? ");
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
                Console.Write("\nQuantas formas de pagamento deseja utilizar (de 1 a 4): ");
                int qtdFormasPagamento = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < qtdFormasPagamento; i++)
                {
                    Console.Write($"\nDigite o código da condição de pagamento {i+1} (1 a 4): ");
                    int formaPagamento = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Digite o valor da condição de pagamento {i+1} a ser selecionada: ");
                    double valorFormaPagamento = Convert.ToDouble(Console.ReadLine());
                    AdicionaFormaPagamento(formaPagamento, valorFormaPagamento);
                }
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
                Console.WriteLine($"Valor do frete: R$ {valorFrete:F2}"+
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
            Console.ReadKey();
        }
    }
}
