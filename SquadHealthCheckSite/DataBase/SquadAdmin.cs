namespace SquadHealthCheck
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SquadAdmin")]
    public partial class SquadAdmin
    {
        public long Id { get; set; }

        public int Squad { get; set; }

        public Guid Adminhash { get; set; }
    }
}
