using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Interfaces
{
    public interface IFormaEntrega
    {
        int Id { get; set; }
        string Descricao { get; set; }

        void CalculaFrete(decimal subtotal);

        string ObterMensagem(decimal valorFrete);

    }
}
