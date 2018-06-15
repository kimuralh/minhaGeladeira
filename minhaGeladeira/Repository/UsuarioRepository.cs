using minhaGeladeira.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minhaGeladeira.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(minhaGeladeiraEntities context)
            : base(context)
        {

        }

        
        public minhaGeladeiraEntities minhaGeladeiraEntities
        {
            get { return Context as minhaGeladeiraEntities; }
        }

        public IEnumerable<UsuarioNZoado> GetTudo()
        {   
               
                var meio = minhaGeladeiraEntities.Usuarios.Where(x => x.Nome == "Lucas");

                var result = meio.Select(e => new UsuarioNZoado
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    RG = e.RG,
                    Telefone = e.Telefone
                });

                return result.ToList();

            
        }

        public Usuario GetUm()
        {
            Usuario usuario = minhaGeladeiraEntities.Usuarios.Where(x => x.Nome == "Kimura").FirstOrDefault();
            usuario.Membro_Grupo.Add(new Membro_Grupo {Id = 1, Id_Grupo = 1, Id_Usuario = 5 });
            return usuario;
        }

        
    }
}