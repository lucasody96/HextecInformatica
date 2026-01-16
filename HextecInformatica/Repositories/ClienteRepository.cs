using HextecInformatica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Repositories
{
    
    public class ClienteRepository
    {
        public List<Cliente>? ListaClientes { get; set; }

        public Cliente? BuscaClientes(string nome)
        {
            return ListaClientes?.FirstOrDefault(cliente => cliente.Nome == nome);
        }

    }
}
