using HextecInformatica.Entities;
using HextecInformatica.Entities.Core;
using HextecInformatica.Interfaces;

namespace HextecInformatica.Repositories
{
    public class ColaboradorRepository: IRepositories<Colaborador>
    {
        public List<Colaborador> ListaColaboradores { get; private set; } = [];

        public void Adiciona(Colaborador colaborador)
        {
            ListaColaboradores.Add(colaborador);
        }

        public Colaborador? BuscaNome(string nome)
        {
            return ListaColaboradores.FirstOrDefault(colaborador => colaborador.Nome == nome);
        }

        public Colaborador? BuscaID(int id)
        {
            return ListaColaboradores.FirstOrDefault(colaborador => colaborador.Id == id);
        }

    }
}
