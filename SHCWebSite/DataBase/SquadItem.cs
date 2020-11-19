using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquadHealthCheck
{
    public partial class SquadItem
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }

        public int Squad { get; set; }

        public int Item { get; set; }
    }
}
