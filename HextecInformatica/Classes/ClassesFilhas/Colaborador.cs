using HextecInformatica.Classes.ClassesPai;

namespace HextecInformatica.Classes.ClassesFilhas
{
    public class Colaborador: Pessoa
    {
        public string Login { get; private set; }
        public string Senha { get; private set; }

        public Colaborador(string nome,  string cpf, string login, string senha) : base(nome, "F", cpf, "")
        {
            Login = login;
            Senha = senha;
        }

        public override string ToString()
        {
            return $"{Login}";
        }

    }
}
