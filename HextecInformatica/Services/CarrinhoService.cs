using HextecInformatica.Entities;
using HextecInformatica.Entities.Core;
using HextecInformatica.Entities.Descontos;
using HextecInformatica.Interfaces;
using HextecInformatica.Repositories;
using HextecInformatica.Entities.FretesDisponiveis;
using HextecInformatica.Entities.FormasPagamento;

namespace HextecInformatica.Services
{
    public class CarrinhoService
    {

        public List<Produto> ListaProdutosDisponiveis { get; set; } = [];
        public List<Produto> ListaItensCarrinho { get; private set; } = [];

        private readonly List<IFormaEntrega> ListaFormasEntrega =
        [
            new RetiradaLoja(),
            new EntregaPadrao(),
            new EntregaExpressa()
        ];

        private readonly List<IFormasPagamento> ListaFormasPagamentos =
        [
            new PagamentoDinheiro(),
            new PagamentoCartaoCredito(),
            new PagamentoCartaoDebito(),
            new PagamentoBoleto()
        ];

        private readonly List<DescontoCupom> ListaDescontosDisponiveis =
        [
            new Cupom5Desconto(),
            new Cupom10Desconto(),
            new Cupom15Desconto()
        ];

        public decimal Subtotal => ListaItensCarrinho.Sum(item => item.Valor * item.QuantidadeComprada);

        public decimal Frete = 0;
        public decimal DescontoCupom { get; set; }
        public decimal DescontoCashback { get; set; }
        public decimal Pagamentos { get; private set; }
        public decimal Troco { get; private set; }
        public decimal TotalCompra => Math.Round(Subtotal + Frete - DescontoCupom - DescontoCashback - Pagamentos, 2);
        public decimal TotalNotaFiscal => Math.Round(Subtotal + Frete - DescontoCupom - DescontoCashback, 2);
        public bool TrocoFoiConvertido { get; set; } = false;

        public void ImprimeListaProdutosDisponiveis()
        {
            Console.Clear();
            Utils.FormataCabecalho("CATÁLOGO DE PRODUTOS");

            foreach (var Produto in ListaProdutosDisponiveis)
            {
                Produto.ImprimirProduto();
            }

            Utils.ImprimeLinhaSeparadora('-');
        }

        public void AdicionaItensCarrinho(ProdutoRepository produto)
        {
            bool codZero = false;
            while (!codZero)
            {
                int codProdutoSelecionado = Utils.EvitaQuebraCodInt("\nSelecione o item a ser adicionado ao carrinho ou 0 para sair: ");

                if (codProdutoSelecionado > 0)
                {
                    Produto? produtoSelecionado = produto.BuscaID(codProdutoSelecionado);
                    
                    if (produtoSelecionado != null)
                    {
                        if (produtoSelecionado.Estoque > 0)
                        {
                            int quantidadeComprada = Utils.EvitaQuebraCodInt($"Qual a quantidade do produto {produtoSelecionado.Descricao} você quer comprar? ");

                            if (quantidadeComprada <= produtoSelecionado.Estoque)
                            {
                                produtoSelecionado.QuantidadeComprada += quantidadeComprada;
                                produtoSelecionado.Estoque -= quantidadeComprada;

                                // Verifica se já não tem esse item na lista para não duplicar na visualização
                                if (!ListaItensCarrinho.Contains(produtoSelecionado))
                                    ListaItensCarrinho.Add(produtoSelecionado);
                            }
                            else
                                Console.WriteLine("Valor de compra acima do estoque do item, não será adicionado ao carrinho");
                        }
                        else
                            Console.WriteLine($"{produtoSelecionado.Descricao} está com estoque esgotado! Não será adicionado ao carrinho");
                    }
                    else
                        Console.WriteLine("Item inexistente na lista de produtos, tente novamente!");
                }
                else if (codProdutoSelecionado < 0)
                    Console.WriteLine("Valor informado inválido, por favor, tente novamente!");
                else
                    codZero = true;
            }
        }

        public void VisualizaçãoItensCarrinho()
        {
            Console.Clear();
            Utils.FormataCabecalho("ITENS DO CARRINHO");

            foreach (var ProdutoCarrinho in ListaItensCarrinho)
            {
                decimal subTotalItem = ProdutoCarrinho.Valor * ProdutoCarrinho.QuantidadeComprada;
                Console.WriteLine($"| {ProdutoCarrinho.Id} - {ProdutoCarrinho.Descricao,-25} | Qtd: {ProdutoCarrinho.QuantidadeComprada,9} | TOTAL ITEM: {subTotalItem,15:C} |");
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
            Console.WriteLine($"TOTAL A PAGAR: {TotalCompra:C}");
            Utils.ImprimeLinhaSeparadora('=');
        }

        public void RemoveItensCarrinho(ProdutoRepository produto)
        {
            Console.Write("Deseja remover algum item (S/N)? ");
            string? respRemoveItem = Console.ReadLine();

            if (respRemoveItem == "S" || respRemoveItem == "s")
            {
                bool codZero = false;
                while (!codZero)
                {
                    int codProdutoRemovido = Utils.EvitaQuebraCodInt("\nDigite o código do produto a ser removido (0 para sair): ");

                    if (codProdutoRemovido > 0)
                    {
                        Produto? produtoSelecionado = produto.BuscaID(codProdutoRemovido);

                        if (produtoSelecionado != null)
                        {
                            int qtdRemovida = Utils.EvitaQuebraCodInt($"\nDigite a quantidade do produto {produtoSelecionado.Descricao} a ser removida: ");

                            if (qtdRemovida == produtoSelecionado.QuantidadeComprada)
                            {
                                ListaItensCarrinho.Remove(produtoSelecionado);
                                produtoSelecionado.DevolveItemEstoque(qtdRemovida);

                                produtoSelecionado.QuantidadeComprada = 0;
                            }
                            else if (qtdRemovida < produtoSelecionado.QuantidadeComprada && qtdRemovida > 0)
                            {
                                produtoSelecionado.QuantidadeComprada -= qtdRemovida;
                                produtoSelecionado.DevolveItemEstoque(qtdRemovida);

                                Console.WriteLine($"Foram removidas {qtdRemovida} unidades do produto {produtoSelecionado.Descricao}!");
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
                    else if (codProdutoRemovido < 0)
                        Console.WriteLine("Valor informado inválido, por favor, tente novamente!");
                    else
                        codZero = true;
                }
            }
        }

        public void FormaEntrega()
        {
            //opção para ele selecionar a forma de entrega
            Console.Clear();
            Utils.FormataCabecalho($"Escolha o Frete (Subtotal: {Subtotal:C})");
            
            //Cria a lista de opções usando a Interface
            foreach (var formaEntrega in ListaFormasEntrega)
            {
                Console.WriteLine($" [{formaEntrega.Id}] - {formaEntrega.Descricao}");
            }

            int respFormaEntrega = Utils.EvitaQuebraCodInt("\nQual a forma de entrega desejada (informe de 1 a 3)? ");

            IFormaEntrega? entregaSelecionada = ListaFormasEntrega.FirstOrDefault(formaEntrega => formaEntrega.Id == respFormaEntrega);

            bool formaEntregaInvalida = false;
            while (!formaEntregaInvalida)
            {
                if (entregaSelecionada != null)
                {
                    Frete = entregaSelecionada.CalculaFrete(Subtotal);
                    Console.WriteLine(entregaSelecionada.ObterMensagem(Frete));
                    formaEntregaInvalida = true;   
                }
                else
                {
                    Console.WriteLine("Forma de entrega selecionada inválida!");
                    Console.WriteLine("\nPressione alguma tecla para prosseguir!");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("\nPressione alguma tecla para prosseguir!");
            Console.ReadKey();
            
        }

        public void CalculoDescontoCupom()
        {
            if ( Subtotal >= 250)
            {
                Console.Clear();
                Utils.FormataCabecalho("CUPOM DESCONTO DISPONÍVEL");
                if (Subtotal >= 250 && Subtotal < 500)
                    Console.WriteLine("Você ganhou um cupom de desconto de 5% nesta compra! Digite CUPOM5%DESCONTO para utilizá-lo");
                else if (Subtotal >= 500 && Subtotal < 1000)
                    Console.WriteLine("Você ganhou um cupom de desconto de 10% nesta compra! Digite CUPOM10%DESCONTO para utilizá-lo");
                else if (Subtotal >= 1000)
                    Console.WriteLine("Você ganhou um cupom de desconto de 15% nesta compra! Digite CUPOM15%DESCONTO para utilizá-lo");
                Utils.ImprimeLinhaSeparadora('=');

                Console.Write("\nDeseja usar o cupom (S/N)? ");
                string? respPossuiCupomDesconto = Console.ReadLine();

                if (respPossuiCupomDesconto == "S" || respPossuiCupomDesconto == "s")
                {
                    Console.Write($"Informe o cupom de desconto: ");
                    string? cupomDesconto = Console.ReadLine();

                    var DescontoDisponivel = ListaDescontosDisponiveis.FirstOrDefault(desconto =>
                                                 desconto.CodigoCupom == cupomDesconto);

                    if (DescontoDisponivel != null)
                    {
                        decimal valorDescontoCalculado = DescontoDisponivel.CalcularDesconto(Subtotal);
                        if (valorDescontoCalculado > 0)
                        {
                            DescontoCupom += valorDescontoCalculado;
                            Console.WriteLine(DescontoDisponivel.ObterMensagem(valorDescontoCalculado));
                        }
                        else
                            Console.WriteLine("Cupom inválido para o valor da compra atual!");
                    }
                    else
                        Console.WriteLine("Cupom de desconto inválido!");

                    Console.WriteLine("\nPressione alguma tecla para prosseguir!");
                    Console.ReadKey();
                }
            }            
        }

        public void CalculoDescontoCashback(Cliente clienteCashback)
        {
            if (clienteCashback != null && clienteCashback.DescProximaCompra > 0)
            {
                Console.WriteLine($"\nvocê possui R$ {clienteCashback.DescProximaCompra:F2} de desconto acumulado de compras anteriores.");
                Console.Write("Deseja usar o desconto (S/N)? ");
                string? respUsaDescontoAnterior = Console.ReadLine();

                if (respUsaDescontoAnterior == "S" || respUsaDescontoAnterior == "s")
                {
                    bool valRestantePositivo = false;
                    while (valRestantePositivo == false)
                    {
                        Console.WriteLine($"Valor a ser pago: R$ {TotalCompra:F2}");//COLOCAR :F2
                        decimal valorCashbackUsado = Utils.EvitaQuebraCodDecimal("Valor a ser utilizado: R$ ");

                        if (valorCashbackUsado > 0 && valorCashbackUsado <= TotalCompra && valorCashbackUsado <= clienteCashback.DescProximaCompra)
                        {
                            DescontoCashback += valorCashbackUsado;
                            clienteCashback.DebitaDescontoProximaCompra(valorCashbackUsado);

                            Console.WriteLine($"\nDesconto de R$ {DescontoCashback:F2} aplicado com sucesso!");
                            Console.WriteLine($"Saldo restante de cashback: R$ {clienteCashback.DescProximaCompra:F2}");

                            valRestantePositivo = true;
                        }
                        else
                        {
                            Console.WriteLine("\nValor inválido! Verifique se o valor é positivo, se não excede o total da compra ou seu saldo de cashback.");
                            Console.WriteLine($"Saldo Cashback: {clienteCashback.DescProximaCompra:F2} | Valor Compra: {TotalCompra:F2}");
                        }
                    }
                }

                Console.WriteLine("\nPressione alguma tecla para prosseguir!");
                Console.ReadKey();
            }

            
        }

        public void FormaPagamentoSelecionada(int formaPagamentoSelecionada, decimal ValorSelecionado, Cliente ClientePagamento)
        {
            var formaPagamentosDisponiveis = ListaFormasPagamentos.FirstOrDefault(formaPagamento =>
                                                 formaPagamento.Id == formaPagamentoSelecionada);

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
                        }
                        else
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
