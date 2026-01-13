using HextecInformatica.Classes.ClassesFilhas;

namespace HextecInformatica.Classes
{
    public class Dados
    {
        public static void CarregaProdutos(Loja loja)
        {
            loja.AdicionarProduto(new Produto(1, "MOUSE SEM FIO", 65.90m, 32));
            loja.AdicionarProduto(new Produto(2, "PEN DRIVE", 44.90m, 25));
            loja.AdicionarProduto(new Produto(3, "SSD", 390.49m, 10));
            loja.AdicionarProduto(new Produto(4, "MEMÓRIA RAM", 280.89m, 0));
            loja.AdicionarProduto(new Produto(5, "MONITOR", 749.99m, 15));
            loja.AdicionarProduto(new Produto(6, "HEADSET GAMER", 231.89m, 0));
            loja.AdicionarProduto(new Produto(7, "PLACA DE VÍDEO", 2100.99m, 1));
        }

        public static void CarregaColaboradores(Loja loja)
        {
            loja.AdicionaColaborador(new Colaborador("ADMINISTRADOR DO SISTEMA","00000000000", "ADMIN", "admin"));
            loja.AdicionaColaborador(new Colaborador("LUCAS ODY", "03711831036", "LUCAS.ODY", "1234"));
        }
    }
}
