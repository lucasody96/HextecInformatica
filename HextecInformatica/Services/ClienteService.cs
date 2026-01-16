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
        public void VerificaClienteExistente(ClienteRepository clienteRepos,string nome)
        {

            var clienteExiste = clienteRepos.BuscaClientes(nome);

            if (clienteRepos != null)
            {
                ClienteRepository clienteRepos1 = clienteRepos;
                Cliente clienteAtual = clienteRepos1;                
                Console.WriteLine(ClienteLoja!.MensagemBoasVindas());
                Console.ReadKey();
            }
            // Aproveitamos os dados que vieram da busca
            ClienteLoja = ClienteExiste;

            if (ClienteExiste != null)
            {
                Console.WriteLine(ClienteLoja!.MensagemBoasVindas());
                Console.ReadKey();
            }
            else
            {
                ClienteLoja = new Cliente(nomeCliente);
                ClienteLoja.DadosCliente();
                Hextec.CadastrarCliente(ClienteLoja);
                Console.WriteLine(ClienteLoja.MensagemBoasVindas());
                Console.ReadKey();
            }
        }
    }
}
