using System.Web.Mvc;

namespace DSS.Presentation.Web.Controllers
{
    /// <summary>
    /// The main Home controller used to present the initial DSS application information
    /// </summary>
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
    }
}
