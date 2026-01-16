using HextecInformatica.Entities;
using HextecInformatica.Entities.Core;

namespace HextecInformatica.Repositories
{
    public class ColaboradorRepository
    {
        public List<Colaborador> ListaColaboradores { get; private set; } = [];

        public void AdicionaColaborador(Colaborador colaborador)
        {
            ListaColaboradores.Add(colaborador);
        }

    }
}
