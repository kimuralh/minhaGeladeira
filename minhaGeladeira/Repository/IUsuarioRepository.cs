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
        Usuario GetUm();
        IEnumerable<UsuarioNZoado> GetTudo();
    }
}
