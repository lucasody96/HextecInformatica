using HextecInformatica.Entities.Core;
using HextecInformatica.Interfaces;
using HextecInformatica.Repositories;

namespace HextecInformatica.Entities.MenusColaborador
{
    public class MenuCadastroCliente: Menu
    {
        private readonly ClienteRepository ClienteRepository;
        public MenuCadastroCliente(ClienteRepository ClienteRepo)
        {
            Id = 3;
            Descricao = "Cadastro de Cliente";
            ClienteRepository = ClienteRepo;
        }

        public override void AcionaMenu()
        {
            
        }

    }
}
