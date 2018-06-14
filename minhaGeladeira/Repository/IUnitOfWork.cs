using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minhaGeladeira.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGrupoRepository Grupos { get; }
        IItemGeladeiraRepository ItensGeladeira { get; }
        IItemListaCompraRepository ItensListaCompra { get; }
        IMembroGrupoRepository MembrosGrupo { get; }
        IProdutoRepository Produtos { get; }
        IUsuarioRepository Usuarios { get; }

        int Complete();
    }
}
