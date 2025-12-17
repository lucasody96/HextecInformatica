using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class Cliente
    {
        public string Nome {  get; set; }
        public string CpfCnpj {  get; set; }
        private Stack<double> descontoProximaCompra = new Stack<double>();
        public Cliente(string nome, string cpfCnpj) 
        {
            Nome = nome;
            CpfCnpj = cpfCnpj;
        } 
    }
}
