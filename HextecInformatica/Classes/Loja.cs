using HextecInformatica.Classes.Entity.Core;

namespace HextecInformatica.Classes
{
    public class Loja(string nome)
    {
        public string Nome { get; set; } = nome;

        public List<Cliente> ListaClientes { get; set; } = [];
        public List<Produto> ListaProdutos { get; set; } = [];
        public List<Colaborador> ListaColaboradores { get; set; } = [];

        public Utils Utils { get; set; } = new Utils();

        public void AdicionarProduto(Produto produto)
        {
            ListaProdutos.Add(produto);
        }

        public void CadastrarCliente(Cliente NovoCliente)
        {
            ListaClientes.Add(NovoCliente);
        }

        public Cliente? ClienteJaComprou(string nomeInformado)
        {
            return ListaClientes.FirstOrDefault(clientes => clientes.Nome == nomeInformado);
        }

        public void AdicionaColaborador(Colaborador Colaborador)
        {
            ListaColaboradores.Add(Colaborador);
        }

        public Colaborador? VerificaColaboradorLoja(string login, string senha)
        {
            return ListaColaboradores.FirstOrDefault(c => c.Login == login && c.Senha == senha);
        }

        public void RetornaColaboradorLogado(string colaboradorLogado)
        {
            var Colaborador = ListaColaboradores.FirstOrDefault(Colaboradores => Colaboradores.Login == colaboradorLogado);

            if (Colaborador != null)
            {
                Console.WriteLine("\nLogin realizado com sucesso!" +
                                  $"\nSeja bem vindo {Colaborador.Nome}! Pressione alguma tecla para prosseguir.");
            }
        }
    }
}
