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
        public double Valor { get; set; }
        public int Disponivel { get; set; }

        public Produto(int codigo, string descricao, double valor, int disponivel)
        {
            Codigo = codigo;
            Descricao = descricao;
            Valor = valor;
            Disponivel = disponivel;
        }

    }
}
