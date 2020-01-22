namespace SquadHealthCheck
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShcDataModel : DbContext
    {
        public ShcDataModel()
            : base("name=ShcDataModel")
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Squad> Squads { get; set; }
        public virtual DbSet<SquadAdmin> SquadAdmins { get; set; }
        public virtual DbSet<SquadItem> SquadItems { get; set; }
        public virtual DbSet<SquadMember> SquadMembers { get; set; }
        public virtual DbSet<UserItem> UserItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Squad>()
                .Property(e => e.Name)
                .IsFixedLength();
        }
    }
}
