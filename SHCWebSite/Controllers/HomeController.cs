using Microsoft.AspNetCore.Mvc;
using SquadHealthCheck.Models;

namespace SquadHealthCheck.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewerModel vm = new ViewerModel(HttpContext.User.Identity?.Name);
            
            return View(vm);
        }

        public ActionResult Join(int id)
        {
            ViewerModel vm = new ViewerModel(HttpContext.User.Identity?.Name);
            vm.Join(id);
            return Redirect("~/");
        }

        public ActionResult Leave(int id)
        {
            ViewerModel vm = new ViewerModel(HttpContext.User.Identity?.Name);
            vm.Leave(id);
            return Redirect("~/");
        }

     
    }
}