using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minhaGeladeira.Repository
{
    public interface IMembroGrupoRepository : IRepository<Membro_Grupo>
    {
        // membro em mais de um grupo
        // grupos em que o usuario está
        // pessoas membros daquele grupo
        // itens na geladeira daquele grupo
        // itens na lista daquele grupo
        // retirar da lista somente
        // retirar da lista e adicionar na geladeira
        // retirar não o item inteiro da geladeira, mas a quantidade daquele item
        // ideia: no item da lista, adicionar o id de quem está na lista do grupo para comprar
        // itens como carne em vez de adicionar em unidades, pode ser em gramas

    }
}
