using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.FretesDisponiveis
{
    public class EntregaPadrao: IFormaEntrega
    {
        private readonly decimal VALOR_FRETE = 20m;
        private readonly decimal VALOR_FRETE_GRATIS = 300;
        public int Id => 2;
        public string Descricao => "Entrega Padrão";

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
