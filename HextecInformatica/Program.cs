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
            //================================================================
            //PROGRAMA PRINCIPAL
            //================================================================
            Loja Hextec = new Loja("Hextec Informática");

            //Carregar os itens
            Hextec.AdicionarProduto(new Produto(1, "Mouse sem fio", 65.90m, 32));
            Hextec.AdicionarProduto(new Produto(2, "Pen Drive", 44.90m, 25));
            Hextec.AdicionarProduto(new Produto(3, "SSD", 390.49m, 10));
            Hextec.AdicionarProduto(new Produto(4, "Memória Ram", 280.89m, 0));
            Hextec.AdicionarProduto(new Produto(5, "Monitor", 749.99m, 15));
            Hextec.AdicionarProduto(new Produto(6, "Headset Gamer", 231.89m, 0));
            Hextec.AdicionarProduto(new Produto(7, "Placa de vídeo", 2100.99m, 1));

            bool execucaoPrograma = true;

            do
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine($"   Bem-vindo à {Hextec.Nome}");
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
            //5. MÉTODOS/FUNÇÕES Auxiliares
            //================================================================
           
            void IniciarVenda() 
            {
                Cliente ClienteLoja;
              
                Console.Write("\nDigite seu nome: ");
                string nomeCliente = Console.ReadLine();

                //Buscar o cliente usando o método loja
                Cliente clienteExistente = Hextec.ClienteJaComprou(nomeCliente);

                if (clienteExistente != null)
                {
                    // Aproveitamos os dados que vieram da busca
                    ClienteLoja = clienteExistente;

                    Console.WriteLine($"\nSeja bem vindo de volta {nomeCliente}! Pressione enter para seguir com a compra.");
                    Console.ReadKey();
                } 
                else
                {
                    Console.WriteLine($"\nSeja bem vindo {nomeCliente}.");
                    
                    ClienteLoja = new Cliente(nomeCliente);

                    ClienteLoja.DadosCliente();
                    Hextec.CadastrarCliente(ClienteLoja);
                }

                Console.Clear();
                Console.WriteLine("\nLista de produtos disponíveis no nosso estoque para compra: ");
                Console.WriteLine("\nCódigo - Nome do produto - Valor - Em estoque");
                // método "Catálogo de Itens"
                Hextec.CatalogoProdutos();

                //opção para selecionar a quantidade de itens. Criar método
                //Passar a lista da loja para a lista do carrinho
                Carrinho CarrinhoCompraAtual = new Carrinho(Hextec.ListaProdutos);

                bool codZero = false;
                while (!codZero)
                {
                    int codProdutoSelecionado = EvitaQuebraCodInt("\nSelecione o item a ser adicionado ao carrinho ou 0 para sair: ");

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
                string respRemoveItem = Console.ReadLine();

                if (respRemoveItem == "S" || respRemoveItem == "s")
                {
                    codZero = false;
                    while (!codZero)
                    {
                        int codProdutoRemovido = EvitaQuebraCodInt("\nDigite o código do produto a ser removido (0 para sair): ");

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

                // Verificação se todos os itens foram removidos.
                if (CarrinhoCompraAtual.ListaItensCarrinho.Count > 0)
                {
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

                }
                else
                    Console.WriteLine("\nCarrinho está vazio, não é possível prosseguir. Pressione enter para prosseguir.");

                    

                    Console.ReadKey();
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
