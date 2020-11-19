namespace SquadHealthCheck
{
    using System;

    public partial class UserItem
    {
        public long Id { get; set; }

        public Guid Userhash { get; set; }

        public int Squad { get; set; }

        public int Item { get; set; }

        public ItemValue Value { get; set; }
    }
}
