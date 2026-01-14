using HextecInformatica.Classes.Entity.Core;

namespace HextecInformatica.Classes
{
    public class Colaborador : Pessoa
    {
        private static int _contadorColaboradores = 0;
        public string Login { get; private set; }
        public string Senha { get; private set; }

        public Colaborador(string nome, string cpf, string login, string senha)
            : base(nome, cpf, "", "")
        {
            
            _contadorColaboradores++;
            Id = _contadorColaboradores;

            Login = login;
            Senha = senha;
        }

        public override string MensagemBoasVindas()
        {
            return $"\nSeja bem vindo {Nome}! Pressione enter para prosseguir ao menu do colaborador.";
        }
    }
}
