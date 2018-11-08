namespace ToDo.API.Controllers
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Description;
    using ToDo.API.Models;

    public class ToDoListsController : ApiController
    {
        private ToDoListContext db = new ToDoListContext();


        /// <summary>
        /// GET: api/ToDoLists
        /// </summary>
        /// <returns></returns>
        public IQueryable<ToDoList> GetToDoLists()
        {
            return db.ToDoLists;
        }


        /// <summary>
        ///  GET: api/ToDoLists/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(ToDoList))]
        public IHttpActionResult GetToDoList(int id)
        {
            ToDoList toDoList = db.ToDoLists.Find(id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return Ok(toDoList);
        }

        // PUT: api/ToDoLists/{id}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutToDoList(int id, ToDoList toDoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDoList.Id)
            {
                return BadRequest();
            }

            db.Entry(toDoList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoListExists(id))
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

        // POST: api/ToDoLists
        [ResponseType(typeof(ToDoList))]
        public IHttpActionResult PostToDoList(ToDoList toDoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ToDoLists.Add(toDoList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = toDoList.Id }, toDoList);
        }

        // DELETE: api/ToDoLists/{id}
        [ResponseType(typeof(ToDoList))]
        public IHttpActionResult DeleteToDoList(int id)
        {
            ToDoList toDoList = db.ToDoLists.Find(id);
            if (toDoList == null)
            {
                return NotFound();
            }

            db.ToDoLists.Remove(toDoList);
            db.SaveChanges();

            return Ok(toDoList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToDoListExists(int id)
        {
            return db.ToDoLists.Count(e => e.Id == id) > 0;
        }
    }
}