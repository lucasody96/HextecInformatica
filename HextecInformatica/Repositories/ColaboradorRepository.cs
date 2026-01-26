using HextecInformatica.Entities;
using HextecInformatica.Interfaces;

namespace HextecInformatica.Repositories
{
    public class ColaboradorRepository : IRepositories<Colaborador>
    {
        public List<Colaborador> ListaColaboradores { get; private set; } = [];

        public void Adiciona(Colaborador colaborador)
        {
            ListaColaboradores.Add(colaborador);
        }

        public Colaborador? BuscaLoginNome(string login)
        {
            return ListaColaboradores.FirstOrDefault(colaborador => colaborador.Login == login);
        }

        public Colaborador? BuscaID(int id)
        {
            return ListaColaboradores.FirstOrDefault(colaborador => colaborador.Id == id);
        }

        public void Delete(Colaborador colaborador)
        {
            ListaColaboradores?.Remove(colaborador);
        }

        public void Update(Colaborador colaborador, int campoAlterado)
        {

            var clienteSelecionado = BuscaID(colaborador.Id);

            if (clienteSelecionado != null)
            {
                switch (campoAlterado)
                {
                    case 1:
                        Console.Write("Digite o nome do colaborador: ");
                        string? novoNome = Console.ReadLine();
                        clienteSelecionado.Nome = novoNome ?? string.Empty;
                        break;

                    case 2:
                        Console.Write("Digite o CPF do colaborador : ");
                        string? novoCpf = Console.ReadLine();

                        if (novoCpf!.Length != 11)
                        {
                            Console.WriteLine("CPF inválido. Deve conter 11 dígitos.");
                            break;
                        }

                        clienteSelecionado.Cpf = novoCpf ?? string.Empty;

                        break;
                    case 3:

                        Console.Write("Digite o login do colaborador: ");
                        string? novoLogin = Console.ReadLine();
                        clienteSelecionado.Login = novoLogin ?? string.Empty;

                        break;
                    case 4:
                        Console.Write("Digite a senha do colaborador: ");
                        string? novaSenha = Console.ReadLine();
                        Console.Write("Confirme a senha do colaborador: ");
                        string? ConfirmaSenha = Console.ReadLine();

                        if (novaSenha != ConfirmaSenha)
                        {
                            Console.WriteLine("As senhas não coincidem. Tente novamente.");
                            Console.ReadKey();
                            break;
                        }

                        clienteSelecionado.Senha = novaSenha ?? string.Empty;
                        break;

                    default:
                        Console.WriteLine("Campo inválido para atualização.");
                        break;
                }
            }

        }

    }
}
