using System.Data.Entity;
using OnlineAuction.Entities;

namespace OnlineAuction.DAL
{
    public class ContextDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<WinLot> WinLots { get; set; }

        public ContextDb() :
            base("AuctionConnection")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ContextDb>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<WinLot>()
                .HasMany(a => a.Bets)
                .WithRequired()
                .WillCascadeOnDelete(true);
        }
    }
}
