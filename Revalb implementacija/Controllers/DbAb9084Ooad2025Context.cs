using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Revalb.Controllers;

public partial class DbAb9084Ooad2025Context : DbContext
{
    public DbAb9084Ooad2025Context()
    {
    }

    public DbAb9084Ooad2025Context(DbContextOptions<DbAb9084Ooad2025Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Albumi> Albumis { get; set; }

    public virtual DbSet<Korisnici> Korisnicis { get; set; }

    public virtual DbSet<Pjesme> Pjesmes { get; set; }

    public virtual DbSet<Recenzije> Recenzijes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SQL1003.site4now.net;Initial Catalog=db_ab9084_ooad2025;User Id=db_ab9084_ooad2025_admin;Password=OOAD1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Albumi>(entity =>
        {
            entity.HasKey(e => e.IdAlbum);

            entity.ToTable("Albumi");
        });

        modelBuilder.Entity<Korisnici>(entity =>
        {
            entity.HasKey(e => e.IdKorisnik);

            entity.ToTable("Korisnici");

            entity.Property(e => e.IdKorisnik).HasColumnName("idKorisnik");
            entity.Property(e => e.BrojRecenzija).HasColumnName("brojRecenzija");
        });

        modelBuilder.Entity<Pjesme>(entity =>
        {
            entity.HasKey(e => e.IdPjesma);

            entity.ToTable("Pjesme");

            entity.Property(e => e.IdPjesma).HasColumnName("idPjesma");
            entity.Property(e => e.Fajl).HasColumnName("fajl");
            entity.Property(e => e.IdAlbum).HasColumnName("idAlbum");
            entity.Property(e => e.Naziv).HasColumnName("naziv");
            entity.Property(e => e.RedniBroj).HasColumnName("redniBroj");
        });

        modelBuilder.Entity<Recenzije>(entity =>
        {
            entity.HasKey(e => e.IdRecenzija);

            entity.ToTable("Recenzije");

            entity.Property(e => e.IdRecenzija).HasColumnName("idRecenzija");
            entity.Property(e => e.IdAlbum).HasColumnName("idAlbum");
            entity.Property(e => e.IdRecenzent).HasColumnName("idRecenzent");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
