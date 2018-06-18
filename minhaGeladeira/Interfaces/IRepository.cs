using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace minhaGeladeira.Repository
{
    public interface IRepository <TEntity> where TEntity:class
    {

        // Achar objetos ----------------

        TEntity Get(int id);

        //List é anterior à Inumerable, então age como tal
        IEnumerable<TEntity> GetAll();

        // Vai retornar uma "lista" de TEntity, que no caso serão do tipo classe como definido acima
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // Adicionar objetos -------------------

        void Add(TEntity entity);

        // adiciona uma lista de objetos 
        void AddRange(IEnumerable<TEntity> entities);
        
        // Remover objetos ------------------------
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        // This method was not in the videos, but I thought it would be useful to add.
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

    }
}
