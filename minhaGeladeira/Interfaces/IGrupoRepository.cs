using minhaGeladeira.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minhaGeladeira.Repository
{
    public interface IGrupoRepository : IRepository<Grupo>
    {
        IEnumerable<GrupoSimples> GetTudo();
        IEnumerable<GrupoSimples> GetGruposId(int id);
       
    }
}
