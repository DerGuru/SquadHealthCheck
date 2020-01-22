using System;
using System.Collections.Generic;
using System.Linq;

namespace SquadHealthCheck.Models
{
    public class UserSquadData
    {
        private Squad _squad;

        public UserSquadData(Squad squad)
        {
            _squad = squad;
        }
        public int SquadId => _squad.Id;
        public string Name => _squad.Name;
        public List<ItemData> UserItems { get; private set; } = new List<ItemData>();
        public IEnumerable<string> ItemNames => UserItems.Select(x => x.Name);
        public IEnumerable<ItemValue> ItemValues => UserItems.Select(x => x.Value);
        public IEnumerable<double> SquadValues => UserItems.Select(x => x.SquadValue);
    }
}