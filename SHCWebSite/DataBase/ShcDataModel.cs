namespace SquadHealthCheck
{
    using Microsoft.EntityFrameworkCore;

    public partial class ShcDataModel : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Squad> Squads { get; set; }
        public virtual DbSet<SquadAdmin> SquadAdmins { get; set; }
        public virtual DbSet<SquadItem> SquadItems { get; set; }
        public virtual DbSet<SquadMember> SquadMembers { get; set; }
        public virtual DbSet<UserItem> UserItems { get; set; }
    }
}
