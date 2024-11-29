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
                    Transactionrepository.Transfer(sourcecard, destinationcard, amount);
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
