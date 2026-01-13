using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class Produto(int codigo, string descricao, decimal valor, int disponivel)
    {
        public int Codigo { get; set; } = codigo;
        public string Descricao { get; set; } = descricao;
        public decimal Valor { get; set; } = valor;
        public int Estoque { get; set; } = disponivel;
        public int QuantidadeComprada { get; set; }

        public override string ToString()
        {
            return $"{Codigo} | {Descricao} | R$ {Valor} | {Estoque}";
        }
    }
}
