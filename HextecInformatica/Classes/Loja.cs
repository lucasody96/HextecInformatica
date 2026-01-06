using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class Loja
    {
        public string Nome { get; set; }

        public List<Cliente> ListaClientes { get; set; } = new List<Cliente>();

        public List<Produto> ListaProdutos { get; set; } = new List<Produto>();

        public Utils Utils { get; set; } = new Utils();

        public Loja(string nome)
        {
            Nome = nome;
        }

        public void AdicionarProduto(Produto produto)
        {
            ListaProdutos.Add(produto);
        }

        public void CadastrarCliente (Cliente NovoCliente) 
        {
            ListaClientes.Add(NovoCliente);
        }

        public Cliente ClienteJaComprou (string nomeInformado)
        {
            return ListaClientes.FirstOrDefault(clientes => clientes.Nome == nomeInformado);        
        } 

    }
}
