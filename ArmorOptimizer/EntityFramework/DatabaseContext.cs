using Microsoft.EntityFrameworkCore;

namespace ArmorOptimizer.EntityFramework
{
    public partial class DatabaseContext : DbContext
    {
        public virtual DbSet<ArmorType> ArmorType { get; set; }
        public virtual DbSet<ArmorTypeResource> ArmorTypeResource { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ResistConfiguration> ResistConfiguration { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlite(@"data source=C:\Users\Devin\source\repos\ArmorOptimizer\lib\ArmorOptimizer.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArmorTypeResource>(entity =>
            {
                entity.HasKey(e => new { e.ArmorTypeId, e.ResourceId })
                    .HasName("sqlite_autoindex_ArmorTypeResource_1");
            });

            modelBuilder.Entity<ResistConfiguration>(entity =>
            {
                entity.HasIndex(e => new { e.Physical, e.Fire, e.Cold, e.Poison, e.Energy })
                    .HasName("Resists")
                    .IsUnique();
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasIndex(e => new { e.Color, e.ResistId })
                    .HasName("ColorResist")
                    .IsUnique();
            });
        }
    }
}