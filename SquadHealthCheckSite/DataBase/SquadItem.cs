namespace SquadHealthCheck
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SquadItem")]
    public partial class SquadItem
    {
        public long Id { get; set; }

        public int Squad { get; set; }

        public int Item { get; set; }
    }
}
