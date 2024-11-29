using Bank.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Bank.InferaStructure;

public class TransactionConfig : IEntityTypeConfiguration<BankTransaction>
{
    public void Configure(EntityTypeBuilder<BankTransaction> builder)
    {
        builder
     .HasOne(t => t.SourceCard)
     .WithMany(c => c.WithdrawTransactions)
     .HasForeignKey(t => t.SourceCardNumber)
     .OnDelete(DeleteBehavior.Restrict);

        builder
    .HasOne(t => t.DestinationCard)
    .WithMany(c => c.DepositTransactions)
    .HasForeignKey(t => t.DestinationCardNumber)
    .OnDelete(DeleteBehavior.Restrict);
    }
}
