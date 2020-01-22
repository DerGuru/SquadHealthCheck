using Microsoft.AspNet.SignalR;
using Org.BouncyCastle.Ocsp;
using SquadHealthCheck.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SquadHealthCheck.Hubs
{
    public class ViewerHub  : Hub
    {
        public void AddToSquadGroup(string userName)
        {
            ViewerModel vm = new ViewerModel(userName);
            using (var model = new ShcDataModel())
            {
                foreach (var user in model.SquadMembers.Where(x => x.Userhash == vm.Userhash))
                {
                    Groups.Add(Context.ConnectionId, user.Squad.ToString());
                }
            }
        }

        public override Task OnConnected()
        {
            AddToSquadGroup(Context.User.Identity.Name);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            AddToSquadGroup(Context.User.Identity.Name);
            return base.OnConnected();
        }

        public void SetUserValue(int squadid, int itemid, ItemValue value)
        {
            var caller = Clients.Caller;
            var group = Clients.Group(squadid.ToString());
           
            var vm = new ViewerModel(Context.User.Identity.Name);
            var iv = vm.SetUserValue(itemid, value,squadid);


            var ceiling = $"url('/Content/{ ((ItemValue)(int)Math.Ceiling(iv.SquadValue)).ToString()}.png')";
            var floor = $"url('/Content/{ ((ItemValue)(int)Math.Floor(iv.SquadValue)).ToString()}.png')";

            caller.updateId($"#TrafficLight{squadid}_{itemid}", $"url('/Content/{iv.Value.ToString()}.png')");
            group.updateId($"#Ceiling{squadid}_{itemid}", ceiling);
            group.updateId($"#Floor{squadid}_{itemid}", floor);
            group.updateValue($"#Good{squadid}_{itemid}", iv.SquadGoodCount > 0 ? iv.SquadGoodCount.ToString() : " ");
            group.updateValue($"#Medium{squadid}_{itemid}", iv.SquadMediumCount > 0 ? iv.SquadMediumCount.ToString() : " ");
            group.updateValue($"#Bad{squadid}_{itemid}", iv.SquadBadCount > 0 ? iv.SquadBadCount.ToString() : " ");
        }
    }
}