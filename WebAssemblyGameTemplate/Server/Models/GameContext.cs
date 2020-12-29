using Microsoft.EntityFrameworkCore;
using WebAssemblyGameTemplate.Shared;

namespace WebAssemblyGameTemplate.Server.Models
{
    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<SaveState> SaveStates { get; set; }

        public GameContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id);

                b.HasIndex(x => x.LoginCode)
                .IsUnique();
                b.Property(x => x.LoginCode);
                b.Property(x => x.TabCode);

                b.Property(x => x.CreatedAt);
                b.Property(x => x.LastLoginAt);

                b.HasMany(x => x.SaveStates)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId);

                b.Property(x => x.ActiveStateId);

                b.ToTable("Players");
            });

            modelBuilder.Entity<SaveState>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id);

                b.Property(x => x.PlayerId);

                b.ToTable("SaveStates");
            });
        }
    }
}