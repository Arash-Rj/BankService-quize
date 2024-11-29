using Bank.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interfaces
{
    public interface ICardService
    {
        Result IsCardValid(string cardnumber,string password);
        Result IsAmountenough(string sourcecardnumber, string destinationcardnumber,float amount);
        public Result IsCardNumberValid(string cardnumber);
        public Result DeActivateCard(string cardnumber);
    }
}
