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

        // ideal seria se conseguisse trazer a lista de grupos da membrogrupo, mas não foi possivel
        public IEnumerable<UsuarioSimples> GetTudo()
        {

            var meio = minhaGeladeiraEntities.Usuarios;
            //var meio2 = minhaGeladeiraEntities.MembrosGrupo;
            var result = meio.Select(e => new UsuarioSimples
            {
                Id = e.Id,
                Nome = e.Nome,
                RG = e.RG,
                Telefone = e.Telefone,
            });
           
            return result.ToList();

        }

        public UsuarioSimples GetUm(int id)
        {
            Usuario usuario = minhaGeladeiraEntities.Usuarios.Where(x => x.Id == id).FirstOrDefault();
            if(usuario == null)
            {
                
                return null;
            }
            else
            {
                UsuarioSimples user = new UsuarioSimples
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    RG = usuario.RG,
                    Telefone = usuario.Telefone,
                };
                return user;
            }
            
        }

        // pega os usuarios presentes no grupo com este id
        public IEnumerable<UsuarioSimples> GetUsuariosId(int id)
        {
            var usuarios = (from mg in minhaGeladeiraEntities.MembrosGrupo
                            join u in minhaGeladeiraEntities.Usuarios
                            on mg.Id_Usuario equals u.Id
                            where mg.Id_Grupo == id
                            select new UsuarioSimples
                            {
                                Id = mg.Id_Usuario,
                                Nome = u.Nome,
                                RG = u.RG,
                                Telefone = u.Telefone
                            });
            return usuarios.ToList();
        }
    }
}