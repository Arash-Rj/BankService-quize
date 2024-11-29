
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Entities;

public class BankTransaction
{

    public int Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public float Amount { get; set; }

    public string SourceCardNumber { get; set; }
    public Card SourceCard { get; set; }

    public string DestinationCardNumber { get; set; }
    public Card DestinationCard { get; set; }
    public BankTransaction(string destinationcardno, string sourcecardno,float amount)
    {
        TransactionDate = DateTime.Now;
        Amount = amount;
        SourceCardNumber = sourcecardno;
        DestinationCardNumber = destinationcardno;

    }
    public override string ToString()
    {
        return $"Source Card: {SourceCardNumber} || Destination Card: {DestinationCardNumber} || Transaction Date: {TransactionDate} || Amount: {Amount}";
    }
    public BankTransaction() { }
}
