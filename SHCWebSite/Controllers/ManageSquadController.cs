using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SquadHealthCheck.Models;
using System;
using System.Text;

namespace SquadHealthCheck.Controllers
{
    [Authorize]
    [Route("/ManageSquad")]
    public class ManageSquadController : Controller
    {
        private IServiceProvider sp;

        public ManageSquadController(IServiceProvider sp)
        {
            this.sp = sp;
        }
        [Route("")]
        public IActionResult Index()
        {
            AdminModel vm = new AdminModel(new Uri(HttpContext.Request.GetDisplayUrl()), () => sp.GetRequiredService<ShcDataModel>(), HttpContext.User.Identity?.Name);
            return View(vm);
        }

        [Route("Create/{id}")]
        public IActionResult Create(string id)
        {
            string name = Encoding.Default.GetString(Convert.FromBase64String(id));
            AdminModel vm = new AdminModel(new Uri(HttpContext.Request.GetDisplayUrl()), () => sp.GetRequiredService<ShcDataModel>(), HttpContext.User.Identity?.Name);
            if (vm.Create(name))
                return Redirect($"~/ManageSquad/Config/{vm.Squad.Id}");
            else
                return View("SquadExists");
        }

        [Route("Config/{id}")]
        public IActionResult Config(int id)
        {
            AdminModel vm = new AdminModel(new Uri(HttpContext.Request.GetDisplayUrl()), () => sp.GetRequiredService<ShcDataModel>(), HttpContext.User.Identity?.Name, id);
            return View(vm);
        }
    }
}