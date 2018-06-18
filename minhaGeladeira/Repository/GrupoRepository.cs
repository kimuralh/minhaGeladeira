using minhaGeladeira.Models;
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
        public IEnumerable<GrupoSimples> GetTudo()
        {

            var meio = minhaGeladeiraEntities.Grupos;
            //var meio2 = minhaGeladeiraEntities.MembrosGrupo;
            var result = meio.Select(e => new GrupoSimples
            {
                Id_Grupo = e.Id,
                Nome_Grupo = e.Nome
            });

            return result.ToList();

        }

        public IEnumerable<GrupoSimples> GetGruposId(int id)
        {
            var grupo = (from mg in minhaGeladeiraEntities.MembrosGrupo
                         join g in minhaGeladeiraEntities.Grupos
                         on mg.Id_Grupo equals g.Id
                         where mg.Id_Usuario == id
                         select new GrupoSimples
                         {
                             Id_Grupo = mg.Id_Grupo,
                             Nome_Grupo = g.Nome

                         });
            return grupo.ToList();

        }

 

        public minhaGeladeiraEntities minhaGeladeiraEntities
        {
            get { return Context as minhaGeladeiraEntities; }
        }
    }
}