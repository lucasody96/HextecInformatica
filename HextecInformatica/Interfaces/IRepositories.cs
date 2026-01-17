namespace HextecInformatica.Interfaces
{
    public interface IRepositories <T>
    {
        void Adiciona(T entidade);
        T? BuscaNome(string parametro);
        T? BuscaID(int parametro);


    }
}
