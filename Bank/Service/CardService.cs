using Bank.Database;
using Bank.Entities;
using Bank.Interfaces;
using Bank.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Service
{
    public class CardService : ICardService
    {
        ICardRepository CardRepository = new CardRepository();
        private BankDbContext BankDbContext = new BankDbContext();
        private string path =@"D:/programming/BAnk/Bank/Bank/Codes.txt";
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
                    var isactive = CardRepository.IsCardActive(cardnumber);
                    if(!isactive)
                    {
                        return new Result(false, "Card is not active.");
                    }
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
            BankDbContext.Cards.Update(card);
            BankDbContext.SaveChanges();
            return new Result(true,"Your card is deactivated.");
        }

        public Result CardBalance(string cardnumber)
        {
            var isvalid = IsCardNumberValid(cardnumber);
            if(isvalid.IsDone)
            {
               var cardbalance = CardRepository.GetCardBalance(cardnumber);
                return new Result(true, $"Your card balance is: {cardbalance}");
            }
            return isvalid;
        }

        public Result ChangecardPass(string cardnumber, string currentpassword, string newpassword)
        {
           var isvalid =  CardRepository.DoesCardExists(cardnumber,currentpassword);
            if(isvalid.IsDone)
            {
                var card = CardRepository.GetCardByNo(cardnumber);
                card.password = newpassword;
                BankDbContext.Update(card);
                BankDbContext.SaveChanges();
                return new Result(true, "Password changed successfully.");
            }
            return new Result(false,"The entered password is wrong.Please try again.");
        }

        public Result GetCardHoldername(string cardnumber)
        {
          var name = CardRepository.GetCardByNo(cardnumber).HolderName;
            if(name == null)
            {
                return new Result(false, "Could not find holder's name.");

            }
            return new Result(true, $"Reciever's name: {name}");
        }

        public int GenerateRandomCode()
        {
            Random random = new Random();
            int randomcode = random.Next(1000, 10000);
            string resullt = JsonConvert.SerializeObject(randomcode);
            File.WriteAllText(path, resullt);
            return randomcode;
        }
    }
}
