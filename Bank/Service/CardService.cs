using Bank.Database;
using Bank.Entities;
using Bank.Interfaces;
using Bank.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Service
{
    public class CardService : ICardService
    {
        ICardRepository CardRepository = new CardRepository();
        private BankDbContext BankDbContext = new BankDbContext();
        public Result IsAmountenough(string sourcecardnumber, string destinationcardnumber, float amount)
        {
           var destcardamount= CardRepository.GetCardAmount(destinationcardnumber);
            var sourcecardamount = CardRepository.GetCardAmount(sourcecardnumber);
            if(destcardamount > amount && sourcecardamount>amount)
            {
                return new Result(true);
            }
            if(destcardamount < amount)
            {
                return new Result(false, "The Destination card Balance is not enough.");
            }
            return new Result(false, "The Source card Balance is not enough.");
        }

        public Result IsCardValid(string cardnumber, string password)
        {
            if(cardnumber.Length == 16)
            {
               var doesexists =  CardRepository.DoesCardExists(cardnumber, password);
                if(doesexists.IsDone)
                {
                    return new Result(true);
                }
                return new Result(false,doesexists.Message);
            }
            return new Result(false,"The card number must be 16 digits.");
        }
        public Result IsCardNumberValid(string cardnumber)
        {
            if (cardnumber.Length == 16)
            {
                var doesexists = CardRepository.IsCardNumberValid(cardnumber);
                if (doesexists)
                {
                    return new Result(true);
                }
                return new Result(false, "The Card number is not valid.");
            }
            return new Result(false, "The card number must be 16 digits.");
        }
        public Result DeActivateCard(string cardnumber)
        {
            var card = CardRepository.GetCardByNo(cardnumber);
            if(card == null)
            {
                return new Result(false,"The card number is wrong");
            }
            card.IsActive = false;
            BankDbContext.SaveChanges();
            return new Result(true);
        }
    }
}
