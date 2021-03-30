using System;
using System.Linq;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly QLBVXKContext _context;

        public AccountService(QLBVXKContext context)
        {
            _context = context;
        }
        public void CreateAccount(Taikhoan account)
        {
            if (account == null) 
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Taikhoans.Add(account);
            _context.SaveChanges();
        }

        public void DeleteAccount(Taikhoan account)
        {
            if (account == null) 
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Taikhoans.Remove(account);
            _context.SaveChanges();
        }

        public Taikhoan GetAccountById(int id)
        {
            return _context.Taikhoans.Where(p => p.MaTk == id).FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
            
        }

        public void UpdateAccount(Taikhoan account)
        {
        }
    }
}