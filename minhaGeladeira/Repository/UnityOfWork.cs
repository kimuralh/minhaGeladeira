using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minhaGeladeira.Repository
{
    public class UnityOfWork : IUnitOfWork
    {
        
        private readonly minhaGeladeiraEntities _context;

        public UnityOfWork(minhaGeladeiraEntities context)
        {
            _context = context;
            Grupos = new GrupoRepository(_context);
            //ItensGeladeira = new ItemGeladeiraRepository(_context);
            //ItensListaCompra = new ItemListaCompraRepository(_context);
            MembrosGrupo = new MembroGrupoRepository(_context);
            Produtos = new ProdutoRepository(_context);
            Usuarios = new UsuarioRepository(_context);

        }

        public IGrupoRepository Grupos { get; private set; }

        //public IItemGeladeiraRepository ItensGeladeira { get; private set; }

        //public IItemListaCompraRepository ItensListaCompra { get; private set; }

        public IMembroGrupoRepository MembrosGrupo { get; private set; }

        public IProdutoRepository Produtos { get; private set; }

        public IUsuarioRepository Usuarios { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}