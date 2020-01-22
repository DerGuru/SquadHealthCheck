using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadHealthCheck.Models
{
    public class AdminModel : BaseModel
    {
        public string JoinLink => $"{System.Web.HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped)}/Join/{Squad.Id}";
        public Guid Adminhash { get; private set; }
        public List<Squad> Squads 
        {
            get 
            {
                using (var model = new ShcDataModel())
                {
                    var admins = model.SquadAdmins.Where(x => x.Adminhash == Adminhash);
                    return admins.Join(model.Squads, a => a.Squad, b => b.Id, (i, k) => k).Where(u => u != null).ToList();
                }
            }
        }

        public Squad Squad { get; private set; }
        private List<Item> _items;
        public List<Item> SquadItems
        {
            get
            {
                if (_items == null)
                {
                    using (var model = new ShcDataModel())
                    {
                        var squadItems = model.SquadItems.Where(x => x.Squad == Squad.Id);
                        _items = squadItems.Join(model.Items, x => x.Item, y => y.Id, (i, k) => k).Where(u => u != null).ToList();
                    }
                }
                return _items;
            }
        }

        public void DeleteSquad()
        {
            using (var model = new ShcDataModel())
            {
                model.UserItems.RemoveRange(model.UserItems.Where(x => x.Squad == Squad.Id));
                model.SquadAdmins.RemoveRange(model.SquadAdmins.Where(x => x.Squad == Squad.Id));
                model.Squads.RemoveRange(model.Squads.Where(x => x.Id == Squad.Id));
                model.SquadMembers.RemoveRange(model.SquadMembers.Where(x => x.Id == Squad.Id));
                model.SquadItems.RemoveRange(model.SquadItems.Where(x => x.Id == Squad.Id));
                model.SaveChanges();
            }
        }

        public List<Item> AvailableItems 
        {
            get 
            {
                using (var model = new ShcDataModel())
                {
                    return model.Items.Where(x => !SquadItems.Select(sq => sq.Id).Contains(x.Id)).ToList();
                }
            } 
        }

        public void AddItem (int itemId)
        {

            using (var model = new ShcDataModel())
            {
                model.SquadItems.Add(new SquadItem { Item = itemId, Squad = Squad.Id });
                model.SaveChanges();
                   
                var userItems = model.UserItems;
                var item = model.SquadItems.Single(x => x.Item == itemId && x.Squad == Squad.Id);
                var items = userItems.Where(x => x.Squad == Squad.Id)
                        .Select(u => u.Userhash)
                        .Distinct().ToList();
                
                if (!items.Contains(Userhash))
                {
                    items.Add(Userhash);
                }
                
                foreach (var i in items)
                {
                    var userItem = new UserItem { Item = itemId, Squad = Squad.Id, Userhash = i, Value = ItemValue.None };
                    userItems.Add(userItem);
                }
                model.SaveChanges();

            }
        }
        public void RemoveItem(int itemId)
        {
            using (var model = new ShcDataModel())
            {
                var t = model.SquadItems;
                var item = t.Where(x => x.Squad == Squad.Id && x.Item == itemId);
                if (!item.Any()) return;

                var userItems = model.UserItems;
                userItems.RemoveRange(userItems.Where(x => x.Squad == Squad.Id && x.Item == itemId));

                t.RemoveRange(item);
                model.SaveChanges();
            }
        }

        private void CheckAdmin()
        {
            using (var model = new ShcDataModel())
            {
            if (!model.SquadAdmins.Where(x => x.Squad == Squad.Id && x.Adminhash == Adminhash).Any())
                throw new UnauthorizedAccessException("You are not the administrator for this squad!");
            }
        }

        public bool Create(string name)
        {
            bool created = false;
            using (var model = new ShcDataModel())
            {
                var squads = model.Squads;
                var squad = squads.SingleOrDefault(x => x.Name == name);
                if (squad == null)
                {
                    squad = new Squad { Name = name, Description = "" };
                    squads.Add(squad);
                    model.SaveChanges();

                    created = true;

                    squad = squads.SingleOrDefault(x => x.Name == name);
                    Squad = squad;
                    var admins = model.SquadAdmins;
                    admins.Add(new SquadAdmin { Squad = squad.Id, Adminhash = Adminhash, Id = 0 });
                    model.SaveChanges();
                }
            }
            return created;
        }

        public AdminModel(string name) : base(name)
        {
            Adminhash = GetUserHash(Admin(name));
        }

        public AdminModel(string name, int squadid) : this(name)
        {
            using (var model = new ShcDataModel())
            {
                Squad = model.Squads.Where(x => x.Id == squadid).FirstOrDefault();
            }
            CheckAdmin();
        }

        private static string Admin(string name)
        {
            return name + "Admin";
        }

        
    }
}
