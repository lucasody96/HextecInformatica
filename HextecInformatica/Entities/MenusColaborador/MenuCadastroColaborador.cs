using HextecInformatica.Entities.Core;
using HextecInformatica.Interfaces;
using HextecInformatica.Repositories;

namespace HextecInformatica.Entities.MenusColaborador
{
    public class MenuCadastroColaborador: Menu, IMenus<ColaboradorRepository>
    {
        private readonly ColaboradorRepository ColaboradorRepository;
        public MenuCadastroColaborador(ColaboradorRepository colabRepos)
        {
            Id = 4;
            Descricao = "Cadastro de Colaborador";
            ColaboradorRepository = colabRepos;
        }
        public override void AcionaMenu()
        {
            int opcaoSelecionada;
            do
            {
                Console.Clear();
                ExibirCabecalho(Descricao!);

                Visualizar(ColaboradorRepository);

                Console.WriteLine("\nOpções Disponíveis:\n");
                Console.WriteLine("  [1] - Inserir Novo Colaborador");
                Console.WriteLine("  [2] - Remover Colaborador");
                Console.WriteLine("  [3] - Atualizar Dados do Colaborador");
                Console.WriteLine("  [4] - Retornar ao Menu Anterior\n");

                opcaoSelecionada = Utils.EvitaQuebraCodInt("Escolha uma opção: ");

                switch (opcaoSelecionada)
                {
                    case 1:
                        Adicionar(ColaboradorRepository);
                        break;

                    case 2:
                        Remover(ColaboradorRepository);
                        break;
                    case 3:
                        Atualizar(ColaboradorRepository);
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

        public void Adicionar(ColaboradorRepository colabRepos)
        {

            Console.Clear();
            Utils.FormataCabecalho("Adicionar Colaborador");

            Visualizar(colabRepos);

            Console.Write("Digite o nome do colaborador: ");
            string? nomeColaborador = Console.ReadLine();
            Console.Write("Digite o login do colaborador: ");
            string? loginColaborador = Console.ReadLine();
            Console.Write("Digite o CPF do colaborador: ");
            string? cpf = Console.ReadLine();
            Console.Write("Digite a senha do colaborador: ");
            string? senha = Utils.LerSenhaComAsterisco();
            Console.Write("Confirme a senha do colaborador: ");
            string? confirmaSenha = Utils.LerSenhaComAsterisco();

            if (senha != confirmaSenha)
            {
                Console.WriteLine("\nAs senhas não coincidem! Operação cancelada.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Colaborador novoColaborador = new(nomeColaborador!, cpf!, loginColaborador!, senha!);
            colabRepos.Adiciona(novoColaborador);

            Console.WriteLine($"\nColaborador '{novoColaborador.Nome}' cadastrado com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");

            Console.ReadKey();
        }
        public void Remover(ColaboradorRepository colabRepos)
        {
            Console.Clear();
            Utils.FormataCabecalho("Remover Cadastro do Colaborador");
            Visualizar(colabRepos);

            int idColaborador = Utils.EvitaQuebraCodInt("\nDigite o ID do colaborador que deseja remover: ");
            Colaborador? ColaboradorParaRemover = colabRepos.BuscaID(idColaborador);

            if (ColaboradorParaRemover != null)
            {
                Console.WriteLine($"\nTem certeza que deseja remover o colaborador '{ColaboradorParaRemover.Nome}'? (S/N): ");
                string? confirmacao = Console.ReadLine();

                if (confirmacao!.ToUpper() == "S")
                {
                    colabRepos.Delete(ColaboradorParaRemover);
                    Console.WriteLine($"\nColaborador '{ColaboradorParaRemover.Nome}' removido com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nOperação cancelada. Nenhum colaborador foi removido. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine("\nColaborador não encontrado. Verifique o ID e tente novamente.");
            }
        }

        public void Atualizar(ColaboradorRepository colabRepos)
        {
            Console.Clear();
            Utils.FormataCabecalho("Atualizar Dados do Colaborador");
            Visualizar(colabRepos);

            int idColaborador = Utils.EvitaQuebraCodInt("\nDigite o ID do colaborador que deseja atualizar: ");
            Colaborador? colaboradorAtualizar = colabRepos.BuscaID(idColaborador);

            if (colaboradorAtualizar != null)
            {
                Console.WriteLine($"\nAtualizando dados do colaborador '{colaboradorAtualizar.Nome}':");

                Console.WriteLine("Campos disponíveis para atualização: ");
                Console.WriteLine("  [1] - Nome");
                Console.WriteLine("  [2] - CPF");
                Console.WriteLine("  [3] - Login");
                Console.WriteLine("  [4] - Senha");

                int campoEscolhido = Utils.EvitaQuebraCodInt("\nEscolha o campo que deseja atualizar (1-4): ");

                colabRepos.Update(colaboradorAtualizar, campoEscolhido);
            }
            else
            {
                Console.WriteLine("\nProduto não encontrado. Verifique o ID e tente novamente.");
            }
        }
        public void Visualizar(ColaboradorRepository colabRepos)
        {
            Console.WriteLine();
            Utils.FormataCabecalho("COLABORADORES CADASTRADOS");

            Utils.FormataCabecalhoColaborador();

            foreach (var colaborador in colabRepos.ListaColaboradores!)
            {
                Utils.FormataLinhaColaborador(colaborador.Id, colaborador.Nome!, colaborador.Login, colaborador.Cpf);
            }

            Utils.ImprimeLinhaSeparadora('-');
        }
    }
}
