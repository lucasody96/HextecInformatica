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
        
        public Carrinho()
        {

        }

        public Carrinho(decimal subtotal)
        {
            Subtotal = subtotal;
        }

        public Carrinho(List<Produto> listaProdutosDisponiveis)
        {
            ListaProdutosDisponiveis = listaProdutosDisponiveis;
        }

        public string AdicionaItensCarrinho(int codProduto)
        {
            foreach (var ProdutoCatalogo in ListaProdutosDisponiveis)
            {
                if (ProdutoCatalogo.Codigo == codProduto)
                {
                    if (ProdutoCatalogo.Estoque > 0)
                    {
                        ListaItensCarrinho.Add(ProdutoCatalogo);
                        return $"Produto {ProdutoCatalogo.Descricao} adicionado ao carrinho!";
                    }
                    else
                        return $"Produto {ProdutoCatalogo.Descricao} esgotado! Não adicionado ao carrinho. ";
                }       
            }
            return "O item selecionado não está no catálogo de produtos, tente novamente!";
        }

        public void VisualizaçãoItensCarrinho()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("           ITENS DO CARRINHO             " );
            Console.WriteLine("=========================================" );

            foreach (var ProdutoCarrinho in ListaItensCarrinho)
            {
                Console.WriteLine($"Produto: {ProdutoCarrinho.Descricao} | Valor: R$ {ProdutoCarrinho.Valor}");
            }
            Console.WriteLine("=========================================" );
            foreach (var ProdutoCarrinho in ListaItensCarrinho)
            {
                Subtotal += ProdutoCarrinho.Valor;
            }
            Console.WriteLine($"subtotal: R$ {Subtotal}");
            Console.WriteLine("=========================================" );
        }
    }
}
