namespace HextecInformatica.Entities
{
    public class Venda
    {
        private static int _contadorVendas = 0;
        private int NumeroNotaFiscal { get; set; }
        public Venda()
        {
            //geração do ID automático
            _contadorVendas++;
            NumeroNotaFiscal = _contadorVendas;
        }

        public void ImprimeNotaFiscal(Cliente Cliente, Carrinho Carrinho)
        {
            
            //Instancia os formatadores

            Console.Clear();
            //Cabeçalho da nota
            Utils.ImprimeLinhaSeparadora('=');
            Console.WriteLine($"| {"NOTA FISCAL DE VENDA AO CONSUMIDOR",-77}|");
            Console.WriteLine($"| {"HEXTEC INFORMÁTICA LTDA",-77}|");
            Console.WriteLine($"| NÚMERO DA NOTA FISCAL: {NumeroNotaFiscal,-59}|");
            Console.WriteLine($"| Data: {DateTime.Now, -71}|"); //hora atual
            Utils.ImprimeLinhaSeparadora('=');

            // Dados do cliente
            Console.WriteLine($"| DADOS DO CLIENTE: {new string(' ', 59)}|");
            Console.WriteLine($"| Nome: {Cliente.Nome,-71}|");

            if (Cliente.TipoPessoa == "F")
                Console.WriteLine($"| CPF: {Cliente.Cpf,-72}|");
            else
                Console.WriteLine($"| CNPJ: {Cliente.Cnpj,-71}|");
            Utils.ImprimeLinhaSeparadora('-');

            // TABELA DE ITENS
            // Cabeçalho da tabela de itens
            Console.WriteLine($"| {"QTD",-4} | {"DESCRIÇÃO DO PRODUTO",-37} | {"UNITÁRIO",-13} | {"TOTAL",-13} |");
            Utils.ImprimeLinhaSeparadora('-');

            foreach (var itensComprados in Carrinho.ListaItensCarrinho)
            {
                decimal totalItem = itensComprados.Valor * itensComprados.QuantidadeComprada;
                Console.WriteLine($"| {itensComprados.QuantidadeComprada,-4} | {itensComprados.Descricao,-37} | {itensComprados.Valor,-13:C} | {totalItem, -13:C} |");
            }
            Utils.ImprimeLinhaSeparadora('-');

            //Subtotal, frete e descontos
            Utils.ImprimeDetalheFinanceiro("SUBTOTAL DOS ITENS", Carrinho.Subtotal);

            if (Carrinho.Frete > 0)
                Utils.ImprimeDetalheFinanceiro("FRETE", Carrinho.Frete);

            if (Carrinho.DescontoCupom > 0)
                Utils.ImprimeDetalheFinanceiro("DESCONTO (CUPOM)", Carrinho.DescontoCupom, true);

            if (Carrinho.DescontoCashback > 0)
                Utils.ImprimeDetalheFinanceiro("DESCONTO (CASHBACK UTILIZADO)", Carrinho.DescontoCashback, true);

            Utils.ImprimeLinhaSeparadora('=');
            Utils.ImprimeDetalheFinanceiro("VALOR TOTAL DA NOTA", Carrinho.TotalNotaFiscal);
            Utils.ImprimeLinhaSeparadora('=');

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
                Utils.ImprimeLinhaSeparadora('-');

                if (Carrinho.TrocoFoiConvertido)
                    Console.WriteLine($"| {"DESCONTO PRÓXIMA COMPRA:",-62} {Cliente.DescProximaCompra,13:C} |");
                else
                    Console.WriteLine($"| {"TROCO DEVOLVIDO:",-62} {Carrinho.Troco,13:C} |");
            }
            
            
            else if (Carrinho.Troco > 0 && Cliente.DescProximaCompra > 0)
            {
                Utils.ImprimeLinhaSeparadora('-');
                Console.WriteLine($"| {"DESCONTO PRÓXIMA COMPRA:",-62} {Carrinho.Troco,13:C} |");
            }

            Utils.ImprimeLinhaSeparadora('=');

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
