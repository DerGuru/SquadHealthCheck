using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SquadHealthCheck.Models;
using System;

namespace SquadHealthCheck.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IServiceProvider sp;

        public HomeController(IServiceProvider sp)
        {
            this.sp = sp;
        }
        [Route("/")]
        public ActionResult Index( )
        {
            ViewerModel vm = new ViewerModel(() => sp.GetRequiredService<ShcDataModel>(), HttpContext.User.Identity?.Name);
            return View(vm);
        }

        [Route("/Join/{id}")]
        public ActionResult Join(int id)
        {
            ViewerModel vm = new ViewerModel(() => sp.GetRequiredService<ShcDataModel>(), HttpContext.User.Identity?.Name);
            vm.Join(id);
            return Redirect("~/");
        }

        [Route("/Leave/{id}")]
        public ActionResult Leave(int id)
        {
            ViewerModel vm = new ViewerModel(() => sp.GetRequiredService<ShcDataModel>(), HttpContext.User.Identity?.Name);
            vm.Leave(id);
            return Redirect("~/");
        }
    }
}