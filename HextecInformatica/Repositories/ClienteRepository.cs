using HextecInformatica.Entities;
using HextecInformatica.Interfaces;

namespace HextecInformatica.Repositories
{

    public class ClienteRepository : IRepositories<Cliente>
    {
        public List<Cliente>? ListaClientes { get; set; } = [];

        public void Adiciona(Cliente cliente)
        {
            ListaClientes?.Add(cliente);
        }

        public Cliente? BuscaLoginNome(string nome)
        {
            return ListaClientes?.FirstOrDefault(cliente => cliente.Nome == nome);
        }

        public Cliente? BuscaID(int id)
        {
            return ListaClientes?.FirstOrDefault(cliente => cliente.Id == id);
        }

        public void Delete(Cliente cliente)
        {
            ListaClientes?.Remove(cliente);
        }

        public void Update(Cliente cliente, int campoAlterado)
        {
            var clienteSelecionado = BuscaID(cliente.Id);

            if (clienteSelecionado != null)
            {
                switch (campoAlterado)
                {
                    case 1:
                        Console.Write("Digite o nome do cliente: ");
                        string? novoNome = Console.ReadLine();
                        clienteSelecionado.Nome = novoNome ?? string.Empty;
                        break;

                    case 2:
                        Console.Write("Informe o Tipo de Pessoa do cliente: : ");
                        string? NovoTipoPessoa = Console.ReadLine();

                        if (NovoTipoPessoa!.ToUpper() == "F")
                        {
                            clienteSelecionado.TipoPessoa = "F";
                            Update(clienteSelecionado, 3);
                        }else
                        {
                            clienteSelecionado.TipoPessoa = "J";
                            Update(clienteSelecionado, 3);
                        }
                       break;
                    case 3:

                        if (clienteSelecionado.TipoPessoa == "F")
                        {
                            Console.Write("Informe o CPF do cliente: ");
                            string? novoCpf = Console.ReadLine();

                            if (novoCpf!.Length != 11)
                            {
                                Console.WriteLine("CPF inválido. Deve conter 11 dígitos.");
                                break;
                            }

                            clienteSelecionado.Cpf = novoCpf ?? string.Empty;
                            clienteSelecionado.Cnpj = string.Empty;
                        }
                        else if (clienteSelecionado.TipoPessoa == "J")
                        {
                            Console.Write("Informe o CNPJ do cliente: ");
                            string? novoCnpj = Console.ReadLine();

                            if (novoCnpj!.Length != 14)
                            {
                                Console.WriteLine("CNPJ inválido. Deve conter 14 dígitos.");
                                break;
                            }

                            clienteSelecionado.Cnpj = novoCnpj ?? string.Empty;
                            clienteSelecionado.Cpf = string.Empty;
                        }
                        break;
                    default:
                        Console.WriteLine("Campo inválido para atualização.");
                        break;
                }
            }
        }
    }
}
