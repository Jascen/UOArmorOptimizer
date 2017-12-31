using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArmorOptimizer.EntityFramework
{
    public partial class ArmorOptimizerContext : DbContext
    {
        public virtual DbSet<ArmorType> ArmorType { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ResistConfiguration> ResistConfiguration { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }
        public virtual DbSet<ResourceKind> ResourceKind { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite(@"DataSource=C:\Users\Devin\source\repos\ArmorOptimizer\lib\ArmorOptimizer.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArmorType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BaseResistId).HasColumnType("bigint");

                entity.Property(e => e.BaseResourceKindId).HasColumnType("bigint");

                entity.Property(e => e.ItemType).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.SlotId).HasColumnType("bigint");

                entity.HasOne(d => d.BaseResist)
                    .WithMany(p => p.ArmorType)
                    .HasForeignKey(d => d.BaseResistId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.BaseResourceKind)
                    .WithMany(p => p.ArmorType)
                    .HasForeignKey(d => d.BaseResourceKindId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ArmorTypeId).HasColumnType("bigint");

                entity.Property(e => e.ColdResist).HasColumnType("bigint");

                entity.Property(e => e.EnergyResist).HasColumnType("bigint");

                entity.Property(e => e.FireResist).HasColumnType("bigint");

                entity.Property(e => e.LostResistancePoints).HasColumnType("bigint");

                entity.Property(e => e.NumberOfImbues).HasColumnType("bigint");

                entity.Property(e => e.PhysicalResist).HasColumnType("bigint");

                entity.Property(e => e.PoisonResist).HasColumnType("bigint");

                entity.Property(e => e.ResourceId).HasColumnType("bigint");

                entity.Property(e => e.UoId).IsRequired();

                entity.HasOne(d => d.ArmorType)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.ArmorTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ResistConfiguration>(entity =>
            {
                entity.HasIndex(e => new { e.Physical, e.Fire, e.Cold, e.Poison, e.Energy })
                    .HasName("Resists")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Cold).HasColumnType("bigint");

                entity.Property(e => e.Energy).HasColumnType("bigint");

                entity.Property(e => e.Fire).HasColumnType("bigint");

                entity.Property(e => e.Physical).HasColumnType("bigint");

                entity.Property(e => e.Poison).HasColumnType("bigint");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasIndex(e => new { e.Color, e.BonusResistId })
                    .HasName("ColorResist")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BaseResourceKindId).HasColumnType("bigint");

                entity.Property(e => e.BonusResistId).HasColumnType("bigint");

                entity.Property(e => e.Color).HasColumnType("bigint");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.BaseResourceKind)
                    .WithMany(p => p.Resource)
                    .HasForeignKey(d => d.BaseResourceKindId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.BonusResist)
                    .WithMany(p => p.Resource)
                    .HasForeignKey(d => d.BonusResistId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ResourceKind>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
