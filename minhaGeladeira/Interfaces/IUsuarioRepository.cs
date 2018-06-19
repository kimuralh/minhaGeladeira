using minhaGeladeira.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minhaGeladeira.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        //IEnumerable<Usuario> GetTudo();
        UsuarioSimples GetUm(int id);
        IEnumerable<UsuarioSimples> GetTudo();
        IEnumerable<UsuarioSimples> GetUsuariosId(int id);
    }
}
