using Bank.Entities;

namespace Bank.Interfaces;

public interface ICardRepository
{
    Card GetCardByNo(string cardnumber);
    public float GetCardAmount(string cardnumber);
    public Result DoesCardExists(string cardnumber,string password);
    public bool IsCardNumberValid(string cardnumber);
    //public bool IsCardPasswordValid(string currentpassword);
    public bool IsCardActive(string cardnumber);
    public float? GetCardBalance(string cardnumber);
}
