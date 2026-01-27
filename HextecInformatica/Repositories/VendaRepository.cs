using HextecInformatica.Entities;

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

        public List<Venda> BuscaVendaNomeCliente(string nomeCliente)
        {
            List<Venda> vendasEncontradas = [];

            for (var i = 0; i < ListaVendas.Count; i++)
            {
                Venda? vendaAtual = ListaVendas[i];

                if (vendaAtual.ClienteNota.Contains(nomeCliente, StringComparison.CurrentCultureIgnoreCase))
                {
                    vendasEncontradas.Add(vendaAtual);
                }
            }
            return vendasEncontradas;
        }
    }
}
