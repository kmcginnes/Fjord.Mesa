using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fjord.Mesa.Helpers;

namespace Fjord.Mesa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            //var key = db.Entities.Max(e => e.updated_at).ToString("yyyyMMddhhmmss");
            var key = "default_key";
            return Content(AppCache.FromCache<string>("v1/Entities/Index/" + key, IndexGenerate));
        }

        public string IndexGenerate()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";
            return RenderViewToString(this, "Index");
        }

        public static string RenderViewToString(Controller controller, string viewName, object model = null)
        {
            if(model != null)
            {
                controller.ViewData.Model = model;
            }

            try
            {
                using (var sw = new StringWriter())
                {
                    var viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, "");
                    var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
