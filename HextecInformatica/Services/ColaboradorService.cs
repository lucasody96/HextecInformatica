using HextecInformatica.Entities;
using HextecInformatica.Entities.Core;
using HextecInformatica.Entities.MenusColaborador;
using HextecInformatica.Repositories;

namespace HextecInformatica.Services
{
    public class ColaboradorService
    {
        private List<Menu> ListaMenus = [];
        public static Colaborador? VerificaColaboradorExistente(ColaboradorRepository colaboradorRepos)
        {
            Colaborador? colaboradorLogado;
            bool tentarNovamente = true;
            Console.Clear();
            Utils.FormataCabecalho("ACESSO PARA COLABORADORES");

            do
            {
                Console.Write("\nLogin: ");

                string? usuarioColaboradorInput = Console.ReadLine();
                string usuarioColaborador = string.IsNullOrWhiteSpace(usuarioColaboradorInput)
                    ? ""
                    : usuarioColaboradorInput.Trim().ToUpper();

                Console.Write("Senha: ");
                string? senhaColaborador = Utils.LerSenhaComAsterisco();

                colaboradorLogado = colaboradorRepos.BuscaLoginNome(usuarioColaborador);

                if (colaboradorLogado != null && colaboradorLogado.Senha == senhaColaborador)
                {
                    Console.WriteLine(colaboradorLogado.MensagemBoasVindas());
                    Console.ReadKey();
                    tentarNovamente = false;
                }
                else
                {
                    Console.WriteLine("\nLogin ou senha incorretos!");
                    Console.Write("Deseja tentar novamente (S/N)? ");
                    string? respLogin = Console.ReadLine();

                    if (respLogin == "N" || respLogin == "n")
                        tentarNovamente = false;
                }

            } while (tentarNovamente == true);

            return colaboradorLogado;
        }

        public void CarregarMenus(ProdutoRepository prodRepo, ClienteRepository ClienteRepo, ColaboradorRepository ColabRepo)
        {
            ListaMenus =
            [
                new MenuGerenciaEstoque(prodRepo),
                new MenuVisualizaVendas(prodRepo),
                new MenuCadastroCliente(ClienteRepo),
                new MenuCadastroColaborador(ColabRepo),
            ];  
        }

        public void ExibeMenusColaborador()
        {
            Console.Clear();
            Utils.FormataCabecalho("MENUS DISPONÍVEIS PARA COLABORADORES");

            Console.WriteLine("\nOpções Disponíveis:\n");
            foreach (var menu in ListaMenus)
            {
                Console.WriteLine($"  [{menu.Id}] - {menu.Descricao}");
            }
            Console.WriteLine("  [5] - Retornar ao Menu Anterior\n");
        }

        public void AcionaMenuColaborador(int opcaoSelecionada)
        {
            Menu? menuSelecionado = MenuEscolhido(opcaoSelecionada);

            while (menuSelecionado == null)
            {
                Console.WriteLine("Opção inválida! Tente novamente.");
                opcaoSelecionada = Utils.EvitaQuebraCodInt("Escolha uma opção: ");
                menuSelecionado = MenuEscolhido(opcaoSelecionada);
            }

            menuSelecionado.AcionaMenu();
        }

        public Menu? MenuEscolhido(int opcaoSelecionada)
        {
            return ListaMenus.FirstOrDefault(menu => menu.Id == opcaoSelecionada);
        }
    }
}
