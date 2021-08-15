using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class TicketService : ITicketService
    {
        private readonly d1h6lskf7s3bc0Context _context;

        public TicketService(d1h6lskf7s3bc0Context context)
        {
            _context = context;
        }

        public async Task CreateTicketAsync(IEnumerable<Vexe> ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            _context.Vexes.AddRange(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<Vexe> GetTicketByIdAsync(int id)
        {
            return await _context.Vexes.FirstOrDefaultAsync(p => p.MaVe == id);
        }

        public async Task<IEnumerable<Vexe>> GetTicketsAsync()
        {
            return await _context.Vexes.ToListAsync();
        }

        public async Task<IEnumerable<int>> GetSeatsByBusTripIdAsync(int busTripId, string date) {

            return await _context.Vexes.Where(p => p.MaChuyenXe == busTripId && p.NgayDi.Equals(date) && p.TrangThai == true)
                                       .Select(p => p.MaChoNgoi).ToListAsync();
        }

        public async Task<bool?> CheckAvailableAsync(int busTripId, string date, int seatId) {

            return await _context.Vexes.Where(p => p.MaChuyenXe == busTripId && p.NgayDi.Equals(date) && p.MaChoNgoi == seatId)
                                       .Select(p => p.TrangThai).FirstOrDefaultAsync();
        }

        public async Task DeleteTicketAsync(Vexe ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            _context.Vexes.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vexe>> GetTicketsByUserIdAsync(int userId)
        {
            return await _context.Vexes.Where(p => p.MaKh == userId).ToListAsync();
        }

        public async Task<IEnumerable<Vexe>> GetTicketsByBusTripIdAsync(int busTripId)
        {
            return await _context.Vexes.Where(p => p.MaChuyenXe == busTripId).ToListAsync();
        }

        public async Task UpdateTicketAsync(Vexe ticket)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RevenueByDay>> GetRevenueByDayAsync(string date)
        {
            List<RevenueByDay> status;

            status = await _context.Vexes
                        .Where(p => p.NgayDi.Contains(date) && p.TrangThai == true)
                        .GroupBy(p => p.MaChuyenXeNavigation.DonGia)
                        .Select(q => new RevenueByDay {                            
                            LoaiGia = q.Key.GetValueOrDefault(),
                            VeDaBan = q.Count()
                        }).ToListAsync();
            return status;
        }
    }
}