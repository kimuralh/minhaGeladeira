using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minhaGeladeira.Models
{
    public class UsuarioNZoado
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public string Telefone { get; set; }
        //public Membro_Grupo Grupo { get; set; }
    }
}