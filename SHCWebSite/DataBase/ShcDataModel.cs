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
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Squad> Squad { get; set; }
        public virtual DbSet<SquadAdmin> SquadAdmin { get; set; }
        public virtual DbSet<SquadItem> SquadItem { get; set; }
        public virtual DbSet<SquadMember> SquadMember { get; set; }
        public virtual DbSet<UserItem> UserItem { get; set; }
    }
}
