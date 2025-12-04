using System;
using System.Collections.Generic;
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
            //variáveis usadas no escopo principal e funções
            double totalPagamento = 0;
            
            //Métodos/funções
            void ImpressaoCompras(int produtoSelecionado)
            {
                if (produtoSelecionado == CODIGO_PRODUTO_1)
                    Console.WriteLine($"{NOME_PRODUTO_1} - R$ {VALOR_PRODUTO_1:F2}");
                else if (produtoSelecionado == CODIGO_PRODUTO_2)
                    Console.WriteLine($"{NOME_PRODUTO_2} - R$ {VALOR_PRODUTO_2:F2}");
                else if (produtoSelecionado == CODIGO_PRODUTO_3)
                    Console.WriteLine($"{NOME_PRODUTO_3} - R$ {VALOR_PRODUTO_3:F2}");
                else if (produtoSelecionado == CODIGO_PRODUTO_4)
                    Console.WriteLine($"{NOME_PRODUTO_1} - R$ {VALOR_PRODUTO_1:F2}");
                else if (produtoSelecionado == CODIGO_PRODUTO_5)
                    Console.WriteLine($"{NOME_PRODUTO_5} - R$ {VALOR_PRODUTO_5:F2}");
                else if (produtoSelecionado == CODIGO_PRODUTO_6)
                    Console.WriteLine($"{NOME_PRODUTO_6} - R$ {VALOR_PRODUTO_6:F2}");
                else if (produtoSelecionado == CODIGO_PRODUTO_7)
                    Console.WriteLine($"{NOME_PRODUTO_7} - R$ {VALOR_PRODUTO_7:F2}");
                else
                    Console.WriteLine("Código de produto inexistente, não será somado no valor a ser pago!");
            }

            void ValorTotalCompras(int produtoSelecionado)
            {
                if (produtoSelecionado == CODIGO_PRODUTO_1)
                    totalPagamento += VALOR_PRODUTO_1;
                else if (produtoSelecionado == CODIGO_PRODUTO_2)
                    totalPagamento += VALOR_PRODUTO_2;
                else if (produtoSelecionado == CODIGO_PRODUTO_3)
                    totalPagamento += VALOR_PRODUTO_3;
                else if (produtoSelecionado == CODIGO_PRODUTO_4)
                    totalPagamento += VALOR_PRODUTO_4;
                else if (produtoSelecionado == CODIGO_PRODUTO_5)
                    totalPagamento += VALOR_PRODUTO_5;
                else if (produtoSelecionado == CODIGO_PRODUTO_6)
                    totalPagamento += VALOR_PRODUTO_6;
                else if (produtoSelecionado == CODIGO_PRODUTO_7)
                    totalPagamento += VALOR_PRODUTO_7;
            }

            void ImprimeMensagemSucesso(int formaPagamentoSelecionada)
            {
                if (formaPagamentoSelecionada == 1)
                    Console.WriteLine($"\nCompra no valor de R$ {totalPagamento:F2} no cartão foi concluída com sucesso!");
                else if (formaPagamentoSelecionada == 2)
                    Console.WriteLine($"\nCompra no valor de R$ {totalPagamento:F2} em dinheiro foi concluída com sucesso!");
                else
                    Console.WriteLine("\nCondição de pagamento inválida, programa sendo encerrado...");
            }

            //Boas vindas à loja
            Console.WriteLine($"Bem vindo a loja {NOME_LOJA}");
            Console.WriteLine("=============================");
            Console.Write("\nDigite seu nome: ");
            string nomeUsuario = Console.ReadLine();

            //Listar os produtos da loja e permitir que ele escolha - ele só pode escolher 5 produtos
            Console.WriteLine($"\nSeja bem vindo {nomeUsuario}! Lista dos produtos disponíveis no nosso estoque para compra:");
            Console.WriteLine("\nCódigo - Nome do produto - Valor ");
            Console.WriteLine($" {CODIGO_PRODUTO_1} - {NOME_PRODUTO_1} - R$ {VALOR_PRODUTO_1:F2}"+
                              $"\n {CODIGO_PRODUTO_2} - {NOME_PRODUTO_2} - R$ {VALOR_PRODUTO_2:F2}"+
                              $"\n {CODIGO_PRODUTO_3} - {NOME_PRODUTO_3} - R$ {VALOR_PRODUTO_3:F2}"+
                              $"\n {CODIGO_PRODUTO_4} - {NOME_PRODUTO_4} - R$ {VALOR_PRODUTO_4:F2}"+
                              $"\n {CODIGO_PRODUTO_5} - {NOME_PRODUTO_5} - R$ {VALOR_PRODUTO_5:F2}"+
                              $"\n {CODIGO_PRODUTO_6} - {NOME_PRODUTO_6} - R$ {VALOR_PRODUTO_6:F2}"+
                              $"\n {CODIGO_PRODUTO_7} - {NOME_PRODUTO_7} - R$ {VALOR_PRODUTO_7:F2}");
            Console.WriteLine("\nVocê somente pode escolher 5 produtos dos listados acima.");
            Console.WriteLine("Caso não deseje escolher algum produto em uma determinada posição, digite 0");

            //Seleção de produtos e soma do valor total de pagamento
            //Produto selecionado 1
            Console.Write("\nDigite o código do primeiro produto a ser selecionado: ");
            int produtoSelecionado1 = Convert.ToInt32(Console.ReadLine());
            ValorTotalCompras(produtoSelecionado1);
            //Produto selecionado 2
            Console.Write("\nDigite o código do segundo produto a ser selecionado: ");
            int produtoSelecionado2 = Convert.ToInt32(Console.ReadLine());
            ValorTotalCompras(produtoSelecionado2);
            //Produto selecionado 3
            Console.Write("\nDigite o código do terceiro produto a ser selecionado: ");
            int produtoSelecionado3 = Convert.ToInt32(Console.ReadLine());
            ValorTotalCompras(produtoSelecionado3);
            //Produto selecionado 4
            Console.Write("\nDigite o código do quarto produto a ser selecionado: ");
            int produtoSelecionado4 = Convert.ToInt32(Console.ReadLine());
            ValorTotalCompras(produtoSelecionado4);
            //Produto selecionado 5
            Console.Write("\nDigite o código do quinto produto a ser selecionado: ");
            int produtoSelecionado5 = Convert.ToInt32(Console.ReadLine());
            ValorTotalCompras(produtoSelecionado5);

            Console.WriteLine("\nLista de produtos comprados:");
            //Produto selecionado 1
            ImpressaoCompras(produtoSelecionado1);
            //Produto selecionado 2
            ImpressaoCompras(produtoSelecionado2);
            //Produto selecionado 3
            ImpressaoCompras(produtoSelecionado3);
            //Produto selecionado 4
            ImpressaoCompras(produtoSelecionado4);
            //Produto selecionado 5
            ImpressaoCompras(produtoSelecionado5);

            //Pode escolher entre pagar em dinheiro e cartão
            Console.WriteLine("\nSelecione a forma de pagamento conforme listado abaixo:");
            Console.WriteLine("1 - Cartão");
            Console.WriteLine("2 - Dinheiro");
            Console.Write("\nSelecione a forma de pagamento: ");
            int formaPagamento = Convert.ToInt32(Console.ReadLine());

            //Após selecionar dar uma mensagem de sucesso.
            ImprimeMensagemSucesso(formaPagamento);

            Console.ReadKey();
        }
    }
}
