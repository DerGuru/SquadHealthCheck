namespace SquadHealthCheck.Models
{
    public class UserData
    {
        public Squad Squad { get; private set; }
        public Item Item { get; private set; }
        public UserItem UserItem { get; private set; } 

        public UserData(UserItem userItem, Squad squad, Item item)
        {
            Squad = squad;
            Item = item;
            UserItem = userItem;
        }
    }
}