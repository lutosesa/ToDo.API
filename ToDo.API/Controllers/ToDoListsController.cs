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

        #region GetToDoLists        
        /// <summary>
        /// Get all intances/objects of ToDoList available. 
        /// GET: api/ToDoLists
        /// </summary>
        /// <returns></returns>
        public IQueryable<ToDoList> GetToDoLists()
        {
            return db.ToDoLists.OrderByDescending(item => item.Id);
        }
        #endregion


        #region GetToDoList with parameter
        /// <summary>
        /// Get a single intance/object of ToDoList. 
        /// GET: api/ToDoLists/{id}
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
        #endregion


        #region PutToDoList with two parameters        
        /// <summary>
        /// Update a single intance/object of ToDoList.   
        /// PUT: api/ToDoLists/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="toDoList"></param>
        /// <returns></returns>
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
        #endregion


        #region  PostToDoList with parameter        
        /// <summary>
        /// Create a new intance/object of ToDoList. 
        /// POST: api/ToDoLists
        /// </summary>
        /// <param name="toDoList"></param>
        /// <returns></returns>
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
        #endregion


        #region DeleteToDoList with parameter        
        /// <summary>
        /// Delete a singel intance/object of ToDoList. 
        /// DELETE: api/ToDoLists/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        #endregion



        #region Dispose with parameter        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region ToDoListExists with parameter 
        private bool ToDoListExists(int id)
        {
            return db.ToDoLists.Count(e => e.Id == id) > 0;
        }
        #endregion
    }
}