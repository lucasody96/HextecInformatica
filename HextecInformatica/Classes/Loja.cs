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

        public List<Cliente> Clientes { get; set; }

        public List<Produto> ListaProdutos { get; set; }

        public Loja(string nome)
        {
            Nome = nome;

            ListaProdutos = new List<Produto>();

            Clientes = new List<Cliente>();
        }

        public void AdicionarProduto(Produto produto)
        {
            ListaProdutos.Add(produto);
        }

        public bool ClienteJaComprou (string nomeInformado)
        {
            foreach (var cliente in Clientes)
            {
                if (cliente.Nome == nomeInformado)
                {
                    return true;
                }
            }

            return false;
        } 

        public void CatalogoProdutos()
        {
            //Logica semelhante a vista na aula
            foreach(var produtos in ListaProdutos)
            {
                Console.WriteLine(produtos);
            }
        }

    }
}
