using System;
using System.Collections.Generic;
using System.Linq;

namespace HextecInformatica
{
    class Program
    {
        static void Main(string[] args)
        {
            //=================================================================
            // 1. DICTIONARYS/ LISTAS
            //=================================================================
            Dictionary<int, string> nomeProduto = new Dictionary<int, string>();
            Dictionary<int, double> valorProduto= new Dictionary<int, double>();
            Dictionary<int, int> estoqueProduto = new Dictionary<int, int>();
            List<int> listaCarrinho = new List<int>();

            //================================================================
            //2. STACKS E QUEUE
            //================================================================
            Stack<double> descontoProximaCompra = new Stack<double>();
            
            Queue<string> filaPagamentos = new Queue<string>();

            //================================================================
            //3. VARIÁVEIS E CONSTANTES
            //================================================================
            const string NOME_LOJA = "Hextec Informática";

            double totalPagamento = 0, valorFrete = 0, valorDesconto = 0, valRestante = 0, totalMercadoria = 0, valDescPontosFidelidade = 0, descProxCompraAtual = 0, valDescontoFidelidade = 0;
            string nomeCliente = "";
            int pontosFidelidade =0;

            //================================================================
            //4. PROGRAMA PRINCIPAL
            //================================================================
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
                        InciarComoColaborador();
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
                listaCarrinho.Clear();
                filaPagamentos.Clear();
                valRestante = 0; //zerar o valor a ser pago, garantindo estar zerado ao iniciar uma nova compra

                if (nomeCliente == "") {
                    Console.Write("\nDigite seu nome: ");
                    nomeCliente = Console.ReadLine();
                }
                
                // método "Catálogo de Itens"
                ListaItensCadastrados();

                //opção para selecionar a quantidade de itens. Criar método
                AdicionaCarrinhoCompras();

                
                if (listaCarrinho.Count > 0)
                {
                    //visualização dos itens do carrinho + valor a ser pago, subtotal
                    VisualizaçãoItensCarrinho();

                    //Permite ao cliente remover o item do carrinho, caso haja produtos
                    RemoveCarrinhoCompras();

                    //Lógica para considerar somente o total de pagamento do momento 
                    //Sem aplicar frete e outros descontos, ou seja, o total da mercadoria/ total bruto
                    totalMercadoria = valRestante;
                    //opção para ele selecionar a forma de entrega                   
                    FormaEntrega();

                    //opção para colocar um cupom de desconto no final da venda
                    ValorDesconto();

                    //Seleção de produtos e soma do valor total de pagamento
                    //opção para ele pagar com mais de uma forma, colocando o valor em cada uma das formas.
                    //Pode escolher entre pagar em dinheiro e cartão
                    //Usada a funcionalidade queue
                    FormaPagamentoQueue();

                    //Simular uma nota fiscal simples - em texto no terminal.
                    //campos disponíveis, nome da loja, nome usuario/cliente, lista de produtos, valor frete e desconto e total de pagamento
                    ImprimeNotaFiscal();

                    //Se ele gastar mais de 100 reais ele ganha 10 pontos de fidelidade, cada ponto de fidelidade da a ele 0,5% de desconto na próxima compra.
                    PontosFidelidade();
                    
                    Console.WriteLine("\nObrigado pela preferência e volte sempre!");

                }
                Console.WriteLine("\nPressione enter para realizar uma nova compra!");
                Console.ReadLine();               
            }

            void ListaItensCadastrados()
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

            void AdicionaCarrinhoCompras()
            {
                int qtdItensSelecionados = EvitaQuebraCodInt("\nQual a quantidade de itens que deseja comprar? ");

                for (int i = 0; i < qtdItensSelecionados; i++)
                {
                    int codProdutoSelecionado = EvitaQuebraCodInt($"\nDigite o código do produto {i + 1} a ser selecionado: ");

                    do
                    {
                        if (nomeProduto.ContainsKey(codProdutoSelecionado))
                        {
                            if (estoqueProduto[codProdutoSelecionado] > 0)
                            {
                                //Empilha no carrinho (commit)
                                listaCarrinho.Add(codProdutoSelecionado);
                                //Diminuir o estoque do item no dicionário
                                estoqueProduto[codProdutoSelecionado]--;
                                valRestante += valorProduto[codProdutoSelecionado];
                                Console.WriteLine($"Item {nomeProduto[codProdutoSelecionado]} adicionado às suas compras.");
                            }
                            else
                                Console.WriteLine("Item esgotado! Não será adicionado as suas compras");
                        }
                        else
                            Console.WriteLine("Código de item inexistente no catálogo de itens!");

                    } while (!nomeProduto.ContainsKey(codProdutoSelecionado));   
                }
            }

            void VisualizaçãoItensCarrinho()
            {
                Console.WriteLine("\n===========================");
                Console.WriteLine("     ITENS DO CARRINHO     ");
                Console.WriteLine("===========================");

                foreach (var codigoProdutoCarrinho in listaCarrinho)
                {
                    if (nomeProduto.ContainsKey(codigoProdutoCarrinho))
                        Console.WriteLine($"{codigoProdutoCarrinho} - {nomeProduto[codigoProdutoCarrinho]} - R$ {valorProduto[codigoProdutoCarrinho]:F2}");
                }

                Console.WriteLine("===========================");
                Console.WriteLine($"subtotal: R$ {valRestante:F2}");
                Console.WriteLine("===========================");
            }

            void RemoveCarrinhoCompras()
            {
                Console.WriteLine("Deseja remover algum item (S/N)? ");
                string respRemoveItem = Console.ReadLine();

                if (respRemoveItem == "S" || respRemoveItem == "s")
                {
                    //lógica para nao deixar o usuário pedir mais itens que tem no carrinho
                    int qtdItensRemovidos;
                    do
                    {
                        qtdItensRemovidos = EvitaQuebraCodInt("\nQual a quantidade de itens que deseja remover? ");

                        if (qtdItensRemovidos > listaCarrinho.Count)
                        {
                            Console.WriteLine($"Não é possível remover uma quantidade maior do que a listada no carrinho." +
                                               $"Quantidade de itens no carrinho: {listaCarrinho.Count}"                   );
                        }
                        else if (qtdItensRemovidos <= 0)
                        {
                            Console.WriteLine("Informe um número maior que zero");
                        }

                    } while (qtdItensRemovidos > listaCarrinho.Count || qtdItensRemovidos <= 0);

                    for (int i = 0; i < qtdItensRemovidos; i++)
                    {
                        bool codigoValido = false;

                        do
                        {
                            int codProdutoSelecionado = EvitaQuebraCodInt($"\nDigite o código do produto a ser removido: ");

                            if (listaCarrinho.Contains(codProdutoSelecionado))
                            {
                                //remove item do carrinho
                                listaCarrinho.Remove(codProdutoSelecionado);
                                //Diminuir o estoque do item no dicionário
                                estoqueProduto[codProdutoSelecionado]++;
                                valRestante -= valorProduto[codProdutoSelecionado];
                                Console.WriteLine($"Item {nomeProduto[codProdutoSelecionado]} removido das suas compras.\n");
                                VisualizaçãoItensCarrinho();

                                codigoValido = true;
                            }
                            else
                                Console.WriteLine("Código de item inexistente no seu carrinho!");

                        } while (!codigoValido);

                    }

                }
            }

            void FormaEntrega ()
            {
                Console.WriteLine("\nFormas de entrega disponíveis com seus respectivos valores: ");
                Console.WriteLine("1 - Retirada na loja - Grátis");
                Console.WriteLine("2 - Entrega padrão - R$ 20,00, acima de R$ 300,00 é gratis ");
                Console.WriteLine("3 - Entrega expressa - R$ 40,00, acima de R$ 500,00 é grátis: ");
                Console.Write("\nQual a forma de entrega desejada (informe de 1 a 3)? ");
                string respFormaEntrega = Console.ReadLine();

                switch (respFormaEntrega)
                {
                    case "1":
                        valRestante += valorFrete;
                        break;
                    case "2":
                        if (valRestante > 300.00)
                            valRestante += valorFrete;
                        else
                        {
                            valorFrete = 20.00;
                            valRestante += valorFrete;
                        }
                        break;
                    case "3":
                        if (valRestante > 500.00)
                            valRestante += valorFrete;
                        else
                        {
                            valorFrete = 40.00;
                            valRestante += valorFrete;
                        }
                        break;
                }  
                
                if (valorFrete > 0)
                {
                    Console.WriteLine($"\nValor de frete R$ {valorFrete:F2} adicionado ao valor a ser pago. Subtotal: {valRestante:F2}");
                }  
            }

            void ValorDesconto()
            {
                valorDesconto = 0;

                Console.Write("\nPossui cupom de desconto (S/N)? ");
                string respPossuiCupomDesconto = Console.ReadLine();
                if (respPossuiCupomDesconto == "S" || respPossuiCupomDesconto == "s")
                {
                    double valorDescontoCupom = EvitaQuebraCodFloat("Qual o valor de desconto do seu cupom? R$ ");
                    if (valorDescontoCupom > 0)
                    {
                        valRestante -= valorDescontoCupom;
                        valorDesconto += valorDescontoCupom;
                        Console.WriteLine($"Valor de R$ {valorDescontoCupom:F2} do cupom desconto foi adicionado com sucesso!");
                    }
                    else
                        Console.WriteLine("Valor não pode ser R$ 0,00 ou negativo. Tente novamente.");
                }

                double valDescontoAnterior = 0;

                foreach (var valDescCompraAnterior in descontoProximaCompra)
                {
                    valDescontoAnterior += valDescCompraAnterior;
                }

                if (valDescontoAnterior > 0)
               {
                    Console.WriteLine($"\nvocê possui R$ {valDescontoAnterior:F2} de desconto acumulado de compras anteriores.");
                    Console.Write("Deseja usar o desconto (S/N)? ");
                    string respUsaDescontoAnterior = Console.ReadLine();
                    if (respUsaDescontoAnterior == "S" || respUsaDescontoAnterior == "s")
                    {
                        valRestante -= valDescontoAnterior;
                        valorDesconto += valDescontoAnterior;
                        Console.WriteLine($"Valor de R$ {valDescontoAnterior:F2} do cashback adquirido na compra anterior foi adicionado com sucesso!");

                        descontoProximaCompra.Clear();
                    }
                }
                

            }

            void FormaPagamentoQueue()
            {
                totalPagamento = valRestante;
                Console.WriteLine($"\nTotal a ser pago: R$ {totalPagamento}");
                Console.WriteLine("Selecione a forma de pagamento conforme listado abaixo:");
                Console.WriteLine("1 - Dinheiro");
                Console.WriteLine("2 - Cartão de Crédito");
                Console.WriteLine("3 - Cartão de Débito");
                Console.WriteLine("4 - Boleto");

                int numCondicao = 0;
                do
                {
                    Console.WriteLine($"\nSubtotal a ser pago: R$ {valRestante:F2}");

                    int formaPagamento = EvitaQuebraCodInt($"Digite o código da condição de pagamento {numCondicao+1} a ser utilizada: ");
                    double valorFormaPagamento = EvitaQuebraCodFloat($"Valor: R$ ");

                    string descFormaPagamento = "";
                    if (formaPagamento >= 1 && formaPagamento <= 4)
                    {
                        //switch
                        switch (formaPagamento)
                        {
                            case 1:
                                descFormaPagamento = $"Dinheiro: R$ {valorFormaPagamento:F2}";
                                //Nova funcionalidade - lógica de troco
                                double valorEmDinheiro = valorFormaPagamento;

                                if (valorFormaPagamento > valRestante)
                                {
                                    double troco = valorEmDinheiro - valRestante;
                                    Console.WriteLine($"--> Troco a devolver: R$ {troco:F2}");

                                    Console.Write("\nDeseja usar o troco na próxima compra como desconto? ");
                                    string usaTrocoProxCompra = Console.ReadLine();

                                    if (usaTrocoProxCompra == "S" || usaTrocoProxCompra == "s")
                                    {
                                        descontoProximaCompra.Push(troco);
                                        Console.WriteLine($"Valor disponível para ser usado como desconto na próxima compra: R$ {descontoProximaCompra.Peek():F2}");
                                        valRestante = 0;
                                    }
                                    else
                                    {
                                        descFormaPagamento = $"Dinheiro: R$ {valRestante:F2} (Entregue: {valorEmDinheiro:F2}, Troco: {troco:F2})";
                                        valRestante -= valorFormaPagamento;
                                        // Força o arredondamento para 2 casas decimais
                                        valRestante = Math.Round(valRestante, 2);
                                    }

                                }
                                else if (valorEmDinheiro <= valRestante)
                                {
                                    valRestante -= valorFormaPagamento;
                                    valRestante = Math.Round(valRestante, 2);
                                }
                                break;
                            case 2:

                                if (valorFormaPagamento <= valRestante)
                                {
                                    descFormaPagamento = $"Cartão de Crédito: R$ {valorFormaPagamento:F2}";
                                    valRestante -= valorFormaPagamento;
                                    valRestante = Math.Round(valRestante, 2);
                                }
                                else
                                    Console.WriteLine("Valor pago acima do subtotal não permitido para condição de pagamento cartão de crédito");


                                    break;
                            case 3:

                                if (valorFormaPagamento <= valRestante)
                                {
                                    descFormaPagamento = $"Cartão de Débito: R$ {valorFormaPagamento:F2}";
                                    valRestante -= valorFormaPagamento;
                                    valRestante = Math.Round(valRestante, 2);
                                }
                                else
                                    Console.WriteLine("Valor pago acima do subtotal não permitido para condição de pagamento cartão de débito");
                                break;
                            case 4:

                                if (valorFormaPagamento <= valRestante)
                                {
                                    descFormaPagamento = $"Boleto: R$ {valorFormaPagamento:F2}";
                                    valRestante -= valorFormaPagamento;
                                    valRestante = Math.Round(valRestante, 2);
                                }
                                else
                                    Console.WriteLine("Valor pago acima do subtotal não permitido para condição de pagamento boleto");
                                
                                break;
                        }

                        // QUEUE: Enfileira a descrição do pagamento
                        filaPagamentos.Enqueue(descFormaPagamento);
                        numCondicao++;
                    }
                    else
                    {
                        Console.WriteLine("Condição de pagamento informa inválida!");
                    }

                } while (valRestante > 0);
                              
            }

            void ImprimeNotaFiscal() 
            {
                Console.WriteLine("\n\n========================================");
                Console.WriteLine("              NOTA FISCAL              ");
                Console.WriteLine("========================================");
                Console.WriteLine($"Nome do cliente: {nomeCliente}");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                Console.Write("Lista de itens:");

                foreach (var codigoItem in listaCarrinho)
                {
                    Console.WriteLine($"{nomeProduto[codigoItem]} - R$ {valorProduto[codigoItem]}");
                }

                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine($"Valor do frete: R$ {valorFrete:F2}" +
                                  $"\nValor desconto: R$ {valorDesconto:F2}");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine($"Total Pagamento: {totalPagamento:F2}");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine($"Formas de pagamento:");
                while (filaPagamentos.Count > 0)
                {
                    string descCondPagamento = filaPagamentos.Dequeue();
                    if (descCondPagamento != "")
                    {
                        Console.WriteLine($"-> {descCondPagamento}");
                    }
                }
                
                Console.WriteLine("========================================");
            }

            void PontosFidelidade()
            {
                if (totalPagamento > 100.00)
                {

                    valDescontoFidelidade = totalMercadoria * 0.05;
                    descontoProximaCompra.Push(valDescontoFidelidade);

                    Console.WriteLine("\n--- PARABÉNS! ---");
                    Console.WriteLine($"Sua compra gerou um cashback!");
                    Console.WriteLine($"Valor ganho: {descontoProximaCompra.Peek():F2} (5% do total)");
                    Console.WriteLine("Este valor será descontado automaticamente na sua próxima compra.");
                    Console.WriteLine("-----------------");
                }
            }

            void InciarComoColaborador()
            {
                Console.WriteLine("Em construção, saindo do programa...");
            }

            //Métodos para mensagens de exceção (até ser passado sobre isso)
            int EvitaQuebraCodInt(string mensagem)
            {
                int numInteiro;

                Console.Write(mensagem);

                while(!int.TryParse(Console.ReadLine(), out numInteiro))
                {
                    Console.Write("Erro: Valor inválido (Informe apenas números inteiros) \n\n");
                    Console.Write(mensagem);
                }
                return numInteiro;  
            }

            double EvitaQuebraCodFloat(string mensagem)
            {
                double numFloat;

                Console.Write(mensagem);

                while (!double.TryParse(Console.ReadLine(), out numFloat))
                {
                    Console.Write("Erro: Valor inválido , não é permitido informar letras e deve ser informado algum valor\n\n");
                    Console.Write(mensagem);
                }

                return numFloat;
            }
        }
    }
}
