using DataLayer.Contracts;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly RegAppContext _context;
        public AccountRepository(RegAppContext context)
        {
            _context = context;
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u=>u.Username == username);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);

            _context.SaveChanges();
        }

        public void EditUser(User user)
        {
            _context.Users.Update(user);

            _context.SaveChanges();
        }
    }
}
