namespace SquadHealthCheck
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SquadAdmin
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }

        public int Squad { get; set; }

        public byte[] Adminhash { get; set; }
    }
}
