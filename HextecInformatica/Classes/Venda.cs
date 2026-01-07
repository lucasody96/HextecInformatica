using System;
using System.Collections.Generic;
using System.Drawing;
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

            Utils UtilsNotaFiscal = new Utils(); //Instancia os formatadores

            Console.Clear();
            //Cabeçalho da nota
            UtilsNotaFiscal.ImprimeLinhaSeparadora('=');
            Console.WriteLine($"| {"NOTA FISCAL DE VENDA AO CONSUMIDOR",-77}|");
            Console.WriteLine($"| {"HEXTEC INFORMÁTICA LTDA",-77}|"           );
            Console.WriteLine($"| Data: {DateTime.Now, -71}|"); //hora atual
            UtilsNotaFiscal.ImprimeLinhaSeparadora('=');

            // Dados do cliente
            Console.WriteLine($"| DADOS DO CLIENTE: {new string(' ', 59)}|");
            Console.WriteLine($"| Nome: {Cliente.Nome,-71}|");

            if (Cliente.TipoPessoa == "F")
                Console.WriteLine($"| CPF: {Cliente.Cpf,-72}|");
            else
                Console.WriteLine($"| CNPJ: {Cliente.Cnpj,-71}|");
            UtilsNotaFiscal.ImprimeLinhaSeparadora('-');

            // TABELA DE ITENS
            // Cabeçalho da tabela de itens
            Console.WriteLine($"| {"QTD",-4} | {"DESCRIÇÃO DO PRODUTO",-37} | {"UNITÁRIO",-13} | {"TOTAL",-13} |");
            UtilsNotaFiscal.ImprimeLinhaSeparadora('-');

            foreach ( var itensComprados in Carrinho.ListaItensCarrinho)
            {
                decimal totalItem = itensComprados.Valor * itensComprados.QuantidadeComprada;
                Console.WriteLine($"| {itensComprados.QuantidadeComprada,-4} | {itensComprados.Descricao,-37} | {itensComprados.Valor,-13:C} | {totalItem, -13:C} |");
            }
            UtilsNotaFiscal.ImprimeLinhaSeparadora('-');

            //Subtotal, frete e descontos
            UtilsNotaFiscal.ImprimeDetalheFinanceiro("SUBTOTAL DOS ITENS", Carrinho.Subtotal);

            if (Carrinho.Frete > 0)
                UtilsNotaFiscal.ImprimeDetalheFinanceiro("FRETE", Carrinho.Frete);

            if (Carrinho.DescontoCupom > 0)
                UtilsNotaFiscal.ImprimeDetalheFinanceiro("DESCONTO (CUPOM)", Carrinho.DescontoCupom, true);

            if (Carrinho.DescontoCashback > 0)
                UtilsNotaFiscal.ImprimeDetalheFinanceiro("DESCONTO (CASHBACK UTILIZADO)", Carrinho.DescontoCashback, true);

            UtilsNotaFiscal.ImprimeLinhaSeparadora('=');
            UtilsNotaFiscal.ImprimeDetalheFinanceiro("VALOR TOTAL DA NOTA", Carrinho.TotalNotaFiscal);
            UtilsNotaFiscal.ImprimeLinhaSeparadora('=');

            //FORMAS DE PAGAMENTO USADAS
            Console.WriteLine($"| FORMAS DE PAGAMENTO:{new string(' ', 57)}|");

            foreach (var pagamentosUsados in Carrinho.ListaFormasPagamentos)
            {
                if (pagamentosUsados.Valor > 0) // Só mostra se foi usado
                {
                    string textoPag = $"{pagamentosUsados.Descricao}";
                    Console.WriteLine($"| > {textoPag,-60} {pagamentosUsados.Valor,13:C} |");
                }
            }

            // troco se houver
            if (Carrinho.Troco > 0)
            {
                UtilsNotaFiscal.ImprimeLinhaSeparadora('-');

                if (Carrinho.TrocoFoiConvertido)
                    Console.WriteLine($"| {"DESCONTO PRÓXIMA COMPRA:",-62} {Cliente.DescProximaCompra,13:C} |");
                else
                    Console.WriteLine($"| {"TROCO DEVOLVIDO:",-62} {Carrinho.Troco,13:C} |");
            }
            
            
            else if (Carrinho.Troco > 0 && Cliente.DescProximaCompra > 0)
            {
                UtilsNotaFiscal.ImprimeLinhaSeparadora('-');
                Console.WriteLine($"| {"DESCONTO PRÓXIMA COMPRA:",-62} {Carrinho.Troco,13:C} |");
            }

            UtilsNotaFiscal.ImprimeLinhaSeparadora('=');

            //RODAPÉ (CASHBACK GANHO)
            if (Carrinho.Subtotal > 100.00m)
            {
                decimal descontoFidelidade = Carrinho.Subtotal * 0.05m;
                Cliente.AdicionarDescontoProximaCompra(descontoFidelidade);

                Console.WriteLine();
                Console.WriteLine("********************************************************************************"); // 80 asteriscos

                string msgParabens = "PARABÉNS! VOCÊ GANHOU CASHBACK!";
                Console.WriteLine(msgParabens.PadLeft(40 + msgParabens.Length / 2)); // Centraliza +/-

                string msgValor = $"Valor cashback para ser usado em uma próxima compra: {descontoFidelidade:C}";
                Console.WriteLine(msgValor.PadLeft(40 + msgValor.Length / 2));
                Console.WriteLine("********************************************************************************");
            }

            Console.WriteLine("\n\nPressione qualquer tecla para voltar ao menu inicial...");
            Console.ReadKey();
        }

    }

}
