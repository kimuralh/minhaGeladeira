﻿using System;
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
    public class GrupoController : ApiController
    {
        private minhaGeladeiraEntities db = new minhaGeladeiraEntities();

        // GET: api/Grupo
        public IQueryable<Grupo> GetGrupo(int id)
        {
            UnityOfWork unityofWork = new UnityOfWork(new minhaGeladeiraEntities());
            unityofWork.Grupos

        }

        //public IQueryable<Grupo> GetGrupo(int id)
        //{
        //    //return db.Grupo;
        //}

        // GET: api/Grupo/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult GetGrupo(int id)
        {
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return NotFound();
            }

            return Ok(grupo);
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
        public IHttpActionResult PostGrupo(Grupo grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Grupo.Add(grupo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = grupo.Id }, grupo);
        }

        // DELETE: api/Grupo/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult DeleteGrupo(int id)
        {
            Grupo grupo = db.Grupo.Find(id);
            if (grupo == null)
            {
                return NotFound();
            }

            db.Grupo.Remove(grupo);
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
            return db.Grupo.Count(e => e.Id == id) > 0;
        }
    }
}