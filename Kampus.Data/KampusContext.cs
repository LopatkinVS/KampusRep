using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Kampus.Model.Entities;
using Microsoft.AspNetCore.Builder;

namespace Kampus.Data
{
    public class KampusContext : DbContext
    {
        private readonly IConfiguration _configuration;  

        public DbSet<Professor> Professors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<User> User { get; set; }

        public KampusContext(DbContextOptions<KampusContext> options, IConfiguration configuration) : base(options) 
        { 
            _configuration = configuration;
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString ("DefaultConnection"));
        }
    }
}
