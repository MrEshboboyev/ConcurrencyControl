using ConcurrencyControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyControl.Persistence;

public class DemoDbContext(DbContextOptions<DemoDbContext> options) : DbContext(options)
{
    public DbSet<BankAccount> BankAccounts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>()
            .Property(b => b.RowVersion)
            .IsRowVersion();
        
        modelBuilder.Entity<BankAccount>()
            .Property(b => b.Balance)
            .HasColumnType("decimal(18,2)");
    }
}