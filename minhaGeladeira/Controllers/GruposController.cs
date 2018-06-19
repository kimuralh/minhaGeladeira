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
    [RoutePrefix ("api/grupos")]
    public class GruposController : ApiController
    {
        private minhaGeladeiraEntities db = new minhaGeladeiraEntities();

        // GET: api/Grupo
        //public IQueryable<Grupo> GetGrupo(int id)
        //{
        //    UnityOfWork unityofWork = new UnityOfWork(new minhaGeladeiraEntities());
        //    unityofWork.MembrosGrupo.Get

        //}

        [ResponseType(typeof(Grupo))]
        [Route("")]
        public HttpResponseMessage GetGrupos()
        {
            UnityOfWork unitOfWork = new UnityOfWork(new minhaGeladeiraEntities());

            var x = unitOfWork.Grupos.GetTudo();
            return Request.CreateResponse(HttpStatusCode.OK, x);
        }

        // GET: api/grupos/id/usuarios
        [ResponseType(typeof(Usuario))]
        [Route("{id:int}/usuarios")]
        public IHttpActionResult GetUsuarios(int id)
        {
            UnityOfWork unityOfWork = new UnityOfWork(new minhaGeladeiraEntities());
            var usuarios = unityOfWork.Usuarios.GetUsuariosId(id);
            return Ok(usuarios);
        }

        // GET: api/Grupo/5
        [ResponseType(typeof(Grupo))]
        [Route("{id:int}")]
        public HttpResponseMessage GetGrupo(int id)
        {
            UnityOfWork unityOfWork = new UnityOfWork(new minhaGeladeiraEntities());
            
            if (unityOfWork.Grupos.GetUm(id) == null)
            {
               RespostaSimples resp = new RespostaSimples
                {
                    Mensagem = "Não foi possivel achar o Grupo"
                };
                return Request.CreateResponse(HttpStatusCode.NotFound, resp);
            }
            else
            {
                var grupo = unityOfWork.Grupos.GetUm(id);
                return Request.CreateResponse(HttpStatusCode.OK, grupo);
            }
        }

        // PUT: api/Grupo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrupo(int id, Grupo grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grupo.Id)
            {
                return BadRequest();
            }

            db.Entry(grupo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupoExists(id))
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

        // POST: api/Grupo
        [ResponseType(typeof(Grupo))]
        [Route("")]
        public HttpResponseMessage PostGrupo([FromBody] GrupoSimples grupo)
        {
            UnityOfWork unityOfWork = new UnityOfWork(new minhaGeladeiraEntities());
            Grupo estegrupo = new Grupo
            {
                Nome = grupo.Nome_Grupo,
            };
            if(unityOfWork.Grupos.ExisteGrupo(estegrupo.Nome) == true)
            {
                RespostaSimples resp = new RespostaSimples
                {
                    Mensagem = "Já existe um Grupo com este nome"
                };

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            else
            {
                unityOfWork.Grupos.Add(estegrupo);
                unityOfWork.Complete();

                RespostaSimples resp = new RespostaSimples
                {
                    Mensagem = "Grupo Cadastrado"
                };

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
           
        }

        // DELETE: api/Grupo/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult DeleteGrupo(int id)
        {
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return NotFound();
            }

            db.Grupos.Remove(grupo);
            db.SaveChanges();

            return Ok(grupo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GrupoExists(int id)
        {
            return db.Grupos.Count(e => e.Id == id) > 0;
        }
    }
}