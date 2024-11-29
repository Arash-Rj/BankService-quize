using Bank.Connections;
using Bank.Entities;
using Bank.InferaStructure;
using Microsoft.EntityFrameworkCore;

namespace Bank.Database;

public class BankDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(ConnectionStrings.Connection1);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TransactionConfig());

    }

    public DbSet<Card> Cards { get; set; }
    public DbSet<BankTransaction> Transactions { get; set; }

}
