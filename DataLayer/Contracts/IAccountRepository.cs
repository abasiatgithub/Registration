using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Contracts
{
    public interface IAccountRepository
    {
        User GetUserById(int id);
        User GetUserByUsername(string username);
        void CreateUser(User user);
        void EditUser(User user);
    }
}
