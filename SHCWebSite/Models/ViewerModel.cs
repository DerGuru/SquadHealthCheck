using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SquadHealthCheck.Models
{
    public class ViewerModel : BaseModel
    {

        public ViewerModel(Func<ShcDataModel> db, string userName) : base(userName, db)
        {
           
        }

        public ItemData SetUserValue(int itemid, ItemValue value, int squadid)
        {
            ItemData itemData = null;
            var model = DataBase();
            {
                var item = model.UserItem.SingleOrDefault(x => x.Userhash == Userhash && x.Item == itemid && x.Squad == squadid);
                if (item == null)
                {
                    item = new UserItem { Userhash = Userhash, Item = itemid, Squad = squadid, Value = value };
                    model.UserItem.Add(item);
                }
                else
                {
                    item.Value = value;
                }
                model.SaveChanges();
                var titem = model.Item.FirstOrDefault(x => x.Id == itemid);
                itemData = new ItemData(titem, value, model.UserItem.Where(x => x.Squad == squadid && x.Item == itemid && x.Value > ItemValue.None));

                return itemData;
            }
        }

        public void Leave(int id)
        {
            var model = DataBase();
            {
                model.UserItem.RemoveRange(model.UserItem.Where(x => x.Squad == id));
                model.SquadMember.RemoveRange(model.SquadMember.Where(x => x.Squad == id && x.Userhash == Userhash));
                model.SaveChanges();
            }
        }

        private List<UserSquadData> _userData;
        public List<UserSquadData> UserData
        {
            get
            {
                if (_userData == null)
                {
                    _userData = new List<UserSquadData>();
                    var userData = GetUserData();
                    
                    var dic = userData.GroupBy(x => x.Squad.Id).ToDictionary(x => x.Key, y => y.ToList());
                    foreach (var squad in dic)
                    {
                        UserSquadData userSquadData = new UserSquadData(squad.Value.First().Squad);
                        var squadData = GetSquadData(userSquadData.SquadId);
                        userSquadData.UserItems.AddRange(squad.Value.Select(x => new ItemData(x.Item, x.UserItem.Value, squadData.Where(y => y.Item == x.Item.Id))).ToList());
                        _userData.Add(userSquadData);
                    }
                }
                return _userData;
            }
        }

        public List<UserData> GetUserData()
        {
            var model = DataBase();
            {
                var squad = model.Squad; ;
                var items = model.Item;
                return model.UserItem.Where(x => x.Userhash == Userhash)
                    .Join(squad, x => x.Squad, y => y.Id, (UserItem, Squad) => new { UserItem, Squad })
                    .Join(items, x => x.UserItem.Item, y => y.Id, (UserItemSquad, Item) => new UserData(UserItemSquad.UserItem, UserItemSquad.Squad, Item))
                    .ToList();
            }
        }

        public List<UserItem> GetSquadData(int squadId)
        {
            var model = DataBase();
            {
                return model.UserItem.Where(t => t.Squad == squadId && t.Value > ItemValue.None).ToList();
            }
        }


        public void Join(int id)
        {
            var model = DataBase();
            {
                var memberShip = model.SquadMember.SingleOrDefault(v => v.Squad == id && v.Userhash == Userhash);

                if (memberShip == null)
                {
                    memberShip = new SquadMember() { Squad = id, Userhash = Userhash };
                    model.SquadMember.Add(memberShip);

                    model.SaveChanges();
                }

                var squadItems = model.SquadItem.Where(sqi => sqi.Squad == id).Select(x => x.Item);
                var useritems = model.UserItem.Where(ui => ui.Userhash == Userhash && ui.Squad == id).Select(x => x.Item);
                var join = squadItems.Except(useritems);
                foreach (var item in join)
                {
                    model.UserItem.Add(new UserItem { Userhash = Userhash, Item = item, Squad = id, Value = ItemValue.None });
                }

                model.SaveChanges();
            }
        }
    }
}
