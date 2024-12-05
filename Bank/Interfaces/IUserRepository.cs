using Bank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interfaces
{
    public interface IUserRepository
    {
        bool AddUser(string name, string eamil, string password);
        public bool DoesUserExist(string eamil, string password);
        public User GetUserByEmail(string email);
    }
}
