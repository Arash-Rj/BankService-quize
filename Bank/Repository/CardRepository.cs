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
        var card = BankDbContext.Cards.SingleOrDefault(c => c.CardNumber.Equals(cardnumber));
        return card.Balance;
    }
    public Result DoesCardExists(string cardnumber, string password)
    {
        var doesexists = BankDbContext.Cards.Any(c => c.CardNumber == cardnumber && c.password == password);
        if (doesexists)
        {
           return new Result(doesexists);
        }
        return new Result(false,"The card does not exists.");
    }

    public bool IsCardNumberValid(string cardnumber)
    {
        return BankDbContext.Cards.Any(c => c.CardNumber == cardnumber);
    }

    public bool IsCardActive(string cardnumber)
    {
        return BankDbContext.Cards.SingleOrDefault(c => c.CardNumber == cardnumber).IsActive;
    }

    public float? GetCardBalance(string cardnumber)
    {
        return BankDbContext.Cards.FirstOrDefault(c => c.CardNumber.Equals(cardnumber))?.Balance;
    }

    //public bool IsCardPasswordValid(string currentpassword)
    //{
    //    return BankDbContext.Cards.Any(c => c.password == currentpassword);
    //}


}
