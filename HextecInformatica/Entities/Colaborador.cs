using HextecInformatica.Entities.Core;

namespace HextecInformatica.Entities
{
    public class Colaborador : Pessoa
    {
        private static int _contadorColaboradores = 0;
        public string Login { get; set; }
        public string Senha { get; set; }

        public Colaborador(string nome, string cpf, string login, string senha)
            : base(nome,"", cpf, "")
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
