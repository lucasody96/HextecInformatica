using HextecInformatica.Classes.ClassesPai;

namespace HextecInformatica.Classes.ClassesFilhas
{
    public class Colaborador(string nome, string cpf, string login, string senha) : Pessoa(nome, "F", cpf, "")
    {
        public string Login { get; private set; } = login;
        public string Senha { get; private set; } = senha;

        public override string MensagemBoasVindas()
        {
            return $"\nSeja bem vindo {Nome}! Pressione enter para prosseguir ao menu do colaborador.";
        }
    }
}
