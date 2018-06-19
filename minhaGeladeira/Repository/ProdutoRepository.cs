using minhaGeladeira.Models;
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

        public IEnumerable<ProdutoSimples> GetTudo()
        {
            var meio = minhaGeladeiraEntities.Produtos.ToList();
            var result = meio.Select(e => new ProdutoSimples
            {
                Id = e.Id,
                Nome = e.Nome,
                Preco = e.Preco,
            });
            return result.ToList();
            
        }
        public minhaGeladeiraEntities minhaGeladeiraEntities
        {
            get { return Context as minhaGeladeiraEntities; }
        }
    }
}