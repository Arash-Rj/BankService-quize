using Bank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interfaces
{
    public interface ITransactionService
    {
        Result Transfer(string sourcecard, string destinationcard, float amount);
        public List<BankTransaction> CardTransactionList(string cardbnumber);
    }
}
