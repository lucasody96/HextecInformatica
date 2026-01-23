using HextecInformatica.Entities.Core;
using HextecInformatica.Repositories;

namespace HextecInformatica.Entities.MenusColaborador
{
    public class MenuCadastroColaborador: Menu
    {
        private readonly ColaboradorRepository ColaboradorRepository;
        public MenuCadastroColaborador(ColaboradorRepository colabRepos)
        {
            Id = 4;
            Descricao = "Cadastro de Colaborador";
            ColaboradorRepository = colabRepos;
        }
        public override void AcionaMenu()
        {
   
        }
    }
}
