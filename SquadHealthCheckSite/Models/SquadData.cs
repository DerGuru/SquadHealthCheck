using System.Collections.Generic;

namespace SquadHealthCheck
{
    public class SquadData
    {
        public Squad Squad { get; private set; }
        public UserItem UserItem { get; private set; }
        public Item Item { get; private set; }

        public SquadData(Squad squad,Item item, UserItem userItem)
        {
            Squad = squad;
            Item = item;
            UserItem = userItem;
        }
    }
}