using HextecInformatica.Entities;
using HextecInformatica.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Repositories
{
    public class VendaRepository()
    {
        public List<Venda> ListaVendas { get; set; } = [];

        public void Adiciona(Venda venda)
        {
            ListaVendas.Add(venda);
        }

        public Venda? BuscaVendaNumNota(int numNotaFiscal)
        {
            return ListaVendas.FirstOrDefault(venda => venda.NumeroNotaFiscal == numNotaFiscal);
        }

        public Venda? BuscaVendaNomeCliente(string nomeCliente)
        {
            return ListaVendas.FirstOrDefault(venda => venda.ClienteNota == nomeCliente);
        }
    }
}
