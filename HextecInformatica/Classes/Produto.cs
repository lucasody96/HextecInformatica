using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Estoque { get; set; }

        public int QuantidadeComprada {  get; set; }

        public Produto(int codigo, string descricao, decimal valor, int disponivel)
        {
            Codigo = codigo;
            Descricao = descricao;
            Valor = valor;
            Estoque = disponivel;
        }
        public override string ToString()
        {
            return $"{Codigo} | {Descricao} | R$ {Valor} | {Estoque}";
        }

    }
}
