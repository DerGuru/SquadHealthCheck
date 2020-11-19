using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquadHealthCheck
{
    
    public partial class SquadMember
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }

        public int Squad { get; set; }

        public byte[] Userhash { get; set; }
    }
}
