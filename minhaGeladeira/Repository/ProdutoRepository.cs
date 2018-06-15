using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minhaGeladeira.Repository
{
    public class ProdutoRepository: Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(minhaGeladeiraEntities context)
            : base(context)
        {

        }

        public IEnumerable<Produto> GetAllProdutos()
        {
            return minhaGeladeiraEntities.Produtos.ToList();
            
        }
        public minhaGeladeiraEntities minhaGeladeiraEntities
        {
            get { return Context as minhaGeladeiraEntities; }
        }
    }
}