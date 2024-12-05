using Bank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interfaces
{
    public interface IUserService
    {
        public Result Login(string email, string password);
        public Result Register(string name, string email, string password);
        public Result Logout();
    }
}
