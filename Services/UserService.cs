using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly d1h6lskf7s3bc0Context _context;

        public UserService(d1h6lskf7s3bc0Context context)
        {
            _context = context;
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

        public async Task<Nguoidung> GetUserByIdAsync(int id)
        {
            return await _context.Nguoidungs.FirstOrDefaultAsync(p => p.MaNd == id);
        }

        public async Task<IEnumerable<Nguoidung>> GetStaffsAsync()
        {
            return await _context.Nguoidungs.Where(p => p.Vaitro == 2).ToListAsync();
        }

        public async Task UpdateUserAsync(Nguoidung user)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Nguoidung>> SearchStaffsByName(string name)
        {
            return await _context.Nguoidungs.Where(p => (p.Vaitro == 2) && (p.TenNd == name)).ToListAsync();
        }

        public async Task<Nguoidung> GetCustomerByEmailAsync(string email)
        {
            return await _context.Nguoidungs.Where(p => p.Vaitro == 3 && p.MaNdNavigation.Email == email).FirstOrDefaultAsync();
        }
    }
}