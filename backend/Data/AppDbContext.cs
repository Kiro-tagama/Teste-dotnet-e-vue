using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // IDs fixos para evitar mudanças no modelo a cada execução
        var category1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var category2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var category3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");

        var timestamp = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = category1Id, Name = "Electronics", Description = "Electronic devices", CreatedAt = timestamp, UpdatedAt = timestamp },
            new Category { Id = category2Id, Name = "Books", Description = "Various books", CreatedAt = timestamp, UpdatedAt = timestamp },
            new Category { Id = category3Id, Name = "Clothing", Description = "Men and Women Clothing", CreatedAt = timestamp, UpdatedAt = timestamp }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = Guid.Parse("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), CategoryId = category1Id, Name = "Smartphone", Description = "Android phone", Price = 699.99m, Quantity = 10, CreatedAt = timestamp, UpdatedAt = timestamp },
            new Product { Id = Guid.Parse("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), CategoryId = category1Id, Name = "Laptop", Description = "Gaming laptop", Price = 1200.50m, Quantity = 5, CreatedAt = timestamp, UpdatedAt = timestamp },
            new Product { Id = Guid.Parse("aaaaaaa3-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), CategoryId = category1Id, Name = "Smartwatch", Description = "Fitness watch", Price = 199.99m, Quantity = 20, CreatedAt = timestamp, UpdatedAt = timestamp },

            new Product { Id = Guid.Parse("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), CategoryId = category2Id, Name = "C# Programming", Description = "Learn C#", Price = 39.99m, Quantity = 15, CreatedAt = timestamp, UpdatedAt = timestamp },
            new Product { Id = Guid.Parse("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), CategoryId = category2Id, Name = "ASP.NET Core", Description = "Web development", Price = 49.99m, Quantity = 10, CreatedAt = timestamp, UpdatedAt = timestamp },
            new Product { Id = Guid.Parse("bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), CategoryId = category2Id, Name = "Database Design", Description = "SQL and NoSQL databases", Price = 29.99m, Quantity = 12, CreatedAt = timestamp, UpdatedAt = timestamp },

            new Product { Id = Guid.Parse("ccccccc1-cccc-cccc-cccc-cccccccccccc"), CategoryId = category3Id, Name = "T-shirt", Description = "Cotton T-shirt", Price = 19.99m, Quantity = 50, CreatedAt = timestamp, UpdatedAt = timestamp },
            new Product { Id = Guid.Parse("ccccccc2-cccc-cccc-cccc-cccccccccccc"), CategoryId = category3Id, Name = "Jeans", Description = "Blue denim jeans", Price = 49.99m, Quantity = 25, CreatedAt = timestamp, UpdatedAt = timestamp },
            new Product { Id = Guid.Parse("ccccccc3-cccc-cccc-cccc-cccccccccccc"), CategoryId = category3Id, Name = "Jacket", Description = "Winter jacket", Price = 89.99m, Quantity = 8, CreatedAt = timestamp, UpdatedAt = timestamp }
        );
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}