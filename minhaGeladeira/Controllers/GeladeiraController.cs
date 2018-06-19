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

namespace minhaGeladeira.Controllers
{
    [RoutePrefix("api/grupos/{id:int}/geladeira")]
    public class GeladeiraController : ApiController
    {
        private minhaGeladeiraEntities db = new minhaGeladeiraEntities();

        // GET: api/Geladeira
        public IQueryable<Item_Geladeira> GetItem_Geladeira()
        {
            return db.ItensGeladeira;
        }

        // GET: api/Geladeira/5
        [ResponseType(typeof(Item_Geladeira))]
        public IHttpActionResult GetItem_Geladeira(int id)
        {
            Item_Geladeira item_Geladeira = db.ItensGeladeira.Find(id);
            if (item_Geladeira == null)
            {
                return NotFound();
            }

            return Ok(item_Geladeira);
        }

        // PUT: api/Geladeira/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem_Geladeira(int id, Item_Geladeira item_Geladeira)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item_Geladeira.Id)
            {
                return BadRequest();
            }

            db.Entry(item_Geladeira).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Item_GeladeiraExists(id))
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

        // POST: api/Geladeira
        [ResponseType(typeof(Item_Geladeira))]
        public IHttpActionResult PostItem_Geladeira(Item_Geladeira item_Geladeira)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItensGeladeira.Add(item_Geladeira);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item_Geladeira.Id }, item_Geladeira);
        }

        // DELETE: api/Geladeira/5
        [ResponseType(typeof(Item_Geladeira))]
        public IHttpActionResult DeleteItem_Geladeira(int id)
        {
            Item_Geladeira item_Geladeira = db.ItensGeladeira.Find(id);
            if (item_Geladeira == null)
            {
                return NotFound();
            }

            db.ItensGeladeira.Remove(item_Geladeira);
            db.SaveChanges();

            return Ok(item_Geladeira);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Item_GeladeiraExists(int id)
        {
            return db.ItensGeladeira.Count(e => e.Id == id) > 0;
        }
    }
}