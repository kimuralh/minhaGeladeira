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
using minhaGeladeira.Repository;

namespace minhaGeladeira.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        private minhaGeladeiraEntities db = new minhaGeladeiraEntities();

        
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

        // GET: api/usuarios/id
        [ResponseType(typeof(Usuario))]
        [Route("{id:int}")]
        public IHttpActionResult GetUsuario(int id)
        {
            UnityOfWork unityOfWork = new UnityOfWork(new minhaGeladeiraEntities());
            var usuario = unityOfWork.Usuarios.GetUm(id);
            return Ok(usuario);
        }

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