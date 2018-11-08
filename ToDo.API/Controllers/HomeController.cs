namespace ToDo.API.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Demo ToDo List";

            return View();
        }
    }
}
