using KD.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KD.DAL.Context;

public class DatabaseContext : DbContext
{
    public DbSet<UniversityEntity> Universities { get; set; } = null!;
    public DbSet<DrawingEntity> Drawings { get; set; } = null!;
    public DbSet<VariantEntity> Variants { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        if (base.Database.IsRelational())
        {
            base.Database.Migrate();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UniversityEntity>().HasIndex(e => e.Name).IsUnique();
    }
}