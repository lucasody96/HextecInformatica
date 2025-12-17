using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class Venda
    {
        public List<Produto> Carrinho { get; private set; } = new List<Produto>();
        public Queue<string> filaPagamentos { get; private set; } = new Queue<string>();

        public double ValorFrete { get; set; }
        public double ValorDescontoCupom { get; set; }
        public double ValorDescontoCashback { get; set; }
        public double ValorPago { get; set; }

        public Venda(double valorFrete, double valorDescontoCupom, double valorDescontoCashback, double valorPago)
        {
            ValorFrete = valorFrete;
            ValorDescontoCupom = valorDescontoCupom;
            ValorDescontoCashback = valorDescontoCashback;
            ValorPago = valorPago;
        }

    }
}
