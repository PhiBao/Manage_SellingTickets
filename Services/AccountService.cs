using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly d1h6lskf7s3bc0Context _context;

        public AccountService(d1h6lskf7s3bc0Context context)
        {
            _context = context;
        }
        public async Task CreateAccountAsync(Taikhoan account, byte role)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            // Validate
            if (role != 2 && role != 3) return;

            // Create account
            _context.Taikhoans.Add(account);

            await _context.SaveChangesAsync();

            // Get account Id
            var MaNd = await _context.Taikhoans.Where(p => p.Email == account.Email).Select(p => p.MaTk).FirstOrDefaultAsync();

            // Create User
            _context.Nguoidungs.Add(new Nguoidung
            {
                Vaitro = role,
                MaNd = MaNd
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Taikhoan account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            // Delete all user's tickets
            var tickets = await _context.Vexes.Where(p => p.MaKh == account.MaTk).ToListAsync();
            foreach (var ticket in tickets) {
                var review = await _context.Danhgias.Where(p => p.MaVe == ticket.MaVe).FirstOrDefaultAsync();
                _context.Danhgias.Remove(review);
            }
            
            _context.Vexes.RemoveRange(tickets);

            var histories = await _context.Lichsutimkiems.Where(p => p.MaNd == account.MaTk).ToListAsync();
            _context.Lichsutimkiems.RemoveRange(histories);

            var user = await _context.Nguoidungs.Where(p => p.MaNd == account.MaTk).FirstOrDefaultAsync();
            // If user is a staff
            if (user.Vaitro == 2)
            {
                // Find all buses that the staff work
                var buses = await _context.Xes.Where(p => p.MaNv == account.MaTk).ToListAsync();

                List<Chuyenxe> busTrips = new List<Chuyenxe>();
                foreach (var bus in buses)
                {
                    // Find all bus trips of those buses
                    var busTrip = await _context.Chuyenxes.Where(p => p.MaXe == bus.MaXe).ToListAsync();
                    busTrips.AddRange(busTrip);
                }

                // Find and delete all the tickets and seats of those bus trips
                foreach (var busTrip in busTrips)
                {
                    var ticketsByBusTrip = await _context.Vexes.Where(p => p.MaChuyenXe == busTrip.MaChuyenXe).ToListAsync();
                    foreach (var ticket in tickets) {
                        var review = await _context.Danhgias.Where(p => p.MaVe == ticket.MaVe).FirstOrDefaultAsync();
                        _context.Danhgias.Remove(review);
                    }
                    _context.Vexes.RemoveRange(ticketsByBusTrip);
                }

                // Delete all staff's bus trips and buses
                _context.Chuyenxes.RemoveRange(busTrips);
                _context.Xes.RemoveRange(buses);
            }

            // Delete user
            _context.Nguoidungs.Remove(user);
            //Delete account
            _context.Taikhoans.Remove(account);

            await _context.SaveChangesAsync();
        }

        public async Task<Taikhoan> GetAccountByIdAsync(int id)
        {
            return await _context.Taikhoans.Where(p => p.MaTk == id).FirstOrDefaultAsync();
        }

        public async Task<Taikhoan> ValidateAccountAsync(Taikhoan account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            var check = await _context.Taikhoans.Where(p => p.Email.ToLower() == account.Email.ToLower() && p.MatKhau == account.MatKhau).FirstOrDefaultAsync();

            if (check == null)
            {
                return new Taikhoan();
            }

            return check;
        }

        public async Task UpdateAccountAsync(Taikhoan account)
        {
            await _context.SaveChangesAsync();
        }
    }
}