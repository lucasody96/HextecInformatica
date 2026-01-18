using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.FretesDisponiveis
{
    public class EntregaExpressa: IFormaEntrega
    {
        private readonly decimal VALOR_FRETE = 40.00m;
        private readonly decimal VALOR_FRETE_GRATIS = 500.00m;
        public int Id => 3;
        public string Descricao => "Entrega Expressa";

        public Decimal CalculaFrete(decimal subtotal)
        {
            return subtotal >= VALOR_FRETE_GRATIS ? 0m : VALOR_FRETE;
        }

        public string ObterMensagem(decimal valorFrete)
        {
            return valorFrete == 0m ? $"Opção de {Descricao} selecionada e subtotal acima de R$ 300,00. Frete gratuito!" :
                                      $"Opção de {Descricao} selecionada e subtotal abaixo de R$ 300,00. Valor do frete: R$ {VALOR_FRETE}!";
        }
    }
}
