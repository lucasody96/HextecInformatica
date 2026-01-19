using HextecInformatica.Entities;
using HextecInformatica.Entities.Core;


namespace HextecInformatica.Repositories
{
    public class Dados
    {
        public static void CarregaProdutos(ProdutoRepository produtoRepos)
        {
            produtoRepos.Adiciona(new Produto("MOUSE SEM FIO", 65.90m, 32));
            produtoRepos.Adiciona(new Produto("PEN DRIVE", 44.90m, 25));
            produtoRepos.Adiciona(new Produto("SSD", 390.49m, 10));
            produtoRepos.Adiciona(new Produto("MEMÓRIA RAM", 280.89m, 0));
            produtoRepos.Adiciona(new Produto("MONITOR", 749.99m, 15));
            produtoRepos.Adiciona(new Produto("HEADSET GAMER", 231.89m, 0));
            produtoRepos.Adiciona(new Produto("PLACA DE VÍDEO", 2100.99m, 1));
        }

        public static void CarregaColaboradores(ColaboradorRepository colaborador)
        {
            colaborador.Adiciona(new Colaborador("ADMINISTRADOR DO SISTEMA","00000000000", "ADMIN", "admin"));
            colaborador.Adiciona(new Colaborador("LUCAS ODY", "03711831036", "LUCAS.ODY", "1234"));
        }
    }
}
