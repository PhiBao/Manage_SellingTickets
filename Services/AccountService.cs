using System;
using System.Collections.Generic;
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
            // Delete all user's tickets
            var tickets = await _context.Vexes.Where(p => p.MaKh == account.MaTk).ToListAsync();
            _context.Vexes.RemoveRange(tickets);

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

                List<Vexe> ticketsByBusTrips = new List<Vexe>();
                List<Chongoi> seatsByBusTrips = new List<Chongoi>();
                // Find all the tickets and seats of those bus trips
                foreach (var busTrip in busTrips)
                {
                    var ticketsByBusTrip = await _context.Vexes.Where(p => p.MaChuyenXe == busTrip.MaChuyenXe).ToListAsync();
                    ticketsByBusTrips.AddRange(ticketsByBusTrip);
                    var seatsByBusTrip = await _context.Chongois.Where(p => p.MaChuyenXe == busTrip.MaChuyenXe).ToListAsync();
                    seatsByBusTrips.AddRange(seatsByBusTrip);
                }

                // Delete all the tickets and seats of those bus trips 
                _context.Vexes.RemoveRange(ticketsByBusTrips);
                _context.Chongois.RemoveRange(seatsByBusTrips);

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
            return await _context.Taikhoans.Where(p => p.MaTk == id).FirstOrDefaultAsync(); //
        }

        public async Task UpdateAccountAsync(Taikhoan account)
        {
            await _context.SaveChangesAsync();
        }
    }
}