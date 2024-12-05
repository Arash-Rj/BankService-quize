using Bank.Entities;
using Bank.Interfaces;
using Bank.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Service
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository = new UserRepository();
        public Result Login(string email, string password)
        {
            bool exist = _userRepository.DoesUserExist(email, password);
            if (exist)
            {
                var user = _userRepository.GetUserByEmail(email);
                OnlineUser.user = user;
                return new Result(true, "User Logged in successfully.");
            }
            return new Result(false, "User was not found please try again.");
        }

        public Result Logout()
        {
            OnlineUser.user = null;
            return new Result(true);
        }

        public Result Register(string name, string email, string password)
        {
            if (name == "" || email == "" || password == "")
            {
                return new Result(false, "Feilds can not be empty. try again.");
            }
            if (_userRepository.DoesUserExist(email, password))
            {
                return new Result(false, "The User Already exists.");
            }
            if (_userRepository.AddUser(name, email, password))
            {
                return new Result(true, "User Registered successfully.");
            }
            return new Result(false, "User could not be registered.");
        }
    }
}
