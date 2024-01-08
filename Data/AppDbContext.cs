using Microsoft.EntityFrameworkCore;

namespace BankingMVCApp.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.BankAccount)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.BankAccountId);
    }
}
