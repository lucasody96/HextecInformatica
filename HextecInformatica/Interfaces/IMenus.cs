using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Interfaces
{
    public interface IMenus<T>
    {
        void Adiciona(T entidade);
        void Remove(T entidade);
        void Atualiza(T entidade);
    }
}
