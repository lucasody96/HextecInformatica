namespace HextecInformatica.Entities
{
    public class Venda
    {
        private static int _contadorVendas = 0;
        public int NumeroNotaFiscal { get; set; }

        public DateTime DataVenda { get; set; } = DateTime.Now;
        public Venda()
        {
            //geração do ID automático
            _contadorVendas++;
            NumeroNotaFiscal = _contadorVendas;
        }

        

    }

}
