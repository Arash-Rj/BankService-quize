using Bank.Database;
using Bank.Entities;
using Bank.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Repository;

public class TransactionRepository : ITransactionRepository
{
    private BankDbContext BankDbContext = new BankDbContext();
    private readonly ICardRepository cardRepository = new CardRepository();

    public bool Transfer(string sourcecard, string destinationcard, float amount)
    {
        BankTransaction transaction = new BankTransaction(destinationcard,sourcecard,amount);
        var destncardAmount = cardRepository.GetCardAmount(destinationcard);
        var sourccardAmount = cardRepository.GetCardAmount(sourcecard);
        destncardAmount += amount;
        sourccardAmount -= amount;
        BankDbContext.Transactions.Add(transaction);
        BankDbContext.SaveChanges();
        return true;
    }

    public List<BankTransaction> GetCardTransactions(string cardnumber)
    {
       return BankDbContext.Transactions.AsNoTracking().Where(t => t.SourceCardNumber==cardnumber && t.DestinationCardNumber == cardnumber).ToList();
    }
}
