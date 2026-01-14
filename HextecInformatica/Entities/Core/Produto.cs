using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Entities.Core
{
    public class Produto : Entity
    {
        private static int _contadorProdutos = 0;
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Estoque { get; set; } 
        public int QuantidadeComprada { get; set; }

        public Produto(string descricao, decimal valor, int estoque)
        {
            //geração do ID automático
            _contadorProdutos++;
            Id = _contadorProdutos;

            Descricao = descricao;
            Valor = valor;
            Estoque = estoque;
            QuantidadeComprada = 0;
        }

        public override string ToString()
        {
            return $"{Id} | {Descricao} | R$ {Valor} | {Estoque}";
        }
    }
}
