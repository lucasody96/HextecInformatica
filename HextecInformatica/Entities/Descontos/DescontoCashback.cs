using HextecInformatica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Entities.Descontos
{
    public class DescontoCashback : IDesconto
    {
        public string TipoDesconto { get; } = "Cashback";

        public string? CodigoCupom { get; set; }

        public decimal CalcularDesconto(decimal subtotal)
        {
            return 0;
        }
        public string ObterMensagem(decimal valorDesconto)
        {
            return "";
        }

    }
}
