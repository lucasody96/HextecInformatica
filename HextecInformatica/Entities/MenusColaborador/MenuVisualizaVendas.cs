using HextecInformatica.Entities.Core;
using HextecInformatica.Repositories;
using HextecInformatica.Entities;

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

        public void BuscaNumeroNota(VendaRepository vendaRepos)
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
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"\nNenhuma venda encontrada com o número da nota fiscal: {numeroNota}");
            }
        }

        public void BuscarClienteNota(VendaRepository vendaRepos)
        {
            Console.Clear();
            Utils.FormataCabecalho("BUSCA DE VENDAS POR CLIENTE DA NOTA FISCAL");
            Console.Write("Digite o nome do cliente que deseja buscar: ");
            string? nomeCliente = Console.ReadLine();
            var vendasEncontradas = vendaRepos.BuscaVendaNomeCliente(nomeCliente!);

            if (vendasEncontradas != null)
            {
                Utils.FormataCabecalhoVendas();
                Utils.FormataLinhaVenda(
                    vendasEncontradas.NumeroNotaFiscal,
                    vendasEncontradas.DataVenda,
                    vendasEncontradas.ClienteNota,
                    vendasEncontradas.ValorTotalVenda,
                    vendasEncontradas.ValorFrete,
                    vendasEncontradas.ValorDesconto
                );
                Utils.ImprimeLinhaSeparadora('-');
            }
            else
            {
                Console.WriteLine($"\nNenhuma venda encontrada do cliente: {nomeCliente}");
            }
        }

        public void Visualizar(VendaRepository vendaRepos)
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

                // Opcional: Se quiser mostrar os itens logo abaixo de cada venda:
                // ExibirItensDaVenda(notaFiscal); 
            }
        }
    }
}
