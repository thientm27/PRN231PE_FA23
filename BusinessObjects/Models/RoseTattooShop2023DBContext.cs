using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models
{
    public partial class RoseTattooShop2023DBContext : DbContext
    {
        public RoseTattooShop2023DBContext()
        {
        }

        public RoseTattooShop2023DBContext(DbContextOptions<RoseTattooShop2023DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MemberAccount> MemberAccounts { get; set; } = null!;
        public virtual DbSet<RoseTattooType> RoseTattooTypes { get; set; } = null!;
        public virtual DbSet<TattooSticker> TattooStickers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            return configuration.GetConnectionString("DBSQL");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberAccount>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK__MemberAc__0CF04B380FE293FC");

                entity.ToTable("MemberAccount");

                entity.HasIndex(e => e.MemberEmail, "UQ__MemberAc__3F37B77A0C3BABCF")
                    .IsUnique();

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.MemberEmail).HasMaxLength(60);

                entity.Property(e => e.MemberFullName).HasMaxLength(60);

                entity.Property(e => e.MemberPassword).HasMaxLength(40);
            });

            modelBuilder.Entity<RoseTattooType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__RoseTatt__516F03B584CD9DB8");

                entity.ToTable("RoseTattooType");

                entity.Property(e => e.TypeId).HasMaxLength(20);

                entity.Property(e => e.Origin).HasMaxLength(60);

                entity.Property(e => e.RoseTattooDescription).HasMaxLength(250);

                entity.Property(e => e.RoseTattooName).HasMaxLength(80);
            });

            modelBuilder.Entity<TattooSticker>(entity =>
            {
                entity.ToTable("TattooSticker");

                entity.Property(e => e.TattooStickerId).ValueGeneratedNever();

                entity.Property(e => e.ImportDate).HasColumnType("datetime");

                entity.Property(e => e.TattooStickerDescription).HasMaxLength(240);

                entity.Property(e => e.TattooStickerName).HasMaxLength(100);

                entity.Property(e => e.TypeId).HasMaxLength(20);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TattooStickers)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__TattooSti__TypeI__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
