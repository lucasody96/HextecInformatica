using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Interfaces
{
    public interface IDesconto
    {
        decimal CalcularDesconto(decimal subtotal);
        string ObterMensagem(decimal valorDesconto);
    }
}
