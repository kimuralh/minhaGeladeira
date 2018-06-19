using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minhaGeladeira.Models
{
    public class ProdutoSimples
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Nullable<decimal> Preco { get; set; }
    }
}