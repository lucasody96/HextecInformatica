using HextecInformatica.Entities;
using HextecInformatica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }

            return clienteExiste;
        }
    }
}
