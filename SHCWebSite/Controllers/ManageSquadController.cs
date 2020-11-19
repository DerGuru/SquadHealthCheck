using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SquadHealthCheck.Models;
using System;
using System.Text;

namespace SquadHealthCheck.Controllers
{
    public class ManageSquadController : Controller
    {

        // GET: Squad
        public IActionResult Index()
        {
            AdminModel vm = new AdminModel(new Uri(HttpContext.Request.GetDisplayUrl()), HttpContext.User.Identity?.Name);
            return View(vm);
        }

        public IActionResult Create(string id)
        {
            string name = Encoding.Default.GetString(Convert.FromBase64String(id));
            AdminModel vm = new AdminModel(new Uri(HttpContext.Request.GetDisplayUrl()), HttpContext.User.Identity?.Name);
            if (vm.Create(name))
                return Redirect($"~/ManageSquad/Config/{vm.Squad.Id}");
            else
                return View("SquadExists");
        }


        public IActionResult Config(int id)
        {
            AdminModel vm = new AdminModel(new Uri(HttpContext.Request.GetDisplayUrl()), HttpContext.User.Identity?.Name, id);
            return View(vm);
        }



    }
}