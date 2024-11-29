using Bank.Database;
using Bank.Entities;
using Bank.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Repository;

public class CardRepository : ICardRepository
{
    private  BankDbContext BankDbContext = new BankDbContext();

    public Card? GetCardByNo(string cardnumber)
    {
        return BankDbContext.Cards.FirstOrDefault(c => c.CardNumber == cardnumber);
    }
    public float GetCardAmount(string cardnumber)
    {
        return BankDbContext.Cards.SingleOrDefault(c => c.CardNumber.Equals(cardnumber)).Balance;
    }
    public Result DoesCardExists(string cardnumber, string password)
    {
        var doesexists = BankDbContext.Cards.Any(c => c.CardNumber == cardnumber && c.password == password);
        if (doesexists)
        {
          var isactive = BankDbContext.Cards.Any(c => c.CardNumber == cardnumber && c.IsActive == true);
            if (isactive)
            {
                return new Result(isactive);
            }
            return new Result(false,"The card is not active.");   
        }
        return new Result(false,"The card does not exists.");
    }

    public bool IsCardNumberValid(string cardnumber)
    {
        return BankDbContext.Cards.Any(c => c.CardNumber == cardnumber);
    }
}
