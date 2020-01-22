namespace SquadHealthCheck
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserItem")]
    public partial class UserItem
    {
        public long Id { get; set; }

        public Guid Userhash { get; set; }

        public int Squad { get; set; }

        public int Item { get; set; }

        public ItemValue Value { get; set; }
    }
}
