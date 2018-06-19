using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using minhaGeladeira;
using minhaGeladeira.Models;
using minhaGeladeira.Repository;

namespace minhaGeladeira.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        private minhaGeladeiraEntities db = new minhaGeladeiraEntities();

        // retorna informações de todos os usuários cadastrados
        // GET: api/Usuarios
        [Route("")]
        public HttpResponseMessage GetUsuario()
        {
            // Dica: using e unit of work não parecem funcionar juntos
            UnityOfWork unitOfWork = new UnityOfWork(new minhaGeladeiraEntities());

            
                var usuarios = unitOfWork.Usuarios.GetTudo();
                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            
            

            // quando eu tento exibir um usuario que está associado à um grupo no MembroGrupo, dá ruim
        }

        // retorna informações do usuario com este id
        // GET: api/usuarios/id
        [ResponseType(typeof(Usuario))]
        [Route("{id:int}")]
        public HttpResponseMessage GetUsuario(int id)
        {
            UnityOfWork unityOfWork = new UnityOfWork(new minhaGeladeiraEntities());
            if (unityOfWork.Usuarios.GetUm(id) == null)
            {
                //var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                //{
                //    Content = new StringContent(string.Format("No Usuario with ID = {0}", id)),
                //    ReasonPhrase = "Usuario ID Not Found"
                //};

                RespostaSimples resp = new RespostaSimples
                {
                    Mensagem = "Não foi possivel achar o usuario"
                };
                return Request.CreateResponse(HttpStatusCode.NotFound,resp);
            }
            else
            {
                var usuario = unityOfWork.Usuarios.GetUm(id);
                return Request.CreateResponse(HttpStatusCode.OK, usuario);
            }
        }

        // retorna a lista de grupos em que este usuario esta inscrito
        // GET: api/usuarios/id/grupos
        [ResponseType(typeof(Grupo))]
        [Route("{id:int}/grupos")]
        public IHttpActionResult GetGrupo(int id)
        {
            UnityOfWork unityOfWork = new UnityOfWork(new minhaGeladeiraEntities());
            var grupos = unityOfWork.Grupos.GetGruposId(id);
            return Ok(grupos);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        [Route("")]
        [HttpPost]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuarios.Add(usuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuarios.Count(e => e.Id == id) > 0;
        }
    }
}