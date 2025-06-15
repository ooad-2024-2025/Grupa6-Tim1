using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using REVALB.Models;

namespace REVALB.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Comment> Comments { get; set; }   

        public DbSet<Category> Categories { get; set; }

        public DbSet<ScheduledAlbum> ScheduledAlbums { get; set; }

        public DbSet<AnalyticsData> AnalyticsData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // iskljuci cascade delete za Review → User
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // iskljuci cascade delete za Comment → User
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // iskljuci cascade delete za Album → User (Artist)
            modelBuilder.Entity<Album>()
                .HasOne(a => a.Artist)
                .WithMany()
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);
            // kaskadno brisanje Review → Comment
            modelBuilder.Entity<Review>()
                .HasMany(r => r.Comments)
                .WithOne(c => c.Review)
                .HasForeignKey(c => c.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            // kaskadno brisanje Album → Review
            modelBuilder.Entity<Album>()
                .HasMany(a => a.Reviews)
                .WithOne(r => r.Album)
                .HasForeignKey(r => r.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            // Kaskadno brisanje Album → ScheduledAlbum
            modelBuilder.Entity<Album>()
                .HasOne(a => a.ScheduledAlbum)
                .WithOne(sa => sa.Album)
                .HasForeignKey<ScheduledAlbum>(sa => sa.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            // kaskadno brisanje Album → AnalyticsData
            modelBuilder.Entity<Album>()
                .HasOne(a => a.AnalyticsData)
                .WithOne(ad => ad.Album)
                .HasForeignKey<AnalyticsData>(ad => ad.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Categories)
                .WithMany(c => c.Albums)
                .UsingEntity(j => j.ToTable("AlbumCategories"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.FavoriteAlbums)
                .WithMany()
                .UsingEntity(j => j.ToTable("UserFavoriteAlbums"));
        }

    }
}
