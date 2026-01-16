using HextecInformatica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Interfaces
{
    public interface IProdutos
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Estoque { get; set; }
        public int QuantidadeComprada { get; set; }
        public void ImprimirProduto();
        public string ToString();
    }
}
