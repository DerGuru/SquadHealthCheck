using Microsoft.AspNetCore.Mvc;
using SquadHealthCheck.Models;

namespace SquadHealthCheck.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public ActionResult Index()
        {
            ViewerModel vm = new ViewerModel(HttpContext.User.Identity?.Name);
            return View(vm);
        }

        [Route("/Join/{id}")]
        public ActionResult Join(int id)
        {
            ViewerModel vm = new ViewerModel(HttpContext.User.Identity?.Name);
            vm.Join(id);
            return Redirect("~/");
        }

        [Route("/Leave/{id}")]
        public ActionResult Leave(int id)
        {
            ViewerModel vm = new ViewerModel(HttpContext.User.Identity?.Name);
            vm.Leave(id);
            return Redirect("~/");
        }
    }
}