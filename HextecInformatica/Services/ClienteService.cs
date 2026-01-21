using HextecInformatica.Entities;
using HextecInformatica.Repositories;

namespace HextecInformatica.Services
{
    public class ClienteService
    {
        public Cliente? VerificaClienteExistente(ClienteRepository clienteRepos, string nome)
        {
            var clienteExiste = clienteRepos.BuscaNome(nome);

            if (clienteExiste != null)
            {
                Console.WriteLine(clienteExiste.MensagemBoasVindas());
                Console.ReadKey();
                return clienteExiste;
            }else
            {
                Cliente clienteNovo = new(nome);
                DadosCliente(clienteNovo);
                Console.WriteLine(clienteNovo.MensagemBoasVindas());
                clienteRepos.Adiciona(clienteNovo);
                Console.ReadKey();
                return clienteNovo;
            }
        }

        public static void DadosCliente(Cliente cliente)
        {
            bool tipoPessoaCorreto = false;
            while (!tipoPessoaCorreto)
            {
                Console.Write("Trata-se de um cliente pessoa física (F) ou pessoa jurídica (J): ");
                var input = Console.ReadLine();
               cliente.TipoPessoa = input != null ? input.ToUpper() : string.Empty;

                if (cliente.TipoPessoa == "F")
                {
                    bool cpfCorreto = false;
                    do
                    {
                        Console.Write("\nInforme o seu CPF: ");
                        cliente.Cpf = Console.ReadLine() ?? string.Empty;

                        if (cliente.Cpf.Length == 11)
                            cpfCorreto = true;
                        else
                            Console.WriteLine("CPF informado incorreto, digite um CPF válido");

                    } while (!cpfCorreto);

                    tipoPessoaCorreto = true;

                }
                else if (cliente.TipoPessoa == "J")
                {
                    bool cnpjCorreto = false;
                    do
                    {
                        Console.Write("\nInforme o seu CNPJ: ");
                        cliente.Cnpj = Console.ReadLine() ?? string.Empty;

                        if (cliente.Cnpj.Length == 14)
                            cnpjCorreto = true;
                        else
                            Console.WriteLine("CNPJ informado incorreto, digite um CNPJ válido");

                    } while (!cnpjCorreto);

                    tipoPessoaCorreto = true;
                }
                else
                    Console.WriteLine("\nValor informado inválido, informar F para pessoa física ou J para pessoa jurídica");
            }

        }
    }
}
