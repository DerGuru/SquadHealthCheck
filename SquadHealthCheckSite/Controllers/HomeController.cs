using SquadHealthCheck;
using SquadHealthCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using System.IO;

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