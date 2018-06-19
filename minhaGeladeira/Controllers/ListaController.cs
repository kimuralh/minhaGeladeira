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
    [RoutePrefix("api/grupos/{id:int}/lista")]
    public class ListaController : ApiController
    {
        private minhaGeladeiraEntities db = new minhaGeladeiraEntities();

        // GET: api/Lista
        public IQueryable<Item_Lista_Compra> GetItem_Lista_Compra()
        {
            return db.ItensListaCompra;
        }

        // GET: api/Lista/5
        [ResponseType(typeof(Item_Lista_Compra))]
        public IHttpActionResult GetItem_Lista_Compra(int id)
        {
            Item_Lista_Compra item_Lista_Compra = db.ItensListaCompra.Find(id);
            if (item_Lista_Compra == null)
            {
                return NotFound();
            }

            return Ok(item_Lista_Compra);
        }

        // PUT: api/Lista/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem_Lista_Compra(int id, Item_Lista_Compra item_Lista_Compra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item_Lista_Compra.Id)
            {
                return BadRequest();
            }

            db.Entry(item_Lista_Compra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Item_Lista_CompraExists(id))
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

        // POST: api/Lista
        [ResponseType(typeof(Item_Lista_Compra))]
        public IHttpActionResult PostItem_Lista_Compra(Item_Lista_Compra item_Lista_Compra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItensListaCompra.Add(item_Lista_Compra);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item_Lista_Compra.Id }, item_Lista_Compra);
        }

        // DELETE: api/Lista/5
        [ResponseType(typeof(Item_Lista_Compra))]
        public IHttpActionResult DeleteItem_Lista_Compra(int id)
        {
            Item_Lista_Compra item_Lista_Compra = db.ItensListaCompra.Find(id);
            if (item_Lista_Compra == null)
            {
                return NotFound();
            }

            db.ItensListaCompra.Remove(item_Lista_Compra);
            db.SaveChanges();

            return Ok(item_Lista_Compra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Item_Lista_CompraExists(int id)
        {
            return db.ItensListaCompra.Count(e => e.Id == id) > 0;
        }
    }
}