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
            new PagamentoCartaoDebito(),
            new PagamentoCartaoCredito(),
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

        public void ImprimeListaProdutosDisponiveis(ProdutoRepository produto)
        {
            Console.Clear();
            Utils.FormataCabecalho("CATÁLOGO DE PRODUTOS");

            foreach (var Produto in produto.ListaProdutos)
            {
                Utils.FormataLinhaProdutos(Produto.Id, Produto.Descricao, Produto.Valor, Produto.Estoque);
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
            Utils.FormataCabecalho("SELEÇÃO DO FRETE");
            Console.WriteLine("Selecione a forma de entrega conforme listado abaixo:");
            Console.WriteLine();
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
                    Console.WriteLine("\nDigite CUPOM5%DESCONTO para ganhar 5% de desconto nesta compra");
                else if (Subtotal >= 500 && Subtotal < 1000)
                    Console.WriteLine("\nDigite CUPOM10%DESCONTO para utilizá-lo para ganhar 10% de desconto nesta compra");
                else if (Subtotal >= 1000)
                    Console.WriteLine("\nDigite CUPOM15%DESCONTO para utilizá-lo para ganhar 15% de desconto nesta compra");

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
            Console.Clear();
            Utils.FormataCabecalho("CASHBACK COMO DESCONTO");
            if (clienteCashback != null && clienteCashback.DescProximaCompra > 0)
            {
                Console.WriteLine($"\nvocê possui R$ {clienteCashback.DescProximaCompra:F2} de desconto acumulado de compras anteriores.");
                Console.Write("Deseja usar o desconto (S/N)? ");
                string? respUsaDescontoAnterior = Console.ReadLine();

                if (respUsaDescontoAnterior == "S" || respUsaDescontoAnterior == "s")
                {
                    DescontoCashback = new DescontoCashback(clienteCashback).CalcularDesconto(TotalCompra);
                }

                Console.WriteLine("\nPressione alguma tecla para prosseguir!");
                Console.ReadKey();
            }
        }

        public void FormaPagamentoSelecionada(Cliente cliente)
        {
            decimal totalRestante = TotalCompra;

            do
            {
                Console.Clear();
                Utils.FormataCabecalho("SELEÇÃO DAS FORMAS DE PAGAMENTO");
                Console.WriteLine($"\nTotal a ser pago: R$ {totalRestante:F2}");
                Console.WriteLine("Selecione a forma de pagamento conforme listado abaixo:");

                foreach (var formasPagamento in ListaFormasPagamentos)
                {
                    Console.WriteLine($" [{formasPagamento.Id}] - {formasPagamento.Descricao}");
                }
                
                int formaPagamento = Utils.EvitaQuebraCodInt($"\nDigite o código da condição de pagamento a ser utilizada: ");
                decimal valorFormaPagamento = Utils.EvitaQuebraCodDecimal($"Valor: R$ ");

                IFormasPagamento? formaPagamentoSelecionada = ListaFormasPagamentos.FirstOrDefault(formaPagamentoSelecionada =>
                                                 formaPagamentoSelecionada.Id == formaPagamento);

                if (formaPagamentoSelecionada == null)
                {
                    Console.WriteLine("Forma de pagamento selecionada inválida! Pressione uma tecla para prosseguir");
                    Console.ReadKey();
                    continue;
                }

                if (valorFormaPagamento <= 0)
                {
                    Console.WriteLine("O valor do pagamento deve ser maior que zero!");
                    Console.ReadKey();
                    continue;
                }
                
                decimal abaterValor = formaPagamentoSelecionada.ProcessarPagamento(valorFormaPagamento, totalRestante, cliente!);
                totalRestante -= abaterValor;

            } while (totalRestante > 0);

            Console.WriteLine("\nPagamento concluído! Pressione uma tecla para prosseguir!");
            Console.ReadKey();






            //    //    case 2:
            //    //        if (formaPagamentosDisponiveis.Valor <= TotalCompra)
            //    //        {
            //    //            Console.WriteLine($"{formaPagamentosDisponiveis.Descricao}: R$ {ValorSelecionado:F2}");
            //    //            PagamentosRealizados(ValorSelecionado);
            //    //        }
            //    //        else
            //    //            Console.WriteLine("Valor pago acima do subtotal não permitido para condição de pagamento cartão de crédito");
            //    //        break;

            //    //    case 3:

            //    //        if (formaPagamentosDisponiveis.Valor <= TotalCompra)
            //    //        {
            //    //            Console.WriteLine($"{formaPagamentosDisponiveis.Descricao}: R$ {ValorSelecionado:F2}");
            //    //            PagamentosRealizados(ValorSelecionado);
            //    //        }
            //    //        else
            //    //            Console.WriteLine("Valor pago acima do subtotal não permitido para condição de pagamento cartão de débito");
            //    //        break;
            //    //    case 4:

            //    //        if (formaPagamentosDisponiveis.Valor <= TotalCompra)
            //    //        {
            //    //            Console.WriteLine($"{formaPagamentosDisponiveis.Descricao}: R$ {ValorSelecionado:F2}");
            //    //            PagamentosRealizados(ValorSelecionado);
            //    //        }
            //    //        else
            //    //            Console.WriteLine("Valor pago acima do subtotal não permitido para condição de pagamento boleto");
            //    //        break;
            //    //}
            //}
            //else
            //    Console.WriteLine("Condição de pagamento informa inválida!");
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
