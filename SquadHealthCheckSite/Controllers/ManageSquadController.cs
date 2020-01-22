using Microsoft.AspNet.SignalR;
using SquadHealthCheck.Hubs;
using SquadHealthCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SquadHealthCheck.Controllers
{
    public class ManageSquadController : Controller
    {

        // GET: Squad
        public ActionResult Index()
        {
            AdminModel vm = new AdminModel(HttpContext.User.Identity?.Name);
            return View(vm);
        }

        public ActionResult Create(string id)
        {
            string name = Encoding.Default.GetString(Convert.FromBase64String(id));
            AdminModel vm = new AdminModel(HttpContext.User.Identity?.Name);
            if (vm.Create(name))
                return Redirect($"~/ManageSquad/Config/{vm.Squad.Id}");
            else
                return View("SquadExists");
        }

        public ActionResult Delete(int id)
        {
            AdminModel vm = new AdminModel(HttpContext.User.Identity?.Name,id);
            vm.DeleteSquad();
            var hub = GlobalHost.ConnectionManager.GetHubContext<ViewerHub>();
            hub.Clients.Group(id.ToString()).refresh();
            return Redirect($"~/ManageSquad");
        }

        public ActionResult AddItem(int squadId, int itemid)
        {
            AdminModel vm = new AdminModel(HttpContext.User.Identity?.Name, squadId);
            vm.AddItem(itemid);
            var hub = GlobalHost.ConnectionManager.GetHubContext<ViewerHub>();
            hub.Clients.Group(squadId.ToString()).refresh();
            return Redirect($"~/ManageSquad/Config/{vm.Squad.Id}");
        }

        public ActionResult RemoveItem(int squadId, int itemid)
        {
            AdminModel vm = new AdminModel(HttpContext.User.Identity?.Name, squadId);
            vm.RemoveItem(itemid);
            var hub = GlobalHost.ConnectionManager.GetHubContext<ViewerHub>();
            hub.Clients.Group(squadId.ToString()).refresh();
            return Redirect($"~/ManageSquad/Config/{vm.Squad.Id}");
        }

        public ActionResult Config(int id)
        {
            AdminModel vm = new AdminModel(HttpContext.User.Identity?.Name,id);
            return View(vm);
        }

      

    }
}