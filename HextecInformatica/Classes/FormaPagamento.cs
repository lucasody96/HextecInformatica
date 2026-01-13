using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class FormaPagamento(int codigo, string descricao, decimal valor)
    {
        public int Codigo { get; set; } = codigo;
        public string Descricao { get; set; } = descricao;

        public decimal Valor = valor;
    }
}
