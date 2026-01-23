using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Interfaces
{
    public interface IMenus<T>
    {
        void Adicionar(T entidade);
        void Remover(T entidade);
        void Atualizar(T entidade);
        void Visualizar(T entidade);
    }
}
