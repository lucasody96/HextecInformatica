using HextecInformatica.Entities.Core;
using HextecInformatica.Interfaces;
using HextecInformatica.Repositories;

namespace HextecInformatica.Entities.MenusColaborador
{
    public class MenuCadastroCliente: Menu, IMenus<ClienteRepository>
    {
        private readonly ClienteRepository ClienteRepository;
        public MenuCadastroCliente(ClienteRepository ClienteRepo)
        {
            Id = 3;
            Descricao = "Cadastro de Cliente";
            ClienteRepository = ClienteRepo;
        }

        public override void AcionaMenu()
        {
            int opcaoSelecionada;
            do
            {
                Console.Clear();
                ExibirCabecalho(Descricao!);

                Visualizar(ClienteRepository);

                Console.WriteLine("\nOpções Disponíveis:\n");
                Console.WriteLine("  [1] - Inserir Novo Cliente");
                Console.WriteLine("  [2] - Remover Cliente");
                Console.WriteLine("  [3] - Atualizar Dados do Cliente");
                Console.WriteLine("  [4] - Retornar ao Menu Anterior\n");

                opcaoSelecionada = Utils.EvitaQuebraCodInt("Escolha uma opção: ");

                switch (opcaoSelecionada)
                {
                    case 1:
                        Adicionar(ClienteRepository);
                        break;

                    case 2:
                        Remover(ClienteRepository);
                        break;
                    case 3:
                        Atualizar(ClienteRepository);
                        break;
                    case 4:
                        //retornar ao menu anterior
                        Console.WriteLine("Retornando ao menu anterior... Pressione qualquer tecla para continuar");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente...");
                        Console.ReadKey();
                        break;
                }

            } while (opcaoSelecionada != 4);

        }

        public void Adicionar(ClienteRepository clienteRepos)
        {
            Console.Clear();
            Utils.FormataCabecalho("Adicionar Cliente");

            Visualizar(clienteRepos);
            
            Console.Write("Digite o nome do cliente: ");
            string? nomeCliente = Console.ReadLine();
            Console.Write("Digite o tipo de pessoa (F - Física/J - Jurídica): ");
            string? tipoPessoa = Console.ReadLine();

            bool validaCPF = false;
            string? cpf = "";
            string? cnpj = "";

            while (validaCPF == false)
            {
                if (tipoPessoa!.ToUpper() == "F" && tipoPessoa.Length != 11)
                {
                    Console.Write("Digite o CPF do cliente: ");
                    cpf = Console.ReadLine();
                    validaCPF = true;
                }
                else if (tipoPessoa.ToUpper() == "J" && tipoPessoa.Length != 14)
                {
                    Console.Write("Digite o CNPJ do cliente: ");
                    cnpj = Console.ReadLine();
                    validaCPF = true;
                }
                else
                {
                    Console.WriteLine("Tipo de pessoa inválido! Operação cancelada. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    return;
                }
            }

            Cliente novoCliente = new(nomeCliente!, tipoPessoa!, cpf!, cnpj!);
            clienteRepos.Adiciona(novoCliente);

            Console.WriteLine($"\nCliente '{novoCliente.Nome}' cadastrado com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");

            Console.ReadKey();
        }

        public void Remover(ClienteRepository clienteRepos)
        {
            // Implementar lógica para remover produto do estoque
            Console.Clear();
            Utils.FormataCabecalho("Remover Cadastro do Cliente");
            Visualizar(clienteRepos);

            int idCliente = Utils.EvitaQuebraCodInt("\nDigite o ID do cliente que deseja remover: ");
            Cliente? ClienteParaRemover = clienteRepos.BuscaID(idCliente);

            if (ClienteParaRemover != null)
            {
                Console.WriteLine($"\nTem certeza que deseja remover o cliente '{ClienteParaRemover.Nome}'? (S/N): ");
                string? confirmacao = Console.ReadLine();

                if (confirmacao!.ToUpper() == "S")
                {
                    clienteRepos.Delete(ClienteParaRemover);
                    Console.WriteLine($"\nCliente '{ClienteParaRemover.Nome}' removido com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nOperação cancelada. Nenhum cliente foi removido. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine("\nCliente não encontrado. Verifique o ID e tente novamente.");
            }
        }

        public void Atualizar(ClienteRepository clienteRepos)
        {
            Console.Clear();
            Utils.FormataCabecalho("Atualizar Dados do Cliente");
            Visualizar(clienteRepos);

            int idCliente = Utils.EvitaQuebraCodInt("\nDigite o ID do cliente que deseja atualizar: ");
            Cliente? clienteAtualizar = clienteRepos.BuscaID(idCliente);

            if (clienteAtualizar != null)
            {
                Console.WriteLine($"\nAtualizando dados do cliente '{clienteAtualizar.Nome}':");

                Console.WriteLine("Campos disponíveis para atualização: ");
                Console.WriteLine("  [1] - Nome");
                Console.WriteLine("  [2] - Tipo de Pessoa (Física/Jurídica)");
                Console.WriteLine("  [3] - CPF / CNPJ");

                int campoEscolhido = Utils.EvitaQuebraCodInt("\nEscolha o campo que deseja atualizar (1-3): ");

                clienteRepos.Update(clienteAtualizar, campoEscolhido);

            }
            else
            {
                Console.WriteLine("\nProduto não encontrado. Verifique o ID e tente novamente.");
            }

        }

        public void Visualizar(ClienteRepository clienteRepos)
        {
            Console.WriteLine();
            Utils.FormataCabecalho("CLIENTES CADASTRADOS");

            Utils.FormataCabecalhoTabelaClientes();

            foreach (var cliente in clienteRepos.ListaClientes!)
            {
                string documento = cliente.TipoPessoa == "F" ? cliente.Cpf : cliente.Cnpj;

                Utils.FormataLinhaCliente(cliente.Id, cliente.Nome!, cliente.TipoPessoa!, documento!);
            }

            Utils.ImprimeLinhaSeparadora('-');
        }
    }

}
