using HextecInformatica.Classes;
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
            // 1. LISTAS
            //=================================================================

<<<<<<< Updated upstream
            List<Produto> listaProdutos = new List<Produto>();

            List<int> listaCarrinho = new List<int>();
=======
>>>>>>> Stashed changes

            //================================================================
            //2. STACKS E QUEUE
            //================================================================


            //================================================================
            //3. VARIÁVEIS E CONSTANTES
            //================================================================


            //================================================================
            //4. PROGRAMA PRINCIPAL
            //================================================================
            Loja MinhaLoja = new Loja("Hextec Informática");

<<<<<<< Updated upstream
            Cliente ClienteAtual;

=======
            //Carregar os itens
            MinhaLoja.AdicionarProduto(new Produto(1, "Mouse sem fio", 65.90m, 32));
            MinhaLoja.AdicionarProduto(new Produto(2, "Pen Drive", 44.90m, 25));
            MinhaLoja.AdicionarProduto(new Produto(3, "SSD", 390.49m, 10));
            MinhaLoja.AdicionarProduto(new Produto(4, "Memória Ram", 280.89m, 0));
            MinhaLoja.AdicionarProduto(new Produto(5, "Monitor", 749.99m, 15));
            MinhaLoja.AdicionarProduto(new Produto(6, "Headset Gamer", 231.89m, 0));
            MinhaLoja.AdicionarProduto(new Produto(7, "Placa de vídeo", 2100.99m, 1));

            bool execucaoPrograma = true;
>>>>>>> Stashed changes
            do
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine($"   Bem-vindo à {MinhaLoja.Nome}");
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

                        IniciarVenda();
                        break;
                    case "2":
                        //Opção para visualizar o sistema como vendedor/colaborador da loja
                        execucaoPrograma = false;
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
            //5. MÉTODOS/FUNÇÕES
            //================================================================
           
            void IniciarVenda() 
            {
                Cliente ClienteLoja;

<<<<<<< Updated upstream
                if (ClienteAtual == null) {

                    string nomeCliente = "", cpfCliente = "";

                    do
                    {
                        //implementando
                        Console.Write("\nDigite seu nome: ");
                        nomeCliente = Console.ReadLine();

                        Console.WriteLine("\nInforme seu CPF/CNPJ: ");
                        cpfCliente = Console.ReadLine();

                        if (nomeCliente != null)
                        {
                            ClienteAtual.Nome = nomeCliente;
                            ClienteAtual.CpfCnpj = cpfCliente;
                        }
                        else
                        {
                            Console.WriteLine("Não é permitido Nome e CPF/CNPJ em branco, por favor, informar o ");
                        }

                    } while (nomeCliente == "" && cpfCliente == "");

=======
                Console.Write("\nDigite seu nome: ");
                string nomeCliente = Console.ReadLine();

                if (MinhaLoja.ClienteJaComprou(nomeCliente))
                    Console.WriteLine($"\nSeja bem vindo de volta {nomeCliente}!");
                else
                {
                    Console.WriteLine($"\nSeja bem vindo {nomeCliente}");
                    ClienteLoja = new Cliente(nomeCliente);

                    ClienteLoja.DadosCliente();
>>>>>>> Stashed changes
                }

                Console.WriteLine("\nLista de produtos disponíveis no nosso estoque para compra: ");
                Console.WriteLine("\nCódigo - Nome do produto - Valor - Em estoque");
                // método "Catálogo de Itens"
                MinhaLoja.CatalogoProdutos();

                //opção para selecionar a quantidade de itens. Criar método



                //visualização dos itens do carrinho + valor a ser pago, subtotal


                //Permite ao cliente remover o item do carrinho, caso haja produtos


                // Verificação se todos os itens foram removidos.


                //Lógica para considerar somente o total de pagamento do momento 
                //Sem aplicar frete e outros descontos, ou seja, o total da mercadoria/ total bruto

                //opção para ele selecionar a forma de entrega                   


                //opção para colocar um cupom de desconto no final da venda


                //Opção de usar o desconto de cashback da compra anterior


                //Seleção de produtos e soma do valor total de pagamento
                //opção para ele pagar com mais de uma forma, colocando o valor em cada uma das formas.
                //Pode escolher entre pagar em dinheiro e cartão
                //Usada a funcionalidade queue


                //Simular uma nota fiscal simples - em texto no terminal.
                //campos disponíveis, nome da loja, nome usuario/cliente, lista de produtos, valor frete e desconto e total de pagamento


                //Se ele gastar mais de 100 reais ele ganha 10 pontos de fidelidade, cada ponto de fidelidade da a ele 0,5% de desconto na próxima compra.

                Console.ReadKey();
            }


<<<<<<< Updated upstream
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
                Console.Write("Deseja remover algum item (S/N)? ");
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
                bool formaEntregaInvalida = false;

                Console.WriteLine("\nFormas de entrega disponíveis com seus respectivos valores: ");
                Console.WriteLine("1 - Retirada na loja - Grátis");
                Console.WriteLine("2 - Entrega padrão - R$ 20,00, acima de R$ 300,00 é gratis ");
                Console.WriteLine("3 - Entrega expressa - R$ 40,00, acima de R$ 500,00 é grátis: ");

                while (!formaEntregaInvalida)
                {
                    Console.Write("\nQual a forma de entrega desejada (informe de 1 a 3)? ");
                    string respFormaEntrega = Console.ReadLine();

                    switch (respFormaEntrega)
                    {
                        case "1":
                            valRestante += valorFrete;
                            formaEntregaInvalida = true;
                            break;
                        case "2":
                            if (valRestante > 300.00)
                                valRestante += valorFrete;
                            else
                            {
                                valorFrete = 20.00;
                                valRestante += valorFrete;
                            }
                            formaEntregaInvalida = true;
                            break;
                        case "3":
                            if (valRestante > 500.00)
                                valRestante += valorFrete;
                            else
                            {
                                valorFrete = 40.00;
                                valRestante += valorFrete;
                            }
                            formaEntregaInvalida = true;
                            break;
                        default:
                            Console.WriteLine("Forma de entrega selecionada inválida!");
                            break;
                    }
                }
                
                if (valorFrete > 0)
                    Console.WriteLine($"\nValor de frete R$ {valorFrete:F2} adicionado ao valor a ser pago. Subtotal: {valRestante:F2}");
            }

            void ValorDescontoCupom()
            {
                valorDesconto = 0;

                Console.Write("\nPossui cupom de desconto (S/N)? ");
                string respPossuiCupomDesconto = Console.ReadLine();
                if (respPossuiCupomDesconto == "S" || respPossuiCupomDesconto == "s")
                {
                    bool valRestantePositivo = false;
                    while (valRestantePositivo == false)
                    {
                        double valorDescontoCupom = EvitaQuebraCodFloat("Qual o valor de desconto do seu cupom? R$ ");
                        if (valorDescontoCupom > 0)
                        {
                            if (valRestante >= valorDescontoCupom)
                            {
                                valRestante -= valorDescontoCupom;
                                valorDesconto += valorDescontoCupom;
                                Console.WriteLine($"Valor de R$ {valorDescontoCupom:F2} do cupom desconto foi adicionado com sucesso!");
                                valRestantePositivo = true;
                            }
                            else
                            {
                                Console.WriteLine("\nValor da compra não pode ser zero ou negativo, tente novamente");
                            }
                        }
                        else
                            Console.WriteLine("\nValor não pode ser R$ 0,00 ou negativo. Tente novamente.");
                    }
                    
                }
            }

            void ValDescontoTroco()
            {
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
                        bool valRestantePositivo = false;
                        while (valRestantePositivo == false)
                        {
                            double valorCashbackUsado = EvitaQuebraCodFloat("Valor a ser utilizado: R$ ");

                            if (valorCashbackUsado > 0 && valorCashbackUsado <= valRestante && valorCashbackUsado <= valDescontoAnterior)
                            {
                                valRestante -= valorCashbackUsado;
                                valorDesconto += valorCashbackUsado;
                                valDescontoAnterior -= valorCashbackUsado;
                                // Limpa a pilha antiga e adiciona o novo saldo (se houver)
                                descontoProximaCompra.Clear();

                                if (valDescontoAnterior > 0)
                                {
                                    descontoProximaCompra.Push(valDescontoAnterior);
                                }

                                Console.WriteLine($"\nDesconto de R$ {valorCashbackUsado:F2} aplicado com sucesso!");
                                Console.WriteLine($"Saldo restante de cashback: R$ {descontoProximaCompra.Peek():F2}");

                                valRestantePositivo = true;
                            }
                            else
                            {
                                Console.WriteLine("\nValor inválido! Verifique se o valor é positivo, se não excede o total da compra ou seu saldo de cashback.");
                                Console.WriteLine($"Saldo Cashback: {valDescontoAnterior:F2} | Valor Compra: {valRestante:F2}");
                            }
                        }
                        
                    }
                }

            }

            void FormaPagamentoQueue()
            {
                totalPagamento = valRestante;
                Console.WriteLine($"\nTotal a ser pago: R$ {totalPagamento:F2}");
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
                    Console.WriteLine("Este valor poderá ser usado na sua próxima compra.");
                    Console.WriteLine("-----------------");
                }
            }

            void InciarComoColaborador()
            {
                Console.WriteLine("Em construção, saindo do programa...");
            }

            void ProdutosCadastrados()
            {
                //listaProdutos.Add(new Produto(codigo, Descricao, Valor, Disponível));
                listaProdutos.Add(new Produto(1, "Mouse sem fio", 65.90, 32));
                listaProdutos.Add(new Produto(2, "Pen Drive", 44.90, 25));
                listaProdutos.Add(new Produto(3, "SSD", 390.49, 10));
                listaProdutos.Add(new Produto(4, "Memória Ram", 280.89, 0));
                listaProdutos.Add(new Produto(5, "Monitor", 749.99, 15));
                listaProdutos.Add(new Produto(6, "Headset Gamer", 231.89, 0));
                listaProdutos.Add(new Produto(7, "Placa de vídeo", 2100.99, 1));
            }
=======
            
>>>>>>> Stashed changes

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
