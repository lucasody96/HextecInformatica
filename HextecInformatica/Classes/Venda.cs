using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class Venda()
    {
        public int NumeroNotaFIscal { get; set; }
        public void ImprimeNotaFiscal(Cliente Cliente, Carrinho Carrinho)
        {
            Console.WriteLine("\n\n========================================");
            Console.WriteLine("              NOTA FISCAL              ");
            Console.WriteLine("========================================");
            Console.WriteLine($"NÚMERO DA NOTA FISCAL: {NumeroNotaFIscal + 1}");
            Console.WriteLine($"NOME DO CLIENTE: {Cliente.Nome}");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            Console.Write("QUANTIDADE | DESCRIÇÃO PRODUTO | V. UNITÁRIO | V. TOTAL ");

            foreach (var produto in Carrinho.ListaItensCarrinho)
            {
                Console.WriteLine($"\n{produto.QuantidadeComprada} - "                +
                                  $"{produto.Descricao} - "                         +
                                  $"R$ {produto.Valor} - "                          +
                                  $"R$ {produto.Valor * produto.QuantidadeComprada}");
            }

            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine($"VALOR FRETE: R$ {Carrinho.Frete:F2}");

            if (Carrinho.DescontoCupom > 0)
                Console.WriteLine($"\nDESCONTO CUPOM: R$ {Carrinho.DescontoCupom:F2}");

            if (Carrinho.DescontoCashback > 0)
                Console.WriteLine($"\nDESCONTO COMPRAS ANTERIORES: R$ {Carrinho.DescontoCashback:F2}");

            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine($"VALOR TOTAL DA NOTA: {Carrinho.TotalNotaFiscal:F2}");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine($"FORMAS DE PAGAMENTO:");
           
            foreach (var formaPagamento in Carrinho.ListaFormasPagamentos)
            {
                if (formaPagamento.Valor > 0)
                    Console.WriteLine($"{formaPagamento.Codigo} - {formaPagamento.Descricao} - R$ {formaPagamento.Valor:F2}");
            }

            Console.WriteLine("========================================");
        }

        public void PontosFidelidade(Cliente Cliente, Carrinho Carrinho)
        {
            if (Carrinho.Subtotal > 100.00m)
            {
                decimal descontoFidelidade = Carrinho.Subtotal * 0.05m;
                Cliente.AdicionarDescontoProximaCompra(descontoFidelidade);
                Console.WriteLine("\n--- PARABÉNS! ---");
                Console.WriteLine($"Sua compra gerou um cashback!");
                Console.WriteLine($"Valor ganho: {descontoFidelidade:F2} (5% do total)");
                Console.WriteLine("Este valor poderá ser usado na sua próxima compra.");
                Console.WriteLine("-----------------");
            }
        }

    }

}
