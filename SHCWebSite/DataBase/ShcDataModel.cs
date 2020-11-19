using Microsoft.EntityFrameworkCore;
namespace SquadHealthCheck
{

    public partial class ShcDataModel : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(System.Environment.GetEnvironmentVariable("SQL"), (_) => _.EnableRetryOnFailure());
        }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Squad> Squads { get; set; }
        public virtual DbSet<SquadAdmin> SquadAdmins { get; set; }
        public virtual DbSet<SquadItem> SquadItems { get; set; }
        public virtual DbSet<SquadMember> SquadMembers { get; set; }
        public virtual DbSet<UserItem> UserItems { get; set; }
    }
}
