using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.0M, ImageUrl = "image1.jpg" },
            new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 20.0M, ImageUrl = "image2.jpg" }
        );

        base.OnModelCreating(modelBuilder);
    }
}
