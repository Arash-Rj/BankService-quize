using Bank.Database;
using Bank.Entities;
using Bank.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Repository
{
    public class UserRepository : IUserRepository
    {
        private BankDbContext BankDbContext = new BankDbContext();
        public bool AddUser(string name, string eamil, string password)
        {
            var user = new User(name, eamil, password);
            try
            {
                BankDbContext.Users.Add(user);
                BankDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool DoesUserExist(string eamil, string password)
        {
            return BankDbContext.Users.Any(u => u.Email == eamil && u.Password == password);
        }
        public User GetUserByEmail(string email)
        {
            return BankDbContext.Users.AsNoTracking().Single(u => u.Email == email);
        }
    }
}
