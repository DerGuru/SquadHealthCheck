namespace SquadHealthCheck
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class UserItem
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }

        public byte[] Userhash { get; set; }

        public int Squad { get; set; }

        public int Item { get; set; }

        public ItemValue Value { get; set; }
    }
}
