using HextecInformatica.Entities;

namespace HextecInformatica.Interfaces
{
    public interface IRepositories <T>
    {
        void Adiciona(T entidade);
        T? BuscaLoginNome(string parametro);
        T? BuscaID(int parametro);

        void Delete (T entidade);

        void Update(T entidade, int parametro);


    }
}
