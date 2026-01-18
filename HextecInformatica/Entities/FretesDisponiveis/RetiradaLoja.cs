using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.FretesDisponiveis
{
    public class RetiradaLoja: IFormaEntrega
    {
        public int Id => 1;
        public string Descricao => "Retirada na loja";

        public decimal CalculaFrete(decimal subtotal)
        {
            return 0m;
        }

        public string ObterMensagem(decimal valorFrete)
        {
            return $"Opção de {Descricao} selecionada. Frete gratuito!";
        }
    }
}
