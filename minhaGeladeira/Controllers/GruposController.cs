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
                RespostaSimples resp = new RespostaSimples();
                resp.Mensagem = "Não foi possivel achar o grupo";
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
        [Route("")]
        public HttpResponseMessage PutGrupo([FromBody] GrupoSimples grupo)
        {
            UnityOfWork unityOfWork = new UnityOfWork(new minhaGeladeiraEntities());
            if (GrupoExists(grupo.Id_Grupo)  == true)
            {
                unityOfWork.Grupos.AlteraGrupo(grupo);

                try
                {
                    unityOfWork.Complete();
                    RespostaSimples resp = new RespostaSimples();
                    resp.Mensagem = "Grupo atualizado com sucesso";
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(grupo.Id_Grupo))
                    {
                        RespostaSimples resp = new RespostaSimples();
                        resp.Mensagem = "Não foi possivel achar o grupo";
                        return Request.CreateResponse(HttpStatusCode.NotFound, resp);
                    }
                    else
                    {
                        RespostaSimples resp = new RespostaSimples();
                        resp.Mensagem = "Não foi possivel alterar o grupo";
                        return Request.CreateResponse(HttpStatusCode.NotFound, resp);

                    }
                    
                }
            }

            else
            {
                RespostaSimples resp = new RespostaSimples();
                resp.Mensagem = "Id Grupo invalido";
                return Request.CreateResponse(HttpStatusCode.NotFound, resp);
            }
                   

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
                RespostaSimples resp = new RespostaSimples();
                resp.Mensagem = "Já existe um Grupo com esse nome";

                return Request.CreateResponse(HttpStatusCode.Conflict, resp);
            }
            else
            {
                unityOfWork.Grupos.Add(estegrupo);
                unityOfWork.Complete();

                RespostaSimples resp = new RespostaSimples();
                resp.Mensagem = "Grupo Cadastrado";

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
           
        }

        // DELETE: api/Grupo/5
        [ResponseType(typeof(Grupo))]
        [Route("{id:int}")]
        public HttpResponseMessage DeleteGrupo(int id)
        {
            UnityOfWork unityOfWork = new UnityOfWork(new minhaGeladeiraEntities());
            

            
            Grupo grupo = unityOfWork.Grupos.Get(id);
            if (grupo == null)
            {
                RespostaSimples resp = new RespostaSimples();
                resp.Mensagem = "Grupo Não Encontrado";
                
                return Request.CreateResponse(HttpStatusCode.NotFound, resp);
            }
            else
            {
                unityOfWork.Grupos.Remove(unityOfWork.Grupos.Get(id));
                unityOfWork.Complete();
                RespostaSimples resp = new RespostaSimples
                {
                    Mensagem = "Grupo Removido"
                };
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            
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
            UnityOfWork unityOfWork = new UnityOfWork(new minhaGeladeiraEntities());
            if(unityOfWork.Grupos.GetUm(id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}