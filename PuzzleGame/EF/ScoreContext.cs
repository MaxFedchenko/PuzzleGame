using Microsoft.EntityFrameworkCore;
using PuzzleGame.Core;
using PuzzleGame.Entities;

namespace PuzzleGame.EF
{
    public class ScoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Score> Scores { get; set; }

        public ScoreContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity => {
                entity.HasIndex(u => u.Name).IsUnique();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Global.AppSettings.GetConnectionString("DefaultConnection"));
        }
    }
}
