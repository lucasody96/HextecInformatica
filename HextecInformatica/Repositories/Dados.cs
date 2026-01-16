using HextecInformatica.Entities;
using HextecInformatica.Entities.Core;

namespace HextecInformatica.Repositories
{
    public class Dados
    {
        public static void CarregaProdutos(ProdutoRepository produtoRepos)
        {
            produtoRepos.AdicionaProduto(new Produto("MOUSE SEM FIO", 65.90m, 32));
            produtoRepos.AdicionaProduto(new Produto("PEN DRIVE", 44.90m, 25));
            produtoRepos.AdicionaProduto(new Produto("SSD", 390.49m, 10));
            produtoRepos.AdicionaProduto(new Produto("MEMÓRIA RAM", 280.89m, 0));
            produtoRepos.AdicionaProduto(new Produto("MONITOR", 749.99m, 15));
            produtoRepos.AdicionaProduto(new Produto("HEADSET GAMER", 231.89m, 0));
            produtoRepos.AdicionaProduto(new Produto("PLACA DE VÍDEO", 2100.99m, 1));
        }

        public static void CarregaColaboradores(ColaboradorRepository colaborador)
        {
            colaborador.AdicionaColaborador(new Colaborador("ADMINISTRADOR DO SISTEMA","00000000000", "ADMIN", "admin"));
            colaborador.AdicionaColaborador(new Colaborador("LUCAS ODY", "03711831036", "LUCAS.ODY", "1234"));
        }

        public static void FormasPagamentosDisponíveis(Carrinho carrinho)
        {
            carrinho.ListaFormasPagamentos.Add(new FormaPagamento(1, "Dinheiro", 0));
            carrinho.ListaFormasPagamentos.Add(new FormaPagamento(2, "Cartão de Crédito", 0));
            carrinho.ListaFormasPagamentos.Add(new FormaPagamento(3, "Cartão de Débito", 0));
            carrinho.ListaFormasPagamentos.Add(new FormaPagamento(4, "Boleto", 0));
        }
    }
}
