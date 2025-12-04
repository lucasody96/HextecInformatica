using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica
{
    class Program
    {
        static void Main(string[] args)
        {
            //Nome da loja
            const string NOME_LOJA = "Hextec Informática";
            //Código dos produtos previamente cadastrados
            const int CODIGO_PRODUTO_1 = 1;
            const int CODIGO_PRODUTO_2 = 2;
            const int CODIGO_PRODUTO_3 = 3;
            const int CODIGO_PRODUTO_4 = 4;
            const int CODIGO_PRODUTO_5 = 5;
            const int CODIGO_PRODUTO_6 = 6;
            const int CODIGO_PRODUTO_7 = 7;
            //Nome dos produtos previamente cadastrados
            const string NOME_PRODUTO_1 = "Mouse sem fio";
            const string NOME_PRODUTO_2 = "Pen Drive";
            const string NOME_PRODUTO_3 = "SSD";
            const string NOME_PRODUTO_4 = "Memória Ram";
            const string NOME_PRODUTO_5 = "Monitor";
            const string NOME_PRODUTO_6 = "Headset Gamer";
            const string NOME_PRODUTO_7 = "Placa de vídeo"; 
            //Valor dos produtos previamente cadastrados
            const double VALOR_PRODUTO_1 = 65.90;
            const double VALOR_PRODUTO_2 = 44.90;
            const double VALOR_PRODUTO_3 = 390.49;
            const double VALOR_PRODUTO_4 = 280.89; 
            const double VALOR_PRODUTO_5 = 749.99;
            const double VALOR_PRODUTO_6 = 231.89;
            const double VALOR_PRODUTO_7 = 2100.99;
            //Estoque dos produtos previamente cadastrados
            //Simular o estoque, pelo menos dois dos produtos da loja tem que estar estogado.
            const int ESTOQUE_PRODUTO_1 = 32;
            const int ESTOQUE_PRODUTO_2 = 25;
            const int ESTOQUE_PRODUTO_3 = 10;
            const int ESTOQUE_PRODUTO_4 = 0;
            const int ESTOQUE_PRODUTO_5 = 15;
            const int ESTOQUE_PRODUTO_6 = 0;
            const int ESTOQUE_PRODUTO_7 = 1;
            //variáveis usadas no escopo principal e funções
            double totalPagamento = 0, valorFrete = 0, valorDesconto = 0;
            string impressaoItensNota = "", descFormaPagamento = "";
            int codItemRemovido, codItemAdicionadoNota1, codItemAdicionadoNota2, codItemAdicionadoNota3,
                codItemAdicionadoNota4, codItemAdicionadoNota5, codItemAdicionadoNota6, codItemAdicionadoNota7, pontosFidelidade;

            //Métodos/funções
            void AdicionaItemNotaFiscal(int produtoSelecionado)
            {
                if (produtoSelecionado == CODIGO_PRODUTO_1)
                {
                    impressaoItensNota += $"{NOME_PRODUTO_1}.....R$ {VALOR_PRODUTO_1:F2}";
                    totalPagamento += VALOR_PRODUTO_1;                    
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_2)
                {
                    impressaoItensNota += $"{NOME_PRODUTO_2}.....R$ {VALOR_PRODUTO_2:F2}";
                    totalPagamento += VALOR_PRODUTO_2;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_3)
                {
                    impressaoItensNota += $"{NOME_PRODUTO_3}.....R$ {VALOR_PRODUTO_3:F2}";
                    totalPagamento += VALOR_PRODUTO_3;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_4)
                {
                    impressaoItensNota += $"{NOME_PRODUTO_4}.....R$ {VALOR_PRODUTO_4:F2}";
                    totalPagamento += VALOR_PRODUTO_4;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_5)
                {
                    impressaoItensNota += $"{NOME_PRODUTO_5}.....R$ {VALOR_PRODUTO_5:F2}";
                    totalPagamento += VALOR_PRODUTO_5;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_6)
                {
                    impressaoItensNota += $"{NOME_PRODUTO_6}.....R$ {VALOR_PRODUTO_6:F2}";
                    totalPagamento += VALOR_PRODUTO_6;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_7)
                {
                    impressaoItensNota += $"{NOME_PRODUTO_7}.....R$ {VALOR_PRODUTO_7:F2}";
                    totalPagamento += VALOR_PRODUTO_6;
                }
                else
                    Console.WriteLine("Código de produto inexistente, não será somado no valor a ser pago!");
            }

            void RemoveItemNotaFiscal (int produtoSelecionado)
            {
                //fazer semelhante ao romaneio com uma linha de devolução
                if (produtoSelecionado == CODIGO_PRODUTO_1)
                {
                    impressaoItensNota += $"(Devolvido) {NOME_PRODUTO_1}.....R$ {VALOR_PRODUTO_1:F2}";
                    totalPagamento -= VALOR_PRODUTO_1;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_2)
                {
                    impressaoItensNota += $"(Devolvido) {NOME_PRODUTO_2}.....R$ {VALOR_PRODUTO_2:F2}";
                    totalPagamento -= VALOR_PRODUTO_2;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_3)
                {
                    impressaoItensNota += $"(Devolvido) {NOME_PRODUTO_3}.....R$ {VALOR_PRODUTO_3:F2}";
                    totalPagamento -= VALOR_PRODUTO_3;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_4)
                {
                    impressaoItensNota += $"(Devolvido) {NOME_PRODUTO_4}.....R$ {VALOR_PRODUTO_4:F2}";
                    totalPagamento -= VALOR_PRODUTO_4;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_5)
                {
                    impressaoItensNota += $"(Devolvido) {NOME_PRODUTO_5}.....R$ {VALOR_PRODUTO_5:F2}";
                    totalPagamento -= VALOR_PRODUTO_5;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_6)
                {
                    impressaoItensNota += $"(Devolvido) {NOME_PRODUTO_6}.....R$ {VALOR_PRODUTO_6:F2}";
                    totalPagamento -= VALOR_PRODUTO_6;
                }
                else if (produtoSelecionado == CODIGO_PRODUTO_7)
                {
                    impressaoItensNota += $"(Devolvido) {NOME_PRODUTO_7}.....R$ {VALOR_PRODUTO_7:F2}";
                    totalPagamento -= VALOR_PRODUTO_6;
                }
                else
                    Console.WriteLine("Código de produto inexistente, não será removido no valor a ser pago!");
            }

            void CupomDesconto(double valDescontoCupom)
            {
                totalPagamento -= valDescontoCupom;
                Console.WriteLine($"Valor de R$ {valDescontoCupom} do cupom desconto foi adicionado com sucesso!");
            }

            void FormaEntrega (string FormaEntregaSelecionada)
            {
                valorFrete = 0;
                if (FormaEntregaSelecionada == "1") 
                    totalPagamento += valorFrete;
                else if (FormaEntregaSelecionada == "2")
                {
                    if (totalPagamento > 300.00)
                        totalPagamento += valorFrete;
                    else
                    {
                        valorFrete = 20.00;
                        totalPagamento += valorFrete;
                    }
                }else if (FormaEntregaSelecionada == "3")
                {
                    if (totalPagamento > 500.00)
                        totalPagamento += valorFrete;
                    else
                    {
                        valorFrete = 40.00;
                        totalPagamento += valorFrete;
                    }
                }else
                {
                    Console.WriteLine("Valor informado incorretamente, será considerado a forma de entrega de retirada na loja");
                    totalPagamento += valorFrete;
                }
                    
            }

            void AdicionaFormaPagamento(int formaPagamentoSelecionada, double valorFormaPagamentoSelecionada)
            {
                if (formaPagamentoSelecionada == 1)
                    descFormaPagamento += $"Dinheiro - R$ {valorFormaPagamentoSelecionada}";
                else if(formaPagamentoSelecionada == 2)
                    descFormaPagamento += $"Cartão de Crédito - R$ {valorFormaPagamentoSelecionada}";
                else if(formaPagamentoSelecionada == 3)
                    descFormaPagamento += $"Cartão de Débito - R$ {valorFormaPagamentoSelecionada}";
                else if(formaPagamentoSelecionada==4)
                    descFormaPagamento += $"Boleto - R$ {valorFormaPagamentoSelecionada}";
            }

            void PontosFidelidade(double totalPagamentoNota)
            {
                if (totalPagamentoNota > 100.00)
                {
                    pontosFidelidade = 10;
                    Console.WriteLine($"{pontosFidelidade} pontos"+
                                       "\nCada ponto de fidelidade é convertido em 0,5% de desconto na próxima compra!");
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
            Console.WriteLine($" {CODIGO_PRODUTO_1} - {NOME_PRODUTO_1} - R$ {VALOR_PRODUTO_1:F2} |"+
                              $"\n {CODIGO_PRODUTO_2} - {NOME_PRODUTO_2} - R$ {VALOR_PRODUTO_2:F2} | {ESTOQUE_PRODUTO_1}"+
                              $"\n {CODIGO_PRODUTO_3} - {NOME_PRODUTO_3} - R$ {VALOR_PRODUTO_3:F2} | {ESTOQUE_PRODUTO_1}" +
                              $"\n {CODIGO_PRODUTO_4} - {NOME_PRODUTO_4} - R$ {VALOR_PRODUTO_4:F2} | {ESTOQUE_PRODUTO_1}" +
                              $"\n {CODIGO_PRODUTO_5} - {NOME_PRODUTO_5} - R$ {VALOR_PRODUTO_5:F2} | {ESTOQUE_PRODUTO_1}" +
                              $"\n {CODIGO_PRODUTO_6} - {NOME_PRODUTO_6} - R$ {VALOR_PRODUTO_6:F2} | {ESTOQUE_PRODUTO_1}"+
                              $"\n {CODIGO_PRODUTO_7} - {NOME_PRODUTO_7} - R$ {VALOR_PRODUTO_7:F2} | {ESTOQUE_PRODUTO_1}");
            //opção para selecionar a quantidade de itens.
            Console.Write("\nQual a quantidade de itens que deseja comprar (de 1 a 7 itens)? ");
            int qtdItensSelecionados = Convert.ToInt32(Console.ReadLine());

            //Seleção de produtos e soma do valor total de pagamento
            if (qtdItensSelecionados < 1)
                Console.WriteLine("Quantidade de itens informada menor que 1, programa sendo encerrado...");
            else if (qtdItensSelecionados > 7)
                Console.WriteLine("Quantidade de itens informada menor que 1, programa sendo encerrado...");
            else
            {
                if (qtdItensSelecionados >= 1) 
                {
                    //Produto selecionado 1
                    Console.Write("\nDigite o código do primeiro produto a ser selecionado: ");
                    codItemAdicionadoNota1 = Convert.ToInt32(Console.ReadLine());
                    AdicionaItemNotaFiscal(codItemAdicionadoNota1);
                }
                if (qtdItensSelecionados >= 2)
                {
                    //Produto selecionado 2
                    Console.Write("\nDigite o código do segundo produto a ser selecionado: ");
                    codItemAdicionadoNota2 = Convert.ToInt32(Console.ReadLine());
                    AdicionaItemNotaFiscal(codItemAdicionadoNota2);
                }
                if (qtdItensSelecionados >= 3)
                {
                    //Produto selecionado 3
                    Console.Write("\nDigite o código do terceiro produto a ser selecionado: ");
                    codItemAdicionadoNota3 = Convert.ToInt32(Console.ReadLine());
                    AdicionaItemNotaFiscal(codItemAdicionadoNota3);
                }
                if (qtdItensSelecionados >= 4)
                {
                    //Produto selecionado 4
                    Console.Write("\nDigite o código do quarto produto a ser selecionado: ");
                    codItemAdicionadoNota4 = Convert.ToInt32(Console.ReadLine());
                    AdicionaItemNotaFiscal(codItemAdicionadoNota4);
                }
                if (qtdItensSelecionados >= 5)
                {
                    //Produto selecionado 5
                    Console.Write("\nDigite o código do quinto produto a ser selecionado: ");
                    codItemAdicionadoNota5 = Convert.ToInt32(Console.ReadLine());
                    AdicionaItemNotaFiscal(codItemAdicionadoNota5);
                }
                if (qtdItensSelecionados >= 6)
                {
                    //Produto selecionado 6
                    Console.Write("\nDigite o código do sexto produto a ser selecionado: ");
                    codItemAdicionadoNota6 = Convert.ToInt32(Console.ReadLine());
                    AdicionaItemNotaFiscal(codItemAdicionadoNota6);
                }
                if (qtdItensSelecionados >= 7)
                {
                    //Produto selecionado 7
                    Console.Write("\nDigite o código do sétimo produto a ser selecionado: ");
                    codItemAdicionadoNota7 = Convert.ToInt32(Console.ReadLine());
                    AdicionaItemNotaFiscal(codItemAdicionadoNota7);
                }

                //opção para retirar ate 3 itens se ele quiser.
                Console.Write("Deseja retirar algum item (S - sim ou N - não)? ");
                string retornoSeRemoveItem = Console.ReadLine();

                if (retornoSeRemoveItem == "S" || retornoSeRemoveItem == "s")
                {
                    Console.Write("Quantos itens deseja remover? ");
                    int qtdItensRemovidos = Convert.ToInt32(Console.ReadLine());

                    if (qtdItensRemovidos >= 1)
                    {
                        Console.Write("Digite o código do produto a remover: ");
                        codItemRemovido = Convert.ToInt32(Console.ReadLine());
                        RemoveItemNotaFiscal(codItemRemovido);
                    }
                    if (qtdItensRemovidos >= 2)
                    {
                        Console.Write("Digite o código do produto a remover: ");
                        codItemRemovido = Convert.ToInt32(Console.ReadLine());
                        RemoveItemNotaFiscal(codItemRemovido);

                    }
                    if (qtdItensRemovidos >= 3)
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
                    Console.Write("Qual o valor de desconto do seu cupom?");
                    valorDesconto = Convert.ToDouble(Console.ReadLine());
                    CupomDesconto(valorDesconto);
                }
                
                //opção para ele pagar com mais de uma forma, colocando o valor em cada uma das formas.
                //Pode escolher entre pagar em dinheiro e cartão
                Console.WriteLine("\nSelecione a forma de pagamento conforme listado abaixo:");
                Console.WriteLine("1 - Dinheiro");
                Console.WriteLine("2 - Cartão de Crédito");
                Console.WriteLine("3 - Cartão de Débito");
                Console.WriteLine("4 - Boleto");
                Console.Write("\nQuantas formas de pagamento deseja utilizar (de 1 a 4): ");
                int formaPagamento = Convert.ToInt32(Console.ReadLine());

                if (formaPagamento >= 1) 
                {
                    Console.Write("Digite o valor da primeira forma de pagamento selecionada: ");
                    double valorFormaPagamento1 = Convert.ToDouble(Console.ReadLine());
                    AdicionaFormaPagamento(formaPagamento, valorFormaPagamento1);
                }
                if (formaPagamento >= 2)
                {
                    Console.Write("Digite o valor da segunda forma de pagamento selecionada: ");
                    double valorFormaPagamento2 = Convert.ToDouble(Console.ReadLine());
                    AdicionaFormaPagamento(formaPagamento, valorFormaPagamento2);
                }
                if (formaPagamento >= 3)
                {
                    Console.Write("Digite o valor da terceira forma de pagamento selecionada: ");
                    double valorFormaPagamento3 = Convert.ToDouble(Console.ReadLine());
                    AdicionaFormaPagamento(formaPagamento, valorFormaPagamento3);
                }
                if (formaPagamento >= 4)
                {
                    Console.Write("Digite o valor da quarta forma de pagamento selecionada: ");
                    double valorFormaPagamento4 = Convert.ToDouble(Console.ReadLine());
                    AdicionaFormaPagamento(formaPagamento, valorFormaPagamento4);
                }

                //Se ele gastar mais de 100 reais ele ganha 10 pontos de fidelidade, cada ponto de fidelidade da a ele 0,5% de desconto na próxima compra.
                Console.Write("\nPontos de fidelidade adquiridos com esta compra: ");
                PontosFidelidade(totalPagamento);

                


                //Simular uma nota fiscal simples - em texto no terminal.
            }


            Console.ReadKey();
        }
    }
}
