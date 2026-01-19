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

        public Cliente? BuscaNome(string nome)
        {
            return ListaClientes?.FirstOrDefault(cliente => cliente.Nome == nome);
        }

        public Cliente? BuscaID(int id)
        {
            return ListaClientes?.FirstOrDefault(cliente => cliente.Id == id);
        }

    }
}
