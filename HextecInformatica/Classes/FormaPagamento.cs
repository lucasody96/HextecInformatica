using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class FormaPagamento
    {
        public int Codigo {  get; set; }
        public string Descricao { get; set; }

        public decimal Valor = 0;

        public FormaPagamento(int codigo, string descricao, decimal valor) 
        {
            Codigo = codigo;
            Descricao = descricao;
            Valor = valor;
        }
    }
}
