using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minhaGeladeira.Repository
{
    public class GrupoRepository: Repository<Grupo>,IGrupoRepository
    {
        public GrupoRepository(minhaGeladeiraEntities context)
            : base(context)
        {

        }
        public minhaGeladeiraEntities minhaGeladeiraEntities
        {
            get { return Context as minhaGeladeiraEntities; }
        }
    }
}