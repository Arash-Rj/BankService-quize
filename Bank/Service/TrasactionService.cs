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
    public class TrasactionService : ITransactionService
    {
        ITransactionRepository Transactionrepository = new TransactionRepository();
        ICardService cardservice = new CardService();
        public Result Transfer(string sourcecard, string destinationcard, float amount)
        {
            var transactionamount = Transactionrepository.TransactionAmountInDay(sourcecard);
            if(transactionamount >= 1250)
            {
                return new Result(false, " Transfer limit has been exceeded.");
            }
            if(transactionamount + amount > 1250)
            {
                return new Result(false, $"The Transfer limit will be  exceeded.Entered amonut must be less than {1250 - transactionamount}");
            }    
            if(amount < 0)
            {
                return new Result(false, "The transfer amount must be greater than zero.");
            }
            var isdestvalid = cardservice.IsCardNumberValid(destinationcard);
            if(isdestvalid.IsDone)
            {
                var isenough = cardservice.IsAmountenough(sourcecard, destinationcard,amount);
                if(isenough.IsDone)
                {
                    var isdone = false;
                    try
                    {
                        isdone = Transactionrepository.Transfer(sourcecard, destinationcard, amount);
                    }
                   catch(Exception ex)
                    {
                        return new Result(false,"An Error accured during transaction.");
                    }
                    return new Result(isdone);
                }
                return new Result(false,isenough.Message);
            }
            return isdestvalid;
        }
        public List<BankTransaction> CardTransactionList(string cardbnumber)
        {
           return Transactionrepository.GetCardTransactions(cardbnumber);
        }
    }
}
