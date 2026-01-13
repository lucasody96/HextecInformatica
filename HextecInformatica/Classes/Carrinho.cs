using HextecInformatica.Classes.ClassesFilhas;

namespace HextecInformatica.Classes
{
    public class Carrinho
    {
        public List<Produto> ListaProdutosDisponiveis { get; set; } = [];
        public List<Produto> ListaItensCarrinho { get; private set; } = [];

        public List<FormaPagamento> ListaFormasPagamentos { get; private set; } = [];

        public Utils Utils { get; set; } = new Utils();

        public decimal Subtotal => ListaItensCarrinho.Sum(item => item.Valor * item.QuantidadeComprada);

        public decimal Frete { get; set; }

        public decimal DescontoCupom { get; set; }

        public decimal DescontoCashback { get; set; }

        public decimal Pagamentos {  get; private set; }

        public decimal Troco { get; private set; }

        public decimal TotalCompra => Math.Round(Subtotal + Frete - DescontoCupom - DescontoCashback - Pagamentos, 2);

        public decimal TotalNotaFiscal => Math.Round(Subtotal + Frete - DescontoCupom - DescontoCashback, 2);

        public bool TrocoFoiConvertido { get; set; } = false;


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
                    int quantidadeComprada = Utils.EvitaQuebraCodInt($"Qual a quantidade do produto {ProdutoCatalogo.Descricao} você quer comprar? ");

                    if (quantidadeComprada <= ProdutoCatalogo.Estoque)
                    {
                        ProdutoCatalogo.QuantidadeComprada += quantidadeComprada;
                        ProdutoCatalogo.Estoque -= quantidadeComprada;

                        // Verifica se já não tem esse item na lista para não duplicar na visualização
                        if (!ListaItensCarrinho.Contains(ProdutoCatalogo))
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


            Utils.FormataCabecalho("ITENS DO CARRINHO");

            foreach (var ProdutoCarrinho in ListaItensCarrinho)
            {
                decimal subTotalItem = ProdutoCarrinho.Valor * ProdutoCarrinho.QuantidadeComprada;
                Console.WriteLine($"| {ProdutoCarrinho.Codigo} - {ProdutoCarrinho.Descricao,-25} | Qtd: {ProdutoCarrinho.QuantidadeComprada,9} | TOTAL ITEM: {subTotalItem,15:C} |");
            }

            Utils.ImprimeLinhaSeparadora('=');

            Console.WriteLine($"Subtotal: {Subtotal:C}");
            if (Frete > 0)
                Console.WriteLine($"Frete: {Frete:C}");
            if (DescontoCupom > 0)
                Console.WriteLine($"Cupom Desconto: -{DescontoCupom:C}");
            if (DescontoCashback > 0)
                Console.WriteLine($"Cashback: -{DescontoCashback:C}");

            Utils.ImprimeLinhaSeparadora('-');
            Console.WriteLine($"TOTAL A PAGAR: {TotalCompra:C}"); // Chama a propriedade automática TotalCompra
            Utils.ImprimeLinhaSeparadora('=');
        }

        public void RemoveItensCarrinho(int codProdutoRemovido)
        {
            //uso do linq para achar o item ao invés do foreach
            var itemASerRemovido = ListaItensCarrinho.FirstOrDefault(item => item.Codigo == codProdutoRemovido);

            if (itemASerRemovido != null)
            {
                int qtdRemovida = Utils.EvitaQuebraCodInt($"\nDigite a quantidade do produto {itemASerRemovido.Descricao} a ser removida: ");

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

        public void FormaEntrega(int respFormaEntrega)
        {
            switch (respFormaEntrega)
            {
                case 1:
                    Frete = 0;

                    Console.WriteLine("Opção de retirada na loja selecionada. Frete gratuito!");
                    break;
                case 2:
                    if (Subtotal > 300.00m)
                    {
                        Frete = 0;
                        Console.WriteLine("Opção de entrega padrão selecionada e subtotal acima de R$ 300,00. Frete gratuito!");
                    }
                    else
                    {
                        Frete = 20.00m;
                        Console.WriteLine($"Opção de entrega padrão selecionada e subtotal abaixo de R$ 300,00. Valor do frete: R$ {Frete}!");
                    }
                    break;
                case 3:
                    if (Subtotal > 500.00m)
                    {
                        Frete = 0;
                        Console.WriteLine("Opção de entrega expressa selecionada e subtotal acima de R$ 500,00. Frete gratuito!");
                    }
                    else
                    {
                        Frete = 40.00m;
                        Console.WriteLine($"Opção de entrega expressa selecionada e subtotal abaixo de R$ 500,00. Valor do frete: R$ {Frete}!");
                    }
                    break;
                default:
                    Console.WriteLine("Forma de entrega selecionada inválida!");
                    break;
            }
        }

        public void CalculoDescontoCupom()
        {
            Console.Write($"Informe o cupom de desconto: ");
            string? cupomDesconto = Console.ReadLine();

            switch (cupomDesconto)
            {
                case "CUPOM5%DESCONTO":
                    DescontoCupom = Subtotal * 0.05m;
                    Console.WriteLine($"Desconto de cupom R$ {DescontoCupom:F2} aplicado com sucesso!");
                    break;
                case "CUPOM10%DESCONTO":
                    DescontoCupom = Subtotal * 0.10m;
                    Console.WriteLine($"Desconto de cupom R$ {DescontoCupom:F2} aplicado com sucesso!");
                    break;
                case "CUPOM15%DESCONTO":
                    DescontoCupom = Subtotal * 0.15m;
                    Console.WriteLine($"Desconto de cupom R$ {DescontoCupom:F2} aplicado com sucesso!");
                    break;
                default:
                    break;
            }
        }

        public void CalculoDescontoCashback(Cliente ClienteCashback)
        {
            bool valRestantePositivo = false;
            while (valRestantePositivo == false)
            {
                Console.WriteLine($"Valor a ser pago: R$ {TotalCompra:F2}");//COLOCAR :F2
                decimal valorCashbackUsado = Utils.EvitaQuebraCodDecimal("Valor a ser utilizado: R$ ");

                if (valorCashbackUsado > 0 && valorCashbackUsado <= TotalCompra && valorCashbackUsado <= ClienteCashback.DescProximaCompra)
                {
                    DescontoCashback += valorCashbackUsado;
                    ClienteCashback.DebitaDescontoProximaCompra(valorCashbackUsado);

                    Console.WriteLine($"\nDesconto de R$ {DescontoCashback:F2} aplicado com sucesso!");
                    Console.WriteLine($"Saldo restante de cashback: R$ {ClienteCashback.DescProximaCompra:F2}");

                    valRestantePositivo = true;
                }
                else
                {
                    Console.WriteLine("\nValor inválido! Verifique se o valor é positivo, se não excede o total da compra ou seu saldo de cashback.");
                    Console.WriteLine($"Saldo Cashback: {ClienteCashback.DescProximaCompra:F2} | Valor Compra: {TotalCompra:F2}");
                }
            }
        }

        public void FormaPagamentoSelecionada(int formaPagamentoSelecionada, decimal ValorSelecionado, Cliente ClientePagamento)
        {

            //carrega as formas de pagamento disponíveis
            FormasPagamentosDisponíveis();

            var formaPagamentosDisponiveis = ListaFormasPagamentos.FirstOrDefault(formaPagamento =>
                                                 formaPagamento.Codigo == formaPagamentoSelecionada);

            if (formaPagamentosDisponiveis != null)
            {
                formaPagamentosDisponiveis.Valor = ValorSelecionado;

                switch (formaPagamentoSelecionada)
                {
                    case 1:

                        Console.WriteLine($"{formaPagamentosDisponiveis.Descricao}: R$ {ValorSelecionado:F2}");
                        
                        if (formaPagamentosDisponiveis.Valor > TotalCompra)
                        {
                            Troco = formaPagamentosDisponiveis.Valor - TotalCompra;
                            Console.WriteLine($"--> Troco a devolver: R$ {Troco:F2}");

                            Console.Write("\nDeseja usar o troco na próxima compra como desconto (S/N)? ");
                            string? usaTrocoProxCompra = Console.ReadLine();

                            if (usaTrocoProxCompra == "S" || usaTrocoProxCompra == "s")
                            {
                                ClientePagamento.AdicionarDescontoProximaCompra(Troco);
                                TrocoFoiConvertido = true;

                                Console.WriteLine($"Valor disponível para ser usado como desconto na próxima compra: R$ {ClientePagamento.DescProximaCompra:F2}");
                                PagamentosRealizados(TotalCompra);
                            }
                            else
                            {
                                Console.WriteLine($"{formaPagamentosDisponiveis.Descricao}: R$ {TotalCompra:F2} (Entregue: {ValorSelecionado:F2}, Troco: {Troco:F2})");
                                PagamentosRealizados(TotalCompra);
                            }
                        }
                        else if (formaPagamentosDisponiveis.Valor <= TotalCompra)
                            PagamentosRealizados(ValorSelecionado);
                        break;

                    case 2:
                        if (formaPagamentosDisponiveis.Valor <= TotalCompra)
                        {
                            Console.WriteLine($"{formaPagamentosDisponiveis.Descricao}: R$ {ValorSelecionado:F2}");
                            PagamentosRealizados(ValorSelecionado);
                        }else
                            Console.WriteLine("Valor pago acima do subtotal não permitido para condição de pagamento cartão de crédito");
                        break;

                    case 3:

                        if (formaPagamentosDisponiveis.Valor <= TotalCompra)
                        {
                            Console.WriteLine($"{formaPagamentosDisponiveis.Descricao}: R$ {ValorSelecionado:F2}");
                            PagamentosRealizados(ValorSelecionado);
                        }
                        else
                            Console.WriteLine("Valor pago acima do subtotal não permitido para condição de pagamento cartão de débito");
                        break;
                    case 4:

                        if (formaPagamentosDisponiveis.Valor <= TotalCompra)
                        {
                            Console.WriteLine($"{formaPagamentosDisponiveis.Descricao}: R$ {ValorSelecionado:F2}");
                            PagamentosRealizados(ValorSelecionado);
                        }
                        else
                            Console.WriteLine("Valor pago acima do subtotal não permitido para condição de pagamento boleto");
                        break;
                }
            }
            else
                Console.WriteLine("Condição de pagamento informa inválida!");
        }

        private void FormasPagamentosDisponíveis()
        {
            ListaFormasPagamentos.Add(new FormaPagamento(1, "Dinheiro", 0));
            ListaFormasPagamentos.Add(new FormaPagamento(2, "Cartão de Crédito", 0));
            ListaFormasPagamentos.Add(new FormaPagamento(3, "Cartão de Débito", 0));
            ListaFormasPagamentos.Add(new FormaPagamento(4, "Boleto", 0));
        }

        private void PagamentosRealizados(decimal valorPago) 
        {
            Pagamentos += valorPago;
        }

        public void LimpaCarrinho() 
        {
            foreach (var itensCarrinho in ListaItensCarrinho)
            {
                itensCarrinho.QuantidadeComprada = 0;
            }

            ListaItensCarrinho.Clear();
            Pagamentos = 0;
            DescontoCashback = 0;
            DescontoCupom = 0;
            Frete = 0;
        }
    }
}
