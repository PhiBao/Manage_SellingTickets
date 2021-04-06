using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class RevenueService : IRevenueService
    {
        private readonly QLBVXKContext _context;

        public RevenueService(QLBVXKContext context)
        {
            _context = context;
        }
        public async Task CreateRevenueAsync(Doanhthungay dayliRevenue)
        {
            if (dayliRevenue == null)
            {
                throw new ArgumentNullException(nameof(dayliRevenue));
            }

            _context.Doanhthungays.Add(dayliRevenue);
            await _context.SaveChangesAsync();
        }

        public async Task<Doanhthungay> GetRevenueByIdAsync(int id)
        {
            return await _context.Doanhthungays.Where(p => p.MaDoanhThuNgay == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Doanhthungay>> GetRevenuesAsync()
        {
            return await _context.Doanhthungays.ToListAsync();
        }
    }
}