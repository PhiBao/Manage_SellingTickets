using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

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

                _context.Doanhthungays.Add(dayliRevenue);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRevenueAsync(Doanhthungay dayliRevenue)
        {
            await _context.SaveChangesAsync();
        }
    }
}