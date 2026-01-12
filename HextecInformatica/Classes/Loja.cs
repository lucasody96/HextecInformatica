using HextecInformatica.Classes.ClassesFilhas;

namespace HextecInformatica.Classes
{
    public class Loja
    {
        public string Nome { get; set; }

        public List<Cliente> ListaClientes { get; set; } = new List<Cliente>();

        public List<Produto> ListaProdutos { get; set; } = new List<Produto>();

        public List<Colaborador> ListaColaboradores { get; set; } = new List<Colaborador>();

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

        public void AdicionaColaborador(Colaborador Colaborador)
        {
            ListaColaboradores.Add(Colaborador);
        }

        public bool VerificaColaboradorLoja(string login, string senha) 
        {
            return ListaColaboradores.Any(Colaborador => Colaborador.Login == login && Colaborador.Senha == senha);
        }

        public void RetornaColaboradorLogado(string colaboradorLogado)
        {
            var Colaborador = ListaColaboradores.FirstOrDefault(Colaboradores => Colaboradores.Login == colaboradorLogado);

            if (Colaborador != null)
            {
                Console.WriteLine("\nLogin realizado com sucesso!" +
                                  $"\nSeja bem vindo {Colaborador.ToString()}! Pressione alguma tecla para prosseguir.");
            }
        }

    }
}
