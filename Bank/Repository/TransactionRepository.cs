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
        var destncard = cardRepository.GetCardByNo(destinationcard);
        var sourccard = cardRepository.GetCardByNo(sourcecard);
        sourccard.Balance -= amount;
        try
        {
            destncard.Balance += amount;
    
        }
        catch (Exception ex)
        {
            sourccard.Balance += amount;
            throw new Exception();
        }      

        BankDbContext.Cards.Update(destncard);
        BankDbContext.Cards.Update(sourccard);
        BankDbContext.Transactions.Add(transaction);
        BankDbContext.SaveChanges();
        return true;
    }
    public float TransactionAmountInDay(string cardnumber)
    {
        var transactions = BankDbContext.Transactions.Where(t => t.SourceCardNumber == cardnumber && t.TransactionDate.DayOfYear == DateTime.Now.DayOfYear).ToList();
        float amount = 0;
        transactions.ForEach(t => amount += t.Amount);
        return amount;
    }
    public List<BankTransaction> GetCardTransactions(string cardnumber)
    {
       return BankDbContext.Transactions.AsNoTracking().Where(t => t.SourceCardNumber==cardnumber && t.DestinationCardNumber == cardnumber).ToList();
    }
}
