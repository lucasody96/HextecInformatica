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
            }else
            {
                Cliente clienteNovo = new(nome);
                clienteNovo.DadosCliente();
                Console.WriteLine(clienteNovo.MensagemBoasVindas());
                clienteRepos.Adiciona(clienteNovo);
                Console.ReadKey();
            }

            return clienteExiste;
        }
    }
}
