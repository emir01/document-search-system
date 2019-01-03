using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using DSS.Data.Model.Context;
using StackExchange.Profiling;

namespace DSS.Presentation.Web.Controllers
{
    public class ProfilerTestsController : Controller
    {
        //
        // GET: /ProfilerTests/
        public ActionResult Index()
        {
            var profiler = MiniProfiler.Current;

            var db = new DsContext();

            var data = new List<string>();

            using (profiler.Step("Geting data"))
            {
                data = db.Documents.Select(t => t.Title).ToList();    
            }


            return View(model: data);
        }
	}
}