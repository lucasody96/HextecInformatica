using HextecInformatica.Entities;
using HextecInformatica.Entities.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Services
{
    public class CarrinhoService
    {
        public List<Produto> ListaProdutosDisponiveis { get; set; } = [];
        public List<Produto> ListaItensCarrinho { get; private set; } = [];

        public Carrinho Carrinho { get; set; } = new();

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

        public void AdicionaItensCarrinho()
        {
            bool codZero = false;
            while (!codZero)
            {
                int codProdutoSelecionado = Utils.EvitaQuebraCodInt("\nSelecione o item a ser adicionado ao carrinho ou 0 para sair: ");

                if (codProdutoSelecionado > 0)
                {
                    var ProdutoCatalogo = ListaProdutosDisponiveis.FirstOrDefault(produto => produto.Id == codProdutoSelecionado);

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

            Console.WriteLine($"Subtotal: {Carrinho.Subtotal:C}");
            if (Carrinho.Frete > 0)
                Console.WriteLine($"Frete: {Carrinho.Frete:C}");
            if (Carrinho.DescontoCupom > 0)
                Console.WriteLine($"Cupom Desconto: -{Carrinho.DescontoCupom:C}");
            if (Carrinho.DescontoCashback > 0)
                Console.WriteLine($"Cashback: -{Carrinho.DescontoCashback:C}");

            Utils.ImprimeLinhaSeparadora('-');
            Console.WriteLine($"TOTAL A PAGAR: {Carrinho.TotalCompra:C}");
            Utils.ImprimeLinhaSeparadora('=');
        }

        public void RemoveItensCarrinho(int codProdutoRemovido)
        {
            //uso do linq para achar o item ao invés do foreach
            var itemASerRemovido = ListaItensCarrinho.FirstOrDefault(item => item.Id == codProdutoRemovido);

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
            var itemDevolvido = ListaProdutosDisponiveis.FirstOrDefault(item => item.Id == codProduto);

            if (itemDevolvido != null)
            {
                itemDevolvido.Estoque += QtdDevolvida;
            }
        }

        public void FormaEntrega(int respFormaEntrega)
        {
            Frete = 0;
            switch (respFormaEntrega)
            {

                case 1:

                    Console.WriteLine("Opção de retirada na loja selecionada. Frete gratuito!");
                    break;
                case 2:
                    if (Subtotal > 300.00m)
                        Console.WriteLine("Opção de entrega padrão selecionada e subtotal acima de R$ 300,00. Frete gratuito!");
                    else
                    {
                        Frete = 20.00m;
                        Console.WriteLine($"Opção de entrega padrão selecionada e subtotal abaixo de R$ 300,00. Valor do frete: R$ {Frete}!");
                    }
                    break;
                case 3:
                    if (Subtotal > 500.00m)
                        Console.WriteLine("Opção de entrega expressa selecionada e subtotal acima de R$ 500,00. Frete gratuito!");
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
}
