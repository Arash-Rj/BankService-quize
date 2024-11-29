using System.ComponentModel.DataAnnotations;

namespace Bank.Entities;

public class Card
{
    [Key]
    public string CardNumber { get; set; }
    public string password { get; set; }
    public string HolderName { get; set; }
    public bool IsActive { get; set; }
    public float Balance { get; set; }
    public  List<BankTransaction> DepositTransactions { get; set; }
    public  List<BankTransaction> WithdrawTransactions { get; set; }
    public Card()
    {

    }
}
