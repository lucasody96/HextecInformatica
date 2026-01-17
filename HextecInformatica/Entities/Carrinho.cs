using HextecInformatica.Entities.Core;

namespace HextecInformatica.Entities
{
    public class Carrinho()
    {
        public decimal Subtotal { get; set; }
        public decimal Frete { get; set; }
        public decimal DescontoCupom { get; set; }
        public decimal DescontoCashback { get; set; }
        public decimal Pagamentos { get; private set; }
        public decimal Troco { get; private set; }
        public decimal TotalCompra => Math.Round(Subtotal + Frete - DescontoCupom - DescontoCashback - Pagamentos, 2);
        public decimal TotalNotaFiscal => Math.Round(Subtotal + Frete - DescontoCupom - DescontoCashback, 2);
        public bool TrocoFoiConvertido { get; set; } = false;
    }
}
