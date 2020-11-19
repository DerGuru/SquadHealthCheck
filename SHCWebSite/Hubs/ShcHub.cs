using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.SignalR;
using SquadHealthCheck.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SquadHealthCheck.Hubs
{
    public class ShcHub  : Hub
    {
        public async Task AddToSquadGroup(string userName)
        {
            ViewerModel vm = new ViewerModel(userName);
            using (var model = new ShcDataModel())
            {
                foreach (var user in model.SquadMembers.Where(x => x.Userhash == vm.Userhash))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, user.Squad.ToString());
                }
            }
        }
        public override async Task OnConnectedAsync()
        {
            await AddToSquadGroup(Context.User.Identity.Name);
            await base.OnConnectedAsync();
        }

        public async Task SetUserValue(int squadid, int itemid, ItemValue value)
        {
            var caller = Clients.Caller;
            var group = Clients.Group(squadid.ToString());
           
            var vm = new ViewerModel(Context.User.Identity.Name);
            var iv = vm.SetUserValue(itemid, value,squadid);


            var ceiling = $"url('/Content/{ ((ItemValue)(int)Math.Ceiling(iv.SquadValue)).ToString()}.png')";
            var floor = $"url('/Content/{ ((ItemValue)(int)Math.Floor(iv.SquadValue)).ToString()}.png')";
            await Task.WhenAll(
            caller.SendCoreAsync("updateId", new object[] { $"#TrafficLight{squadid}_{itemid}", $"url('/Content/{iv.Value.ToString()}.png')" }),
            group.SendCoreAsync("updateId", new object[] { $"#Ceiling{squadid}_{itemid}", ceiling }),
            group.SendCoreAsync("updateId", new object[] { $"#Floor{squadid}_{itemid}", floor }),
            group.SendCoreAsync("updateValue", new object[] { $"#Good{squadid}_{itemid}", iv.SquadGoodCount > 0 ? iv.SquadGoodCount.ToString() : " " }),
            group.SendCoreAsync("updateValue", new object[] { $"#Medium{squadid}_{itemid}", iv.SquadMediumCount > 0 ? iv.SquadMediumCount.ToString() : " "}),
            group.SendCoreAsync("updateValue", new object[] { $"#Bad{squadid}_{itemid}", iv.SquadBadCount > 0 ? iv.SquadBadCount.ToString() : " " })
            );
        }

        public async Task Delete(int id)
        {
            var HttpContext = Context.GetHttpContext();
            AdminModel vm = new AdminModel(new Uri(HttpContext.Request.GetDisplayUrl()),HttpContext.User.Identity?.Name, id);
            vm.DeleteSquad();
            await Clients.Group(id.ToString()).SendCoreAsync("refresh",new object[] { });
            await Clients.Caller.SendCoreAsync("refresh", new object[] { });
        }

        public async Task AddItem(int squadId, int itemid)
        {
            var HttpContext = Context.GetHttpContext();
            AdminModel vm = new AdminModel(new Uri(HttpContext.Request.GetDisplayUrl()), HttpContext.User.Identity?.Name, squadId);
            vm.AddItem(itemid);
            await Clients.Group(squadId.ToString()).SendCoreAsync("refresh", new object[] { });
            await Clients.Caller.SendCoreAsync("refresh", new object[] { });
        }

        public async Task RemoveItem(int squadId, int itemid)
        {
            var HttpContext = Context.GetHttpContext();
            AdminModel vm = new AdminModel(new Uri(HttpContext.Request.GetDisplayUrl()), HttpContext.User.Identity?.Name, squadId);
            vm.RemoveItem(itemid);
            await Clients.Group(squadId.ToString()).SendCoreAsync("refresh", new object[] { });
            await Clients.Caller.SendCoreAsync("refresh", new object[] { });
        }

        
    }
}