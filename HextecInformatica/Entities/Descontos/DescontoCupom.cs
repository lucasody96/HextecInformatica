using HextecInformatica.Interfaces;

namespace HextecInformatica.Entities.Descontos
{
    public abstract class DescontoCupom : IDesconto
    {
        public string TipoDesconto { get; } = "Cupom";

        public string? CodigoCupom { get; protected set; } = string.Empty;

        public decimal PercentualDesconto { get; set; }

        public decimal ValorMinimoCompra { get; set; }
        public decimal CalcularDesconto(decimal subtotal)
        {
            return subtotal >= ValorMinimoCompra ? subtotal * PercentualDesconto : 0;   
        }
        public string ObterMensagem(decimal valorDesconto)
        {
            return $"Desconto de cupom R$ {valorDesconto:F2} aplicado com sucesso!";
        }
    }

    public class Cupom5Desconto: DescontoCupom
    {
        public Cupom5Desconto()
        {
            CodigoCupom = "CUPOM5%DESCONTO";
            PercentualDesconto = 0.05m;
            ValorMinimoCompra = 250.00m;
        }
    }

    public class Cupom10Desconto : DescontoCupom
    {
        public Cupom10Desconto()
        {
            CodigoCupom = "CUPOM10%DESCONTO";
            PercentualDesconto = 0.10m;
            ValorMinimoCompra = 500.00m;
        }
    }

    public class Cupom15Desconto : DescontoCupom
    {
        public Cupom15Desconto()
        {
            CodigoCupom = "CUPOM15%DESCONTO";
            PercentualDesconto = 0.15m;
            ValorMinimoCompra = 1000.00m;
        }
    }
}
