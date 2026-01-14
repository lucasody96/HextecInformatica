using HextecInformatica.Entities.Core;

namespace HextecInformatica.Entities
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

        public void GerenciamentoEstoque(int? menuSelecionado)
        {
            
            
            
            

            switch (menuSelecionado)
            {
                case 1:
                    //Console.WriteLine("  [1] - Visualização");
                    Console.Clear();
                    Utils.FormataCabecalho("VISUALIZAÇÃO DE ITENS DO ESTOQUE");
                    Utils.FormataCabecalhoTabela();

                    foreach (var Produto in ListaProdutos)
                    {
                        Utils.FormataLinhaProdutos(Produto.Id, 
                                                   Produto.Descricao, 
                                                   Produto.Valor, 
                                                   Produto.Estoque);
                    }

                    Utils.ImprimeLinhaSeparadora('=');
                    break;
                case 2:
                    //Console.WriteLine("  [2] - Entrada");


                    break;
                case 3:
                    //Console.WriteLine("  [3] - Ajuste");


                    break;
                case 4:
                    //Console.WriteLine("  [4] - Logout\n");


                    break;
                default:
                    Console.WriteLine("Opção inválida!\n");
                    break;
            }
        }
    }
}
