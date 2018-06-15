using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minhaGeladeira.Repository
{
    public class MembroGrupoRepository : Repository<Membro_Grupo>, IMembroGrupoRepository
    {
        public MembroGrupoRepository(minhaGeladeiraEntities context)
            : base(context)
        {

        }
        public minhaGeladeiraEntities minhaGeladeiraEntities
        {
            get { return Context as minhaGeladeiraEntities; }
        }
    }
}