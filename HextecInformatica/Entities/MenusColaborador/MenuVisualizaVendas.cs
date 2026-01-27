using HextecInformatica.Entities.Core;
using HextecInformatica.Repositories;

namespace HextecInformatica.Entities.MenusColaborador
{
    public class MenuVisualizaVendas: Menu
    {
        private readonly VendaRepository VendaRepository;
        public MenuVisualizaVendas(VendaRepository vendaRepos)
        {
            Id = 2;
            Descricao = "Visualizar Vendas";
            VendaRepository = vendaRepos;
            
        }
        public override void AcionaMenu()
        {
            int opcaoSelecionada;
            do
            {
                Console.Clear();
                ExibirCabecalho(Descricao!);

                Visualizar(VendaRepository);
                Console.WriteLine("\nOpções Disponíveis:\n");
                Console.WriteLine("  [1] - Buscar venda pelo número da nota");
                Console.WriteLine("  [2] - Buscar venda pelo cliente da nota");
                Console.WriteLine("  [3] - Retornar ao Menu Anterior\n");

                opcaoSelecionada = Utils.EvitaQuebraCodInt("Escolha uma opção: ");

                switch (opcaoSelecionada)
                {
                    case 1:
                        BuscaNumeroNota(VendaRepository);
                        break;

                    case 2:
                        BuscarClienteNota(VendaRepository);
                        break;
                    case 3:
                        //retornar ao menu anterior
                        Console.WriteLine("Retornando ao menu anterior... Pressione qualquer tecla para continuar");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente...");
                        Console.ReadKey();
                        break;
                }

            } while (opcaoSelecionada != 3);

        }

        public static void BuscaNumeroNota(VendaRepository vendaRepos)
        {
            Console.Clear();
            Utils.FormataCabecalho("BUSCA DE VENDAS POR NÚMERO DA NOTA FISCAL");
            int numeroNota = Utils.EvitaQuebraCodInt("Digite o número da nota fiscal que deseja buscar: ");

            var vendaEncontrada = vendaRepos.BuscaVendaNumNota(numeroNota);
            if (vendaEncontrada != null)
            {
                Console.WriteLine();
                Utils.FormataCabecalhoVendas();
                Utils.FormataLinhaVenda(
                    vendaEncontrada.NumeroNotaFiscal,
                    vendaEncontrada.DataVenda,
                    vendaEncontrada.ClienteNota,
                    vendaEncontrada.ValorTotalVenda,
                    vendaEncontrada.ValorFrete,
                    vendaEncontrada.ValorDesconto
                );

                Utils.ExibirItensDaVenda(vendaEncontrada);
                Console.WriteLine("Pressione alguma tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"\nNenhuma venda encontrada com o número da nota fiscal: {numeroNota}");
            }
        }

        public static void BuscarClienteNota(VendaRepository vendaRepos)
        {
            Console.Clear();
            Utils.FormataCabecalho("BUSCA DE VENDAS POR CLIENTE DA NOTA FISCAL");
            Console.Write("Digite o nome do cliente que deseja buscar: ");
            string? nomeCliente = Console.ReadLine();

            var listaVendasEncontradas = vendaRepos.BuscaVendaNomeCliente(nomeCliente!);

            if (listaVendasEncontradas.Count > 0)
            {
                Utils.FormataCabecalhoVendas();
                foreach (var vendas in listaVendasEncontradas)
                Utils.FormataLinhaVenda(
                    vendas.NumeroNotaFiscal,
                    vendas.DataVenda,
                    vendas.ClienteNota,
                    vendas.ValorTotalVenda,
                    vendas.ValorFrete,
                    vendas.ValorDesconto
                );

                Console.WriteLine("Pressione alguma tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"\nNenhuma venda encontrada do cliente: {nomeCliente}");
            }
        }

        public static void Visualizar(VendaRepository vendaRepos)
        {
            Console.WriteLine();

            // 1. Cabeçalho Geral
            Utils.FormataCabecalhoVendas();

            // 2. Loop das Vendas
            foreach (var notaFiscal in vendaRepos.ListaVendas!)
            {
                Utils.FormataLinhaVenda(
                    notaFiscal.NumeroNotaFiscal,
                    notaFiscal.DataVenda,
                    notaFiscal.ClienteNota,
                    notaFiscal.ValorTotalVenda,
                    notaFiscal.ValorFrete,
                    notaFiscal.ValorDesconto
                );
            }
        }
    }
}
