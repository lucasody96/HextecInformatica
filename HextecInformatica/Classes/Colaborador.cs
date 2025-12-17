using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    internal class Colaborador
    {
        public string NomeColaborador { get; set; }
        public string Senha { get; set; }
        public string Cargo { get; set; }

        public Colaborador(string nomeColaborador, string senha, string cargo)
        {
            NomeColaborador = nomeColaborador;
            Senha = senha;
            Cargo = cargo;
        }


    }
}
