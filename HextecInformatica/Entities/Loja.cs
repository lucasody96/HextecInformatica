using HextecInformatica.Entities.Core;

namespace HextecInformatica.Entities
{
    public class Loja(string nome)
    {
        public string Nome { get; set; } = nome;

        public List<Cliente> ListaClientes { get; set; } = [];
        public List<Produto> ListaProdutos { get; set; } = [];
        public List<Colaborador> ListaColaboradores { get; set; } = [];

    }
}
