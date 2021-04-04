using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly QLBVXKContext _context;

        public UserService(QLBVXKContext context)
        {
            _context = context;
        }

        public async Task DeleteUserAsync(Nguoidung user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Nguoidungs.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Nguoidung> GetCustomerByIdAsync(int id)
        {
            return await _context.Nguoidungs.Where(p => p.Vaitro == 3).FirstOrDefaultAsync(p => p.MaNd == id);
        }

        public async Task<IEnumerable<Nguoidung>> GetCustomersAsync()
        {
            return await _context.Nguoidungs.Where(p => p.Vaitro == 3).ToListAsync();
        }

        public async Task<Nguoidung> GetStaffByIdAsync(int id)
        {
            return await _context.Nguoidungs.Where(p => p.Vaitro == 2).FirstOrDefaultAsync(p => p.MaNd == id);
        }

        public async Task<IEnumerable<Nguoidung>> GetStaffsAsync()
        {
            return await _context.Nguoidungs.Where(p => p.Vaitro == 2).ToListAsync();
        }

        public async Task UpdateUserAsync(Nguoidung user)
        {
            await _context.SaveChangesAsync();
        }

    }
}