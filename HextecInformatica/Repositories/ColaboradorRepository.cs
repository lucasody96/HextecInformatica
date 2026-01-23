using HextecInformatica.Entities;
using HextecInformatica.Interfaces;

namespace HextecInformatica.Repositories
{
    public class ColaboradorRepository : IRepositories<Colaborador>
    {
        public List<Colaborador> ListaColaboradores { get; private set; } = [];

        public void Adiciona(Colaborador colaborador)
        {
            ListaColaboradores.Add(colaborador);
        }

        public Colaborador? BuscaLoginNome(string login)
        {
            return ListaColaboradores.FirstOrDefault(colaborador => colaborador.Login == login);
        }

        public Colaborador? BuscaID(int id)
        {
            return ListaColaboradores.FirstOrDefault(colaborador => colaborador.Id == id);
        }

        public void Delete(Colaborador colaborador)
        {
            ListaColaboradores?.Remove(colaborador);
        }

        public void Update(Colaborador colaborador, int campoAlterado)
        {

        }

    }
}
