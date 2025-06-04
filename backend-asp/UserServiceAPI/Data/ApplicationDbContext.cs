using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Data.Entities;

namespace UserServiceAPI.Data;


public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.Id);  

        modelBuilder.Entity<Staff>()
            .HasKey(s => s.Id);  
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

