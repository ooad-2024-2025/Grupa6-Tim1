using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Revalb.Models;

namespace Revalb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Pjesma> Pjesme { get; set; }
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<Album> Albumi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Korisnik>().ToTable("Korisnici");
            modelBuilder.Entity<Pjesma>().ToTable("Pjesme");
            modelBuilder.Entity<Recenzija>().ToTable("Recenzije");
            modelBuilder.Entity<Album>().ToTable("Albumi");

            base.OnModelCreating(modelBuilder);
        }
    }
}
