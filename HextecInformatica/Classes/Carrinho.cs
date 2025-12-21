using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class Carrinho
    {
        public List<Produto> ListaProdutosDisponiveis { get; set; } = new List<Produto>();
        public List<Produto> ListaItensCarrinho { get; private set; } = new List<Produto>();
        public decimal Subtotal { get; private set; }
        
        public Carrinho(decimal subtotal)
        {
            Subtotal = subtotal;
        }

        public Carrinho(List<Produto> listaProdutosDisponiveis)
        {
            ListaProdutosDisponiveis = listaProdutosDisponiveis;
        }

        public void AdicionaItensCarrinho(int codProduto)
        {
            var ProdutoCatalogo = ListaProdutosDisponiveis.FirstOrDefault(produto => produto.Codigo == codProduto);

            if (ProdutoCatalogo != null)
            {
                if (ProdutoCatalogo.Estoque > 0)
                {
                    Console.Write($"Qual a quantidade do produto {ProdutoCatalogo.Descricao} você quer comprar? ");
                    int quantidadeComprada = Convert.ToInt32(Console.ReadLine());

                    if (quantidadeComprada <= ProdutoCatalogo.Estoque)
                    {
                        ProdutoCatalogo.QuantidadeComprada += quantidadeComprada;
                        ProdutoCatalogo.Estoque -= quantidadeComprada;
                        ListaItensCarrinho.Add(ProdutoCatalogo);
                        
                    }
                    else
                        Console.WriteLine("Valor de compra acima do estoque do item, não será adicionado ao carrinho");
                }
                else
                    Console.WriteLine($"{ProdutoCatalogo.Descricao} está com estoque esgotado! Não será adicionado ao carrinho");

            }
            else
                Console.WriteLine("Item inexistente na lista de produtos, tente novamente!");
        }

        public void VisualizaçãoItensCarrinho()
        {
            Console.Clear();
            Console.WriteLine("\n========================================");
            Console.WriteLine("           ITENS DO CARRINHO             " );
            Console.WriteLine("=========================================" );

            Subtotal = 0;

            foreach (var ProdutoCarrinho in ListaItensCarrinho)
            {
                decimal subTotalItem = ProdutoCarrinho.Valor * ProdutoCarrinho.QuantidadeComprada;
              
                Console.WriteLine($"{ProdutoCarrinho.Codigo} - {ProdutoCarrinho.Descricao} | Quantidade: {ProdutoCarrinho.QuantidadeComprada} | Valor: R$ {subTotalItem}");
                Subtotal += subTotalItem;
            }
            Console.WriteLine("=========================================" );
            Console.WriteLine($"subtotal: R$ {Subtotal}");
            Console.WriteLine("=========================================" );
        }

        
        public void RemoveItensCarrinho (int codProdutoRemovido)
        {
            //uso do linq para achar o item ao invés do foreach
            var itemASerRemovido = ListaItensCarrinho.FirstOrDefault(item => item.Codigo == codProdutoRemovido);

            if (itemASerRemovido != null)
            {
                Console.Write($"\nDigite a quantidade do produto {itemASerRemovido.Descricao} a ser removida: ");
                int qtdRemovida = Convert.ToInt32(Console.ReadLine());

                if (qtdRemovida == itemASerRemovido.QuantidadeComprada)
                {
                    ListaItensCarrinho.Remove(itemASerRemovido);
                    DevolveItemEstoque(codProdutoRemovido, qtdRemovida);

                    itemASerRemovido.QuantidadeComprada = 0;                   
                }
                else if (qtdRemovida < itemASerRemovido.QuantidadeComprada && qtdRemovida > 0)
                {
                    itemASerRemovido.QuantidadeComprada -= qtdRemovida;
                    DevolveItemEstoque(codProdutoRemovido, qtdRemovida);
                    Console.WriteLine($"Foram removidas {qtdRemovida} unidades do produto {itemASerRemovido.Descricao}!");
                    
                }
                else
                {
                    Console.WriteLine("Quantidade informada inválida, não será removido o item do carrinho, preesione enter para prosseguir");
                    Console.ReadKey();
                }
                    

                VisualizaçãoItensCarrinho();
            }
            else
                Console.WriteLine("Produto não encontrado no carrinho");
        }

        public void DevolveItemEstoque(int codProduto, int QtdDevolvida)
        {
            var itemDevolvido = ListaProdutosDisponiveis.FirstOrDefault(item => item.Codigo == codProduto);

            if (itemDevolvido != null)
            {
                itemDevolvido.Estoque += QtdDevolvida;
            }
        }
    }
}
