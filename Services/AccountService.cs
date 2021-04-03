using System;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly QLBVXKContext _context;

        public AccountService(QLBVXKContext context)
        {
            _context = context;
        }
        public async Task CreateAccountAsync(Taikhoan account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Taikhoans.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Taikhoan account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Taikhoans.Remove(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Taikhoan> GetAccountByIdAsync(int id)
        {
            return await _context.Taikhoans.Where(p => p.MaTk == id).FirstOrDefaultAsync(); //
        }

        public async Task UpdateAccountAsync(Taikhoan account)
        {
            await _context.SaveChangesAsync();
        }
    }
}