using System;
using System.Collections.Generic;
using System.Linq;

namespace SquadHealthCheck.Models
{
    public class AdminModel : BaseModel
    {
        public string JoinLink => $"{baseUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped)}/Join/{Squad.Id}";
        public byte[] Adminhash { get; }
        public List<Squad> Squads
        {
            get
            {
                var model = DataBase();
                {
                    var admins = model.SquadAdmin.Where(x => x.Adminhash == Adminhash);
                    return admins.Join(model.Squad, a => a.Squad, b => b.Id, (i, k) => k).Where(u => u != null).ToList();
                }
            }
        }

        public Squad Squad { get; private set; }
        private List<Item> _items;
        private readonly Uri baseUri;

        public List<Item> SquadItems
        {
            get
            {
                if (_items == null)
                {
                    var model = DataBase();
                    {
                        var squadItems = model.SquadItem.Where(x => x.Squad == Squad.Id);
                        _items = squadItems.Join(model.Item, x => x.Item, y => y.Id, (i, k) => k).Where(u => u != null).ToList();
                    }
                }
                return _items;
            }
        }

        public void DeleteSquad()
        {
            var model = DataBase();
            {
                model.UserItem.RemoveRange(model.UserItem.Where(x => x.Squad == Squad.Id));
                model.SquadAdmin.RemoveRange(model.SquadAdmin.Where(x => x.Squad == Squad.Id));
                model.Squad.RemoveRange(model.Squad.Where(x => x.Id == Squad.Id));
                model.SquadMember.RemoveRange(model.SquadMember.Where(x => x.Id == Squad.Id));
                model.SquadItem.RemoveRange(model.SquadItem.Where(x => x.Id == Squad.Id));
                model.SaveChanges();
            }
        }

        public List<Item> AvailableItems
        {
            get
            {
                var model = DataBase();
                {
                    return model.Item.Where(x => !SquadItems.Select(sq => sq.Id).Contains(x.Id)).ToList();
                }
            }
        }

        public void AddItem(int itemId)
        {

            var model = DataBase();
            {
                model.SquadItem.Add(new SquadItem { Item = itemId, Squad = Squad.Id });
                model.SaveChanges();

                var userItems = model.UserItem;
                var item = model.SquadItem.Single(x => x.Item == itemId && x.Squad == Squad.Id);
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
            var model = DataBase();
            {
                var t = model.SquadItem;
                var item = t.Where(x => x.Squad == Squad.Id && x.Item == itemId);
                if (!item.Any()) return;

                var userItems = model.UserItem;
                userItems.RemoveRange(userItems.Where(x => x.Squad == Squad.Id && x.Item == itemId));

                t.RemoveRange(item);
                model.SaveChanges();
            }
        }

        private void CheckAdmin()
        {
            var model = DataBase();
            {
                if (!model.SquadAdmin.Where(x => x.Squad == Squad.Id && x.Adminhash == Adminhash).Any())
                    throw new UnauthorizedAccessException("You are not the administrator for this squad!");
            }
        }

        public bool Create(string name)
        {
            bool created = false;
            var model = DataBase();
            {
                var squads = model.Squad;
                var squad = squads.SingleOrDefault(x => x.Name == name);
                if (squad == null)
                {
                    squad = new Squad { Name = name, Description = "" };
                    squads.Add(squad);
                    model.SaveChanges();

                    created = true;

                    squad = squads.SingleOrDefault(x => x.Name == name);
                    Squad = squad;
                    var admins = model.SquadAdmin;
                    admins.Add(new SquadAdmin { Squad = squad.Id, Adminhash = Adminhash, Id = 0 });
                    model.SaveChanges();
                }
            }
            return created;
        }

        public AdminModel(Uri baseUri, Func<ShcDataModel> db, string name) : base(name, db)
        {
            this.baseUri = baseUri;

            Adminhash = GetUserHash(Admin(name));
        }

        public AdminModel(Uri baseUri, Func<ShcDataModel> db, string name, int squadid) : this(baseUri, db, name)
        {

            var model = DataBase();
            Squad = model.Squad.Where(x => x.Id == squadid).FirstOrDefault();
            CheckAdmin();
        }

        private static string Admin(string name)
        {
            return name + "Admin";
        }
    }
}
