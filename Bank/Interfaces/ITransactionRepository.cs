
using Bank.Entities;
using System.Transactions;

namespace Bank.Interfaces;

public interface ITransactionRepository
{
       bool Transfer(string sourcecard, string destinationcard,float amount);
       List<BankTransaction> GetCardTransactions(string cardnumber);
    public float TransactionAmountInDay(string cardnumber);
}
