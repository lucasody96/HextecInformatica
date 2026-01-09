using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class Colaborador
    {
        public string Nome { get; set; }

        public string Login {  get; set; }
        public string Senha { get; set; }
        
        public Colaborador(string nomeColaborador, string login, string senha)
        {
            Nome = nomeColaborador;
            Login = login;
            Senha = senha;
        }

    }
}
