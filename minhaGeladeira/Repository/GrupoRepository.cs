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

        public GrupoSimples GetUm(int id)
        {
            var meio = minhaGeladeiraEntities.Grupos.Where(x => x.Id == id).FirstOrDefault();
            if(meio != null)
            {
                GrupoSimples result = new GrupoSimples
                {
                    Id_Grupo = meio.Id,
                    Nome_Grupo = meio.Nome,
                };
                return result;

            }
            else
            {
                return null;
            }
            

        }

        public bool ExisteGrupo(string nome)
        {
            if (minhaGeladeiraEntities.Grupos.Where(x => x.Nome.Trim() == nome.Trim()).Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public minhaGeladeiraEntities minhaGeladeiraEntities
        {
            get { return Context as minhaGeladeiraEntities; }
        }
    }
}