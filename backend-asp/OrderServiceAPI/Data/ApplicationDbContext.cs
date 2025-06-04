using Microsoft.EntityFrameworkCore;
using OrderServiceAPI.Data.Entities;

namespace OrderServiceAPI.Data;


public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasKey(c => c.Id);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

