using Microsoft.EntityFrameworkCore;

using PuzzleGame.Entities;

namespace PuzzleGame.EF
{
    public class ScoreContext : DbContext
    {
        private readonly string conn_string;

        public DbSet<User> Users { get; set; }
        public DbSet<Score> Scores { get; set; }

        public ScoreContext(IConfiguration app_config)
        {
            conn_string = app_config.GetConnectionString("DefaultConnection")!;
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
            optionsBuilder.UseSqlServer(conn_string);
        }
    }
}
